using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PixelTown.Models
{
    public partial class UserJoinRoom
    {
        public int Id { get; set; }
        public DateTime? Time { get; set; }
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public int? CharacterId { get; set; }
        public string State { get; set; }

        public virtual Character Character { get; set; }
        public virtual Room Room { get; set; }
        public virtual Account User { get; set; }
    }
}
