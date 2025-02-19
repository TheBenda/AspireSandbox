﻿// <auto-generated />
using System;
using AKS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AKS.Infrastructure.Migrations
{
    [DbContext(typeof(PrimaryDbContext))]
    partial class PrimaryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AKS.Domain.Entities.BattleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("GroupCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CustomerId" }, "IX_Orders_CustomerId");

                    b.ToTable("BattleGroups");
                });

            modelBuilder.Entity("AKS.Domain.Entities.BattleGroupUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Accuracy")
                        .HasColumnType("integer");

                    b.Property<int>("Attack")
                        .HasColumnType("integer");

                    b.Property<Guid>("BattleGroupId")
                        .HasColumnType("uuid");

                    b.Property<int>("Defense")
                        .HasColumnType("integer");

                    b.Property<int>("Health")
                        .HasColumnType("integer");

                    b.Property<int>("Movement")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Points")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Range")
                        .HasColumnType("numeric");

                    b.Property<string>("Rule")
                        .HasColumnType("text");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BattleGroupId");

                    b.HasIndex(new[] { "UnitId" }, "IX_BattleGroupUnit_UnitId");

                    b.ToTable("BattleGroupUnits");
                });

            modelBuilder.Entity("AKS.Domain.Entities.BattleGroupUnitEquipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Attack")
                        .HasColumnType("integer");

                    b.Property<Guid>("BattleGroupUnitId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Points")
                        .HasColumnType("numeric");

                    b.Property<string>("Rule")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BattleGroupUnitId");

                    b.HasIndex(new[] { "EquipmentId" }, "IX_BattleGroupUnitEquipment_EquipmentId");

                    b.ToTable("BattleGroupUnitEquipments");
                });

            modelBuilder.Entity("AKS.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AKS.Domain.Entities.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Attack")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Points")
                        .HasColumnType("numeric");

                    b.Property<string>("Rule")
                        .HasColumnType("text");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UnitId" }, "IX_Units_UnitId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("AKS.Domain.Entities.Unit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Accuracy")
                        .HasColumnType("integer");

                    b.Property<int>("Attack")
                        .HasColumnType("integer");

                    b.Property<int>("Defense")
                        .HasColumnType("integer");

                    b.Property<int>("Health")
                        .HasColumnType("integer");

                    b.Property<int>("Movement")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Points")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Range")
                        .HasColumnType("numeric");

                    b.Property<string>("Rule")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("AKS.Domain.Entities.BattleGroup", b =>
                {
                    b.HasOne("AKS.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AKS.Domain.Entities.BattleGroupUnit", b =>
                {
                    b.HasOne("AKS.Domain.Entities.BattleGroup", "BattleGroup")
                        .WithMany("BattleGroupUnits")
                        .HasForeignKey("BattleGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BattleGroup");
                });

            modelBuilder.Entity("AKS.Domain.Entities.BattleGroupUnitEquipment", b =>
                {
                    b.HasOne("AKS.Domain.Entities.BattleGroupUnit", "BattleGroupUnit")
                        .WithMany("BattleGroupUnitEquipments")
                        .HasForeignKey("BattleGroupUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BattleGroupUnit");
                });

            modelBuilder.Entity("AKS.Domain.Entities.Customer", b =>
                {
                    b.OwnsOne("AKS.Domain.Values.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(90)
                                .HasColumnType("character varying(90)");

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision");

                            b1.Property<string>("State")
                                .HasMaxLength(60)
                                .HasColumnType("character varying(60)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(180)
                                .HasColumnType("character varying(180)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(18)
                                .HasColumnType("character varying(18)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("AKS.Domain.Entities.Equipment", b =>
                {
                    b.HasOne("AKS.Domain.Entities.Unit", "Unit")
                        .WithMany("Equipments")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("AKS.Domain.Entities.BattleGroup", b =>
                {
                    b.Navigation("BattleGroupUnits");
                });

            modelBuilder.Entity("AKS.Domain.Entities.BattleGroupUnit", b =>
                {
                    b.Navigation("BattleGroupUnitEquipments");
                });

            modelBuilder.Entity("AKS.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("AKS.Domain.Entities.Unit", b =>
                {
                    b.Navigation("Equipments");
                });
#pragma warning restore 612, 618
        }
    }
}
