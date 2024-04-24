using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jorge_api.Migrations
{
    /// <inheritdoc />
    public partial class Update_overall_added_inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Products_ProoductId",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "SalesDetails",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "ProoductId",
                table: "SalesDetails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDetails_ProoductId",
                table: "SalesDetails",
                newName: "IX_SalesDetails_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    InStore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Inventory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Products_ProductId",
                table: "SalesDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Products_ProductId",
                table: "SalesDetails");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "SalesDetails",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SalesDetails",
                newName: "ProoductId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDetails_ProductId",
                table: "SalesDetails",
                newName: "IX_SalesDetails_ProoductId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SalesDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Products_ProoductId",
                table: "SalesDetails",
                column: "ProoductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
