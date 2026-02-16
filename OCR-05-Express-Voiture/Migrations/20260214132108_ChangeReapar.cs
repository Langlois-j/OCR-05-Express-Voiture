using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OCR_05_Express_Voiture.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReapar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepairAmount",
                table: "Car",
                newName: "SellPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellPrice",
                table: "Car",
                newName: "RepairAmount");
        }
    }
}
