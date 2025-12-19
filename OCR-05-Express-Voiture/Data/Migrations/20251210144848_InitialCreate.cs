using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OCR_05_Express_Voiture.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CarBrandId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModel_CarBrand_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrand",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    TrimLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConstructionYear = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    ForSell = table.Column<bool>(type: "bit", nullable: false),
                    Sold = table.Column<bool>(type: "bit", nullable: false),
                    RepairAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_CarBrand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "CarBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarModel_ModelId",
                        column: x => x.ModelId,
                        principalTable: "CarModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    RepairTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repair_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repair_RepairType_RepairTypeId",
                        column: x => x.RepairTypeId,
                        principalTable: "RepairType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarBrand",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Renault" },
                    { 2, "Mazda" },
                    { 3, "Jeep" },
                    { 4, "Ford" },
                    { 5, "Honda" },
                    { 6, "Volkswagen" }
                });

            migrationBuilder.InsertData(
                table: "CarModel",
                columns: new[] { "Id", "BrandId", "CarBrandId", "Name" },
                values: new object[,]
                {
                    { 1, 2, null, "Miata" },
                    { 2, 2, null, "CX-5" },
                    { 3, 3, null, "Wrangler" },
                    { 4, 3, null, "Cherokee" },
                    { 5, 4, null, "Mustang" },
                    { 6, 4, null, "F-150" },
                    { 7, 5, null, "Civic" },
                    { 8, 5, null, "Accord" },
                    { 9, 1, null, "Clio" },
                    { 10, 1, null, "Megane" },
                    { 11, 6, null, "Golf" },
                    { 12, 6, null, "Passat" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_BrandId",
                table: "Car",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ModelId",
                table: "Car",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_CarBrandId",
                table: "CarModel",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_CarId",
                table: "Repair",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_RepairTypeId",
                table: "Repair",
                column: "RepairTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "RepairType");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "CarBrand");
        }
    }
}
