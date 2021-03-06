﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public static class SystemConst
    {
        public static string WebUrl = System.Configuration.ConfigurationManager.AppSettings["WebUrl"];
        public static string WebUrlIP = System.Configuration.ConfigurationManager.AppSettings["WebUrlIP"];

        public class Session
        {
            private Session() { }

            public const string LoginSystem = "LoginSystem";
            public const string LoginAccount = "LoginAccount";
        }

        public class Business
        {
            private Business() { }

            public const string FilePath = "/File/Enterprise/{0}";
            public const string FileTempPath = "/File/Temp";

            public const string key = "abcdefgh";
            public const string iv = "12345678";

        }
    }
}
