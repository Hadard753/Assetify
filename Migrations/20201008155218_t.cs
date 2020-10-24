using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assetify.Migrations
{
    public partial class t : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    Full = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    Latitude = table.Column<int>(nullable: false),
                    Longitude = table.Column<int>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    ProfileImgPath = table.Column<string>(nullable: true),
                    LastSeenFavorite = table.Column<DateTime>(nullable: false),
                    LastSeenMessages = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressID = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    EstimatedPrice = table.Column<int>(nullable: false),
                    Furnished = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Condition = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    GardenSize = table.Column<int>(nullable: false),
                    BalconySize = table.Column<int>(nullable: false),
                    Rooms = table.Column<double>(nullable: false),
                    Floor = table.Column<int>(nullable: false),
                    TotalFloor = table.Column<int>(nullable: false),
                    NumOfParking = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsElevator = table.Column<bool>(nullable: false),
                    IsBalcony = table.Column<bool>(nullable: false),
                    IsTerrace = table.Column<bool>(nullable: false),
                    IsStorage = table.Column<bool>(nullable: false),
                    IsRenovated = table.Column<bool>(nullable: false),
                    IsRealtyCommission = table.Column<bool>(nullable: false),
                    IsAircondition = table.Column<bool>(nullable: false),
                    IsMamad = table.Column<bool>(nullable: false),
                    IsPandorDoors = table.Column<bool>(nullable: false),
                    IsAccessible = table.Column<bool>(nullable: false),
                    IsKosherKitchen = table.Column<bool>(nullable: false),
                    IsKosherBoiler = table.Column<bool>(nullable: false),
                    IsOnPillars = table.Column<bool>(nullable: false),
                    IsBars = table.Column<bool>(nullable: false),
                    IsForSell = table.Column<bool>(nullable: false),
                    IsCommercial = table.Column<bool>(nullable: false),
                    IsRoomates = table.Column<bool>(nullable: false),
                    IsImmediate = table.Column<bool>(nullable: false),
                    IsNearTrainStation = table.Column<bool>(nullable: false),
                    IsNearLightTrainStation = table.Column<bool>(nullable: false),
                    IsNearBeach = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RemovedReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetID);
                    table.ForeignKey(
                        name: "FK_Assets_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Searches",
                columns: table => new
                {
                    SearchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    IsCommercial = table.Column<bool>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Neighborhoods = table.Column<string>(nullable: true),
                    IsForSell = table.Column<bool>(nullable: false),
                    MinPrice = table.Column<int>(nullable: false),
                    MaxPrice = table.Column<int>(nullable: false),
                    MinSize = table.Column<int>(nullable: false),
                    MaxSize = table.Column<int>(nullable: false),
                    MinGardenSize = table.Column<int>(nullable: false),
                    MaxGardenSize = table.Column<int>(nullable: false),
                    MinRooms = table.Column<int>(nullable: false),
                    MaxRooms = table.Column<int>(nullable: false),
                    MinFloor = table.Column<int>(nullable: false),
                    MaxFloor = table.Column<int>(nullable: false),
                    MinTotalFloor = table.Column<int>(nullable: false),
                    MaxTotalFloor = table.Column<int>(nullable: false),
                    TypeIdIn = table.Column<int>(nullable: false),
                    MinEntryDate = table.Column<DateTime>(nullable: false),
                    FurnishedIn = table.Column<int>(nullable: false),
                    Orientations = table.Column<int>(nullable: false),
                    IsElevator = table.Column<bool>(nullable: false),
                    IsBalcony = table.Column<bool>(nullable: false),
                    IsTerrace = table.Column<bool>(nullable: false),
                    IsStorage = table.Column<bool>(nullable: false),
                    IsRenovated = table.Column<bool>(nullable: false),
                    IsRealtyCommission = table.Column<bool>(nullable: false),
                    IsAircondition = table.Column<bool>(nullable: false),
                    IsMamad = table.Column<bool>(nullable: false),
                    IsPandorDoors = table.Column<bool>(nullable: false),
                    IsAccessible = table.Column<bool>(nullable: false),
                    IsKosherKitchen = table.Column<bool>(nullable: false),
                    IsKosherBoiler = table.Column<bool>(nullable: false),
                    IsOnPillars = table.Column<bool>(nullable: false),
                    IsBars = table.Column<bool>(nullable: false),
                    IsRoomates = table.Column<bool>(nullable: false),
                    IsImmediate = table.Column<bool>(nullable: false),
                    IsNearTrainStation = table.Column<bool>(nullable: false),
                    IsNearLightTrainStation = table.Column<bool>(nullable: false),
                    IsNearBeach = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Searches", x => x.SearchID);
                    table.ForeignKey(
                        name: "FK_Searches_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    AssetImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetID = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Updated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.AssetImageID);
                    table.ForeignKey(
                        name: "FK_Images_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orientations",
                columns: table => new
                {
                    AssetOrientationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetID = table.Column<int>(nullable: false),
                    Orientation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orientations", x => x.AssetOrientationID);
                    table.ForeignKey(
                        name: "FK_Orientations_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAsset",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    AssetID = table.Column<int>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false),
                    Action = table.Column<int>(nullable: false),
                    ActionTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAsset", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserAsset_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAsset_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AddressID",
                table: "Assets",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AssetID",
                table: "Images",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_Orientations_AssetID",
                table: "Orientations",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_Searches_UserID",
                table: "Searches",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAsset_AssetID",
                table: "UserAsset",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAsset_UserID",
                table: "UserAsset",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Orientations");

            migrationBuilder.DropTable(
                name: "Searches");

            migrationBuilder.DropTable(
                name: "UserAsset");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
