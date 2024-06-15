using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicios.Api.ShoppingCart.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationMySql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingCartSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartSession", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingCartDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SelectedProduct = table.Column<string>(type: "longtext", nullable: false),
                    CartSessionId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartDetail_ShoppingCartSession_CartSessionId",
                        column: x => x.CartSessionId,
                        principalTable: "ShoppingCartSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartDetail_CartSessionId",
                table: "ShoppingCartDetail",
                column: "CartSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartDetail");

            migrationBuilder.DropTable(
                name: "ShoppingCartSession");
        }
    }
}
