﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YouLearn.Infra.Persistencia.EF;

namespace YouLearn.Infra.Migrations
{
    [DbContext(typeof(YouLearnContext))]
    [Migration("20190625204536_Space")]
    partial class Space
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YouLearn.Domain.Entidades.Canal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("IdUsuario");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UrlLogo")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Canal");
                });

            modelBuilder.Entity("YouLearn.Domain.Entidades.PlayList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("IdUsuario");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("PlayList");
                });

            modelBuilder.Entity("YouLearn.Domain.Entidades.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("YouLearn.Domain.Entidades.Video", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<Guid?>("IdCanal");

                    b.Property<Guid?>("IdPlayList");

                    b.Property<Guid?>("IdUsuario");

                    b.Property<string>("IdVideoYoutube")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OrdemNaPlayList");

                    b.Property<int>("Status");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("IdCanal");

                    b.HasIndex("IdPlayList");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("YouLearn.Domain.Entidades.Canal", b =>
                {
                    b.HasOne("YouLearn.Domain.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario");
                });

            modelBuilder.Entity("YouLearn.Domain.Entidades.PlayList", b =>
                {
                    b.HasOne("YouLearn.Domain.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario");
                });

            modelBuilder.Entity("YouLearn.Domain.Entidades.Usuario", b =>
                {
                    b.OwnsOne("YouLearn.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UsuarioId");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasColumnName("Endereco")
                                .HasMaxLength(100);

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuario");

                            b1.HasOne("YouLearn.Domain.Entidades.Usuario")
                                .WithOne("Email")
                                .HasForeignKey("YouLearn.Domain.ValueObjects.Email", "UsuarioId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("YouLearn.Domain.ValueObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<Guid>("UsuarioId");

                            b1.Property<string>("PrimeiroNome")
                                .IsRequired()
                                .HasColumnName("PrimeiroNome")
                                .HasMaxLength(50);

                            b1.Property<string>("UltimoNome")
                                .IsRequired()
                                .HasColumnName("UltimoNome")
                                .HasMaxLength(50);

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuario");

                            b1.HasOne("YouLearn.Domain.Entidades.Usuario")
                                .WithOne("Nome")
                                .HasForeignKey("YouLearn.Domain.ValueObjects.Nome", "UsuarioId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("YouLearn.Domain.Entidades.Video", b =>
                {
                    b.HasOne("YouLearn.Domain.Entidades.Canal", "Canal")
                        .WithMany()
                        .HasForeignKey("IdCanal");

                    b.HasOne("YouLearn.Domain.Entidades.PlayList", "PlayList")
                        .WithMany()
                        .HasForeignKey("IdPlayList");

                    b.HasOne("YouLearn.Domain.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}