﻿// <auto-generated />
using System;
using Avocado.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Avocado.Infrastructure.Migrations
{
    [DbContext(typeof(AvocadoContext))]
    [Migration("20180706001122_AddAccount")]
    partial class AddAccount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Avocado.Domain.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Avocado.Domain.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
