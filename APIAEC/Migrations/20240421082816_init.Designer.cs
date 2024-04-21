﻿// <auto-generated />
using APIAEC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIAEC.Migrations
{
    [DbContext(typeof(ITIContext))]
    [Migration("20240421082816_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIAEC.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ManagerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Department");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ManagerName = "Ahmed",
                            Name = "SD"
                        },
                        new
                        {
                            Id = 2,
                            ManagerName = "Ahmed",
                            Name = "OS"
                        });
                });

            modelBuilder.Entity("APIAEC.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Alex",
                            DepartmentId = 1,
                            Name = "Ahmed",
                            Salary = 10000
                        },
                        new
                        {
                            Id = 2,
                            Address = "Alex",
                            DepartmentId = 2,
                            Name = "Safa",
                            Salary = 10000
                        },
                        new
                        {
                            Id = 3,
                            Address = "Alex",
                            DepartmentId = 2,
                            Name = "Sara",
                            Salary = 10000
                        });
                });

            modelBuilder.Entity("APIAEC.Models.Employee", b =>
                {
                    b.HasOne("APIAEC.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("APIAEC.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
