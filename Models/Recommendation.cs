using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public class Recommendation
    {
        public int Score { get; set; }
        public Asset Asset { get; set; }
    }
}
