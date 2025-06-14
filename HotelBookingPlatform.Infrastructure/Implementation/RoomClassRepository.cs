﻿using Microsoft.EntityFrameworkCore;

namespace HotelBookingPlatform.Infrastructure.Implementation
{
    public class RoomClassRepository : GenericRepository<RoomClass>, IRoomClasseRepository
    {
        public RoomClassRepository(AppDbContext context)
            : base(context) { }
        private IQueryable<RoomClass> ApplyIncludes(IQueryable<RoomClass> query, bool includeDiscounts, bool includeAmenities, bool includeRooms, bool includeHotel)
        {
            if (includeDiscounts)
            {
                query = query.Include(rc => rc.Discounts);
            }
            if (includeAmenities)
            {
                query = query.Include(rc => rc.Amenities);
            }
            if (includeRooms)
            {
                query = query.Include(rc => rc.Rooms);
            }
            if (includeHotel)
            {
                query = query.Include(rc => rc.Hotel);
            }
            return query;
        }

        public async Task<IEnumerable<RoomClass>> SearchCriteria(
         string name,
         string desc,
         int pageSize = 10,
         int pageNumber = 1)
        {
            var query = _appDbContext.RoomClasses.AsQueryable();

            query = ApplyIncludes(query, includeDiscounts: true, includeAmenities: false, includeRooms: false, includeHotel: false)
                .Where(rc => (string.IsNullOrEmpty(name) || rc.Name.Contains(name)) &&
                             (string.IsNullOrEmpty(desc) || rc.Description.Contains(desc)));

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<RoomClass>> GetAllAsync()
        {
            return await ApplyIncludes(_appDbContext.RoomClasses, includeDiscounts: true, includeAmenities: false, includeRooms: false, includeHotel: false)
                .ToListAsync();
        }

        public async Task<RoomClass> GetByIdAsync(int id)
        {
            return await ApplyIncludes(_appDbContext.RoomClasses, includeDiscounts: false, includeAmenities: false, includeRooms: false, includeHotel: true)
                .FirstOrDefaultAsync(rc => rc.RoomClassID == id);
        }

        public async Task<RoomClass> GetRoomClassWithAmenitiesAsync(int roomClassId)
        {
            return await ApplyIncludes(_appDbContext.RoomClasses, includeDiscounts: false, includeAmenities: true, includeRooms: false, includeHotel: false)
                .FirstOrDefaultAsync(rc => rc.RoomClassID == roomClassId);
        }

        public async Task<RoomClass> GetRoomClassWithRoomsAsync(int id)
        {
            return await ApplyIncludes(_appDbContext.RoomClasses, includeDiscounts: false, includeAmenities: false, includeRooms: true, includeHotel: false)
                .FirstOrDefaultAsync(rc => rc.RoomClassID == id);
        }
        public async Task<IEnumerable<RoomClass>> GetByHotelIdAsync(int hotelId)
        {
            return await _appDbContext.RoomClasses
                .Where(rc => rc.HotelId == hotelId)
                .ToListAsync();
        }
    }


}