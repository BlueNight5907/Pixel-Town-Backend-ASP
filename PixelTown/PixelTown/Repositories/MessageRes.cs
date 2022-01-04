using PixelTown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelTown.Repositories
{
    public class MessageRes
    {
        public static object GetMessages(string roomId, long time)
        {
            using (var context = new PixelTownContext())
            {
                var messages = from message in context.Message join user in context.Account on message.UserId equals user.Id
                               orderby message.Time descending 
                               where message.Time < time && message.RoomId == roomId 
                               select new {
                                   userId = user.Id,
                                   name = user.Name,
                                   avatar = user.Avatar,
                                   message = message.Message1,
                                   time = message.Time
                               };
                return messages.Take(20).ToList();
            }
        }

        public static object GetFiles(string roomId, long time)
        {
            using (var context = new PixelTownContext())
            {
                var messages = from fileInfor in context.FileMessage
                               join user in context.Account on fileInfor.UserId equals user.Id
                               orderby fileInfor.Time descending
                               where fileInfor.Time < time && fileInfor.RoomId == roomId
                               select new
                               {
                                   id = fileInfor.Id,
                                   userId = user.Id,
                                   name = user.Name,
                                   avatar = user.Avatar,
                                   fileURL = fileInfor.UrlFile,
                                   time = fileInfor.Time
                               };
                return messages.Take(20).ToList();
            }
        }

        public static object GetLastFile(string roomId,long time)
        {
            using (var context = new PixelTownContext())
            {
                var messages = from fileInfor in context.FileMessage
                               join user in context.Account on fileInfor.UserId equals user.Id
                               orderby fileInfor.Time descending
                               where  fileInfor.RoomId == roomId && fileInfor.Time > time
                               select new
                               {
                                   id = fileInfor.Id,
                                   userId = user.Id,
                                   name = user.Name,
                                   avatar = user.Avatar,
                                   fileURL = fileInfor.UrlFile,
                                   time = fileInfor.Time
                               };
                return messages.Take(1).ToList();
            }
        }

        public static bool AddMessage(string roomId, string userId, string mess)
        {
            using (var context = new PixelTownContext())
            {
                long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var message = new Message() { Message1 = mess, RoomId = roomId, UserId = userId, Time = milliseconds };
                context.Message.Add(message);
                var result = context.SaveChanges();
                if(result > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public static FileMessage AddFile(string roomId, string userId, string url)
        {
            using (var context = new PixelTownContext())
            {
                long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var fileMessage = new FileMessage() { RoomId = roomId, UserId = userId, Time = milliseconds, UrlFile = url };
                context.FileMessage.Add(fileMessage);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return fileMessage;
                }
                return null;
            }
        }
    }
}
