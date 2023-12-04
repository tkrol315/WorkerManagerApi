using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerManager.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class CompletedByWorkerWithIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompletedByWorkerWithId",
                table: "Tasks",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedByWorkerWithId",
                table: "Tasks");
        }
    }
}
