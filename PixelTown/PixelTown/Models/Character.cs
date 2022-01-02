using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PixelTown.Models
{
    public partial class Character
    {
        public Character()
        {
            UserJoinRoom = new HashSet<UserJoinRoom>();
        }

        public int Id { get; set; }
        public string CharacterName { get; set; }

        public virtual ICollection<UserJoinRoom> UserJoinRoom { get; set; }
    }
}
