﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReadingSyllables.Models;

#nullable disable

namespace ReadingSyllables.Migrations
{
    [DbContext(typeof(SyllablesContext))]
    partial class SyllablesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

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

                    b.Property<int>("ShowCounter")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Syllables");
                });

            modelBuilder.Entity("ReadingSyllables.Models.UploadedFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("HashSum")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("UploadedFileType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HashSum")
                        .IsUnique();

                    b.ToTable("UploadedFiles");
                });

            modelBuilder.Entity("ReadingSyllables.Models.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Construction")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NextShow")
                        .HasColumnType("TEXT");

                    b.Property<int>("Show")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShowCounter")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Words");
                });

            modelBuilder.Entity("SyllableWord", b =>
                {
                    b.Property<int>("SyllablesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WordsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SyllablesId", "WordsId");

                    b.HasIndex("WordsId");

                    b.ToTable("SyllableWord");
                });

            modelBuilder.Entity("SyllableWord", b =>
                {
                    b.HasOne("ReadingSyllables.Models.Syllable", null)
                        .WithMany()
                        .HasForeignKey("SyllablesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReadingSyllables.Models.Word", null)
                        .WithMany()
                        .HasForeignKey("WordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
