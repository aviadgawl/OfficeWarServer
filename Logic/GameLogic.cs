using System.Collections.Generic;
using SignalRServer.Models;
using System.Linq;

namespace SignalRServer.Logic
{
    public class GameLogic
    {
        public List<Player> Players { get; set; }

        public GameLogic()
        {
            Players = new List<Player>();
        }

        public void Attack(string receiverId , string attackerId){
            Player reciver = Players.First(x => x.id == receiverId);
            Player attacker = Players.First(x => x.id == attackerId);
            attacker.mana -= 1;
            reciver.hp -=  attacker.attack;
            if(reciver.hp <= 0) attacker.attack += 1;
        }

        public void Heal(string receiverId , string attackerId){
            Player reciver = Players.First(x => x.id == receiverId);
            Player attacker = Players.First(x => x.id == attackerId);
            attacker.mana -= 1;
            reciver.hp +=  attacker.attack;
        }

        public void Join(Player player){
            Player playerExists = Players.FirstOrDefault(x => x.id == player.id);
            if(playerExists == null) Players.Add(player);
        }

        public void Exit(string playerId){
            Player playerExists = Players.FirstOrDefault(x => x.id == playerId);
            if(playerExists != null) Players.Remove(playerExists);
        }

        public void Refresh(string playerId){
            Player playerExists = Players.FirstOrDefault(x => x.id == playerId);
            if(playerExists != null) playerExists.mana = 2;
        }
    }
}