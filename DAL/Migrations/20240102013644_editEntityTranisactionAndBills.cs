using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryWebApiFinal.Migrations
{
    public partial class editEntityTranisactionAndBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "BrrowStatus",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnStats",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Books",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrrowStatus",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReturnStats",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
