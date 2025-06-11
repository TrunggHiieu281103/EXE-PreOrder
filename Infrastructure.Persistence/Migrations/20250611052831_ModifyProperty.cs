using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserAddresses_UserAddress",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserAddress",
                table: "Orders",
                newName: "UserAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserAddress",
                table: "Orders",
                newName: "IX_Orders_UserAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAddresses_UserAddressId",
                table: "Orders",
                column: "UserAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserAddresses_UserAddressId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserAddressId",
                table: "Orders",
                newName: "UserAddress");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserAddressId",
                table: "Orders",
                newName: "IX_Orders_UserAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAddresses_UserAddress",
                table: "Orders",
                column: "UserAddress",
                principalTable: "UserAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
