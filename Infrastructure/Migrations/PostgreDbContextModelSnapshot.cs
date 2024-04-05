﻿// <auto-generated />
using System;
using Domain.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(PostgreDbContext))]
    partial class PostgreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "public", "reaction_type", new[] { "like", "dislike" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "public", "role", new[] { "default", "moderator", "admin" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "public", "status", new[] { "denied", "moderation", "published" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories", "public");
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IssuerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Posted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PublicationId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IssuerId");

                    b.HasIndex("PublicationId");

                    b.ToTable("Comments", "public");
                });

            modelBuilder.Entity("Domain.Entities.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("IssuerId")
                        .HasColumnType("integer");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<Status>("Status")
                        .HasColumnType("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("XmlDocumentUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IssuerId");

                    b.ToTable("Publications", "public");
                });

            modelBuilder.Entity("Domain.Entities.Reactions.Reaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<int>("IssuerId")
                        .HasColumnType("integer");

                    b.Property<ReactionType>("ReactionType")
                        .HasColumnType("reaction_type");

                    b.HasKey("Id");

                    b.HasIndex("IssuerId");

                    b.ToTable("Reaction", "public");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Reaction");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Themes", "public");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool?>("Gender")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<Role>("Role")
                        .HasColumnType("role");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", "public");
                });

            modelBuilder.Entity("PublicationTheme", b =>
                {
                    b.Property<int>("PublicationsId")
                        .HasColumnType("integer");

                    b.Property<int>("ThemesId")
                        .HasColumnType("integer");

                    b.HasKey("PublicationsId", "ThemesId");

                    b.HasIndex("ThemesId");

                    b.ToTable("PublicationTheme", "public");
                });

            modelBuilder.Entity("PublicationUser", b =>
                {
                    b.Property<int>("FavoritesId")
                        .HasColumnType("integer");

                    b.Property<int>("FavouritesId")
                        .HasColumnType("integer");

                    b.HasKey("FavoritesId", "FavouritesId");

                    b.HasIndex("FavouritesId");

                    b.ToTable("PublicationUser", "public");
                });

            modelBuilder.Entity("Domain.Entities.Reactions.CommentReaction", b =>
                {
                    b.HasBaseType("Domain.Entities.Reactions.Reaction");

                    b.Property<int>("CommentId")
                        .HasColumnType("integer");

                    b.HasIndex("CommentId");

                    b.HasDiscriminator().HasValue("CommentReaction");
                });

            modelBuilder.Entity("Domain.Entities.Reactions.PublicationReaction", b =>
                {
                    b.HasBaseType("Domain.Entities.Reactions.Reaction");

                    b.Property<int>("PublicationId")
                        .HasColumnType("integer");

                    b.HasIndex("PublicationId");

                    b.HasDiscriminator().HasValue("PublicationReaction");
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.HasOne("Domain.Entities.User", "Issuer")
                        .WithMany("Comments")
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Publication", "Publication")
                        .WithMany("Comments")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issuer");

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("Domain.Entities.Publication", b =>
                {
                    b.HasOne("Domain.Entities.User", "Issuer")
                        .WithMany("Publications")
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Issuer");
                });

            modelBuilder.Entity("Domain.Entities.Reactions.Reaction", b =>
                {
                    b.HasOne("Domain.Entities.User", "Issuer")
                        .WithMany("Reactions")
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Issuer");
                });

            modelBuilder.Entity("Domain.Entities.Theme", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("Themes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PublicationTheme", b =>
                {
                    b.HasOne("Domain.Entities.Publication", null)
                        .WithMany()
                        .HasForeignKey("PublicationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Theme", null)
                        .WithMany()
                        .HasForeignKey("ThemesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PublicationUser", b =>
                {
                    b.HasOne("Domain.Entities.Publication", null)
                        .WithMany()
                        .HasForeignKey("FavoritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("FavouritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Reactions.CommentReaction", b =>
                {
                    b.HasOne("Domain.Entities.Comment", "Comment")
                        .WithMany("Reactions")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("Domain.Entities.Reactions.PublicationReaction", b =>
                {
                    b.HasOne("Domain.Entities.Publication", "Publication")
                        .WithMany("Reactions")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Navigation("Themes");
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("Domain.Entities.Publication", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Publications");

                    b.Navigation("Reactions");
                });
#pragma warning restore 612, 618
        }
    }
}
