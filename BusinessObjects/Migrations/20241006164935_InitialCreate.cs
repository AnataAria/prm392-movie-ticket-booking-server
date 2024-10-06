using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__3214EC277E146503", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__3214EC275B251E3B", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__3214EC276E6B0E00", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_type",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__3214EC2702EE2C12", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Wallet = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__3214EC27C96DC41F", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Account__RoleID__48CFD27E",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    Ticket_Quantity = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Date_Start = table.Column<DateOnly>(type: "date", nullable: true),
                    Date_End = table.Column<DateOnly>(type: "date", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    SponsorID = table.Column<int>(type: "int", nullable: true),
                    ServiceSponsor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Event__3214EC276088597E", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Event_SponsorID",
                        column: x => x.SponsorID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Event__CategoryI__49C3F6B7",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ticket__3214EC277FE16B60", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Ticket__EventID__4D94879B",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Solved_ticket",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    TicketID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Total_Price = table.Column<int>(type: "int", nullable: true),
                    PromotionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Solved_t__3214EC27A69936BB", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Solved_ti__Accou__4AB81AF0",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Solved_ti__Promo__4BAC3F29",
                        column: x => x.PromotionID,
                        principalTable: "Promotion",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Solved_ti__Ticke__4CA06362",
                        column: x => x.TicketID,
                        principalTable: "Ticket",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: true),
                    Solved_ticketID = table.Column<int>(type: "int", nullable: true),
                    TypeID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__3214EC27A304AC82", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Transacti__Event__4E88ABD4",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Transacti__Solve__4F7CD00D",
                        column: x => x.Solved_ticketID,
                        principalTable: "Solved_ticket",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Transacti__TypeI__5070F446",
                        column: x => x.TypeID,
                        principalTable: "Transaction_type",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction_history",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    TransactionID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__3214EC2741B4EC9E", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Transacti__Trans__5165187F",
                        column: x => x.TransactionID,
                        principalTable: "Transaction",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleID",
                table: "Account",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CategoryID",
                table: "Event",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_SponsorID",
                table: "Event",
                column: "SponsorID");

            migrationBuilder.CreateIndex(
                name: "IX_Solved_ticket_AccountID",
                table: "Solved_ticket",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Solved_ticket_PromotionID",
                table: "Solved_ticket",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_Solved_ticket_TicketID",
                table: "Solved_ticket",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventID",
                table: "Ticket",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_EventID",
                table: "Transaction",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Solved_ticketID",
                table: "Transaction",
                column: "Solved_ticketID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TypeID",
                table: "Transaction",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_history_TransactionID",
                table: "Transaction_history",
                column: "TransactionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction_history");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Solved_ticket");

            migrationBuilder.DropTable(
                name: "Transaction_type");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
