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
    [Migration("20230930181419_mig4")]
    partial class mig4
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
                    b.Property<int>("NroQuarto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NroHospedes")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Valor")
                        .HasColumnType("REAL");

                    b.HasKey("NroQuarto");

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

                    b.Property<int?>("PacoteId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<float>("ValorServico")
                        .HasColumnType("REAL");

                    b.HasKey("IdServico");

                    b.HasIndex("PacoteId");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("ReservaHotel.Models.Servico", b =>
                {
                    b.HasOne("ReservaHotel.Models.Pacote", "Pacote")
                        .WithMany("Servicos")
                        .HasForeignKey("PacoteId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Pacote");
                });

            modelBuilder.Entity("ReservaHotel.Models.Pacote", b =>
                {
                    b.Navigation("Servicos");
                });
#pragma warning restore 612, 618
        }
    }
}
