// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Goal.Demo.Infra.Data;

namespace Goal.Demo.Infra.Data.Migrations
{
    [DbContext(typeof(SampleContext))]
    [Migration("20201111134505_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Goal.Infra.Data.Auditing.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AuditDateTimeUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditType")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChangedColumns")
                        .HasColumnType("TEXT");

                    b.Property<string>("KeyValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("NewValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("OldValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Goal.Demo.Domain.Aggregates.People.Document", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Goal.Demo.Domain.Aggregates.People.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Goal.Demo.Domain.Aggregates.People.Document", b =>
                {
                    b.HasOne("Goal.Demo.Domain.Aggregates.People.Person", "Person")
                        .WithOne("Cpf")
                        .HasForeignKey("Goal.Demo.Domain.Aggregates.People.Document", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Goal.Demo.Domain.Aggregates.People.Person", b =>
                {
                    b.OwnsOne("Goal.Demo.Domain.Aggregates.People.Name", "Name", b1 =>
                        {
                            b1.Property<string>("PersonId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("TEXT");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("TEXT");

                            b1.HasKey("PersonId");

                            b1.ToTable("People");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Goal.Demo.Domain.Aggregates.People.Person", b =>
                {
                    b.Navigation("Cpf");
                });
#pragma warning restore 612, 618
        }
    }
}
