using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public class Search
    {
        public int? SearchID { get; set; }
        public int? UserID { get; set; }
        public bool IsCommercial { get; set; }
        public string? City { get; set; }

        public string? Street { get; set; }

        public string? Neighborhoods { get; set; }

        public bool IsForSell { get; set; }
        [Display(Name = "Minimum price")]


        public int? MinPrice { get; set; }
        [Display(Name = "Maximum price")]

        public int? MaxPrice { get; set; }
        [Display(Name = "Minimum size")]

        public int? MinSize { get; set; }
        [Display(Name = "Maximum size")]

        public int? MaxSize { get; set; }
        [Display(Name = "Minimum Garden size")]

        public int? MinGardenSize { get; set; }
        [Display(Name = "Maximum Garden size")]

        public int? MaxGardenSize { get; set; }
        [Display(Name = "Minimum rooms")]

        public int? MinRooms { get; set; }
        [Display(Name = "Maximum rooms")]

        public int? MaxRooms { get; set; }
        [Display(Name = "Minimum floors")]

        public int? MinFloor { get; set; }
        [Display(Name = "Maximum floors")]

        public int? MaxFloor { get; set; }
        [Display(Name = "Minimum floors")]

        public int? MinTotalFloor { get; set; }
        [Display(Name = "Maximum total floors")]

        public int? MaxTotalFloor { get; set; }
        public AssetType TypeIdIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime? MinEntryDate { get; set; }
        public FurnishedType FurnishedIn { get; set; }
        public OrientationType Orientations { get; set; }



        [Display(Name = "Elevator")]
        public bool IsElevator { get; set; }
        [Display(Name = "Balcony")]
        public bool IsBalcony { get; set; }
        [Display(Name = "Terrace")]
        public bool IsTerrace { get; set; }

        [Display(Name = "Storage")]
        public bool IsStorage { get; set; }

        [Display(Name = "Renovated")]
        public bool IsRenovated { get; set; }

        [Display(Name = "Realty Commision")]
        public bool IsRealtyCommission { get; set; }

        [Display(Name = "Aircondition")]
        public bool IsAircondition { get; set; }

        [Display(Name = "Mamad")]
        public bool IsMamad { get; set; }

        [Display(Name = "Pandor Doors")]
        public bool IsPandorDoors { get; set; }

        [Display(Name = "Accessible")]
        public bool IsAccessible { get; set; }

        [Display(Name = "Kosher Kitchen")]
        public bool IsKosherKitchen { get; set; }

        [Display(Name = "Kosher Boiler")]
        public bool IsKosherBoiler { get; set; }

        [Display(Name = "On Pillars")]
        public bool IsOnPillars { get; set; }
        [Display(Name = "Bars")]
        public bool IsBars { get; set; }
        [Display(Name = "Roomate Suitable")]
        public bool IsRoomates { get; set; }
        [Display(Name = "Immediate Entry")]
        public bool IsImmediate { get; set; }

        [Display(Name = "Near Train Station")]
        public bool IsNearTrainStation { get; set; }
        [Display(Name = "Near Light Train Station")]
        public bool IsNearLightTrainStation { get; set; }
        [Display(Name = "Near Beach")]
        public bool IsNearBeach { get; set; }


    }
}
