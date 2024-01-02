using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace GladiatorColosseum
{
    class Contest
    {
        static Random r = new Random();
        //Contests
        public static void ColosseumContest(int? playerId)
        {

            Console.WriteLine("\nTryck på valfri knapp och starta matchen...");
            Console.ReadKey();

            Combat(playerId);

        }
        public static void Combat(int? playerId)
        {

            HighScore highscore = new HighScore();

            Player currentPlayer = highscore.getPlayer(playerId);

            //Variabel för standardvärde för fiende
            int x = 10;

            //Kontroll att spelaren existerar
            if (currentPlayer == null)
            {
                Console.WriteLine($"Spelare med ID {playerId} hittades inte. Gå tillbaka till start.");
                Console.ReadKey();
                Console.Clear();
                Program.Start();
            }
            else
            {

                //Spelarens värden
                int points = Convert.ToInt32(currentPlayer.Points)!;
                int health = Convert.ToInt32(currentPlayer.Health)!;
                int defence = Convert.ToInt32(currentPlayer.Defence)!;
                int strength = Convert.ToInt32(currentPlayer.Strength)!;
                int life = Convert.ToInt32(currentPlayer.Life)!;

                //Fiendens värden
                string n = GetEnemy();
                int p = r.Next(2, x);
                int h = r.Next(5, x);

                //Medan fiendens värde är högre än noll körs matchen. 
                while (h > 0)
                {
                    int damage = 0;
                    int attack = 0;

                    Console.Clear();

                    //Information om fienden
                    Console.WriteLine("Du möter " + n);
                    Console.WriteLine("Styrka: " + p + " | Hälsa: " + h);

                    //Valmöjligheter
                    Console.WriteLine("##############");
                    Console.WriteLine("#  (A)ttack  #");
                    Console.WriteLine("# (F)örsvara #");
                    Console.WriteLine("#   (H)ela   #");
                    Console.WriteLine("##############");

                    //Information om spelaren
                    Console.WriteLine("Poäng: " + points + "      Liv: " + life + "     Hälsa: " + health);

                    string input = Console.ReadLine()!;

                    if (input.ToLower() == "a" || input.ToLower() == "attack")
                    {
                        //Skada som tas när attack görs. 
                        damage = p - defence;

                        //Kontroll så att skadan inte läggs på minus
                        if (damage < 0)
                        {
                            damage = 0;
                        }

                        //Attackens effekt räknas ut genom att randomisera en siffra ur spelarens strength värde och en siffra mellan 1-5.
                        //Detta gör Attackens kraft mindre förutsägbar och spelet roligare
                        attack = r.Next(0, strength) + r.Next(1, 5);

                        Program.Print("Du skadar " + n + " med " + attack + " och tar själv " + damage + " i skada.");
                    }
                    if (input.ToLower() == "f" || input.ToLower() == "försvara")
                    {
                        //Skada som tas när spelaren försvarar sig. 
                        damage = (p / 4) - defence;

                        //Kontroll så att skadan inte läggs på minus
                        if (damage < 0)
                        {
                            damage = 0;
                        }

                        //Attackens effekt räknas ut genom att randomisera en siffra ur spelarens strength värde och en siffra mellan 1-5.
                        //Detta gör Attackens kraft mindre förutsägbar och spelet roligare
                        attack = r.Next(0, strength) / 2;

                        Program.Print("Du försvarar dig och får " + damage + " i skada. Du lyckas skada " + n + " med " + attack + ".");

                    }
                    if (input.ToLower() == "h" || input.ToLower() == "hela")
                    {
                        //Hela
                        if (life == 0)
                        {
                            //Om inga liv finna att använda
                            Program.Print("Du har inga fler liv att använda för att hela dig.");

                            //Skada som tas när helning misslyckas görs. 
                            damage = p - defence;

                            //Kontroll så att skadan inte läggs på minus
                            if (damage < 0)
                            {
                                damage = 0;
                            }

                            Program.Print(n + " skadar dig med " + damage + ".");
                        }
                        else
                        {
                            //Om liv finns att använda
                            int lifeValue = 10;
                            Program.Print("Du får tillbaka " + lifeValue + " i HP.");
                            health += lifeValue;
                            life -= 1;

                            //Skada som tas efter helning görs. 
                            damage = (p / 2) - defence;

                            //Kontroll så att skadan inte läggs på minus
                            if (damage < 0)
                            {
                                damage = 0;
                            }

                            Program.Print(n + " skadar dig med " + damage + ".");
                        }


                    }

                    //Skada delas ut till spelaren
                    health -= damage;

                    //Skada delas ut till fienden
                    h -= attack;

                    updateStats();

                    if (health <= 0)
                    {
                        //Om spelarens hälsa är lägre än 0 avbruts match-loopen
                        health = 0;
                        break;
                    }
                    else
                    {
                        //Efter att val har gjorts och skada delats ut.
                        Console.ReadKey();
                    }

                }

                if (health != 0)
                {

                    Program.Print("Matchen är vunnen av " + currentPlayer.Name + "!");

                    //Om matchen har vunnits av spelaren adderas 100 poäng till spelarens poäng
                    int winPoints = 100;
                    points += winPoints;

                    Program.Print(currentPlayer.Name + " får " + winPoints + " poäng och har nu totalt " + points + " poäng.");

                    //Kontroll om spelarens poäng är jämnt delbart med 3, då har en hel dag gått 
                    if (points % 3 == 0)
                    {
                        //Ett extra liv adderas
                        life++;
                        //Starkare värden
                        health++;
                        strength++;
                        defence++;
                        //Starkare motståndare
                        x++;
                    }

                    updateStats();

                }
                else
                {
                    Program.Print(currentPlayer.Name + " förlorade matchen. Bättre lycka nästa gång.");
                    Console.WriteLine("Tryck på valfri knapp för att gå tillbaka till start...");
                    Console.ReadKey();
                    Console.Clear();
                    Program.Start();
                }

                void updateStats()
                {

                    Player updateValues = new Player()
                    {
                        Id = playerId,
                        Name = currentPlayer.Name,
                        Health = health,
                        Life = life,
                        Points = points,
                        Defence = defence,
                        Strength = strength
                    };

                    highscore.updatePlayer(playerId, updateValues);

                    // Get the updated player from HighScore
                    currentPlayer = highscore.getPlayer(playerId);
                }

            }

        }

        public static string GetEnemy()
        {
            string[] arr = [
                "Tigern Tony",
                "Björnen Börje",
                "Ormen Ove",
                "Spindeln Sissela",
                "Krokodilen Konny",
                "Höken Harald",
                "Tjuren Tina",
                "Elefanten Ellen"
            ];

            string name = arr[r.Next(0, arr.Length)];
            return name;
        }
    }
}