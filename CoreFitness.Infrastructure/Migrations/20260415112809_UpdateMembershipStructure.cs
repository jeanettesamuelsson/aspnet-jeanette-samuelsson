using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreFitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMembershipStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Users",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "UQ_Memberships_UserId",
                table: "Memberships");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Memberships",
                newName: "Benefits");

            migrationBuilder.AddColumn<string>(
                name: "CurrentMembershipId",
                table: "Members",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MembershipId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_CurrentMembershipId",
                table: "Members",
                column: "CurrentMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MembershipId",
                table: "AspNetUsers",
                column: "MembershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Memberships_MembershipId",
                table: "AspNetUsers",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Memberships_CurrentMembershipId",
                table: "Members",
                column: "CurrentMembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Memberships_MembershipId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Memberships_CurrentMembershipId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_CurrentMembershipId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MembershipId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentMembershipId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Benefits",
                table: "Memberships",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Memberships_UserId",
                table: "Memberships",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Users",
                table: "Memberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
