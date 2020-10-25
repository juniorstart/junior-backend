﻿// <auto-generated />
using System;
using JuniorStart.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JuniorStart.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20201025153052_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("JuniorStart.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MessageId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<int?>("RoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SendTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Sender")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("JuniorStart.Entities.RecruitmentInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RecruitmentId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text");

                    b.Property<bool>("CompanyReply")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DateOfCompanyReply")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LinkToApplication")
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<string>("WorkPlace")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("RecruitmentInformations");
                });

            modelBuilder.Entity("JuniorStart.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoomId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("JuniorStart.Entities.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TaskId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<int>("TodoListId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TodoListId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("JuniorStart.Entities.TodoList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TodoListId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("JuniorStart.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<int?>("RoomId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JuniorStart.Entities.Message", b =>
                {
                    b.HasOne("JuniorStart.Entities.Room", null)
                        .WithMany("Messages")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("JuniorStart.Entities.RecruitmentInformation", b =>
                {
                    b.HasOne("JuniorStart.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuniorStart.Entities.Task", b =>
                {
                    b.HasOne("JuniorStart.Entities.TodoList", "TodoList")
                        .WithMany("Tasks")
                        .HasForeignKey("TodoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuniorStart.Entities.TodoList", b =>
                {
                    b.HasOne("JuniorStart.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuniorStart.Entities.User", b =>
                {
                    b.HasOne("JuniorStart.Entities.Room", null)
                        .WithMany("Users")
                        .HasForeignKey("RoomId");
                });
#pragma warning restore 612, 618
        }
    }
}
