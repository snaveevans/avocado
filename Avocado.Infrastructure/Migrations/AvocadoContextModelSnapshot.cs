﻿// <auto-generated />
using System;
using Avocado.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Avocado.Infrastructure.Migrations
{
    [DbContext(typeof(AvocadoContext))]
    partial class AvocadoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Avocado.Domain.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Avocado.Domain.Entities.Member", b =>
                {
                    b.Property<Guid>("AccountId");

                    b.Property<Guid>("EventId");

                    b.Property<string>("AttendanceStatus")
                        .IsRequired();

                    b.Property<Guid?>("EventId1");

                    b.Property<string>("Role")
                        .IsRequired();

                    b.HasKey("AccountId", "EventId");

                    b.HasIndex("EventId");

                    b.HasIndex("EventId1");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Avocado.Infrastructure.Authentication.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("Picture");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Avocado.Infrastructure.Authentication.Login", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<string>("Provider");

                    b.Property<string>("ProviderKey");

                    b.HasKey("Id");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("Avocado.Domain.Entities.Member", b =>
                {
                    b.HasOne("Avocado.Domain.Entities.Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Avocado.Domain.Entities.Event")
                        .WithMany()
                        .HasForeignKey("EventId1");
                });
#pragma warning restore 612, 618
        }
    }
}
