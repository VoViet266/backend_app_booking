using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace his_backend.Migrations
{
    /// <inheritdoc />
    public partial class themthongtin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bhxh",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cmnd",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "diachi",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "gioitinh",
                schema: "benhnhan",
                table: "app_users",
                type: "numeric(1,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "holot",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "madt",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maloaigiayto",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "manghe",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maqg",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "VN");

            migrationBuilder.AddColumn<string>(
                name: "matinh",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maxa",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ngaycap",
                schema: "benhnhan",
                table: "app_users",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ngaysinh",
                schema: "benhnhan",
                table: "app_users",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nhommau",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "noicap",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ten",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "app_users_cmnd_idx",
                schema: "benhnhan",
                table: "app_users",
                column: "cmnd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "app_users_cmnd_idx",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "bhxh",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "cmnd",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "diachi",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "gioitinh",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "holot",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "madt",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "maloaigiayto",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "manghe",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "maqg",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "matinh",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "maxa",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "ngaycap",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "ngaysinh",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "nhommau",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "noicap",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "ten",
                schema: "benhnhan",
                table: "app_users");
        }
    }
}
