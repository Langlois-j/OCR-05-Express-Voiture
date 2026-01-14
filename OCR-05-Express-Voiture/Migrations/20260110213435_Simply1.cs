using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OCR_05_Express_Voiture.Migrations
{
    /// <inheritdoc />
    public partial class Simply1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarBrand_CarBrandId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarModel_CarModelId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_CarBrand_CarBrandId",
                table: "CarModel");

            migrationBuilder.DropTable(
                name: "CarRepair");

            migrationBuilder.DropTable(
                name: "RepairType");

            migrationBuilder.DropIndex(
                name: "IX_Car_CarModelId",
                table: "Car");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepairDescription",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Car_BrandId",
                table: "Car",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarBrand_BrandId",
                table: "Car",
                column: "BrandId",
                principalTable: "CarBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarModel_CarBrandId",
                table: "Car",
                column: "CarBrandId",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_CarBrand_CarBrandId",
                table: "CarModel",
                column: "CarBrandId",
                principalTable: "CarBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarBrand_BrandId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarModel_CarBrandId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_CarBrand_CarBrandId",
                table: "CarModel");

            migrationBuilder.DropIndex(
                name: "IX_Car_BrandId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "RepairDescription",
                table: "Car");

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
                name: "CarRepair",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    RepairTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRepair", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRepair_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRepair_RepairType_RepairTypeId",
                        column: x => x.RepairTypeId,
                        principalTable: "RepairType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RepairType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Restauration Complete" },
                    { 2, "Rotule Avant" },
                    { 3, "Rotule Arriere" },
                    { 4, "Radiateur  " },
                    { 5, "Pneus Avant" },
                    { 6, "Pneus Arriere" },
                    { 7, "Freins" },
                    { 8, "Climatisation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarModelId",
                table: "Car",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRepair_CarId",
                table: "CarRepair",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRepair_RepairTypeId",
                table: "CarRepair",
                column: "RepairTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarBrand_CarBrandId",
                table: "Car",
                column: "CarBrandId",
                principalTable: "CarBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarModel_CarModelId",
                table: "Car",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_CarBrand_CarBrandId",
                table: "CarModel",
                column: "CarBrandId",
                principalTable: "CarBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
