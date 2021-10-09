using Microsoft.EntityFrameworkCore.Migrations;

namespace vtbbook.Core.DataAccess.Migrations
{
    public partial class addfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Users_DbUserId",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "DbUserId",
                table: "Coupons",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Coupons_DbUserId",
                table: "Coupons",
                newName: "IX_Coupons_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Coupons",
                newName: "DbUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons",
                newName: "IX_Coupons_DbUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Users_DbUserId",
                table: "Coupons",
                column: "DbUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
