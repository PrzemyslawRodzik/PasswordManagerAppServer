﻿using Microsoft.EntityFrameworkCore;


namespace PasswordManagerAppResourceServer.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDevice> UserDevices { get; set; }
        public virtual DbSet<Totp_user> Totp_Users { get; set; }
        public virtual DbSet<LoginData> LoginDatas { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<PaypalAccount> PaypalAccounts { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<VisitorAgent> VisitorAgents { get; set; }
        public virtual DbSet<SharedLoginData> SharedLoginsData { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<SharedLoginData>()
        .HasKey(bc => new { bc.LoginDataId, bc.UserId });
            modelBuilder.Entity<SharedLoginData>()
                .HasOne(bc => bc.LoginData)
                .WithMany(b => b.SharedLoginDatas)
                .HasForeignKey(bc => bc.LoginDataId);
            modelBuilder.Entity<SharedLoginData>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.SharedLoginDatas)
                .HasForeignKey(bc => bc.UserId);

            */

        }
        



        
        



        
        
    }
}
