﻿namespace HotelBookingPlatform.Domain.DTOs.InvoiceRecord
{
    public class InvoiceCreateRequest
    {
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public string RoomClassName { get; set; }
        public string RoomNumber { get; set; }
        public decimal PriceAtBooking { get; set; }
        public decimal? DiscountPercentageAtBooking { get; set; }
    }
}