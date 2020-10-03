using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public class AssetImage
    {
        public int AssetImageID { get; set; }
        public int AssetID { get; set; }
        public string Path{ get; set; }
        public string Type{ get; set; }
        public bool Updated{ get; set; }
    }
}
