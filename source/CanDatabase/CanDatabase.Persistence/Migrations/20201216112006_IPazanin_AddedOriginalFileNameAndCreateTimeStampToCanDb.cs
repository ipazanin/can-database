using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CanDatabase.Persistence.Migrations
{
    public partial class IPazanin_AddedOriginalFileNameAndCreateTimeStampToCanDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateTimeStamp",
                table: "CanDbs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "CanDbs",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "CanDbs");

            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "CanDbs");
        }
    }
}
