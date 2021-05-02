﻿// <auto-generated />
using System;
using MSP.BetterCalm.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    [DbContext(typeof(BetterCalmContext))]
    partial class BetterCalmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MSP.BetterCalm.Domain.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("MSP.BetterCalm.Domain.Expertise", b =>
                {
                    b.Property<int>("IdPsychologist")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicalCondition")
                        .HasColumnType("int");

                    b.HasKey("IdPsychologist", "IdMedicalCondition");

                    b.HasIndex("IdMedicalCondition");

                    b.ToTable("Expertise");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.MedicalCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedicalCondition");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Meeting", b =>
                {
                    b.Property<int>("IdPsychologist")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("AdressMeeting")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("IdPsychologist", "IdUser");

                    b.HasIndex("IdUser");

                    b.ToTable("Meeting");
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

            modelBuilder.Entity("MSP.BetterCalm.Domain.Psychologist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdressMeeting")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeetingType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Psychologist");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MinSeconds")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sound")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cellphone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MedicalConditionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MedicalConditionId");

                    b.ToTable("User");
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

            modelBuilder.Entity("MSP.BetterCalm.Domain.Expertise", b =>
                {
                    b.HasOne("MSP.BetterCalm.Domain.MedicalCondition", "MedicalCondition")
                        .WithMany("Expertise")
                        .HasForeignKey("IdMedicalCondition")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MSP.BetterCalm.Domain.Psychologist", "Psychologist")
                        .WithMany("Expertise")
                        .HasForeignKey("IdPsychologist")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalCondition");

                    b.Navigation("Psychologist");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Meeting", b =>
                {
                    b.HasOne("MSP.BetterCalm.Domain.Psychologist", "Psychologist")
                        .WithMany("Meeting")
                        .HasForeignKey("IdPsychologist")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MSP.BetterCalm.Domain.User", "User")
                        .WithMany("Meeting")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Psychologist");

                    b.Navigation("User");
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

            modelBuilder.Entity("MSP.BetterCalm.Domain.User", b =>
                {
                    b.HasOne("MSP.BetterCalm.Domain.MedicalCondition", "MedicalCondition")
                        .WithMany()
                        .HasForeignKey("MedicalConditionId");

                    b.Navigation("MedicalCondition");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Category", b =>
                {
                    b.Navigation("CategoryTrack");

                    b.Navigation("PlaylistCategory");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.MedicalCondition", b =>
                {
                    b.Navigation("Expertise");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Playlist", b =>
                {
                    b.Navigation("PlaylistCategory");

                    b.Navigation("PlaylistTrack");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Psychologist", b =>
                {
                    b.Navigation("Expertise");

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.Track", b =>
                {
                    b.Navigation("CategoryTrack");

                    b.Navigation("PlaylistTrack");
                });

            modelBuilder.Entity("MSP.BetterCalm.Domain.User", b =>
                {
                    b.Navigation("Meeting");
                });
#pragma warning restore 612, 618
        }
    }
}
