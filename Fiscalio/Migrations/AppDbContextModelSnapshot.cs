﻿// <auto-generated />
using System;
using Fiscalio.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fiscalio.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Fiscalio.Models.Item", b =>
                {
                    b.Property<int>("IdItem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdNota")
                        .HasColumnType("int");

                    b.Property<string>("Produto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Valor")
                        .HasColumnType("float");

                    b.HasKey("IdItem");

                    b.HasIndex("IdNota");

                    b.ToTable("Itens");
                });

            modelBuilder.Entity("Fiscalio.Models.NotaFiscal", b =>
                {
                    b.Property<int>("IdNota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Emissor")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("text");

                    b.HasKey("IdNota");

                    b.ToTable("NotaFiscais");
                });

            modelBuilder.Entity("Fiscalio.Models.Item", b =>
                {
                    b.HasOne("Fiscalio.Models.NotaFiscal", null)
                        .WithMany()
                        .HasForeignKey("IdNota")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}