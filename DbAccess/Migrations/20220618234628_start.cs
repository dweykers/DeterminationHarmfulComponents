using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbAccess.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "product_information");

            migrationBuilder.CreateTable(
                name: "barcode",
                schema: "product_information",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор штрих-кода продукта"),
                    barcode_number = table.Column<string>(type: "varchar(13)", nullable: false, comment: "номер штрих-кода продукта")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barcode", x => x.id);
                },
                comment: "Штрих-код продукта");

            migrationBuilder.CreateTable(
                name: "product",
                schema: "product_information",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор продукта"),
                    product_name = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Название продукта"),
                    barcode_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_barcode_barcode_id",
                        column: x => x.barcode_id,
                        principalSchema: "product_information",
                        principalTable: "barcode",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Продукт");

            migrationBuilder.CreateTable(
                name: "product_composition",
                schema: "product_information",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор компонента пищевого продукта"),
                    product_composition_name = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Название компонента пищевого продукта"),
                    hazard_description = table.Column<string>(type: "varchar(1000)", nullable: false, comment: "Описание вредности компонента пищевого продукта"),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_composition", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_composition_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "product_information",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Состав продукта с его вредностью/безопасностью");

            migrationBuilder.CreateIndex(
                name: "IX_product_barcode_id",
                schema: "product_information",
                table: "product",
                column: "barcode_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_composition_product_id",
                schema: "product_information",
                table: "product_composition",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_composition",
                schema: "product_information");

            migrationBuilder.DropTable(
                name: "product",
                schema: "product_information");

            migrationBuilder.DropTable(
                name: "barcode",
                schema: "product_information");
        }
    }
}
