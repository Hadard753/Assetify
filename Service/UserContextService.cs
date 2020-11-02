using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Assetify.Service
{

    public class UserContext
    {
        public string sessionID { get; set; }
        public bool isAdmin { get; set; }
    }
    public static class UserContextService
    {

        public static UserContext GetUserContext(HttpContext httpContext)
        {

            UserContext userSessionID = new UserContext();
            if (httpContext.Session.GetString("AdminIDSession") != null)
            {
                userSessionID.sessionID = httpContext.Session.GetString("AdminIDSession");
                userSessionID.isAdmin = true;
            }
            else
            {
                userSessionID.sessionID = httpContext.Session.GetString("UserIDSession");
            }

            return userSessionID;
        }
    }
}
