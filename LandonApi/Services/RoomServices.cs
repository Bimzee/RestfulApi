using LandonApi.IServices;
using LandonApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public class RoomServices : IRoomService
    {
        private readonly HotelApiDbContext _context;

        public RoomServices(HotelApiDbContext context)
        {
            this._context = context;
        }
        public async Task<Room> GetRoomasync(Guid roomId)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == roomId);

            if (entity == null)
            {
                return null;
            }
            return new Room
            {
                Href =  null,//Url.Link(nameof(GetRoomById), new { roomId = entity.Id }),
                Name = entity.Name,
                Rate = entity.Rate / 100.0m
            };

        }
    }
}
