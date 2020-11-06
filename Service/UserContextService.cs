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
        public string userSessionID { get; set; }
        public string adminSessionID { get; set; }
        public bool isAdmin { get; set; }
    }
    public static class UserContextService
    {

        public static UserContext GetUserContext (HttpContext httpContext)
        {

            UserContext userSessionID = new UserContext();
            
            if (httpContext.Session.GetString("AdminIDSession") != null)
            {
                userSessionID.adminSessionID = httpContext.Session.GetString("AdminIDSession");
                userSessionID.isAdmin = true;
            }
            userSessionID.userSessionID = httpContext.Session.GetString("UserIDSession");

            return userSessionID;
        }
    }
}
