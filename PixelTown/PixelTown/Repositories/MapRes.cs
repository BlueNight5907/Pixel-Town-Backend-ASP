using PixelTown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelTown.Repositories
{
    public class MapRes
    {
        public static List<Map> getall()
        {
            using (var context = new PixelTownContext())
            {
                return context.Map.ToList();
            }
        }

        public static Map getById(int id)
        {
            using (var context = new PixelTownContext())
            {
                var map = context.Map.Where(s => s.Id.Equals(id)).SingleOrDefault();
                if (map != null)
                {
                    return map;
                } else
                {
                    return null;
                }
            }
        }
    }
}
