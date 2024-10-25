using OrderEntity = Domain.Entities.OrderEntities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderEntities;

namespace Persistence.Data.Configuration
{
    internal class OrderConfigrations : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, address => address.WithOwner());

            builder.HasMany(order => order.OrderItems)
                .WithOne();

            builder.Property(order => order.paymentStatus)
                .HasConversion(
                s => s.ToString(),
                s => Enum.Parse<OrderPaymentStatus>(s));

            builder.HasOne(order => order.deliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,3)");
        }
    }
}
