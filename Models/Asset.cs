using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Range(1, int.MaxValue, ErrorMessage="Price is eaither lower than ziro or too expenssive for this site! please change your input" )]
        [Required]
        public int Price { get; set; }
        public int EstimatedPrice { get; set; }

        [Display(Name = "Furnished")]
        public FurnishedType Furnished { get; set; }
        [Display(Name = "Asset Type")]
        public AssetType TypeId { get; set; }

        public AssetCondition Condition { get; set; }
        [Range(10, int.MaxValue,ErrorMessage ="Size should be larger than 10 (no dog houses here :D ), and not too big (We don't post castles)")]
        
        public int Size { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="Your Garden can't be smaller than 0 and shouldn't be too large (sorry no parks :D )")]
        public int? GardenSize { get; set; }
        [Range(0, int.MaxValue, ErrorMessage ="Balcony size should be at least 0 M*M and not too large")]

        public int? BalconySize { get; set; }
        [Range(1, 100,ErrorMessage ="Room number cunnot be less than 1, And we don't support houses with more than 100 rooms (No castles here :D )")]
        [Required]
        public double Rooms { get; set; }
        [Range(-5, 100, ErrorMessage ="Floor can be at most 100 and at least -5")]
        public int Floor { get; set; }

        [Range(1, 100, ErrorMessage ="More that 100 floors? you are in the wrong site :D, minimum value is 0! ")]
        [Display(Name = "Total Floor")]
        public int TotalFloor { get; set; }
        [Range(0, 100, ErrorMessage ="Cant have less than 0 for parking places, and not more than 100")]

        [Display(Name = "Number Of Parkings")]
        public int;? NumOfParking { get; set; }

        public string? Description { get; set; }
        
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

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public bool IsActive { get; set; }
        public string RemovedReason { get; set; }
        public ICollection<UserAsset> Users { get; set; }
        public ICollection<AssetImage> Images { get; set; }
        public ICollection<AssetOrientation> Orientations { get; set; }
        
        public Address Address { get; set; }

        [NotMapped]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public bool IsFavorite { set; get; }


        [NotMapped]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public bool IsOwner { set; get; }
    }
}
