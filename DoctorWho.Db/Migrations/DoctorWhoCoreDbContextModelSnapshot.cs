﻿// <auto-generated />
using System;
using DoctorWho.Db.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoctorWho.Db.Migrations
{
    [DbContext(typeof(DoctorWhoCoreDbContext))]
    partial class DoctorWhoCoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanionEpisode", b =>
                {
                    b.Property<int>("CompanionsCompanionId")
                        .HasColumnType("int");

                    b.Property<int>("EpisodesEpisodeId")
                        .HasColumnType("int");

                    b.HasKey("CompanionsCompanionId", "EpisodesEpisodeId");

                    b.HasIndex("EpisodesEpisodeId");

                    b.ToTable("CompanionEpisode");
                });

            modelBuilder.Entity("DoctorWho.Db.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            AuthorName = "Mohammad"
                        },
                        new
                        {
                            AuthorId = 2,
                            AuthorName = "Huda"
                        },
                        new
                        {
                            AuthorId = 3,
                            AuthorName = "Rula"
                        },
                        new
                        {
                            AuthorId = 4,
                            AuthorName = "Lara"
                        },
                        new
                        {
                            AuthorId = 5,
                            AuthorName = "Rami"
                        });
                });

            modelBuilder.Entity("DoctorWho.Db.Companion", b =>
                {
                    b.Property<int>("CompanionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanionId"));

                    b.Property<string>("CompanionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhoPlayed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanionId");

                    b.ToTable("Companions");

                    b.HasData(
                        new
                        {
                            CompanionId = 1,
                            CompanionName = "Companion1",
                            WhoPlayed = "Ali"
                        },
                        new
                        {
                            CompanionId = 2,
                            CompanionName = "Companion2",
                            WhoPlayed = "Kareem"
                        },
                        new
                        {
                            CompanionId = 3,
                            CompanionName = "Companion3",
                            WhoPlayed = "Tim"
                        },
                        new
                        {
                            CompanionId = 4,
                            CompanionName = "Companion4",
                            WhoPlayed = "Abdullah"
                        },
                        new
                        {
                            CompanionId = 5,
                            CompanionName = "Companion5",
                            WhoPlayed = "Noor"
                        });
                });

            modelBuilder.Entity("DoctorWho.Db.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("FirstEpisodeDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastEpisodeDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            DoctorId = 1,
                            BirthDate = new DateTime(1964, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorName = "Doctor1",
                            DoctorNumber = 11,
                            FirstEpisodeDate = new DateTime(2005, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastEpisodeDate = new DateTime(2005, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            DoctorId = 2,
                            BirthDate = new DateTime(1971, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorName = "Doctor2",
                            DoctorNumber = 22,
                            FirstEpisodeDate = new DateTime(2005, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastEpisodeDate = new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            DoctorId = 3,
                            BirthDate = new DateTime(1982, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorName = "Doctor3",
                            DoctorNumber = 33,
                            FirstEpisodeDate = new DateTime(2010, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastEpisodeDate = new DateTime(2013, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            DoctorId = 4,
                            BirthDate = new DateTime(1969, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorName = "Doctor4",
                            DoctorNumber = 44,
                            FirstEpisodeDate = new DateTime(2013, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastEpisodeDate = new DateTime(2017, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            DoctorId = 5,
                            BirthDate = new DateTime(1982, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorName = "Doctor5",
                            DoctorNumber = 55,
                            FirstEpisodeDate = new DateTime(2017, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastEpisodeDate = new DateTime(2012, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("DoctorWho.Db.Enemy", b =>
                {
                    b.Property<int>("EnemyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnemyId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnemyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EnemyId");

                    b.ToTable("Enemies");

                    b.HasData(
                        new
                        {
                            EnemyId = 1,
                            Description = "Red Enemy",
                            EnemyName = "Sawsan"
                        },
                        new
                        {
                            EnemyId = 2,
                            Description = "Green Enemy",
                            EnemyName = "Firas"
                        },
                        new
                        {
                            EnemyId = 3,
                            Description = "Blue Enemy",
                            EnemyName = "Ahmad"
                        },
                        new
                        {
                            EnemyId = 4,
                            Description = "Yellow enemy",
                            EnemyName = "Ramy"
                        },
                        new
                        {
                            EnemyId = 5,
                            Description = "Pink Enemy",
                            EnemyName = "Rana"
                        });
                });

            modelBuilder.Entity("DoctorWho.Db.Episode", b =>
                {
                    b.Property<int>("EpisodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EpisodeId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EpisodeDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("int");

                    b.Property<string>("EpisodeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeriesNumber")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EpisodeId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Episodes");

                    b.HasData(
                        new
                        {
                            EpisodeId = 1,
                            AuthorId = 1,
                            DoctorId = 1,
                            EpisodeDate = new DateTime(2005, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EpisodeNumber = 1,
                            EpisodeType = "Regular",
                            Notes = "First episode in the first series",
                            SeriesNumber = 1,
                            Title = "Wedding"
                        },
                        new
                        {
                            EpisodeId = 2,
                            AuthorId = 2,
                            DoctorId = 2,
                            EpisodeDate = new DateTime(2006, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EpisodeNumber = 4,
                            EpisodeType = "NotRegular",
                            SeriesNumber = 2,
                            Title = "Halloween"
                        },
                        new
                        {
                            EpisodeId = 3,
                            AuthorId = 2,
                            DoctorId = 1,
                            EpisodeDate = new DateTime(2010, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EpisodeNumber = 11,
                            EpisodeType = "Regular",
                            Notes = "Best Episode",
                            SeriesNumber = 3,
                            Title = "The Eleventh Hour"
                        },
                        new
                        {
                            EpisodeId = 4,
                            AuthorId = 5,
                            DoctorId = 1,
                            EpisodeDate = new DateTime(2014, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EpisodeNumber = 2,
                            EpisodeType = "NotRegular",
                            SeriesNumber = 4,
                            Title = "Listen"
                        },
                        new
                        {
                            EpisodeId = 5,
                            AuthorId = 3,
                            DoctorId = 4,
                            EpisodeDate = new DateTime(2018, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EpisodeNumber = 1,
                            EpisodeType = "Regular",
                            Notes = "First episode in the fifth series",
                            SeriesNumber = 5,
                            Title = "The Woman Who Fell to Earth"
                        },
                        new
                        {
                            EpisodeId = 6,
                            AuthorId = 1,
                            EpisodeDate = new DateTime(2015, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EpisodeNumber = 12,
                            EpisodeType = "NotRegular",
                            SeriesNumber = 7,
                            Title = "TestNullDoctor"
                        });
                });

            modelBuilder.Entity("DoctorWho.Db.FrequerntCompinaion", b =>
                {
                    b.Property<string>("CompanionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("CompanionAppearances");

                    b.ToTable("FrequerntCompinaions");
                });

            modelBuilder.Entity("DoctorWho.Db.ProceduresModels.FrequentEnemy", b =>
                {
                    b.Property<int>("EnemyAppearances")
                        .HasColumnType("int");

                    b.Property<string>("EnemyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FrequentEnemies");
                });

            modelBuilder.Entity("EnemyEpisode", b =>
                {
                    b.Property<int>("EnemiesEnemyId")
                        .HasColumnType("int");

                    b.Property<int>("EpisodesEpisodeId")
                        .HasColumnType("int");

                    b.HasKey("EnemiesEnemyId", "EpisodesEpisodeId");

                    b.HasIndex("EpisodesEpisodeId");

                    b.ToTable("EnemyEpisode");
                });

            modelBuilder.Entity("CompanionEpisode", b =>
                {
                    b.HasOne("DoctorWho.Db.Companion", null)
                        .WithMany()
                        .HasForeignKey("CompanionsCompanionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorWho.Db.Episode", null)
                        .WithMany()
                        .HasForeignKey("EpisodesEpisodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorWho.Db.Episode", b =>
                {
                    b.HasOne("DoctorWho.Db.Author", "Author")
                        .WithMany("Episodes")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorWho.Db.Doctor", "Doctor")
                        .WithMany("Episodes")
                        .HasForeignKey("DoctorId");

                    b.Navigation("Author");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("EnemyEpisode", b =>
                {
                    b.HasOne("DoctorWho.Db.Enemy", null)
                        .WithMany()
                        .HasForeignKey("EnemiesEnemyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorWho.Db.Episode", null)
                        .WithMany()
                        .HasForeignKey("EpisodesEpisodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorWho.Db.Author", b =>
                {
                    b.Navigation("Episodes");
                });

            modelBuilder.Entity("DoctorWho.Db.Doctor", b =>
                {
                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
