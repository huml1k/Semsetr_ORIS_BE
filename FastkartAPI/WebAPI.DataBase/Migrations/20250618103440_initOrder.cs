using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastkartAPI.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class initOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderModels",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "null"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrackingId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModels", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderModels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderModels_TrackingId",
                table: "OrderModels",
                column: "TrackingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderModels_UserId",
                table: "OrderModels",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderModels");
        }
    }
}
