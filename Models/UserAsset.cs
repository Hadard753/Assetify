using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{

    public enum ActionType
    {
        PUBLISH,
        LIKE,
        CONTACT
    }

    public class UserAsset
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int AssetID { get; set; }
        public bool IsSeen { get; set; }
        public ActionType Action { get; set; }
        public DateTime ActionTime { get; set; }

        public Asset Asset { get; set; }
        public User User { get; set; }
    }
}
