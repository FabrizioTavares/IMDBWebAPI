using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Implementmodelchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direction_Movies_MovieId",
                table: "Direction");

            migrationBuilder.DropForeignKey(
                name: "FK_Direction_Participants_ParticipantId",
                table: "Direction");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Movies_MovieId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_User_VoterId",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Direction",
                table: "Direction");

            migrationBuilder.RenameTable(
                name: "Vote",
                newName: "Votes");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Direction",
                newName: "Directions");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_VoterId",
                table: "Votes",
                newName: "IX_Votes_VoterId");

            migrationBuilder.RenameIndex(
                name: "IX_Direction_ParticipantId",
                table: "Directions",
                newName: "IX_Directions_ParticipantId");

            migrationBuilder.AlterColumn<string>(
                name: "CharacterName",
                table: "Performances",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                columns: new[] { "MovieId", "VoterId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directions",
                table: "Directions",
                columns: new[] { "MovieId", "ParticipantId" });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hierarchy = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Hierarchy", "IsActive", "Password", "Username" },
                values: new object[] { 1, 100, true, "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_Movies_MovieId",
                table: "Directions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_Participants_ParticipantId",
                table: "Directions",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Movies_MovieId",
                table: "Votes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_VoterId",
                table: "Votes",
                column: "VoterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directions_Movies_MovieId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Directions_Participants_ParticipantId",
                table: "Directions");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Movies_MovieId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_VoterId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directions",
                table: "Directions");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "Vote");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Directions",
                newName: "Direction");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_VoterId",
                table: "Vote",
                newName: "IX_Vote_VoterId");

            migrationBuilder.RenameIndex(
                name: "IX_Directions_ParticipantId",
                table: "Direction",
                newName: "IX_Direction_ParticipantId");

            migrationBuilder.AlterColumn<string>(
                name: "CharacterName",
                table: "Performances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                columns: new[] { "MovieId", "VoterId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Direction",
                table: "Direction",
                columns: new[] { "MovieId", "ParticipantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Direction_Movies_MovieId",
                table: "Direction",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Direction_Participants_ParticipantId",
                table: "Direction",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Movies_MovieId",
                table: "Vote",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_User_VoterId",
                table: "Vote",
                column: "VoterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
