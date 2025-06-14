﻿namespace HotelBookingPlatform.Infrastructure.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.HotelId);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.StarRating)
                .IsRequired();

            builder.Property(h => h.Description)
                .HasMaxLength(500);

            builder.Property(h => h.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(h => h.CreatedAtUtc)
                .IsRequired();

            builder.HasMany(h => h.RoomClasses)
                .WithOne(rc => rc.Hotel)
                .HasForeignKey(rc => rc.HotelId);
        }
    }
}