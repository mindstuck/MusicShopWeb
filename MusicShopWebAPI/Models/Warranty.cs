using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShopWebAPI.Models
{
    public class Warranty
    {
        public int Id { get; set; }
        public int MusicInstrumentId { get; set; }
        public int DaysDuration { get; set; }
        public DateTime DatePurchased { get; set; }

        public MusicInstrument MusicInstrument { get; set; }
    }
}
