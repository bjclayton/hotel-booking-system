﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingPlatform.Domain.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public string UserId { get; set; }
        public LocalUser User { get; set; }
        public string? CreatedById { get; set; }
        public LocalUser ? CreatedBy { get; set; }
        public Guid? GuestId { get; set; }
        public Guest? Guest { get; set; }
        public BookingStatus Status { get; set; }
        public string ConfirmationNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AfterDiscountedPrice { get; set; }
        public DateTime BookingDateUtc { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public DateTime CheckInDateUtc { get; set; }
        public DateTime CheckOutDateUtc { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        
        public ICollection<InvoiceRecord> Invoice { get; set; } = new List<InvoiceRecord>();
    }
}