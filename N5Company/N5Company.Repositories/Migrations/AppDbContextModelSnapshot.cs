﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using N5Company.Repositories;

namespace N5Company.Repositories.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("N5Company.Entities.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmployeeForename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PermissionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PermissionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionTypeId");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            EmployeeForename = "Forename 1",
                            EmployeeSurname = "Surname 1",
                            PermissionDate = DateTime.Now,
                            PermissionTypeId = 1
                        },
                        new
                        {
                            Id = 101,
                            EmployeeForename = "Forename 2",
                            EmployeeSurname = "Surname 2",
                            PermissionDate = DateTime.Now,
                            PermissionTypeId = 2
                        },
                        new
                        {
                            Id = 102,
                            EmployeeForename = "Forename 3",
                            EmployeeSurname = "Surname 3",
                            PermissionDate = DateTime.Now,
                            PermissionTypeId = 3
                        });
                });

            modelBuilder.Entity("N5Company.Entities.Models.PermissionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PermissionTypes");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            Description = "Description 1"
                        },
                        new
                        {
                            Id = 101,
                            Description = "Description 2"
                        },
                        new
                        {
                            Id = 102,
                            Description = "Description 3"
                        });
                });

            modelBuilder.Entity("N5Company.Entities.Models.Permission", b =>
                {
                    b.HasOne("N5Company.Entities.Models.PermissionType", "PermissionType")
                        .WithMany()
                        .HasForeignKey("PermissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermissionType");
                });
#pragma warning restore 612, 618
        }
    }
}
