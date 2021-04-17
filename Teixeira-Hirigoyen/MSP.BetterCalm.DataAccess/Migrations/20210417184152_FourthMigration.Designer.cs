﻿// <auto-generated />
using MSP.BetterCalm.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    [DbContext(typeof(BetterCalmContext))]
    [Migration("20210417184152_FourthMigration")]
    partial class FourthMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MSP.BetterCalm.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.CategoryTrack", b =>
                {
                    b.Property<int>("IdCategory")
                        .HasColumnType("int");

                    b.Property<int>("IdTrack")
                        .HasColumnType("int");

                    b.HasKey("IdCategory", "IdTrack");

                    b.HasIndex("IdTrack");

                    b.ToTable("CategoryTrack");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.PlaylistCategory", b =>
                {
                    b.Property<int>("IdCategory")
                        .HasColumnType("int");

                    b.Property<int>("IdPlaylist")
                        .HasColumnType("int");

                    b.HasKey("IdCategory", "IdPlaylist");

                    b.HasIndex("IdPlaylist");

                    b.ToTable("PlaylistCategory");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.PlaylistTrack", b =>
                {
                    b.Property<int>("IdPlaylist")
                        .HasColumnType("int");

                    b.Property<int>("IdTrack")
                        .HasColumnType("int");

                    b.HasKey("IdPlaylist", "IdTrack");

                    b.HasIndex("IdTrack");

                    b.ToTable("PlaylistTrack");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sound")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.CategoryTrack", b =>
                {
                    b.HasOne("MSP.BetterCalm.Domain.Category", "Category")
                        .WithMany("CategoryTrack")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MSP.BetterCalm.Domain.Track", "Track")
                        .WithMany("CategoryTrack")
                        .HasForeignKey("IdTrack")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.PlaylistCategory", b =>
                {
                    b.HasOne("MSP.BetterCalm.Domain.Category", "Category")
                        .WithMany("PlaylistCategory")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MSP.BetterCalm.Domain.Playlist", "Playlist")
                        .WithMany("PlaylistCategory")
                        .HasForeignKey("IdPlaylist")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.PlaylistTrack", b =>
                {
                    b.HasOne("MSP.BetterCalm.Domain.Playlist", "Playlist")
                        .WithMany("PlaylistTrack")
                        .HasForeignKey("IdPlaylist")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MSP.BetterCalm.Domain.Track", "Track")
                        .WithMany("PlaylistTrack")
                        .HasForeignKey("IdTrack")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Category", b =>
                {
                    b.Navigation("CategoryTrack");

                    b.Navigation("PlaylistCategory");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Playlist", b =>
                {
                    b.Navigation("PlaylistCategory");

                    b.Navigation("PlaylistTrack");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Track", b =>
                {
                    b.Navigation("CategoryTrack");

                    b.Navigation("PlaylistTrack");
                });
#pragma warning restore 612, 618
        }
    }
}
