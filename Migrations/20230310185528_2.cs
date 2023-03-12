using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XLNFaultReportSystem.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Faults_AssetId",
                table: "Faults",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faults_Assets_AssetId",
                table: "Faults",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "AssetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faults_Assets_AssetId",
                table: "Faults");

            migrationBuilder.DropIndex(
                name: "IX_Faults_AssetId",
                table: "Faults");
        }
    }
}
