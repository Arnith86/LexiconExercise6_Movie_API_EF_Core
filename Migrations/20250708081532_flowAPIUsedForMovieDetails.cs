using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class flowAPIUsedForMovieDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDetails_Movies_MovieId",
                table: "MovieDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieDetails",
                table: "MovieDetails");

            migrationBuilder.RenameTable(
                name: "MovieDetails",
                newName: "MoviesDetails");

            migrationBuilder.RenameIndex(
                name: "IX_MovieDetails_MovieId",
                table: "MoviesDetails",
                newName: "IX_MoviesDetails_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviesDetails",
                table: "MoviesDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesDetails_Movies_MovieId",
                table: "MoviesDetails",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesDetails_Movies_MovieId",
                table: "MoviesDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviesDetails",
                table: "MoviesDetails");

            migrationBuilder.RenameTable(
                name: "MoviesDetails",
                newName: "MovieDetails");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesDetails_MovieId",
                table: "MovieDetails",
                newName: "IX_MovieDetails_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieDetails",
                table: "MovieDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDetails_Movies_MovieId",
                table: "MovieDetails",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
