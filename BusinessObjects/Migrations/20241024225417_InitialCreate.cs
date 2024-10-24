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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__3214EC277E146503", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CinemaRoom",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CinemaRoom__ID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotion__ID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__ID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_type",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__3214EC2702EE2C12", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CinemaRoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Seat__ID", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Seat__CinemaRoomID",
                        column: x => x.CinemaRoomID,
                        principalTable: "CinemaRoom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "Movie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    Date_Start = table.Column<DateOnly>(type: "date", nullable: true),
                    Date_End = table.Column<DateOnly>(type: "date", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Event__3214EC276088597E", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Movie_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Movie__CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ShowTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinemaRoomID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    TicketQuantity = table.Column<int>(type: "int", nullable: false),
                    ShowDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvaliableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ShowTime__ID", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Showtime__CinemaRoomID",
                        column: x => x.CinemaRoomID,
                        principalTable: "CinemaRoom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Showtime__MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: true),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    ShowtimeID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ticket__3214EC277FE16B60", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Ticket__MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Ticket__ShowtimeID",
                        column: x => x.ShowtimeID,
                        principalTable: "ShowTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Ticket__TicketID",
                        column: x => x.SeatID,
                        principalTable: "Seat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Solved_ticket",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        name: "FK__Solved_Ticket__PromotionID",
                        column: x => x.PromotionID,
                        principalTable: "Promotion",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Solved_Ticket__TicketID",
                        column: x => x.TicketID,
                        principalTable: "Ticket",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Solved_ti__Accou__4AB81AF0",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: true),
                    Solved_ticketID = table.Column<int>(type: "int", nullable: true),
                    TypeID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__3214EC27A304AC82", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Transaction_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Transaction__SolvedTicketID",
                        column: x => x.Solved_ticketID,
                        principalTable: "Solved_ticket",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Transaction__TypeID",
                        column: x => x.TypeID,
                        principalTable: "Transaction_type",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction_history",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "IX_Movie_AccountId",
                table: "Movie",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_CategoryID",
                table: "Movie",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_CinemaRoomID",
                table: "Seat",
                column: "CinemaRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTime_CinemaRoomID",
                table: "ShowTime",
                column: "CinemaRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTime_MovieID",
                table: "ShowTime",
                column: "MovieID");

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
                name: "IX_Ticket_MovieID",
                table: "Ticket",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatID",
                table: "Ticket",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ShowtimeID",
                table: "Ticket",
                column: "ShowtimeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_MovieID",
                table: "Transaction",
                column: "MovieID");

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
                name: "ShowTime");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "CinemaRoom");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
