using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Data.Migrations
{
    public partial class PropertyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RealEstateProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propertyname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propertyprice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    depositamount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propertyaddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OffertypeLiist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertytypeList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertystatusList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FurnishedstatusList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bedroomsList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bathroomsList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    balconysList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OffertypeId = table.Column<int>(type: "int", nullable: false),
                    PropertytypeId = table.Column<int>(type: "int", nullable: false),
                    PropertystatusId = table.Column<int>(type: "int", nullable: false),
                    FurnishedstatusId = table.Column<int>(type: "int", nullable: false),
                    bedroomsId = table.Column<int>(type: "int", nullable: false),
                    bathroomsId = table.Column<int>(type: "int", nullable: false),
                    balconysId = table.Column<int>(type: "int", nullable: false),
                    carpetarea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propertyage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalfloors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    floorroom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propertydescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagesCounts = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSaved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateProperty_Balconys_balconysId",
                        column: x => x.balconysId,
                        principalTable: "Balconys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateProperty_Bathrooms_bathroomsId",
                        column: x => x.bathroomsId,
                        principalTable: "Bathrooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateProperty_Bedrooms_bedroomsId",
                        column: x => x.bedroomsId,
                        principalTable: "Bedrooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateProperty_furnishedstatus_FurnishedstatusId",
                        column: x => x.FurnishedstatusId,
                        principalTable: "furnishedstatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateProperty_offertype_OffertypeId",
                        column: x => x.OffertypeId,
                        principalTable: "offertype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateProperty_propertystatus_PropertystatusId",
                        column: x => x.PropertystatusId,
                        principalTable: "propertystatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateProperty_propertytype_PropertytypeId",
                        column: x => x.PropertytypeId,
                        principalTable: "propertytype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_savedProps_PostId",
                table: "savedProps",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_PostId",
                table: "Requests",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateProperty_balconysId",
                table: "RealEstateProperty",
                column: "balconysId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateProperty_bathroomsId",
                table: "RealEstateProperty",
                column: "bathroomsId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateProperty_bedroomsId",
                table: "RealEstateProperty",
                column: "bedroomsId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateProperty_FurnishedstatusId",
                table: "RealEstateProperty",
                column: "FurnishedstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateProperty_OffertypeId",
                table: "RealEstateProperty",
                column: "OffertypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateProperty_PropertystatusId",
                table: "RealEstateProperty",
                column: "PropertystatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateProperty_PropertytypeId",
                table: "RealEstateProperty",
                column: "PropertytypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RealEstateProperty_PostId",
                table: "Requests",
                column: "PostId",
                principalTable: "RealEstateProperty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_savedProps_RealEstateProperty_PostId",
                table: "savedProps",
                column: "PostId",
                principalTable: "RealEstateProperty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RealEstateProperty_PostId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_savedProps_RealEstateProperty_PostId",
                table: "savedProps");

            migrationBuilder.DropTable(
                name: "RealEstateProperty");

            migrationBuilder.DropIndex(
                name: "IX_savedProps_PostId",
                table: "savedProps");

            migrationBuilder.DropIndex(
                name: "IX_Requests_PostId",
                table: "Requests");
        }
    }
}
