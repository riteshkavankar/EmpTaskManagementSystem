﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using employeetaskapis.Data;

#nullable disable

namespace employeetaskapis.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("employeetaskapis.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            Email = "Albert@example.com",
                            Name = "Albert Joe"
                        },
                        new
                        {
                            EmployeeId = 2,
                            Email = "John@example.com",
                            Name = "John Snow"
                        },
                        new
                        {
                            EmployeeId = 3,
                            Email = "Mike@example.com",
                            Name = "Mike James"
                        });
                });

            modelBuilder.Entity("employeetaskapis.Models.EmployeeDocument", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentId"));

                    b.Property<byte[]>("FileContent")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("DocumentId");

                    b.HasIndex("TaskId");

                    b.ToTable("Documents");

                    b.HasData(
                        new
                        {
                            DocumentId = 1,
                            FileContent = new byte[] { 72, 101, 108, 108, 111, 44, 32, 116, 104, 105, 115, 32, 105, 115, 32, 116, 104, 101, 32, 99, 111, 110, 116, 101, 110, 116, 32, 116, 111, 32, 116, 104, 105, 115, 32, 100, 111, 99, 117, 109, 101, 110, 116, 46 },
                            FileName = "Document1.txt",
                            TaskId = 1
                        });
                });

            modelBuilder.Entity("employeetaskapis.Models.EmployeeTask", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            TaskId = 1,
                            Description = "Task Description",
                            DueDate = new DateTime(2024, 8, 11, 22, 44, 58, 45, DateTimeKind.Local).AddTicks(9613),
                            EmployeeId = 1,
                            Status = "Open",
                            Title = "First Task"
                        });
                });

            modelBuilder.Entity("employeetaskapis.Models.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("NoteId");

                    b.HasIndex("TaskId");

                    b.ToTable("Notes");

                    b.HasData(
                        new
                        {
                            NoteId = 1,
                            Content = "This is a note.",
                            CreatedAt = new DateTime(2024, 8, 4, 22, 44, 58, 45, DateTimeKind.Local).AddTicks(9659),
                            TaskId = 1
                        });
                });

            modelBuilder.Entity("employeetaskapis.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            EmployeeId = 3,
                            Password = "admin",
                            Role = "Admin",
                            Username = "admin"
                        },
                        new
                        {
                            UserId = 2,
                            EmployeeId = 2,
                            Password = "manager",
                            Role = "Manager",
                            Username = "manager"
                        },
                        new
                        {
                            UserId = 3,
                            EmployeeId = 1,
                            Password = "employee",
                            Role = "Employee",
                            Username = "employee"
                        });
                });

            modelBuilder.Entity("employeetaskapis.Models.EmployeeDocument", b =>
                {
                    b.HasOne("employeetaskapis.Models.EmployeeTask", "Task")
                        .WithMany("Documents")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("employeetaskapis.Models.EmployeeTask", b =>
                {
                    b.HasOne("employeetaskapis.Models.Employee", "Employee")
                        .WithMany("Tasks")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("employeetaskapis.Models.Note", b =>
                {
                    b.HasOne("employeetaskapis.Models.EmployeeTask", "Task")
                        .WithMany("Notes")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("employeetaskapis.Models.User", b =>
                {
                    b.HasOne("employeetaskapis.Models.Employee", "Employee")
                        .WithOne()
                        .HasForeignKey("employeetaskapis.Models.User", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("employeetaskapis.Models.Employee", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("employeetaskapis.Models.EmployeeTask", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
