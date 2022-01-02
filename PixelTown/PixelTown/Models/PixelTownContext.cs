using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PixelTown.Models
{
    public partial class PixelTownContext : DbContext
    {
        public PixelTownContext()
        {
        }

        public PixelTownContext(DbContextOptions<PixelTownContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<FileMessage> FileMessage { get; set; }
        public virtual DbSet<GoogleAuth> GoogleAuth { get; set; }
        public virtual DbSet<Map> Map { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<UserAccessRoom> UserAccessRoom { get; set; }
        public virtual DbSet<UserJoinRoom> UserJoinRoom { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=PixelTown;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Access>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Access)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Access_Account");
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.SignalrId)
                    .HasColumnName("SignalrID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Character>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CharacterName).HasMaxLength(50);
            });

            modelBuilder.Entity<FileMessage>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Room)
                    .WithMany()
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_FileMessage_Room");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_FileMessage_Account");
            });

            modelBuilder.Entity<GoogleAuth>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GoogleAuth)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_GoogleAuth_Account");
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MapName).HasMaxLength(100);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Message1).HasColumnName("Message");

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Message_Room");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Message_Account");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MapId).HasColumnName("MapID");

                entity.Property(e => e.RoomName).HasMaxLength(1000);

                entity.Property(e => e.RoomPass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Map)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_Map");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Account1");
            });

            modelBuilder.Entity<UserAccessRoom>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.UserAccessRoom)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserAccessRoom_Room1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAccessRoom)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserAccessRoom_Account");
            });

            modelBuilder.Entity<UserJoinRoom>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CharacterId).HasColumnName("CharacterID");

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.UserJoinRoom)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserJoinRoom_Character1");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.UserJoinRoom)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserJoinRoom_Room1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserJoinRoom)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserJoinRoom_Account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
