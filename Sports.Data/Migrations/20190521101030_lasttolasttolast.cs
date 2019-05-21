using Microsoft.EntityFrameworkCore.Migrations;

namespace Sports.Data.Migrations
{
    public partial class lasttolasttolast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "Tests",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                table: "Tests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
