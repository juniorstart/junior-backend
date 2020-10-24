using System;
using System.Collections.Generic;
using JuniorStart.Entities;
using Task = System.Threading.Tasks.Task;

namespace JuniorStart.Controllers.Interfaces
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(Message message);
        Task ReceiveRoom(Room room);
        Task ReceiveRooms(List<Tuple<int, string>> roomNames);
    }
}
