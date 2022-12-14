// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using payment.DatabaseContext;

#nullable disable

namespace payment.Migrations
{
    [DbContext(typeof(PayMentDbContext))]
    partial class PayMentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("payment.model.Wallet", b =>
                {
                    b.Property<string>("UniqueWalletId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Debit")
                        .HasColumnType("float");

                    b.Property<bool?>("IsBusinessWallet")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LocationIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserIdentifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UniqueWalletId");

                    b.ToTable("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
