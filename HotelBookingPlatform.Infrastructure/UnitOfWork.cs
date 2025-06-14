﻿using System;
using ILog = HotelBookingPlatform.Domain.ILogger.ILog;
namespace HotelBookingPlatform.Infrastructure
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly UserManager<LocalUser> _userManager;
        private readonly ILog _logger;

        public UnitOfWork(AppDbContext context, UserManager<LocalUser> userManager)
        {
            _context = context;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

            HotelRepository = new HotelRepository(_context);
            BookingRepository = new BookingRepository(_context);
            RoomClasseRepository = new RoomClassRepository(_context);
            RoomRepository = new RoomRepository(_context);
            CityRepository = new CityRepository(_context);
            OwnerRepository = new OwnerRepository(_context);
            DiscountRepository = new DiscountRepository(_context);
            ReviewRepository = new ReviewRepository(_context);
            InvoiceRecordRepository = new InvoiceRecordRepository(_context);
            AmenityRepository = new AmenityRepository(_context);
            UserRepository = new UserRepository(_userManager, _context);
            ImageRepository = new ImageRepository(_context);
        }
        public IHotelRepository HotelRepository { get; set; }
        public IBookingRepository BookingRepository { get; set; }
        public IRoomClasseRepository RoomClasseRepository { get; set; }
        public IRoomRepository RoomRepository { get; set; }
        public ICityRepository CityRepository { get; set; }
        public IOwnerRepository OwnerRepository { get; set; }
        public IDiscountRepository DiscountRepository { get; set; }
        public IReviewRepository ReviewRepository { get; set; }
        public IInvoiceRecordRepository InvoiceRecordRepository { get; set; }
        public IAmenityRepository AmenityRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IImageRepository ImageRepository { get; set; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }

}