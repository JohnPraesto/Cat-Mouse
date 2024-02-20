using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Cat___Mouse
{
    // Varannan gång jaga varandra. Typ man måste fånga inom 10 sec så får man ett poäng.
    // Bäst till 3. Typ world chase tag.
    // Problem: user input are recorded before play begins. It should not.
    // Problem: Opening menu doesnt show without user input during gameplay. Anwser: The program stops by key = Console.ReadKey();
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chase each other. One player has 10 seconds to catch the other. " +
            "Player One gets one point if catch, Player Two gets one point if evade. " +
            "Player who has the most points after three rounds wins. " +
            "If there is a tie rounds keep going until one player has the lead after a complete round..");

            string b = char.ConvertFromUtf32(0x25A1);
            Coordinate white = new Coordinate("■", 40, 20);
            Coordinate black = new Coordinate(b, 0, 0);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            int chaser = 0;
            int round = 1;
            bool take = false;
            Stopwatch timer = new Stopwatch();
            int sec = 0;

            ConsoleKeyInfo move;
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(17, 0);
                Console.Write($"ROUND {round}");
                Console.SetCursorPosition(12, 1);

                if (chaser % 2 == 0)
                    Console.WriteLine("BLACK chase WHITE"); // When chaser is even its black box turn to chase
                else
                    Console.WriteLine("WHITE chase BLACK"); // When chaser is uneven its white box turn to chase

                Console.WriteLine("\n          BLACK BOX - WHITE BOX");
                Console.WriteLine("                  " + black.score + " - " + white.score);
                Console.Write("\n          Press Enter to start");
                Console.ReadLine();

                Console.SetCursorPosition(10, 5); // Erasing "Press Enter to start"
                Console.Write("\n                              ");

                white.X = 40;
                white.Y = 20;
                black.X = 0;
                black.Y = 0;
                sec = 1;
                take = false;

                white.Print();
                black.Print();

                timer.Stop();
                timer.Reset();
                timer.Start();

                while (sec < 4)
                {
                    Console.SetCursorPosition(20, 10);
                    sec = (int)Math.Round(timer.Elapsed.TotalSeconds);
                    Console.Write(5 - sec);
                    Thread.Sleep(1000);
                }

                timer.Stop();
                timer.Reset();
                timer.Start();

                Console.Clear();
                white.Print();
                black.Print();

                while (timer.Elapsed.Seconds < 10)
                {
                    move = Console.ReadKey();
                    Console.Clear();
                    switch (move.Key)
                    {
                        case ConsoleKey.W:
                            black.Y--;
                            break;
                        case ConsoleKey.S:
                            black.Y++;
                            break;
                        case ConsoleKey.D:
                            black.X++;
                            break;
                        case ConsoleKey.A:
                            black.X--;
                            break;

                        case ConsoleKey.UpArrow:
                            white.Y--;
                            break;
                        case ConsoleKey.DownArrow:
                            white.Y++;
                            break;
                        case ConsoleKey.RightArrow:
                            white.X++;
                            break;
                        case ConsoleKey.LeftArrow:
                            white.X--;
                            break;
                    }

                    black.Print();
                    white.Print();

                    if (black.Y == white.Y && black.X == white.X)
                    {
                        GiveScore(black, white, chaser);

                        take = true; // A box captured the other box

                        break;
                    }
                }
                if (!take) // If no box was captured, the score goes to the not captured box
                {
                    GiveScore(white, black, chaser);
                }
                if (!(chaser % 2 == 0))
                {
                    round++;
                    if (round > 3)
                    {
                        if (black.score > white.score)
                        {
                            Console.Clear();
                            Console.WriteLine("BLACK BOX WINS!");
                            Console.WriteLine("\nEnter to exit.");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else if (black.score < white.score)
                        {
                            Console.Clear();
                            Console.WriteLine("WHITE BOX WINS!");
                            Console.WriteLine("\nEnter to exit.");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                }
                chaser++;
            }
        }
        public static void GiveScore(Coordinate first, Coordinate second, int chaser)
        {
            if (chaser % 2 == 0)
                first.score++;
            else
                second.score++;
        }
    }
}