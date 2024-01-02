using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace GladiatorColosseum
{
    public class HighScore
    {
        //Vägen till spelar-listan
        private string filePath = @"players.json";

        //Skapar lista
        private List<Player> players = new List<Player>();

        public HighScore()
        {
            if (File.Exists(@"players.json") == true)
            {
                string? jsonString = File.ReadAllText(filePath);
                players = JsonSerializer.Deserialize<List<Player>>(jsonString)!;
            }
        }

        //Lägger till spelare i listan
        public Player addPlayer(Player player)
        {

            players.Add(player);
            marshal();
            return player;
        }

        //Hämtar enskild spelare utifrån id
        public Player getPlayer(int? id)
        {
            var currentPlayer = players.FirstOrDefault(player => player.Id == id)!;

            return currentPlayer;
        }

        //Hämtar alla spelare från spelar-listan
        public List<Player> getPlayers()
        {
            return players;
        }

        //Uppdaterar en spelares värden
        public Player updatePlayer(int? id, Player updatedValues)
        {
            var playerToUpdate = players.FirstOrDefault(player => player.Id == id)!;

            playerToUpdate.Name = updatedValues.Name;
            playerToUpdate.Health = updatedValues.Health;
            playerToUpdate.Life = updatedValues.Life;
            playerToUpdate.Points = updatedValues.Points;
            playerToUpdate.Defence = updatedValues.Defence;
            playerToUpdate.Strength = updatedValues.Strength;

            marshal();

            return playerToUpdate;

        }

        //Skriver om och serialiserar spelar-listan till json-filen.
        private void marshal()
        {
            var jsonString = JsonSerializer.Serialize(players);
            File.WriteAllText(filePath, jsonString);
        }
    }
}