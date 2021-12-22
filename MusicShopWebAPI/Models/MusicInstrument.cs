    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShopWebAPI
{
    public class MusicInstrument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public bool WarrantySupported { get; set; }
    }
}
