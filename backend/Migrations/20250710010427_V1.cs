using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admini",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pol = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admini", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Filmovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reziser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zanr = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Musterije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pol = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musterije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kapacitet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pol = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ocene",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrednost = table.Column<int>(type: "int", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VremeKomentara = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MusterijaID = table.Column<int>(type: "int", nullable: true),
                    FilmID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocene", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ocene_Filmovi_FilmID",
                        column: x => x.FilmID,
                        principalTable: "Filmovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ocene_Musterije_MusterijaID",
                        column: x => x.MusterijaID,
                        principalTable: "Musterije",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Projekcije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmID = table.Column<int>(type: "int", nullable: true),
                    SalaID = table.Column<int>(type: "int", nullable: true),
                    DatumProjekcije = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekcije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projekcije_Filmovi_FilmID",
                        column: x => x.FilmID,
                        principalTable: "Filmovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Projekcije_Sale_SalaID",
                        column: x => x.SalaID,
                        principalTable: "Sale",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjekcijaID = table.Column<int>(type: "int", nullable: true),
                    MusterijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Karte_Musterije_MusterijaID",
                        column: x => x.MusterijaID,
                        principalTable: "Musterije",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Karte_Projekcije_ProjekcijaID",
                        column: x => x.ProjekcijaID,
                        principalTable: "Projekcije",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karte_MusterijaID",
                table: "Karte",
                column: "MusterijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_ProjekcijaID",
                table: "Karte",
                column: "ProjekcijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocene_FilmID",
                table: "Ocene",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocene_MusterijaID",
                table: "Ocene",
                column: "MusterijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Projekcije_FilmID",
                table: "Projekcije",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "IX_Projekcije_SalaID",
                table: "Projekcije",
                column: "SalaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admini");

            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "Ocene");

            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "Projekcije");

            migrationBuilder.DropTable(
                name: "Musterije");

            migrationBuilder.DropTable(
                name: "Filmovi");

            migrationBuilder.DropTable(
                name: "Sale");
        }
    }
}
