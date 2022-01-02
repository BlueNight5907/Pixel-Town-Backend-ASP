using PixelTown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelTown.Repositories
{
    public class SearchRes
    {
        public static List<Room> search(string key)
        {
            using( var context = new PixelTownContext())
            {
                var keyword = Encoding.UTF8.GetString(Encoding.Default.GetBytes(key));
                var room = context.Room.Where(s => s.Id.Contains(key) || s.RoomName.Contains(keyword)).ToList();
                return room;
            }
        }
    }
}
