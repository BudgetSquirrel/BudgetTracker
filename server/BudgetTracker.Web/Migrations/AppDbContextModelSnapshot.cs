﻿// <auto-generated />
using System;
using BudgetTracker.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BudgetTracker.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("BudgetTracker.Data.Models.BudgetDurationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DurationType");

                    b.Property<int>("EndDayOfMonth");

                    b.Property<int>("NumberDays");

                    b.Property<bool>("RolloverEndDateOnSmallMonths");

                    b.Property<bool>("RolloverStartDateOnSmallMonths");

                    b.Property<int>("StartDayOfMonth");

                    b.HasKey("Id");

                    b.ToTable("BudgetDurations");
                });

            modelBuilder.Entity("BudgetTracker.Data.Models.BudgetModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BudgetStart");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("DurationId");

                    b.Property<string>("Name");

                    b.Property<Guid>("OwnerId");

                    b.Property<Guid?>("ParentBudgetId");

                    b.Property<decimal>("SetAmount");

                    b.HasKey("Id");

                    b.HasIndex("DurationId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("BudgetTracker.Data.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateDeleted");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PassWord");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BudgetTracker.Data.Models.BudgetModel", b =>
                {
                    b.HasOne("BudgetTracker.Data.Models.BudgetDurationModel", "Duration")
                        .WithMany("Budgets")
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BudgetTracker.Data.Models.UserModel", "Owner")
                        .WithMany("Budgets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}