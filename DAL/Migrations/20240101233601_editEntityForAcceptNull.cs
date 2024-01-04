using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryWebApiFinal.Migrations
{
    public partial class editEntityForAcceptNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounters_AccounterId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Librarians_LibrarianId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "LibrarianId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccounterId",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounters_AccounterId",
                table: "Bills",
                column: "AccounterId",
                principalTable: "Accounters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Librarians_LibrarianId",
                table: "Transactions",
                column: "LibrarianId",
                principalTable: "Librarians",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounters_AccounterId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Librarians_LibrarianId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "LibrarianId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccounterId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounters_AccounterId",
                table: "Bills",
                column: "AccounterId",
                principalTable: "Accounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Librarians_LibrarianId",
                table: "Transactions",
                column: "LibrarianId",
                principalTable: "Librarians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
