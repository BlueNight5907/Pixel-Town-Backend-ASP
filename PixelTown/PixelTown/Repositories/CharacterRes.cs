using PixelTown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelTown.Repositories
{
    public class CharacterRes
    {
        public static List<Character> getall()
        {
            using (var context = new PixelTownContext())
            {
                return context.Character.ToList();
            }
        }

        public static Character getById(int id)
        {
            using (var context = new PixelTownContext())
            {
                var result = context.Character.Where(s => s.Id.Equals(id)).SingleOrDefault();
                return result;
            }
        }

        public static Character createCharacter(string name)
        {
            using (var context = new PixelTownContext())
            {
                Character character = new Character();
                character.CharacterName = name;
                context.Character.Add(character);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return character;
                } else
                {
                    return null;
                }
            }
        }

        public static bool deleteCharacter(int id)
        {
            using (var context = new PixelTownContext())
            {
                var character = context.Character.Where(s => s.Id.Equals(id)).SingleOrDefault();
                context.Character.Remove(character);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}