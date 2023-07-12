using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISysWebAppBack.Migrations
{
    /// <inheritdoc />
    public partial class postgresql_migration_453 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
                    SubdivisionCode = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
                    ProjectCode = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: true),
                    Surname = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: true),
                    Patronymic = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: true),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: true),
                    Phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    JobTitle = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
                    EmployeeCode = table.Column<int>(type: "integer", nullable: false),
                    IdHeadManager = table.Column<string>(type: "text", nullable: true),
                    HeadManagerId = table.Column<string>(type: "text", nullable: true),
                    IdDepartment = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_IdDepartment",
                        column: x => x.IdDepartment,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_HeadManagerId",
                        column: x => x.HeadManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    IdEmployee = table.Column<string>(type: "text", nullable: false),
                    IdProject = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => x.Key);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employees_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_IdProject",
                        column: x => x.IdProject,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_IdEmployee",
                table: "EmployeeProjects",
                column: "IdEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_IdProject",
                table: "EmployeeProjects",
                column: "IdProject");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeCode",
                table: "Employees",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HeadManagerId",
                table: "Employees",
                column: "HeadManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdDepartment",
                table: "Employees",
                column: "IdDepartment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
