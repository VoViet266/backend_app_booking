using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace his_backend.Migrations
{
    /// <inheritdoc />
    public partial class App_user : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "benhnhan");

            migrationBuilder.CreateTable(
                name: "app_users",
                schema: "benhnhan",
                columns: table => new
                {
                    mand = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    hoten = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    sodienthoai = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    matkhauhash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    NgaySinh = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    GioiTinh = table.Column<string>(type: "text", nullable: true),
                    Cccd = table.Column<string>(type: "text", nullable: true),
                    NgayCapCccd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TenThanhPho = table.Column<string>(type: "text", nullable: true),
                    TenXa = table.Column<string>(type: "text", nullable: true),
                    DiaChiChiTiet = table.Column<string>(type: "text", nullable: true),
                    DanToc = table.Column<string>(type: "text", nullable: true),
                    TonGiao = table.Column<string>(type: "text", nullable: true),
                    QuocTich = table.Column<string>(type: "text", nullable: true),
                    vaitro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "user"),
                    isactive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    mabn = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ngaytao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    landangnhapcuoi = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    refresh_token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    refresh_token_het_han = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_users", x => x.mand);
                });

            migrationBuilder.CreateIndex(
                name: "app_users_sdt_unique",
                schema: "benhnhan",
                table: "app_users",
                column: "sodienthoai",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_users",
                schema: "benhnhan");
        }
    }
}
