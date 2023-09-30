﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReservaHotel.Data;

#nullable disable

namespace ReservaHotel.Migrations
{
    [DbContext(typeof(BDContext))]
    [Migration("20230930173426_novaMig")]
    partial class novaMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("ReservaHotel.Models.Cliente", b =>
                {
                    b.Property<string>("Cpf")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Cpf");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ReservaHotel.Models.Pacote", b =>
                {
                    b.Property<int>("IdPacote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("ValorPacote")
                        .HasColumnType("REAL");

                    b.HasKey("IdPacote");

                    b.ToTable("Pacotes");
                });

            modelBuilder.Entity("ReservaHotel.Models.Quarto", b =>
                {
                    b.Property<int>("nroQuarto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("nroHospedes")
                        .HasColumnType("INTEGER");

                    b.Property<float>("valor")
                        .HasColumnType("REAL");

                    b.HasKey("nroQuarto");

                    b.ToTable("Quartos");
                });

            modelBuilder.Entity("ReservaHotel.Models.Servico", b =>
                {
                    b.Property<int>("IdServico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PacoteIdPacote")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ValorServico")
                        .HasColumnType("REAL");

                    b.HasKey("IdServico");

                    b.HasIndex("PacoteIdPacote");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("ReservaHotel.Models.Servico", b =>
                {
                    b.HasOne("ReservaHotel.Models.Pacote", null)
                        .WithMany("Servicos")
                        .HasForeignKey("PacoteIdPacote");
                });

            modelBuilder.Entity("ReservaHotel.Models.Pacote", b =>
                {
                    b.Navigation("Servicos");
                });
#pragma warning restore 612, 618
        }
    }
}