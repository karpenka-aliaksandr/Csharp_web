using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApp.Migrations
{
    /// <inheritdoc />
    public partial class ProductChangeGroupNameDoubleTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "group_id",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "products",
                newName: "group_id");

            migrationBuilder.RenameIndex(
                name: "IX_products_GroupId",
                table: "products",
                newName: "IX_products_group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_groups_group_id",
                table: "products",
                column: "group_id",
                principalTable: "groups",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_groups_group_id",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "group_id",
                table: "products",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_products_group_id",
                table: "products",
                newName: "IX_products_GroupId");

            migrationBuilder.AddForeignKey(
                name: "group_id",
                table: "products",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "id");
        }
    }
}
