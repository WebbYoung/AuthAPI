using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber_RegionCode",
                table: "Users",
                type: "int",
                unicode: false,
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber_Number",
                table: "Users",
                type: "varchar(12)",
                unicode: false,
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldUnicode: false,
                oldMaxLength: 12);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber_RegionCode",
                table: "Users",
                type: "int",
                unicode: false,
                maxLength: 3,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber_Number",
                table: "Users",
                type: "varchar(12)",
                unicode: false,
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldUnicode: false,
                oldMaxLength: 12,
                oldNullable: true);
        }
    }
}
