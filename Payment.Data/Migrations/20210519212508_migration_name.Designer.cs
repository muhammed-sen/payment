﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payment.Data.Context;

namespace Payment.Data.Migrations
{
    [DbContext(typeof(PaymentContext))]
    [Migration("20210519212508_migration_name")]
    partial class migration_name
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Payment.Data.Domain.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 4755L,
                            Balance = 1001.88
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 9834L,
                            Balance = 456.44999999999999
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 7735L,
                            Balance = 89.359999999999999
                        });
                });

            modelBuilder.Entity("Payment.Data.Domain.AccountTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<double>("Commission")
                        .HasColumnType("REAL");

                    b.Property<int>("MessageType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Origin")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountTransactions");
                });

            modelBuilder.Entity("Payment.Data.Domain.AccountTransaction", b =>
                {
                    b.HasOne("Payment.Data.Domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
