using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public class User
    {
        public int UserID { get; set; }
        //public int AddressID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsVerified { get; set; }
        public string ProfileImgPath { get; set; }
        public DateTime LastSeenFavorite { get; set; }
        public DateTime LastSeenMessages { get; set; }

        public ICollection<UserAsset> Assets { get; set; }
        public ICollection<Search> SearchHistory { get; set; }
        //public Address Address { get; set; }

    }
}
