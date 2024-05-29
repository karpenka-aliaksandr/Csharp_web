using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApp.Migrations
{
    /// <inheritdoc />
    public partial class ProductChangeGroupName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_groups_GroupId",
                table: "products");

            migrationBuilder.AddForeignKey(
                name: "group_id",
                table: "products",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "group_id",
                table: "products");

            migrationBuilder.AddForeignKey(
                name: "FK_products_groups_GroupId",
                table: "products",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "id");
        }
    }
}
