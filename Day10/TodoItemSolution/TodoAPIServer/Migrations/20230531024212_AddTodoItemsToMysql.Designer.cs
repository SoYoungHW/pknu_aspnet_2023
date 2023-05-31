﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoAPIServer.Models;

#nullable disable

namespace TodoAPIServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230531024212_AddTodoItemsToMysql")]
    partial class AddTodoItemsToMysql
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TodoAPIServer.Models.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("IsComplete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Title")
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Title");

                    b.Property<DateTime?>("TodoDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("TodotItems");
                });
#pragma warning restore 612, 618
        }
    }
}
