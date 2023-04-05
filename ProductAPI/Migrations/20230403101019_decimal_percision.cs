using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class decimal_percision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 3, 15, 40, 19, 230, DateTimeKind.Local).AddTicks(3483),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 4, 3, 15, 8, 9, 553, DateTimeKind.Local).AddTicks(9071));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,0)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 3, 15, 8, 9, 553, DateTimeKind.Local).AddTicks(9071),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2023, 4, 3, 15, 40, 19, 230, DateTimeKind.Local).AddTicks(3483));
        }
    }
}
