﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Test.Persistence;

#nullable disable

namespace Test.Per.Migrations
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Test.Domain.ExceptionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BodyParameters")
                        .HasColumnType("text")
                        .HasColumnName("body_parameters");

                    b.Property<string>("QueryParameters")
                        .HasColumnType("text")
                        .HasColumnName("query_parameters");

                    b.Property<string>("StackTrace")
                        .HasColumnType("text")
                        .HasColumnName("stack_trace");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id")
                        .HasName("pk_exception_logs");

                    b.ToTable("exception_logs", (string)null);
                });

            modelBuilder.Entity("Test.Domain.Node", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<int>("TreeId")
                        .HasColumnType("integer")
                        .HasColumnName("tree_id");

                    b.HasKey("Id")
                        .HasName("pk_nodes");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_nodes_parent_id");

                    b.HasIndex("TreeId")
                        .HasDatabaseName("ix_nodes_tree_id");

                    b.ToTable("nodes", (string)null);
                });

            modelBuilder.Entity("Test.Domain.Tree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_trees");

                    b.ToTable("trees", (string)null);
                });

            modelBuilder.Entity("Test.Domain.Node", b =>
                {
                    b.HasOne("Test.Domain.Node", "Parent")
                        .WithMany("ChildTreeNodes")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_nodes_nodes_parent_id");

                    b.HasOne("Test.Domain.Tree", "Tree")
                        .WithMany("Nodes")
                        .HasForeignKey("TreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_nodes_trees_tree_id");

                    b.Navigation("Parent");

                    b.Navigation("Tree");
                });

            modelBuilder.Entity("Test.Domain.Node", b =>
                {
                    b.Navigation("ChildTreeNodes");
                });

            modelBuilder.Entity("Test.Domain.Tree", b =>
                {
                    b.Navigation("Nodes");
                });
#pragma warning restore 612, 618
        }
    }
}
