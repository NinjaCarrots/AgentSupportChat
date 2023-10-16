﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SupportChat.Data.Context;

#nullable disable

namespace SupportChat.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231015191904_initialcreate")]
    partial class initialcreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SupportChat.Models.Agent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Concurrency")
                        .HasColumnType("int");

                    b.Property<double>("Efficiency")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SeniorityId")
                        .HasColumnType("int");

                    b.Property<bool>("isShiftActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SeniorityId");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("SupportChat.Models.ChatSession", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"));

                    b.Property<int>("AgentId")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("SessionId");

                    b.ToTable("ChatSessions");
                });

            modelBuilder.Entity("SupportChat.Models.Seniority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaxConcurrentChats")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SeniorityMultiplier")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("ShiftEndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ShiftStartTime")
                        .HasColumnType("time");

                    b.Property<bool>("TeamLeadResponsibilities")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Seniorities");
                });

            modelBuilder.Entity("SupportChat.Models.Agent", b =>
                {
                    b.HasOne("SupportChat.Models.Seniority", "Seniority")
                        .WithMany()
                        .HasForeignKey("SeniorityId");

                    b.Navigation("Seniority");
                });
#pragma warning restore 612, 618
        }
    }
}
