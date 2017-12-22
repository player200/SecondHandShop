namespace SecondHandShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangedAttributeValidationToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlPath",
                table: "Pictures",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 700);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlPath",
                table: "Pictures",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(700)",
                oldMaxLength: 700,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);
        }
    }
}