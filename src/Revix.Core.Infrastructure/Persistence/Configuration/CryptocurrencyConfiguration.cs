using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revix.Core.Domain.Entities;

namespace Revix.Core.Infrastructure.Persistence.Configuration;

public class CryptocurrencyConfiguration : IEntityTypeConfiguration<Cryptocurrency>
{
    public void Configure(EntityTypeBuilder<Cryptocurrency> builder)
    {
        builder.Property(e => e.Id).HasColumnName("Id");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries),
            new ValueComparer<string[]>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToArray()));
        ;
    }
}