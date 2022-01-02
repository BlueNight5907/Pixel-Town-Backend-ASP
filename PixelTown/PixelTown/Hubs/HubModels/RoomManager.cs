using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelTown.Hubs.HubModels
{
    public class RoomManager
    {
        /// <summary>
        /// Room List (key:RoomId)
        /// </summary>
        private ConcurrentDictionary<string, RoomInfo> rooms;

        public RoomManager()
        {
            rooms = new ConcurrentDictionary<string, RoomInfo>();
        }

        public RoomInfo CreateRoom(string roomId,string hostId, string hostName)
        {
            //create new room info
            var roomInfo = new RoomInfo
            {
                RoomId = roomId,
                HostName = hostName,
                HostId = hostId
            };
            bool result = rooms.TryAdd(roomId, roomInfo);

            if (result)
            {
                return roomInfo;
            }
            else
            {
                return null;
            }
        }

        public void DeleteRoom(string roomId)
        {
            string correspondingRoomId = null;
            foreach (var pair in rooms)
            {
                if (pair.Value.RoomId.Equals(roomId))
                {
                    correspondingRoomId = pair.Key;
                }
            }

            if (correspondingRoomId != null || correspondingRoomId != "")
            {
                rooms.TryRemove(correspondingRoomId, out _);
            }
        }

        public List<RoomInfo> GetAllRooms()
        {
            return rooms.Values.ToList();
        }

        public RoomInfo GetRoomInfor(string roomId)
        {
            var roomList = rooms.Values;
            var roomInfo = (from room in roomList where room.RoomId == roomId select room).SingleOrDefault();
            return roomInfo;
        }
    }
    public class RoomInfo
    {
        public string RoomId { get; set; }
        public string HostName { get; set; }
        public string HostId { get; set; }
        public ConnectionMapping Users = new ConnectionMapping();
    }


    public class ConnectionMapping
    {
        private Dictionary<string, ConnectionInfor> _connections =
            new Dictionary<string, ConnectionInfor>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public bool Add(string key, ConnectionInfor infor)
        {
            lock (_connections)
            {
                ConnectionInfor connectionInfor;
                if (_connections.TryGetValue(key, out connectionInfor))
                {
                    connectionInfor.character = infor.character;
                    connectionInfor.position = null;
                    return true;
                }

                _connections.Add(key, infor);
                return true;
            }
        }

        public ConnectionInfor GetUser(string key)
        {
            ConnectionInfor connectionInfor;
            if (_connections.TryGetValue(key, out connectionInfor))
            {
                return connectionInfor;
            }
            return null;
        }

        public List<ConnectionInfor> GetAll()
        {
            return _connections.Values.ToList();
        }

        public void Remove(string key)
        {
            lock (_connections)
            {
                Console.WriteLine(key);
                ConnectionInfor connectionInfor;
                if (!_connections.TryGetValue(key, out connectionInfor))
                {
                    return;
                }
                _connections.Remove(key);
            }
        }
    }

    public class Vector
    {
        public float x { get; set; }
        public float y { get; set; }
        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class ConnectionInfor
    {
        public string signalrID { get; set; }
        public string name { get; set; }
        public string character { get; set; }
        public Vector position { get; set; }
        public ConnectionInfor(string id, string name, string character)
        {
            this.signalrID = id;
            this.name = name;
            this.character = character;
        }
    }
}
