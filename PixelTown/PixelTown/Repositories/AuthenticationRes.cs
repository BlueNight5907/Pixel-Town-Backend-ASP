﻿using PixelTown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelTown.Repositories
{
    public class AuthenticationRes
    {
        public static Account Login(string username, string password)
        {
            using (var context = new PixelTownContext())
            {
                var account = context.Account.Where(s => s.Email.Equals(username)).SingleOrDefault();
                if(account == null)
                {
                    return null;
                } else
                {
                    bool hashPassword = BCrypt.Net.BCrypt.Verify(password, account.Password);
                    if (hashPassword)
                    {
                        account.Password = null;
                        return account;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public static bool Register(string name, string username, string password, DateTime birthday, string address)
        {
            Account acc = new Account();
            acc.Id = Guid.NewGuid().ToString("N");
            acc.Name = name;
            acc.Email = username;
            acc.Password = BCrypt.Net.BCrypt.HashPassword(password);
            acc.Birthday = birthday;
            acc.Address = address;
            acc.Type = "User";
            acc.Active = true;
            using (var context = new PixelTownContext()) {
                context.Account.Add(acc);
                var result = context.SaveChanges();
                if(result > 0)
                {
                    return true;
                } else
                {
                    return false;
                }
            } 
        }   
    }
}