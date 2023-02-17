using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XLNFaultReportSystem.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faults",
                columns: table => new
                {
                    FaultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactHoursFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactHoursTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncidentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntermittentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntermittentStatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosticResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faults", x => x.FaultId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faults");
        }
    }
}
