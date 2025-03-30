using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchiToursRemake
{
    public static class Session
    {
        public static int? UserID { get; private set; }

        public static void Login(int userId)
        {
            UserID = userId;
        }

        public static void Logout()
        {
            UserID = null;
        }

        public static bool IsLoggedIn()
        {
            return UserID.HasValue;
        }
    }
}
