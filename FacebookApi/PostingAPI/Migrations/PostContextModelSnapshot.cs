﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PostingAPI.Models;

namespace PostingAPI.Migrations
{
    [DbContext(typeof(PostContext))]
    partial class PostContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("PostingAPI.Models.PostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateOfDeletion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateOfPosting")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PostId")
                        .HasColumnType("text");

                    b.Property<string>("TextOfLink")
                        .HasColumnType("text");

                    b.Property<string>("TextOfPost")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PostModels");
                });
#pragma warning restore 612, 618
        }
    }
}
