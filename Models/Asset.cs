using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Models
{
    public enum AssetType
    {
        Apartment,
        GardenApartment,
        Villa,
        Panthouse,
        Land,
        MultiFamily,
        Duplex,
        Basement,
        Triplex,
        ResidentialUnit,
        Studio,
        Storeroom,
        ParkingLot,
        Sablet,
        General
    }

    public enum AssetCondition
    {
        FromContractor,
        New,
        Renovated,
        Preserve,
        RequireRenovation
    }
    public enum FurnishedType
    {
        No,
        Partial,
        Full
    }

    public class Asset
    {
        public int AssetID { get; set; }
        public int AddressID { get; set; }

        [Range(1, 100000000)]
        [Required]
        public int Price { get; set; }
        public int EstimatedPrice { get; set; }

        [Display(Name = "Furnished")]
        public FurnishedType Furnished { get; set; }
        [Display(Name = "Asset Type")]
        public AssetType TypeId { get; set; }

        public AssetCondition Condition { get; set; }
        [Range(1, 100000)]
        [Required]
        public int Size { get; set; }

        public int GardenSize { get; set; }
        public int BalconySize { get; set; }
        [Range(1, 50)]
        [Required]
        public double Rooms { get; set; }
        [Range(1, 100)]
        [Required]
        public int Floor { get; set; }

        [Range(1, 100)]
        [Display(Name = "Total Floor")]
        public int TotalFloor { get; set; }

        [Display(Name = "Number Of Parkings")]
        public int NumOfParking { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; }

        public DateTime CreatedAt { get; set; }


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
        [Display(Name = "Sell")]
        public bool IsForSell { get; set; }
        [Display(Name = "Commercial")]
        public bool IsCommercial { get; set; }
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
        public bool IsActive { get; set; }
        public string RemovedReason { get; set; }
        public ICollection<UserAsset> Users { get; set; }
        public ICollection<AssetImage> Images { get; set; }
        public ICollection<AssetOrientation> Orientations { get; set; }

        public Address Address { get; set; }

        

    }
}
