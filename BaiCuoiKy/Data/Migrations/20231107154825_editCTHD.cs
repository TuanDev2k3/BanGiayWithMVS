using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaiCuoiKy.Data.Migrations
{
    /// <inheritdoc />
    public partial class editCTHD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDon_Product_SanPhamId",
                table: "ChiTietHoaDon");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoaDon_SanPhamId",
                table: "ChiTietHoaDon");

            migrationBuilder.DropColumn(
                name: "SanPhamId",
                table: "ChiTietHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_ProductId",
                table: "ChiTietHoaDon",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDon_Product_ProductId",
                table: "ChiTietHoaDon",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDon_Product_ProductId",
                table: "ChiTietHoaDon");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoaDon_ProductId",
                table: "ChiTietHoaDon");

            migrationBuilder.AddColumn<int>(
                name: "SanPhamId",
                table: "ChiTietHoaDon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_SanPhamId",
                table: "ChiTietHoaDon",
                column: "SanPhamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDon_Product_SanPhamId",
                table: "ChiTietHoaDon",
                column: "SanPhamId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
