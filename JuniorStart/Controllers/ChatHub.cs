using System;
using System.Collections.Generic;
using System.Linq;
using JuniorStart.Controllers.Interfaces;
using JuniorStart.Entities;
using JuniorStart.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace JuniorStart.Controllers
{
    public class ChatHub : Hub<ITypedHubClient>
    {
        private static readonly Dictionary<string, List<string>> RoomConnections = new Dictionary<string, List<string>>();
        private readonly ApplicationContext _context;

        public ChatHub(ApplicationContext context) => this._context = context;

        public override Task OnConnectedAsync()
        {
            List<Tuple<int,string>> rooms = _context.Rooms.AsNoTracking().Select(x => Tuple.Create(x.Id, x.Name)).ToList();
            Clients.Client(Context.ConnectionId).ReceiveRooms(rooms).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public void EnterRoom(string roomName, int userId)
        {

            Room room = _context.Rooms.Include(x => x.Messages).Include(y => y.Users).SingleOrDefault(room1 => room1.Name.Equals(roomName));
            User user = _context.Users.Single(x => x.Id.Equals(userId));

            if (room != null)
            {

                if (!RoomConnections.ContainsKey(roomName))
                {
                    RoomConnections.Add(roomName, new List<string>());
                }

                List<string> otherUsers = RoomConnections[roomName];
                RoomConnections[roomName].Add(Context.ConnectionId);

                room.Users.Add(user);
                _context.SaveChanges();
                if (otherUsers.Count > 0)
                {
                    Clients.Clients(otherUsers).ReceiveRoom(room).GetAwaiter().GetResult();
                }

                Clients.Client(Context.ConnectionId).ReceiveRoom(room).GetAwaiter().GetResult();
            }
        }

        public void ChangeRoom(string roomName, int userId)
        {
            string name = string.Empty;

            foreach (var rooms in RoomConnections)
            {
                if (rooms.Value.Contains(Context.ConnectionId))
                {
                    name = rooms.Key;
                    rooms.Value.Remove(Context.ConnectionId);
                }
            }

            List<string> oldRoomUsers = RoomConnections[name];

            
            User user = _context.Users.Single(x => x.Id.Equals(userId));
            Room oldRoom = _context.Rooms.Include(m => m.Messages).Include(u => u.Users).SingleOrDefault(o => o.Name.Equals(name));

            if (oldRoom is null) return;

            oldRoom.Users.Remove(user);
            _context.SaveChanges();

            Clients.Clients(oldRoomUsers).ReceiveRoom(oldRoom).GetAwaiter().GetResult();

            EnterRoom(roomName, userId);
        }

        public void LeaveRoom(int userId)
        {
            User user = _context.Users.SingleOrDefault(x => x.Id.Equals(userId));
            Room room = _context.Rooms.Include(x => x.Messages).Include(y => y.Users).SingleOrDefault(r => r.Users.Contains(user));
            if (room is null) return;
            room.Users.Remove(user);
            _context.SaveChanges();

            Clients.Clients(RoomConnections[room.Name]).ReceiveRoom(room).GetAwaiter().GetResult();
        }

        public void SendMessage(Message message)
        {
            string name = string.Empty;

            foreach (var rooms in RoomConnections)
            {
                if (rooms.Value.Contains(Context.ConnectionId))
                {
                    name = rooms.Key;
                }
            }
            Room room = _context.Rooms.SingleOrDefault(x => x.Name.Equals(name));
            if (room != null)
            {

                /*
                _context.Messages.Add(message);
                _context.SaveChanges();
                room.Messages.Add(message);
                */
                var users = new List<string>();
                foreach (var rooms in RoomConnections)
                {
                    if (rooms.Value.Contains(Context.ConnectionId))
                    {
                        users = rooms.Value;
                    }
                }
                //_context.SaveChanges();
                
                Clients.Clients(users).BroadcastMessage(message).GetAwaiter().GetResult();
            }
        }
    }

}
