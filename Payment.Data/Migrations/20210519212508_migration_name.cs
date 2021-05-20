using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Data.Migrations
{
    public partial class migration_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<long>(type: "INTEGER", nullable: false),
                    Balance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageType = table.Column<int>(type: "INTEGER", nullable: false),
                    Origin = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Commission = table.Column<double>(type: "REAL", nullable: false),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountId", "Balance" },
                values: new object[] { 1, 4755L, 1001.88 });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountId", "Balance" },
                values: new object[] { 2, 9834L, 456.44999999999999 });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountId", "Balance" },
                values: new object[] { 3, 7735L, 89.359999999999999 });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_AccountId",
                table: "AccountTransactions",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
