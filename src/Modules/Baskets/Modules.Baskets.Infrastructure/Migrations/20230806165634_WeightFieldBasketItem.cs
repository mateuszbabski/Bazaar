using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Baskets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WeightFieldBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalWeight",
                table: "Baskets",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BasketItemWeight",
                table: "BasketItems",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "BasketItemWeight",
                table: "BasketItems");
        }
    }
}
