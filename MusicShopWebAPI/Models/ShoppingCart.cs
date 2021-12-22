using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShopWebAPI.Models
{
    public class ShoppingCart
    {
        public int OrderId { get; set; }
        public int MusicInstrumentId { get; set; }

        public Order Order { get; set; }
        public MusicInstrument MusicInstrument { get; set; }
    }
}
