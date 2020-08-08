using System;
using System.Collections.Generic;
using System.Text;
using UserDatabase;

namespace UserRepository
{
    public class DatabaseManager
    {
        static DatabaseManager()
        {
            Instance = new UserContext();
        }
        public static UserContext Instance { get; private set; }
    }
}

