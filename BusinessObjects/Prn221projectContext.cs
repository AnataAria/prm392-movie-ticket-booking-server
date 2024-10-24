using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects;

public partial class Prn221projectContext : DbContext
{
    private readonly string _connectionString;
    public Prn221projectContext()
    {
    }

    public Prn221projectContext(DbContextOptions<Prn221projectContext> options, IConfiguration configuration)
        : base(options)
    {
        _connectionString = configuration.GetConnectionString("DB");
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Movie> Events { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SolvedTicket> SolvedTickets { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC27C96DC41F");

            entity.ToTable("Account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__RoleID__48CFD27E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC277E146503");

            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC276088597E");

            entity.ToTable("Movie");

            entity.Property(e => e.Id).ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.DateEnd).HasColumnName("Date_End");
            entity.Property(e => e.DateStart).HasColumnName("Date_Start");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Movies)
                  .HasForeignKey(e => e.CategoryId).HasConstraintName("FK__Movie__CategoryID");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotion__ID");

            entity.ToTable("Promotion");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__ID");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SolvedTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Solved_t__3214EC27A69936BB");

            entity.ToTable("Solved_ticket");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.TotalPrice).HasColumnName("Total_Price");

            entity.HasOne(d => d.Account).WithMany(p => p.SolvedTickets)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Solved_ti__Accou__4AB81AF0");

            entity.HasOne(d => d.Promotion).WithMany(p => p.SolvedTickets)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK__Solved_Ticket__PromotionID");

            entity.HasOne(d => d.Ticket).WithMany(p => p.SolvedTickets)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__Solved_Ticket__TicketID");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC277FE16B60");
            entity.Property(e => e.MovieID).HasColumnName("MovieID");
            entity.Property(e => e.SeatID).HasColumnName("SeatID");
            entity.Property(e => e.ShowtimeID).HasColumnName("ShowtimeID");

            entity.ToTable("Ticket");
            entity.HasOne(e => e.Movie)
            .WithMany(e => e.Tickets)
            .HasForeignKey(e => e.MovieID).HasConstraintName("FK__Ticket__MovieID");
            entity.HasOne(e => e.Seat)
            .WithMany(e => e.Tickets)
            .HasForeignKey(e => e.SeatID).HasConstraintName("FK__Ticket__TicketID");
            entity.HasOne(e => e.Showtime)
            .WithMany(e => e.Tickets)
            .HasForeignKey(e => e.MovieID).HasConstraintName("FK__Ticket__ShowtimeID");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC27A304AC82");

            entity.ToTable("Transaction");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.SolvedTicketId).HasColumnName("Solved_ticketID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.MovieID).HasColumnName("MovieID");

            entity.HasOne(d => d.SolvedTicket).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SolvedTicketId)
                .HasConstraintName("FK__Transaction__SolvedTicketID");

            entity.HasOne(d => d.Type).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Transaction__TypeID");
            entity.HasOne(d => d.Movie).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.MovieID)
                .HasConstraintName("FK__Transaction_MovieID");
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC2741B4EC9E");

            entity.ToTable("Transaction_history");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__Transacti__Trans__5165187F");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC2702EE2C12");

            entity.ToTable("Transaction_type");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CinemaRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CinemaRoom__ID");
            entity.ToTable("CinemaRoom");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.RoomName).HasMaxLength(100);
            entity.Property(e => e.Capacity).IsRequired();

            entity.HasMany(e => e.Seats)
                  .WithOne(e => e.CinemaRoom)
                  .HasForeignKey(e => e.CinemaRoomId);

            entity.HasMany(e => e.ShowTimes)
                  .WithOne(e => e.CinemaRoom)
                  .HasForeignKey(e => e.CinemaRoomID);
        });

        modelBuilder.Entity<ShowTime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ShowTime__ID");
            entity.ToTable("ShowTime");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.CinemaRoomID).HasColumnName("CinemaRoomID");

            entity.Property(e => e.MovieID).HasColumnName("MovieID");

            entity.HasOne(e => e.Movie)
                  .WithMany()
                  .HasForeignKey(e => e.MovieID).HasConstraintName("FK__Showtime__MovieID");

            entity.HasOne(e => e.CinemaRoom)
                  .WithMany(c => c.ShowTimes)
                  .HasForeignKey(e => e.CinemaRoomID).HasConstraintName("FK__Showtime__CinemaRoomID");

            entity.HasMany(e => e.Tickets)
                  .WithOne()
                  .HasForeignKey(t => t.ShowtimeID);
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seat__ID");
            entity.ToTable("Seat");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.CinemaRoomId).HasColumnName("CinemaRoomID");

            entity.HasOne(e => e.CinemaRoom)
                  .WithMany(c => c.Seats)
                  .HasForeignKey(e => e.CinemaRoomId).HasConstraintName("");

            entity.HasMany(e => e.Tickets)
                  .WithOne()
                  .HasForeignKey(t => t.SeatID);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
