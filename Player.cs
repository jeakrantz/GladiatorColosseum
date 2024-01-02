using System.Text.Json.Serialization;

namespace GladiatorColosseum
{
    [Serializable]
    public class Player 
    {
        //Standardvärden för spelaren vid start.
        private int? id;
        private string? name;
        private int? health;
        private int? life;
        private int? points;

        private int? defence; 

        private int? strength;


        //Getters och setters
        public int? Id {get{return this.id;} set{this.id = value;}}
        public string? Name {get{return this.name;} set{this.name = value;}}
        public int? Health {get{return this.health;} set{this.health = value;}}
        public int? Life {get{return this.life;} set{this.life = value;}}
        public int? Points {get{return this.points;} set{this.points = value;}}
        public int? Defence {get{return this.defence;} set{this.defence = value;}}
        public int? Strength {get{return this.strength;} set{this.strength = value;}}       
    }
}