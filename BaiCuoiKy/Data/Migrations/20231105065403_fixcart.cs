using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaiCuoiKy.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quatity",
                table: "GioHang",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "GioHang",
                newName: "Quatity");
        }
    }
}
