using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GladiatorColosseum
{
    class Program
    {


        public static void Main(string[] args)
        {
            Console.Clear();
            Start();
        }

        //Startvy när spelet startar. Körs en gång
        public static void Start()
        {

            //Cool logga
            PrintLogo();

            Console.WriteLine("\n\n       (S)tart    (H)ögsta poäng     (O)m spelet \n");
            string input = Console.ReadLine()!;

            if (input.ToLower() == "s" || input.ToLower() == "start")
            {
                StartGame();
            }
            if (input.ToLower() == "h" || input.ToLower() == "högsta poäng")
            {
                HighScore();
            }
            if (input.ToLower() == "o" || input.ToLower() == "om spelet")
            {
                About();
            }


        }

        public static void StartGame()
        {

            //Skapa ny instans av HighScore
            HighScore highscore = new HighScore();

            Console.Clear();
            Print("Du är den nya Gladiatorn på Colosseum. Registrera ditt namn och gå med i kampen.");
            Console.WriteLine("Gladiatorns namn: ");
            string inputName = Console.ReadLine()!;

            //Om namn lämnas tomt sätts ett standardnamn
            if (inputName == "")
            {
                inputName = "Gladiator";
            }

            //Skapa ny spelare
            Player p = new Player();
            {
                p.Id = highscore.getPlayers().Count + 1;
                p.Name = inputName;
                p.Health = 10;
                p.Life = 3;
                p.Points = 0;
                p.Defence = 2;
                p.Strength = 4;
            }

            highscore.addPlayer(p);

            //Välkomnande och rensning av konsolen
            Print("\nVälkommen " + p.Name + "! Din första dag i Colosseum börjar nu...");
            Console.WriteLine("\nTryck knappen s för att skippa introduktionen,");
            string input = Console.ReadLine()!;
            Console.Clear();

            if (input.ToLower() != "s")
            {
                //Introduktion till spelare om hur spelet fungerar
                Print("Varje dag har du tre matcher och om du vinner alla får du chans att kämpa som Gladiator en dag till.\nDu kan hela dig själv med ett liv värt 10HP.\nVarje dag du överlever i arenan får du ett extra liv.\nFör varje vunnen match får du 100 poäng.\n\nLåt spelen börja!");
                Print("\n\nKlockorna ljudar. Publiken jublar. Portarna öppnas.");
                Print("Du tar dina första steg in i Arenan och möter dagens första fiende...");
            }

            while (p.Life > 0)
            {
                //Tre matcher körs.
                Contest.ColosseumContest(p.Id);
                Contest.ColosseumContest(p.Id);
                Contest.ColosseumContest(p.Id);

                //Val att köra tre matcher till
                Print("\nBra jobbat " + p.Name + ", du har överlevt en dag på Colosseum.");
                Console.WriteLine("\nTryck på valfri knapp för en dag till i arenan...");
                Console.ReadKey();
                Console.Clear();
            }

        }

        public static void About()
        {
            Console.WriteLine("Gladiator Colosseum har skapats av Jeanette Krantz\nsom projektuppgift i kursen Programmering i C#.NET.\n");
            Console.WriteLine("Det är ett turbaserat kampspel som utspelar sig på Colosseum i antika Rom. \n");

            Console.WriteLine("--Regler--");
            Console.WriteLine("Varje dag har du tre matcher och om du vinner alla får du chans att kämpa som Gladiator en dag till.\nDu kan hela dig själv med ett liv värt 10HP.\nVarje dag du överlever i arenan får du ett extra liv.\nFör varje vunnen match får du 100 poäng.\n\n");

            Console.WriteLine("Tryck på valfri knapp för att gå tillbaka till start...");
            Console.ReadKey();
            Console.Clear();
            Start();
        }

        public static void HighScore()
        {
            Console.WriteLine("Högsta poäng: \n\n");

            HighScore highscore = new HighScore();

            Player player = new Player();

            var playerList = highscore.getPlayers();

            if (playerList.Count == 0)
            {
                Console.WriteLine("Inga spelare att visa på listan");
            }
            else
            {
                int i = 1;
                foreach (var play in playerList.OrderByDescending(p => p.Points).Take(10))
                {
                    Console.WriteLine("#" + i + " Namn: " + play.Name + " Poäng: " + play.Points + "\n");
                    i++;
                }
            }

            Console.WriteLine("\nTryck på valfri knapp för att gå tillbaka till start...");
            Console.ReadKey();
            Console.Clear();
            Start();

        }

        public static void Print(string text, int speed = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
            Console.WriteLine();
        }
        public static void PrintLogo()
        {
            string[] Logo = [
                @"   ____ _           _ _       _",
                @"  / ___| | __ _  __| (_) __ _| |_ ___  _ __",
                @" | |  _| |/ _` |/ _` | |/ _` | __/ _ \| '__|",
                @" | |_| | | (_| | (_| | | (_| | || (_) | |",
                @"  \____|_|\__,_|\__,_|_|\__,_|\__\___/|_|",
                @"          ____      _ "                          ,
                @"         / ___|___ | | ___  ___ ___  ___ _   _ _ __ ___" ,
                @"        | |   / _ \| |/ _ \/ __/ __|/ _ \ | | | '_ ` _ \ ",
                @"        | |__| (_) | | (_) \__ \__ \  __/ |_| | | | | | |",
                @"         \____\___/|_|\___/|___/___/\___|\__,_|_| |_| |_|"
            ];

            foreach (string i in Logo)
            {
                Console.WriteLine(i);
                Thread.Sleep(60);
            }
            Console.WriteLine();
        }

    }
}