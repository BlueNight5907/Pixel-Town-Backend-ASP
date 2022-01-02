using PixelTown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelTown.Repositories
{
    public class AccountRes
    {
        public static List<Account> get()
        {
            using (var context = new PixelTownContext())
            {
                var account = context.Account;
                foreach (var acc in account)
                {
                    acc.Password = null;
                }
                return account.ToList();
            }
        }

        public static Account getById(String id)
        {
            using (var context = new PixelTownContext())
            {
                var account = context.Account.Where(s => s.Id.Equals(id)).SingleOrDefault();
                if (account == null)
                {
                    return null;
                } else
                {
                    account.Password = null;
                    return account;
                }
            }
        }
        // upadte for user and only admin
        public static Account updateAccount(String id, String name, DateTime birthDay, String address, string avatar)
        {
            using (var context = new PixelTownContext())
            {
                var acc = context.Account.Where(s => s.Id.Equals(id)).SingleOrDefault();
                acc.Name = name;
                acc.Birthday = birthDay;
                acc.Address = address;
                acc.Avatar = avatar;
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return acc;
                } else
                {
                    return null;
                }
            }
        }

        public static Account AdminupdateAccount(String id, String name, string password ,DateTime birthDay, String address, string avatar)
        {
            using (var context = new PixelTownContext())
            {
                var acc = context.Account.Where(s => s.Id.Equals(id)).SingleOrDefault();
                acc.Name = name;
                acc.Password = BCrypt.Net.BCrypt.HashPassword(password);
                acc.Birthday = birthDay;
                acc.Address = address;
                acc.Avatar = avatar;
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return acc;
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool deleteAccount(string id)
        {
            using (var context = new PixelTownContext())
            {
                var acc = context.Account.Where(s => s.Id.Equals(id)).SingleOrDefault();
                var room = context.Room.Where(s => s.UserId.Equals(id)).ToList();
                context.Room.RemoveRange(room);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    context.Account.Remove(acc);
                    var result1 = context.SaveChanges();
                    if (result1 > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool changePassword(string id, string oldPass, string pass)
        {
            using (var context = new PixelTownContext())
            {
                var acc = context.Account.Where(s => s.Id.Equals(id)).SingleOrDefault();
                bool hashPassword = BCrypt.Net.BCrypt.Verify(oldPass, acc.Password);
                if(hashPassword)
                {
                    acc.Password = BCrypt.Net.BCrypt.HashPassword(pass);
                    var result = context.SaveChanges();
                    if(result > 0)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return false;
                }
            }
        }
    }
}
