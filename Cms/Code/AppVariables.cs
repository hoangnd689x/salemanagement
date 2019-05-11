using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Code
{
    public class AppVariables
    {
        public static string AuthenticationScheme
        {
            get
            {
                return "AppAuthenticationScheme";
            }
        }

        public static string AppUserState
        {
            get
            {
                return "AppUserState";
            }
        }
    }
}
