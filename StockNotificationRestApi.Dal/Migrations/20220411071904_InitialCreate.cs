using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockNotificationRestApi.Dal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUser = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(maxLength: 256, nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 256, nullable: false),
                    UserId = table.Column<string>(maxLength: 256, nullable: false),
                    UserEmail = table.Column<string>(maxLength: 256, nullable: false),
                    NotificationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockNotifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockNotifications_ProductId_UserId_NotificationType",
                table: "StockNotifications",
                columns: new[] { "ProductId", "UserId", "NotificationType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockNotifications");
        }
    }
}
