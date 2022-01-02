using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PixelTown.Models
{
    public partial class Account
    {
        public Account()
        {
            Access = new HashSet<Access>();
            GoogleAuth = new HashSet<GoogleAuth>();
            Message = new HashSet<Message>();
            Room = new HashSet<Room>();
            UserAccessRoom = new HashSet<UserAccessRoom>();
            UserJoinRoom = new HashSet<UserJoinRoom>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public bool? Active { get; set; }
        public string Avatar { get; set; }
        public string SignalrId { get; set; }

        public virtual ICollection<Access> Access { get; set; }
        public virtual ICollection<GoogleAuth> GoogleAuth { get; set; }
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<UserAccessRoom> UserAccessRoom { get; set; }
        public virtual ICollection<UserJoinRoom> UserJoinRoom { get; set; }
    }
}
