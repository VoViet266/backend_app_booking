using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace his_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddHoSoBenhNhan_MultiProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cccd",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "DanToc",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "DiaChiChiTiet",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "NgayCapCccd",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "NgaySinh",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "QuocTich",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "TenThanhPho",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "TenXa",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "TonGiao",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "hoten",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "mabn",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "vaitro",
                schema: "benhnhan",
                table: "app_users");

            migrationBuilder.CreateTable(
                name: "nguoibenhdangky",
                schema: "benhnhan",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    holot = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ten = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ngaysinh = table.Column<DateOnly>(type: "date", nullable: true),
                    gioitinh = table.Column<decimal>(type: "numeric(1,0)", nullable: true),
                    diachi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    sodienthoai = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    cmnd = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ngaycap = table.Column<DateOnly>(type: "date", nullable: true),
                    noicap = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    maloaigiayto = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    maqg = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValue: "VN"),
                    nhommau = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    bhxh = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    madt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    manghe = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    maxa = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    matinh = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nguoibenhdangky", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_user_hoso",
                schema: "benhnhan",
                columns: table => new
                {
                    appuserid = table.Column<int>(type: "integer", nullable: false),
                    hosoid = table.Column<int>(type: "integer", nullable: false),
                    quanhe = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "ban_than"),
                    lamacdinh = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ngaylienket = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_user_hoso", x => new { x.appuserid, x.hosoid });
                    table.ForeignKey(
                        name: "FK_app_user_hoso_app_users_appuserid",
                        column: x => x.appuserid,
                        principalSchema: "benhnhan",
                        principalTable: "app_users",
                        principalColumn: "mand",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_user_hoso_nguoibenhdangky_hosoid",
                        column: x => x.hosoid,
                        principalSchema: "benhnhan",
                        principalTable: "nguoibenhdangky",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_user_hoso_hosoid",
                schema: "benhnhan",
                table: "app_user_hoso",
                column: "hosoid");

            migrationBuilder.CreateIndex(
                name: "nguoibenhdangky_cmnd_idx",
                schema: "benhnhan",
                table: "nguoibenhdangky",
                column: "cmnd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_user_hoso",
                schema: "benhnhan");

            migrationBuilder.DropTable(
                name: "nguoibenhdangky",
                schema: "benhnhan");

            migrationBuilder.AddColumn<string>(
                name: "Cccd",
                schema: "benhnhan",
                table: "app_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DanToc",
                schema: "benhnhan",
                table: "app_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChiChiTiet",
                schema: "benhnhan",
                table: "app_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                schema: "benhnhan",
                table: "app_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgayCapCccd",
                schema: "benhnhan",
                table: "app_users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NgaySinh",
                schema: "benhnhan",
                table: "app_users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "QuocTich",
                schema: "benhnhan",
                table: "app_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenThanhPho",
                schema: "benhnhan",
                table: "app_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenXa",
                schema: "benhnhan",
                table: "app_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TonGiao",
                schema: "benhnhan",
                table: "app_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hoten",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mabn",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vaitro",
                schema: "benhnhan",
                table: "app_users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "user");
        }
    }
}
