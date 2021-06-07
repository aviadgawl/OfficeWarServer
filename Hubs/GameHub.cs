using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SignalRServer.Models;
using SignalRServer.Logic;

namespace SignalRServer.Hubs
{
    public class GameHub : Hub
    {
        public static GameLogic game = new GameLogic();
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("--> Connection Closed: " + Context.ConnectionId);
            game.Exit(Context.ConnectionId);
            Clients.All.SendAsync("ConnectionLost", JsonConvert.SerializeObject(game.Players));
            return base.OnDisconnectedAsync(exception);
        }

        public async Task AttackMessage(string user, string message)
        {
            Console.WriteLine("Broadcast AttackMessage");
            Console.WriteLine(message);

            ActionMessage attackMessage = JsonConvert.DeserializeObject<ActionMessage>(message);
            game.Attack(attackMessage.reciverId, attackMessage.giverId);

            await Clients.All.SendAsync("AttackMessage", JsonConvert.SerializeObject(game.Players));
        }

        public async Task JoinGameMessage(string user, string message)
        {
            Console.WriteLine("Broadcast JoinGameMessage");
            Console.WriteLine(message);

            Player player = JsonConvert.DeserializeObject<Player>(message);
            game.Join(player);

            await Clients.All.SendAsync("JoinGameMessage", JsonConvert.SerializeObject(game.Players));
        }

        public async Task HealMessage(string user, string message)
        {
            Console.WriteLine("Broadcast Heal");
            Console.WriteLine(message);

            ActionMessage attackMessage = JsonConvert.DeserializeObject<ActionMessage>(message);
            game.Heal(attackMessage.reciverId, attackMessage.giverId);

            await Clients.All.SendAsync("HealMessage", JsonConvert.SerializeObject(game.Players));
        }

        public async Task RefreshMessage(string user, string message)
        {
            Console.WriteLine("Broadcast RefreshMessage");
            Console.WriteLine(user);

            game.Refresh(user);

            await Clients.All.SendAsync("RefreshMessage", JsonConvert.SerializeObject(game.Players));
        }
    }
}