using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public class UserSearch
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserIndex
    {
        public IEnumerable<User> users { get; set; }
        public UserSearch userSearch { get; set; }

    }
}
