using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public enum OrientationType
    {
        North,
        East,
        West,
        South
    }

    public class AssetOrientation
    {
        public int AssetOrientationID { get; set; }
        public int AssetID { get; set; }
        public OrientationType Orientation { get; set; }
    }
}
