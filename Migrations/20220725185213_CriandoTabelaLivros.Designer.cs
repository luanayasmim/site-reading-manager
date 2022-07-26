﻿// <auto-generated />
using System;
using API_Livros.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_Livros.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20220725185213_CriandoTabelaLivros")]
    partial class CriandoTabelaLivros
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API_Livros.Models.LivroModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataLancamento")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeAutor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeLivro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumPaginas")
                        .HasColumnType("int");

                    b.Property<bool>("StatusLivro")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Livros");
                });
#pragma warning restore 612, 618
        }
    }
}
