using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace his_backend.Migrations
{
    /// <inheritdoc />
    public partial class themdangkykham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dangkykham",
                schema: "benhnhan",
                columns: table => new
                {
                    madk = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    mabn = table.Column<int>(type: "integer", nullable: false),
                    mapk = table.Column<int>(type: "integer", nullable: false),
                    mabs = table.Column<int>(type: "integer", nullable: false),
                    hoten = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    diachi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    sdt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cmnd = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    quanhe = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    tennguoithan = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ngaysinh = table.Column<DateTime>(type: "date", nullable: false),
                    timeslot = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ngay = table.Column<DateTime>(type: "date", nullable: false),
                    ngaysua = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    mack = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    giatien = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    trangthai = table.Column<decimal>(type: "numeric(1,0)", nullable: false),
                    loaikham = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ghichu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    loaibenhnhan = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    bhyt = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phikham = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    phidv = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    phithuoc = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    xoa = table.Column<bool>(type: "boolean", nullable: false),
                    hisid = table.Column<int>(type: "integer", nullable: false),
                    mngthisid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    hiqrcode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dangkykham", x => x.madk);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dangkykham",
                schema: "benhnhan");
        }
    }
}
