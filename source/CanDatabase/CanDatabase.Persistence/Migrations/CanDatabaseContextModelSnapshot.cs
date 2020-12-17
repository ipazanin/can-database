﻿// <auto-generated />
using System;
using CanDatabase.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CanDatabase.Persistence.Migrations
{
    [DbContext(typeof(CanDatabaseContext))]
    partial class CanDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CanDatabase.Domain.Models.CanDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreateTimeStamp")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("OriginalFileName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("CanDbs");
                });

            modelBuilder.Entity("CanDatabase.Domain.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CanDbId")
                        .HasColumnType("int");

                    b.Property<long>("CanId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("CanDbId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CanDatabase.Domain.Models.NetworkNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CanDbId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("CanDbId");

                    b.ToTable("NetworkNodes");
                });

            modelBuilder.Entity("CanDatabase.Domain.Models.Signal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("StartBit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("Signals");
                });

            modelBuilder.Entity("CanDatabase.Domain.Models.Message", b =>
                {
                    b.HasOne("CanDatabase.Domain.Models.CanDb", "CanDb")
                        .WithMany("Messages")
                        .HasForeignKey("CanDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CanDb");
                });

            modelBuilder.Entity("CanDatabase.Domain.Models.NetworkNode", b =>
                {
                    b.HasOne("CanDatabase.Domain.Models.CanDb", "CanDb")
                        .WithMany("NetworkNodes")
                        .HasForeignKey("CanDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CanDb");
                });

            modelBuilder.Entity("CanDatabase.Domain.Models.Signal", b =>
                {
                    b.HasOne("CanDatabase.Domain.Models.Message", "Message")
                        .WithMany("Signals")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");
                });

            modelBuilder.Entity("CanDatabase.Domain.Models.CanDb", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("NetworkNodes");
                });

            modelBuilder.Entity("CanDatabase.Domain.Models.Message", b =>
                {
                    b.Navigation("Signals");
                });
#pragma warning restore 612, 618
        }
    }
}
