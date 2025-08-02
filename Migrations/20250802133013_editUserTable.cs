using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPortal.Migrations
{
    /// <inheritdoc />
    public partial class editUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtpGeneratedAt",
                table: "Users",
                newName: "OtpExpiresAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtpExpiresAt",
                table: "Users",
                newName: "OtpGeneratedAt");
        }
    }
}
