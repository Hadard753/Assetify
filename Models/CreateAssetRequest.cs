using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public class CreateAssetRequest : Asset
    {
        public bool PostToFacebook { get; set; }
    }
}
