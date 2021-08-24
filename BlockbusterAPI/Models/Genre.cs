using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Release { get; set; }
        public bool IsActive { get; set; }
    }
}
