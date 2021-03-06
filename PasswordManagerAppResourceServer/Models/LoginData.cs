﻿using PasswordManagerAppResourceServer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManagerAppResourceServer.Models
{
    [Table("login_data")]
    public class LoginData: UserRelationshipModel,ICompromisedModel,IPasswordModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
       
        [Column("email")]
        public string Email { get; set; }
        [Required]
        [Column("login")]
        public string Login { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }
        [Column("website")]
        public string Website { get; set; }
        [Required]
        [Column("compromised")]
        public int Compromised { get; set; }
        [Required]
        [Column("out-of-date")]
        public int OutOfDate { get; set; } = 0;
        [Required]
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
        
        

       


        public ICollection<SharedLoginData> SharedLoginDatas { get; set; }
        
    }
}
