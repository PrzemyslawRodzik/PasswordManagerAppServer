﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PasswordManagerAppResourceServer.Models
{
    public  class UserRelationshipModel
    {
        [Column("user_id")]
        
        public int? UserId { get; set; }
        public User User { get; set; }


    }
}
