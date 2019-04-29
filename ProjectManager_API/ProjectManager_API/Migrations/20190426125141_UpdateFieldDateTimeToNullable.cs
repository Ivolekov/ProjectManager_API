using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ProjectManager_API.Migrations
{
    public partial class UpdateFieldDateTimeToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<DateTime>(
            //name: "CreateDate",
            //table: "Projects",
            //nullable: true);
            migrationBuilder.Sql(@" ALTER TABLE Projects
                                    ALTER COLUMN CreateDate datetime NULL");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" ALTER TABLE Projects
                                    ALTER COLUMN CreateDate datetime NOT NULL");
        }
    }
}
