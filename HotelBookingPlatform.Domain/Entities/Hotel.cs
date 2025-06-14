﻿namespace HotelBookingPlatform.Domain.Entities
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int StarRating { get; set; }
        public string? Description { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public int CityID { get; set; }
        public City City { get; set; }
        public int OwnerID { get; set; }
        public Owner Owner { get; set; }
        public ICollection<RoomClass> RoomClasses { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
    }
}