using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PixelTown.Models
{
    public partial class Map
    {
        public Map()
        {
            Room = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string MapName { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
