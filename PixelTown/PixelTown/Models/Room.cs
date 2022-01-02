using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PixelTown.Models
{
    public partial class Room
    {
        public Room()
        {
            Message = new HashSet<Message>();
            UserAccessRoom = new HashSet<UserAccessRoom>();
            UserJoinRoom = new HashSet<UserJoinRoom>();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public int? MapId { get; set; }
        public string RoomName { get; set; }
        public string RoomPass { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }

        public virtual Map Map { get; set; }
        public virtual Account User { get; set; }
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<UserAccessRoom> UserAccessRoom { get; set; }
        public virtual ICollection<UserJoinRoom> UserJoinRoom { get; set; }
    }
}
