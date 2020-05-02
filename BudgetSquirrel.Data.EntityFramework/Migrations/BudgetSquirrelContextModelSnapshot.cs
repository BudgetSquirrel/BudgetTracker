﻿// <auto-generated />
using System;
using BudgetSquirrel.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BudgetSquirrel.Data.EntityFramework.Migrations
{
    [DbContext(typeof(BudgetSquirrelContext))]
    partial class BudgetSquirrelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("BudgetSquirrel.Data.EntityFramework.Models.BudgetDurationRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int?>("EndDayOfMonth")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("NumberDays")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("RolloverEndDateOnSmallMonths")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BudgetDurations");
                });

            modelBuilder.Entity("BudgetSquirrel.Data.EntityFramework.Models.BudgetRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BudgetStart")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DurationId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("FundBalance")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentBudgetId")
                        .HasColumnType("TEXT");

                    b.Property<double?>("PercentAmount")
                        .HasColumnType("REAL");

                    b.Property<decimal?>("SetAmount")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DurationId");

                    b.HasIndex("ParentBudgetId");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("BudgetSquirrel.Data.EntityFramework.Models.UserRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BudgetSquirrel.Data.EntityFramework.Models.BudgetRecord", b =>
                {
                    b.HasOne("BudgetSquirrel.Data.EntityFramework.Models.BudgetDurationRecord", "Duration")
                        .WithMany()
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetSquirrel.Data.EntityFramework.Models.BudgetRecord", "ParentBudget")
                        .WithMany("SubBudgets")
                        .HasForeignKey("ParentBudgetId");

                    b.HasOne("BudgetSquirrel.Data.EntityFramework.Models.UserRecord", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}