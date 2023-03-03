﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReadingSyllables.Models;

#nullable disable

namespace ReadingSyllables.Migrations
{
    [DbContext(typeof(SyllablesContext))]
    [Migration("20230228162953_more fields")]
    partial class morefields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("ReadingSyllables.Models.Syllable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NextShow")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Show")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Syllables");
                });
#pragma warning restore 612, 618
        }
    }
}