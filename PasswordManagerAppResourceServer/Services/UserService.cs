﻿﻿using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.DataProtection;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.WebUtilities;
using PasswordManagerAppResourceServer.Interfaces;
using PasswordManagerAppResourceServer.Models;
using OtpNet;
using EmailService;
using PasswordManagerAppResourceServer.Controllers;
using PasswordManagerAppResourceServer.Results;
using PasswordManagerAppResourceServer.Handlers;


namespace PasswordManagerAppResourceServer.Services
{
    public class UserService : IUserService
    {



        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public DataProtectionHelper dataProtectionHelper;
        private readonly IEmailSender _emailSender;
        

        public event EventHandler<Message> EmailSendEvent;


        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IDataProtectionProvider provider,IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;  
             _emailSender = emailSender;
            dataProtectionHelper = new DataProtectionHelper(provider);

        }

        private void UserService_EmailSendEvent(object sender, Message e)
        {
            _emailSender.SendEmailAsync(e);
        }

        public User Create(string email, string password)
        {   
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (VerifyEmail(email))
                throw new AppException("Email \"" + email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var (publicKey, privateKey) = AsymmetricEncryptionHelper.GenerateKeys(password,keyLength:2048);

            User user = new User();
            user.Email = email;
            user.Password = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);
            user.TwoFactorAuthorization = 0;
            user.PasswordNotifications = 1;
            user.AuthenticationTime = 5;
            user.Admin = 0;
            user.PrivateKey = privateKey;
            user.PublicKey = publicKey;


            _unitOfWork.Users.Add<User>(user);
             _unitOfWork.SaveChanges();





            //EmailSendEvent?.Invoke(this, new Message(new string[] { user.Email }, "Zalozyles konto na PasswordManager.com", "Witamy w PasswordManager web api " + user.Email));

            return user;
        }
        public int GetAuthUserId()
        {   
            
            try
            {
                int id =  Int32.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            
                return id;
            }catch(NullReferenceException)
            {
                return -1;
            }
            
        }

        

        public void Update(User user, string password = null)
        {
            // TO DO
        }



        public bool VerifyEmail(string email)
        {
        
            return _unitOfWork.Users.CheckIfUserExist(email);
        }




        public User Authenticate(string email, string password)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _unitOfWork.Users.SingleOrDefault<User>(x => x.Email == email);


            // check if user exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, Convert.FromBase64String(user.Password), Convert.FromBase64String(user.PasswordSalt)))
                return null;

            // authentication successful



            return user;
        }



        public ClaimsIdentity GetClaimIdentity(User authUser)
        {



           // ManageAuthorizedDevices(authUser);  // zapisywać w bazie dodatkowo wcześniejsze adresy ip ?


            var claims = new List<Claim>
{           new Claim(ClaimTypes.Name,authUser.Id.ToString()),
            new Claim(ClaimTypes.Email,authUser.Email),
            new Claim("Admin", authUser.Admin.ToString()),
            new Claim("TwoFactorAuth", authUser.TwoFactorAuthorization.ToString()),
            new Claim("PasswordNotifications", authUser.PasswordNotifications.ToString()),
            new Claim("AuthTime", authUser.AuthenticationTime.ToString()),



        };
            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        }







        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.Users.GetAll<User>();
        }




        public User GetById(int id)
        {   
            
            return _unitOfWork.Users.Find<User>(id);
            
        }

        public bool DeleteUser(int id)
        {
             User user = GetById(id);


            if (user != null)
            {
                _unitOfWork.Users.Remove<User>(user);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;

           
           
        }


        public Task<User> AuthenticateExternal(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddExternal(string id, string email)
        {
            throw new NotImplementedException();
        }
        public bool ChangeMasterPassword(string password,string authUserId)
        {   
            var authUserIdToInt32 = Int32.Parse(authUserId);
            User user = _unitOfWork.Users.Find<User>(authUserIdToInt32);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.Password = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);
            try
            {
                _unitOfWork.Users.Update<User>(user);
                _unitOfWork.SaveChanges();
                EmailSendEvent?.Invoke(this, 
                new Message(new string[] { user.Email },"PasswordManagerApp Password Change", "Your password has been changed  "+ DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd' 'HH:mm:ss") + ".")
                );
                return true;
            }catch(Exception)
            {
                return false;
            }
            


            



        }

        #region private methods
        private  void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public  bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }






        private string GenerateTotpToken(User authUser)
        {   string totpToken;
            string sysKey = "ajskSJ62j%sjs.;'[ah1";
            var key_b = Encoding.UTF8.GetBytes(sysKey + authUser.Email);


            Totp totp = new Totp(secretKey: key_b, mode: OtpHashMode.Sha512, step: 300, timeCorrection: new TimeCorrection(DateTime.UtcNow));
            totpToken = totp.ComputeTotp(DateTime.UtcNow);


            return totpToken;
        }
        private void SaveToDb(User authUser, string totpToken)
        {
            bool tokenIsActive = _unitOfWork.Users.IsTokenActive(authUser);  
            if (tokenIsActive)
                return;
            
            _unitOfWork.Context.Totp_Users.Add(
                new Totp_user() {
                    Token = totpToken,
                    Create_date = DateTime.UtcNow,
                    Expire_date = DateTime.UtcNow.AddSeconds(300),
                    User = authUser




                });
            _unitOfWork.SaveChanges();
        }

        
        #endregion

        public bool AddNewDeviceToDb(string newOsHash, User authUser)
        {



            if (!_unitOfWork.Context.UserDevices.Any(b => b.User == authUser && b.DeviceGuid == newOsHash))

            {

                UserDevice usd = new UserDevice();
                usd.User = authUser;
                usd.Authorized = 1;
                usd.DeviceGuid = newOsHash;
                usd.IpAddress = GetUserIpAddress(authUser);

                _unitOfWork.Context.UserDevices.Add(usd);
                _unitOfWork.SaveChanges();


                return true;


            }
            return false;
        }
       

        /* private void ManageAuthorizedDevices(User authUser)
        {
            
            var c = cookieHandler.GetClientInfo();
            string browser = c.UA.Family.ToString() + " " + c.UA.Major.ToString();


           
            bool IsNewUserDevice = false;
            string guidDevice = "";
            bool IsUserGuidDeviceMatch = true;


            var deviceCookieExist = cookieHandler.CheckIfCookieExist("DeviceInfo");
            if (deviceCookieExist)
            {
                var GuidDeviceFromCookie = cookieHandler.ReadAndDecryptCookie("DeviceInfo");
                IsUserGuidDeviceMatch = CheckUserGuidDeviceInDb(GuidDeviceFromCookie, authUser);
                if (IsUserGuidDeviceMatch)
                    return;
            
            }
            if(deviceCookieExist==false || IsUserGuidDeviceMatch==false)
            {
                

                guidDevice = Guid.NewGuid().ToString();
                cookieHandler.CreateCookie("DeviceInfo", guidDevice, null);
                var userGuidDeviceHash = dataToSHA256(guidDevice);
                var ipMatchWithPrevious = CheckPreviousUserIp(authUser);
                 IsNewUserDevice = AddNewDeviceToDb(userGuidDeviceHash, authUser);
                
                
                if( ipMatchWithPrevious )
                    IsNewUserDevice = false;
                
                
                   
                
               
                
                

            }
            



            

         
            if (IsNewUserDevice)
                EmailSendEvent?.Invoke(this, 
                new Message(new string[] { authUser.Email }, "Nowe urządzenie " + c.OS.ToString(), "Zarejestrowano logowanie z nowego adresu ip: "+GetUserIpAddress(authUser)+", system : " + c.OS.ToString() + " " + browser + " dnia " + DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd' 'HH:mm:ss") + ".")
                );
         




        } */

        public void InformAllUsersAboutOldPasswords()
        {   
            
                var allloginDatasList = _unitOfWork.Context.LoginDatas.ToList();
                if(allloginDatasList is null)
                    return;
                var loginDatasListWithOldPasswords = allloginDatasList.Where(x => (DateTime.UtcNow.ToLocalTime() - x.ModifiedDate).Days>=30 ).ToList();
                if(loginDatasListWithOldPasswords is null)
                    return;
                string websitesList = "";
                foreach (var item in loginDatasListWithOldPasswords.GroupBy(x => x.UserId))
                {   
                    websitesList="";
                    var userEmail = GetById(item.Key).Email;
                    item.ToList().ForEach(x => websitesList+=x.Website+", "   );


                    string message = $"wykryto {item.Count()} hasła nie zmieniane od 30 dni dla podanych stron internetowych : {websitesList}!";


                    _emailSender.SendEmailAsync(new Message(new string[] { userEmail },"PasswordManagerApp stare hasła", message));


                }
       
            
            
            
            
            
            
                
        }
        public void InformUserAboutOldPasswords(int userId)
        {
             string userEmail = GetById(userId).Email;
             var allUserLoginData = _unitOfWork.Context.LoginDatas.Where(x=>x.UserId==userId).ToList();
                if(allUserLoginData is null)
                    return;
                var loginDataListWithOldPasswords = allUserLoginData.Where(x => (DateTime.UtcNow.ToLocalTime() - x.ModifiedDate).Days>=30 ).ToList();
                if(loginDataListWithOldPasswords is null)
                    return;
                string websitesList = "";

                loginDataListWithOldPasswords.ForEach(x=>websitesList+=x.Website+", ");
                
                string message = $"wykryto {loginDataListWithOldPasswords.Count} hasła nie zmieniane od 30 dni dla podanych stron internetowych : {websitesList}.";

              //  _emailSender.SendEmailAsync(new Message(new string[] { userEmail },"PasswordManagerApp stare hasła", message));
        }
        private string GetUserIpAddress(User user) => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

        private bool CheckPreviousUserIp(User authUser)
        {
            string currentIpAddress =GetUserIpAddress(authUser);
            var ipMatched = _unitOfWork.Context.UserDevices.Any(x=>x.User==authUser && x.IpAddress.Equals(currentIpAddress));
            return ipMatched;
        }

        private bool CheckUserGuidDeviceInDb(string GuidDeviceFromCookie, User authUser)
        {

            
            var GuidDeviceHashFromCookie = dataToSHA256(GuidDeviceFromCookie);
            
            
            if (_unitOfWork.Context.UserDevices.Any(ud => ud.User == authUser && ud.DeviceGuid == GuidDeviceHashFromCookie))
                return true;
            else
                return false;

        }
        public void CreateAndSendAuthorizationToken(int authUserId,string userPassword)
        {
            User authUser = GetById(authUserId);
            string token = authUser.Id.ToString() + "|"+authUser.Email +"|"+DateTime.UtcNow.AddMinutes(10).ToString();
            token = dataProtectionHelper.Encrypt(token,userPassword);
            string url  = QueryHelpers.AddQueryString("https://localhost:5004/auth/deleteaccount2step", "token", token);
            EmailSendEvent?.Invoke(this, new Message(new string[] { authUser.Email }, "Link do usuniecia konta. Pass Manager App", "Link do usuniecia konta w serwisie Pass Manager App : " + url + " dla uzytkownika: " + authUser.Email + " Podany link bedzie aktywny przez 10 minut."));
            

        }
        public bool ValidateToken(string token,string password)
        {   
            
            var authUserId = _httpContextAccessor.HttpContext.User.Identity.Name;
            User authUser = GetById(Int32.Parse(authUserId));
            string decryptedToken = "";
            try
            {
                decryptedToken = dataProtectionHelper.Decrypt(token, password);
            }
            catch(CryptographicException)
            {
                return false;
            }
            
            
            var tokenArray = decryptedToken.Split("|");
           
            
            
            if(tokenArray[0].Equals(authUserId) && tokenArray[1].Equals(authUser.Email) )
            {
                DateTime expiredDate = DateTime.Parse(tokenArray[2]);
                if(DateTime.Compare(DateTime.UtcNow,expiredDate)<0   )
                    return true;

            }
            return false;

        }










        public void SendTotpToken(User authUser)
        {
            string totpToken;
            bool isActive =  _unitOfWork.Users.IsTokenActive(authUser);
           
            if (isActive)
            {
                totpToken = _unitOfWork.Users.GetActiveToken(authUser);
                EmailSendEvent?.Invoke(this, new Message(new string[] { authUser.Email }, "Jednorazowy kod dostępu. Pass Manager App", "Jednorazowy kod dostępu do konta: " + totpToken + " dla uzytkownika: " + authUser.Email + " Podany kod musisz wprowadzic w ciagu 5min"));
                
                return;
            }
            
            totpToken = GenerateTotpToken(authUser);
            SaveToDb(authUser, totpToken);

            EmailSendEvent?.Invoke(this, new Message(new string[] { authUser.Email }, "Jednorazowy kod dostępu. Pass Manager App", "Jednorazowy kod dostępu do konta: " + totpToken + " dla uzytkownika: " + authUser.Email+ " Podany kod musisz wprowadzic w ciagu 5min"));


        }

        

        private enum ResultsToken
        {
            NotMatched,
            Matched,
            Expired,
        }


        public int VerifyTotpToken(User authUser,string totpToken)
        {
            
            
            string sysKey = "ajskSJ62j%sjs.;'[ah1";
            
            long lastUse;
            Totp totp = new Totp(secretKey: Encoding.UTF8.GetBytes(sysKey + authUser.Email), mode: OtpHashMode.Sha512, step: 300,timeCorrection:new TimeCorrection(DateTime.UtcNow));
           
            
            var activeTokenRecordFromDb = _unitOfWork.Context.Totp_Users.Where(b => b.User == authUser && b.Token == totpToken).FirstOrDefault();
            if (activeTokenRecordFromDb != null)
            {
               
               
                if (activeTokenRecordFromDb.Expire_date >= DateTime.UtcNow)
                {
                    return totp.VerifyTotp(totpToken, out lastUse,window:new VerificationWindow(1,1)) ? (int)ResultsToken.Matched:(int)ResultsToken.NotMatched; 
                }
                else
                {
                    return (int)ResultsToken.Expired;
                }

            }
            else
            { 
                return (int)ResultsToken.NotMatched;
            }

        }

        private string dataToSHA256(string data)
        {
            SHA256 mysha256 = SHA256.Create();
            return Convert.ToBase64String(mysha256.ComputeHash(Encoding.UTF8.GetBytes(data)));
            
        }

        public void UpdatePreferences(UpdatePreferencesWrapper upPreferences, int userId)
        {
            var aa = upPreferences.TwoFactor;
            var bb = upPreferences.PassNotifications;
            var cc = upPreferences.VerificationTime;
            User user = _unitOfWork.Users.Find<User>(userId);
            user.AuthenticationTime = Int32.Parse(upPreferences.VerificationTime);
            user.PasswordNotifications = Int32.Parse(upPreferences.PassNotifications);
            user.TwoFactorAuthorization = Int32.Parse(upPreferences.TwoFactor);
            _unitOfWork.Users.Update<User>(user);
            _unitOfWork.SaveChanges();
        }
    }
}