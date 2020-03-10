using LandonApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.IServices
{
    public interface IRoomService
    {
        Task<Room> GetRoomasync (Guid roomId);
    }
}
