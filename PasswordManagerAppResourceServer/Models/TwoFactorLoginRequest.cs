﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManagerAppResourceServer.Models
{
    public class TwoFactorLoginRequest
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
