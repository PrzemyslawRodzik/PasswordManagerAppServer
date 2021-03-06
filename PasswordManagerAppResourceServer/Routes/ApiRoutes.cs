﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManagerAppResourceServer.Routes
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Notes
        {
            public const string GetAll = Base + "/notes";

            public const string Update = Base + "/notes/{noteId}";

            public const string Delete = Base + "/notes/{noteId}";

            public const string Get = Base + "/notes/{noteId}";

            public const string Create = Base + "/notes";
        }

        public static class Tags
        {
            public const string GetAll = Base + "/tags";

            public const string Get = Base + "/tags/{tagName}";

            public const string Create = Base + "/tags";

            public const string Delete = Base + "/tags/{tagName}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string Refresh = Base + "/identity/refresh";

            
        }
    }
}
