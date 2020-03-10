using LandonApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly HotelInfo _hotelInfo;

        public InfoController(IOptions<HotelInfo> _hotelInfoWrapper)
        {
            this._hotelInfo = _hotelInfoWrapper.Value;
        }
        [HttpGet(Name =nameof(GetInfo))]
        [ProducesResponseType(200)]
        public ActionResult<HotelInfo> GetInfo()
        {
            this._hotelInfo.Href = Url.Link(nameof(RoomsController.GetRooms), null);

            return this._hotelInfo;
        }
    }
}
