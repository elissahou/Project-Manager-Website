﻿// <auto-generated />
using System;
using E9U.Tangello.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E9U.Tangello.Data.Migrations.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20200528014620_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("E9U.Tangello.Data.Entities.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("E9U.Tangello.Data.Entities.CategoryToProjectTypeMapping", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryID");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("ProjectTypeID");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("ID");

                    b.ToTable("CategoryToProjectTypeMappings");
                });

            modelBuilder.Entity("E9U.Tangello.Data.Entities.ProjectName", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ProjectNames");
                });

            modelBuilder.Entity("E9U.Tangello.Data.Entities.ProjectNameToCategoryMapping", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryID");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("ProjectNameID");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("ID");

                    b.ToTable("ProjectNameToCategoryMappings");
                });

            modelBuilder.Entity("E9U.Tangello.Data.Entities.ProjectType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ProjectTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
