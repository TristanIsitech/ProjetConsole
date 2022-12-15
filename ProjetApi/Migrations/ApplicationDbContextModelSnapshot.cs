﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetApi.Context;

#nullable disable

namespace ProjetApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjetApi.Entities.Element", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("name");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("ProjetApi.Entities.Faiblesse", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("faiblesse_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("resistance_name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("faiblesse_name");

                    b.HasIndex("resistance_name");

                    b.ToTable("Faiblesses");
                });

            modelBuilder.Entity("ProjetApi.Entities.Pokemon", b =>
                {
                    b.Property<int>("numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("element_name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("evolution_numero")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("sous_evolution_numero")
                        .HasColumnType("int");

                    b.HasKey("numero");

                    b.HasIndex("element_name");

                    b.HasIndex("evolution_numero")
                        .IsUnique();

                    b.HasIndex("sous_evolution_numero");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("ProjetApi.Entities.Faiblesse", b =>
                {
                    b.HasOne("ProjetApi.Entities.Element", "faiblesse_")
                        .WithMany()
                        .HasForeignKey("faiblesse_name");

                    b.HasOne("ProjetApi.Entities.Element", "resistance_")
                        .WithMany()
                        .HasForeignKey("resistance_name");

                    b.Navigation("faiblesse_");

                    b.Navigation("resistance_");
                });

            modelBuilder.Entity("ProjetApi.Entities.Pokemon", b =>
                {
                    b.HasOne("ProjetApi.Entities.Element", "element")
                        .WithMany("pokemon")
                        .HasForeignKey("element_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetApi.Entities.Pokemon", "evolution_")
                        .WithOne()
                        .HasForeignKey("ProjetApi.Entities.Pokemon", "evolution_numero");

                    b.HasOne("ProjetApi.Entities.Pokemon", "sous_evolution_")
                        .WithMany()
                        .HasForeignKey("sous_evolution_numero");

                    b.Navigation("element");

                    b.Navigation("evolution_");

                    b.Navigation("sous_evolution_");
                });

            modelBuilder.Entity("ProjetApi.Entities.Element", b =>
                {
                    b.Navigation("pokemon");
                });
#pragma warning restore 612, 618
        }
    }
}
