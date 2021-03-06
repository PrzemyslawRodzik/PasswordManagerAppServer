﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManagerAppResourceServer.Models
{
    [Table("personal_info")]
    public class PersonalInfo:UserRelationshipModel
    {
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("second_name")]
        public string SecondName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("date_of_birth",TypeName ="Date")]
        public DateTime DateOfBirth { get; set; }


        public ICollection<Address> Addresses { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        


    }
}
