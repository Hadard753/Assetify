using Assetify.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Assetify.Data
{
    public class DbInitializer
    {
        public static void Initialize(AssetifyContext context)
        {
            context.Database.EnsureCreated();
            if (context.Assets.Any()) { return; }

            var address = new Address[]
            {
                new Address{City="Holon",Street="Emek Dotan", Building="3", Full="Emek Dotan 3, Holon", IsPublic=false, Neighborhood="Kiryat sharet", Latitude=32.0063753, Longitude=34.7917401 },
                new Address{City="Maor",Street="Azait", Building="101", Full="Azait 101, Moshav Maor", IsPublic=true, Neighborhood="", Latitude=32.4219787, Longitude=35.0057886 },
                new Address{City="Tel Aviv",Street="Begin Road", Building="150", Full="", IsPublic=false, Neighborhood="", Latitude=32.0790551, Longitude=34.7970892 },
                new Address{City="Bat Yam",Street="Hahazmaot", Building="150", Full="", IsPublic=false, Neighborhood="", Latitude=32.029274, Longitude=34.7521107 },
                new Address{City="Harish",Street="Turkiz", Building="9", Full="", IsPublic=true, Neighborhood="Avnei Hen", Latitude=32.4624368, Longitude=35.0484248 },
                new Address{City="Holon",Street="Harokmim", Building="26", Full="", IsPublic=true, Neighborhood="", Latitude=32.0077569, Longitude=34.8028279 },
                new Address{City="Tibiria",Street="Cian Beach",Building="1",Full="Cian Beach 1,Tibiria", IsPublic=true, Neighborhood="Cian Beach", Latitude=32.793605, Longitude=35.541714 },// 7,
                new Address{City="Ramat Gan",Street="Tel Yehuda",Building="2",Full="Tell Yehuda 2,Ramat Gan", IsPublic=true, Neighborhood="Tel Yehuda", Latitude=32.073424, Longitude=34.821151 }, //8
                new Address{City="Netanya",Street="Kiryat Rabin",Building="3",Full="Kiryat Rabin 3,Netanya", IsPublic=true, Neighborhood="Kiryat Rabin", Latitude=32.312725, Longitude=34.881646 }, //9
                new Address{City="Petah Tikva",Street="Hacongress",Building="5",Full="Hacongress 5,Petah Tikva", IsPublic=true, Neighborhood="Center", Latitude=32.093987, Longitude=34.892226 }, // 10
                new Address{City="Kfar Saba",Street="Zetler",Building="16",Full="Zetler 16,Kvar Saba", IsPublic=true, Neighborhood="Kfar Saba Hayeruka", Latitude=32.196857, Longitude=34.888570 }, // 11
                new Address{City="Tel Aviv",Street="Levin Kipnis",Building="11",Full="Levin Kipnis 11,Tel Aviv", IsPublic=true, Neighborhood="Hamashtela", Latitude=32.128161, Longitude=34.831489 }, // 12
                new Address{City="Ashdod",Street="Shevet Naftali",Building="12",Full="Shevet Naftali 12, Ashdod", IsPublic=true, Neighborhood="Rova Yod-Bet", Latitude=31.776679, Longitude=34.633100 }, // 13
                new Address{City="Haifa",Street="Hertzel",Building="2",Full="Hertzerl 2, Ashdod", IsPublic=true, Neighborhood="Hadar", Latitude=32.812981, Longitude=34.995513 }, // 14
              //  new Address{City="Tibiria",Street="Oranim", Building="1", Full="Oranim 1, Tibiria", IsPublic=false, Neighborhood="Ramot Tibiria", Latitude=333, Longitude=333 }
            };

            foreach (Address a in address)
            {
                context.Addresses.Add(a);
            }

            context.SaveChanges();

            DateTime now = DateTime.Now;
            var users = new User[]
            {
                new User{Email = "test@c.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "test", LastName = "Stam", Phone = "052-2222222", IsVerified = true, ProfileImgPath = "student.PNG", LastSeenFavorite = now, LastSeenMessages = now },
                new User{Email = "test2@c.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "exam", LastName = "Publisher", Phone = "052-33333333", IsVerified = false, ProfileImgPath = "student.PNG", LastSeenFavorite = now, LastSeenMessages = now },
                new User{Email = "galhrrs@gmail.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gal", LastName = "Harris", Phone = "055-6633084", IsVerified = false, ProfileImgPath = "Happyhouse1.jpg", LastSeenFavorite = now, LastSeenMessages = now},
                new User{Email = "a@a.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "git-avatar.png", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "b@b.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala2", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "git-avatar.png", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "c@c.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala3", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "git-avatar.png", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "d@d.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala4", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "git-avatar.png", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "e@e.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala5", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "github12.gif", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "f@f.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala6", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "github12.gif", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "g@g.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala7", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "github12.gif", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "h@h.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala8", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "github12.gif", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "i@i.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala9", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "github12.gif", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "galhrrsa10@gmail.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala10", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "github-logo.png", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "galhrrsa11@gmail.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala11", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "github-logo.png", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "galhrrsa12@gmail.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala12", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "Happyhouse1.jpg", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "galhrrsa13@gmail.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala13", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "Happyhouse1.jpg", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "galhrrsa14@gmail.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala14", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "Happyhouse1.jpg", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "galhrrsa15@gmail.com", Password = "AKlBXpxayQs1JJXd6H0YmWthV0tPnXLvcXvcJFWM1DOYWVOvsHK1cwrDzsTyfbQ+MA==", FirstName = "Gala15", LastName = "Harrisa", Phone = "055-6633085", IsVerified = false, ProfileImgPath = "Happyhouse1.jpg", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
                new User{Email = "s@b.com", Password = "AECEBqBu+7PDqkFL5nLuWFgcXTmDD0J71Egn6KXMCJMhmLeCMSlEqmrUVFfLkFD9Tg==", FirstName = "Stav", LastName = "Bernaz", Phone = "054-9983997", IsVerified = false, ProfileImgPath = "Happyhouse1.jpg", LastSeenFavorite = now, LastSeenMessages = now, IsAdmin = true},
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            DateTime oneDaysAgo = now.AddDays(-1);
            DateTime twoDaysago = now.AddDays(-2);
            DateTime threeDaysago = now.AddDays(-3);
            DateTime fourDaysago = now.AddDays(-4);
            DateTime fiveDaysago = now.AddDays(-5);
            var assets = new Asset[]
            {
                new Asset{AddressID=14, CreatedAt=oneDaysAgo, Price=950000, BalconySize=0, Rooms=4, Size=90, Floor=1, GardenSize=0, TotalFloor=3, Description="Great for investments", Condition=AssetCondition.New, EntryDate=now, IsAircondition=true, IsActive=true, IsBalcony=false, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Full, IsElevator=false, IsBars=false, IsImmediate=true, IsMamad=true, IsNearBeach=false, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=true, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, IsTerrace=false}, // 10
                new Asset{AddressID=4, CreatedAt=twoDaysago, Price=1350000, BalconySize=17, Condition=AssetCondition.Renovated, Description="Beach View", EntryDate=now, Floor=4, IsAircondition=true, IsActive=true, IsBalcony=true, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Partial, IsElevator=true, IsBars=false, IsImmediate=true, IsMamad=true, IsNearBeach=true, TotalFloor=6, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=false, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=false, IsRealtyCommission=false, IsRenovated=true, IsRoomates=false, IsStorage=false, Rooms=3, Size=70, IsTerrace=false}, // 9
                new Asset{AddressID=4, CreatedAt=fourDaysago,Price=1400000, BalconySize=17, Condition=AssetCondition.New, Description="Big Garden", EntryDate=now, Floor=0, IsAircondition=true, IsActive=true, IsBalcony=true, IsCommercial=false, TypeId=AssetType.GardenApartment, Furnished=FurnishedType.Full, IsElevator=true, IsBars=false, IsImmediate=true, IsMamad=true, IsNearBeach=false, TotalFloor=6, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=false, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, Rooms=4, Size=110, IsTerrace=false, GardenSize=100}, // 8
                new Asset{AddressID=7, CreatedAt=twoDaysago,Price=385000, BalconySize=10, Condition=AssetCondition.New, Description="Near the Kineret", EntryDate=now, Floor=6, IsAircondition=true, IsActive=true, IsBalcony=true, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Full, IsElevator=true, IsBars=false, IsImmediate=true, IsMamad=false, IsNearBeach=true, TotalFloor=6, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=false, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, Rooms=2, Size=43, IsTerrace=false, GardenSize=0}, // 7
                new Asset{AddressID=8, CreatedAt=oneDaysAgo,Price=4600000, BalconySize=3, Condition=AssetCondition.New, Description="Awesome living room sunlight", EntryDate=now, Floor=1, IsAircondition=true, IsActive=true, IsBalcony=true, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Full, IsElevator=true, IsBars=false, IsImmediate=true, IsMamad=false, IsNearBeach=false, TotalFloor=2, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=false, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, Rooms=4, Size=140, IsTerrace=false, GardenSize=0}, // 6
                new Asset{AddressID=9, CreatedAt=twoDaysago,Price=2010000, BalconySize=12, Condition=AssetCondition.New, Description="Great location with 3 airways", EntryDate=now, Floor=5, IsAircondition=true, IsActive=true, IsBalcony=true, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Full, IsElevator=true, IsBars=false, IsImmediate=true, IsMamad=true, IsNearBeach=false, TotalFloor=9, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=true, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, Rooms=5, Size=130, IsTerrace=false, GardenSize=0}, // 5
                new Asset{AddressID=10, CreatedAt=threeDaysago, Price=178500, BalconySize=30, Rooms=4, Size=98, Floor=3, GardenSize=0, TotalFloor=4, Description="Botique Building", Condition=AssetCondition.New, EntryDate=now, IsAircondition=true, IsActive=true, IsBalcony=true, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Full, IsElevator=true, IsBars=false, IsImmediate=true, IsMamad=true, IsNearBeach=false, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=true, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, IsTerrace=false}, // 4
                new Asset{AddressID=11, CreatedAt=fourDaysago, Price=2700000, BalconySize=13, Rooms=3, Size=75, Floor=6, GardenSize=0, TotalFloor=6, Description="Well-kept new apartment", Condition=AssetCondition.New, EntryDate=now, IsAircondition=true, IsActive=true, IsBalcony=true, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Full, IsElevator=true, IsBars=false, IsImmediate=true, IsMamad=true, IsNearBeach=false, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=true, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, IsTerrace=false}, // 3
                new Asset{AddressID=12, CreatedAt=fiveDaysago, Price=3250000, BalconySize=28, Rooms=4, Size=94, Floor=4, GardenSize=0, TotalFloor=7, Description="Fully Furnished with access to gym and pool", Condition=AssetCondition.New, EntryDate=now, IsAircondition=true, IsActive=true, IsBalcony=true, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Full, IsElevator=true, IsBars=false, IsImmediate=true, IsMamad=true, IsNearBeach=false, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=true, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, IsTerrace=false}, // 2
                new Asset{AddressID=13, CreatedAt=oneDaysAgo, Price=2900000, BalconySize=0, Rooms=5, Size=170, Floor=1, GardenSize=20, TotalFloor=1, Description="Beautiful garden and close to shopping center", Condition=AssetCondition.New, EntryDate=now, IsAircondition=true, IsActive=true, IsBalcony=false, IsCommercial=false, TypeId=AssetType.Apartment, Furnished=FurnishedType.Full, IsElevator=false, IsBars=false, IsImmediate=true, IsMamad=true, IsNearBeach=false, IsAccessible=true, IsForSell=true, IsKosherBoiler=false, IsOnPillars=true, IsKosherKitchen=false, IsNearLightTrainStation=true, IsNearTrainStation=true, IsPandorDoors=true, IsRealtyCommission=true, IsRenovated=false, IsRoomates=false, IsStorage=true, IsTerrace=false}, // 1
            };

            foreach (Asset a in assets)
            {
                context.Assets.Add(a);
            }

            context.SaveChanges();

            var userAssets = new UserAsset[]
            {
                new UserAsset{ UserID=2, AssetID=1, ActionTime=twoDaysago, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=2, AssetID=2, ActionTime=fourDaysago, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=12, AssetID=3, ActionTime=twoDaysago, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=11, AssetID=4, ActionTime=oneDaysAgo, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=10, AssetID=5, ActionTime=twoDaysago, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=9, AssetID=6, ActionTime=threeDaysago, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=8, AssetID=7, ActionTime=fiveDaysago, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=7, AssetID=8, ActionTime=fiveDaysago, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=6, AssetID=9, ActionTime=oneDaysAgo, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=6, AssetID=10, ActionTime=oneDaysAgo, Action=ActionType.PUBLISH, IsSeen=true},
                new UserAsset{ UserID=4, AssetID=2, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=2, AssetID=2, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=3, AssetID=2, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=4, AssetID=4, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=5, AssetID=4, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=6, AssetID=2, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=7, AssetID=2, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=8, AssetID=3, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=9, AssetID=3, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=10, AssetID=9, ActionTime=now, Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=11, AssetID=9, ActionTime=now.AddDays(-1), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=12, AssetID=5, ActionTime=now.AddDays(-1), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=13, AssetID=5, ActionTime=now.AddDays(-1), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=5, AssetID=2, ActionTime=now.AddDays(-1), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=6, AssetID=2, ActionTime=now.AddDays(-1), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=7, AssetID=2, ActionTime=now.AddDays(-1), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=8, AssetID=3, ActionTime=now.AddDays(-1), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=9, AssetID=3, ActionTime=now.AddDays(-1), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=10, AssetID=3, ActionTime=now.AddDays(-2), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=11, AssetID=3, ActionTime=now.AddDays(-2), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=12, AssetID=5, ActionTime=now.AddDays(-2), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=2, ActionTime=now.AddDays(-2), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=2, ActionTime=now.AddDays(-2), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=8, ActionTime=now.AddDays(-3), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=8, ActionTime=now.AddDays(-3), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=8, ActionTime=now.AddDays(-3), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=6, ActionTime=now.AddDays(-3), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=7, ActionTime=now.AddDays(-3), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=5, ActionTime=now.AddDays(-4), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=7, ActionTime=now.AddDays(-4), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=7, ActionTime=now.AddDays(-4), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-4), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-4), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-4), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-4), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-5), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-5), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-6), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-6), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-6), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-6), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-6), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-6), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-7), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-7), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-7), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-7), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-7), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-7), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-7), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-8), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-8), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-8), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-8), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-8), Action=ActionType.LIKE, IsSeen=true},
                new UserAsset{ UserID=1, AssetID=9, ActionTime=now.AddDays(-8), Action=ActionType.LIKE, IsSeen=true},
            };

            foreach (UserAsset ua in userAssets)
            {
                context.UserAsset.Add(ua);
            }

            context.SaveChanges();
            var assetsOrientations = new AssetOrientation[]
            {
                new AssetOrientation{AssetID=1, Orientation=OrientationType.West},
                new AssetOrientation{AssetID=1, Orientation=OrientationType.North},
                new AssetOrientation{AssetID=2, Orientation=OrientationType.North},
                new AssetOrientation{AssetID=2, Orientation=OrientationType.West},
                new AssetOrientation{AssetID=2, Orientation=OrientationType.East},
            };

            foreach (AssetOrientation o in assetsOrientations)
            {
                context.Orientations.Add(o);
            }

            var assetsImages = new AssetImage[]
            {
                new AssetImage{AssetID=2, Path="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQv27RpcOyfXCXWNQAIP4ZCE1wog76uF57dbQ&usqp=CAU", Type=""},
                new AssetImage{AssetID=2, Path="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRVKDRaQ_xwdWIn8fZ8i6igk4f1dE_fPjLgZw&usqp=CAU", Type=""},
                new AssetImage{AssetID=2, Path="https://q-xx.bstatic.com/xdata/images/hotel/840x460/134503030.jpg?k=84fc1387bcaaf7bed45609874b06ecacc2ca723de4046de353b2dca04ce937ca&o=", Type=""},
                new AssetImage{AssetID=2, Path="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcT4zjHfKdPkpEAwU7K5wzQSGCvjkIqGAmNh0A&usqp=CAU", Type=""},
                new AssetImage{AssetID=1, Path="data:image/jpeg;base64,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", Type=""},
                new AssetImage{AssetID=1, Path="https://imgcy.trivago.com/c_lfill,d_dummy.jpeg,e_sharpen:60,f_auto,h_450,q_auto,w_450/itemimages/60/52/6052426.jpeg", Type=""},
                new AssetImage{AssetID=3, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=201707&fn=nvFile3910828.jpg", Type=""},
                new AssetImage{AssetID=3, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=201707&fn=nvFile3910829.jpg", Type=""},
                new AssetImage{AssetID=3, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=201707&fn=nvFile3910830.jpg", Type=""},
                new AssetImage{AssetID=3, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=201707&fn=nvFile3910831.jpg", Type=""},
                new AssetImage{AssetID=4, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202006&fn=nvFile3869635.jpg", Type=""},
                new AssetImage{AssetID=4, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202006&fn=nvFile3869637.jpg", Type=""},
                new AssetImage{AssetID=4, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202006&fn=nvFile3869638.jpg", Type=""},
                new AssetImage{AssetID=4, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202006&fn=nvFile3869639.jpg", Type=""},
                new AssetImage{AssetID=5, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202011&fn=nvFile3922433.jpg", Type=""},
                new AssetImage{AssetID=5, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202011&fn=nvFile3922434.jpg", Type=""},
                new AssetImage{AssetID=5, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202011&fn=nvFile3922435.jpg", Type=""},
                new AssetImage{AssetID=5, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202011&fn=nvFile3922436.jpg", Type=""},
                new AssetImage{AssetID=6, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202010&fn=nvFile3912921.jpg", Type=""},
                new AssetImage{AssetID=6, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202010&fn=nvFile3912925.jpg", Type=""},
                new AssetImage{AssetID=6, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202010&fn=nvFile3912923.jpg", Type=""},
                new AssetImage{AssetID=6, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202010&fn=nvFile3912924.jpg", Type=""},
                new AssetImage{AssetID=7, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202010&fn=nvFile3908168.jpg", Type=""},
                new AssetImage{AssetID=7, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202010&fn=nvFile3908169.jpeg", Type=""},
                new AssetImage{AssetID=7, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202010&fn=nvFile3908171.jpg", Type=""},
                new AssetImage{AssetID=8, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202009&fn=nvFile3899814.jpg", Type=""},
                new AssetImage{AssetID=8, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202009&fn=nvFile3899818.jpg", Type=""},
                new AssetImage{AssetID=9, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202008&fn=nvFile3893075.jpg", Type=""},
                new AssetImage{AssetID=9, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202008&fn=nvFile3893076.jpg", Type=""},
                new AssetImage{AssetID=9, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202008&fn=nvFile3893077.jpg", Type=""},
                new AssetImage{AssetID=9, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202008&fn=nvFile3893078.jpg", Type=""},
                new AssetImage{AssetID=10, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202005&fn=nvFile3850357.jpeg", Type=""},
                new AssetImage{AssetID=10, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202005&fn=nvFile3850358.jpeg", Type=""},
                new AssetImage{AssetID=10, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202005&fn=nvFile3850359.jpg", Type=""},
                new AssetImage{AssetID=10, Path="https://uploads.homeless.co.il/getImage.ashx?type=sale&fs=202005&fn=nvFile3850360.jpeg", Type=""},
            };

            foreach (AssetImage i in assetsImages)
            {
                context.Images.Add(i);
            }

            context.SaveChanges();
        }
    }
}
