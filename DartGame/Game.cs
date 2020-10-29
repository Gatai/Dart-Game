using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DartGame
{
    public class Game
    {
        private List<Player> playerList = new List<Player>();
        private Random random = new Random();
        //Create readonly get property using expresion body member syntax
        private int ScoreGoal => 301;
        private int ThrowsCount => 3;

        public void StartGame()
        {
            RulesForTheGame();
            CreatePlayers();
            CreateComputers();

            Console.WriteLine();
            ColorText(ConsoleColor.Cyan, "The game has started!");
            LineOutPut();
            Sleep();

            var winnerList = new List<Player>();

            var counterTurn = 0;
            while (winnerList.Count == 0)
            {
                    counterTurn++;
                foreach (var player in playerList)
                {
                    Console.WriteLine();
                    ColorText(ConsoleColor.Cyan, $"Its {player.Name} turn. Current round {counterTurn}!");
                    LineOutPut();

                    //List for alla throw
                    var throwList = new List<int>();

                    if (player.IsHuman)
                    {
                        //A player only have 3 throws
                        for (int i = 0; i < ThrowsCount; i++)
                        {
                            var arrowThrow = GetNumber($"{i + 1} throw: ", 0, 20);

                            throwList.Add(arrowThrow);
                        }
                    }
                    else
                    {
                        //A Computer only have 3 throws
                        for (int i = 0; i < ThrowsCount; i++)
                        {
                            var arrowThrow = random.Next(0, 21);
                            Console.WriteLine($"{i + 1} throw: {arrowThrow}");
                            throwList.Add(arrowThrow);
                            Sleep();
                        }
                    }
                    //Puts all throws in a list. 
                    player.AddTurn(throwList);

                    Console.WriteLine();
                    ColorText(ConsoleColor.Cyan, $"{player.Name}'s points on each turn and throw: ");
                    LineOutPut();

                    //Print the turns and all 3 throws
                    for (int i = 0; i < player.GetTurns().Count; i++)
                    {
                        Console.WriteLine($"{i + 1} turn: { player.GetTurns()[i]}");

                    }
                    Sleep();
                    Console.WriteLine();
                    if (player.Calculatepoints() >= ScoreGoal)
                    {
                        winnerList.Add(player);
                    }
                    ColorText(ConsoleColor.Cyan, $"Total sum for {player.Name} : {player.Calculatepoints()} points");
                    LineOutPut();
                    Console.WriteLine();
                }
            }

            Sleep();
            PrintWinner(GetWinners(winnerList));
            LineOutPut();

            Sleep();
            Console.WriteLine();
            PrintScoreBoard(playerList, "Players score board: ");
            Console.WriteLine();

            PlayAgainMeny();
        }

        public void PrintScoreBoard(List<Player> playerList, string text)
        {
            //Print text with color
            ColorText(ConsoleColor.Cyan, text);

            Console.WriteLine();

            //Sort the playerlist
            var sortedList = playerList.OrderByDescending(m => m.Calculatepoints()).ToList();

            var rank = 1;
            for (int i = 0; i < sortedList.Count(); i++)
            {
                //Write the player rank with sorted list
                Console.WriteLine($"{rank}: {sortedList[i].Name} with {sortedList[i].Calculatepoints()} points.");

                /*
                 * Check the length of the list
                   To then avoid error in [i + 1].
                   Check if there is a player who has the same score.If not then go into the if statement.
                   otherwise return the same rank
                 */
                if (i == sortedList.Count() - 1 || sortedList[i].Calculatepoints() != sortedList[i + 1].Calculatepoints())
                {
                    rank++;
                }
            }
        }

        //If the player wants to play again
        public void PlayAgainMeny()
        {
            ColorText(ConsoleColor.Magenta, "Wants to play again? Answer with y/n: ");

            var playagain = GetBool("");

            if (playagain)
            {
                Game game = new Game();
                game.StartGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public void PrintWinner(List<Player> winnerList)
        {
            foreach (var winner in winnerList)
            {
                ColorText(ConsoleColor.Green, $"The winner is {winner.Name} with {winner.Calculatepoints()} points");

                Console.WriteLine();
                ColorText(ConsoleColor.Cyan, $"Below you see the {winner.Name} throws and rounds:");

                //Prints the player throws and turns
                foreach (var player in playerList)
                {
                    if (winner == player)
                    {
                        for (int i = 0; i < player.GetTurns().Count; i++)
                        {
                            Console.WriteLine($"{i + 1} turn: { player.GetTurns()[i]}");
                        }
                    }
                }
            }
        }

        public void RulesForTheGame()
        {
            ColorText(ConsoleColor.Cyan, "Welcome to Dart Game!");
            LineOutPut();

            Console.WriteLine();
            ColorText(ConsoleColor.Cyan, "Here is some information for the game: ");

            Console.WriteLine("1.Every player gets three throws!");
            Console.WriteLine("2.You can write between 0-20 points only!");
            Console.WriteLine();
        }

        public void CreatePlayers()
        {
            ColorText(ConsoleColor.Cyan, "Write how many human players? ");

            var numberOfPlayers = GetNumber("", 1, 10);

            for (int i = 0; i < numberOfPlayers; i++)
            {
                /*
                The player have to write a name
                Not allowed to enter a blank space
                 */
                var playerName = "";

                while (playerName == "")
                {
                    Console.Write($"Write {i + 1} player name: ");
                    playerName = Console.ReadLine();
                }

                AddPlayer(playerName, true);
            }
        }

        public void CreateComputers()
        {
            ColorText(ConsoleColor.Cyan, "Play with computer? Answer with y/n: ");

            var playWithComputer = GetBool("");

            if (playWithComputer)
            {
                //1- 10 computer player can it be 
                var computerCount = GetNumber("How many computer player? ", 1, 10);

                for (int i = 0; i < computerCount; i++)
                {
                    playerList.Add(new Player($"Computer {i + 1}", false));
                }
            }
        }

        //Get number from user and validate        
        //int min and max can be null with "?"
        public int GetNumber(string text, int? min = null, int? max = null)
        {
            Console.Write(text);

            while (true)
            {
                var tempNumber = 0;
                var inputNumber = Console.ReadLine();

                var isNumber = int.TryParse(inputNumber, out tempNumber);
                if (!isNumber)
                {
                    Console.WriteLine("Invalid input, must be a number!");
                }
                //HasValue checks if min has a value or not.
                else if (min.HasValue && max != null && tempNumber < min || tempNumber > max)
                {
                    Console.WriteLine($"The input must be between {min} - {max}.");
                }
                else
                {
                    //Return the number
                    return tempNumber;
                }
            }
        }

        //Get bool from user
        public bool GetBool(string text)
        {
            Console.Write(text);

            while (true)
            {
                var input = Console.ReadLine();

                if (input.ToLower() == "n")
                {
                    return false;
                }
                else if (input.ToLower() == "y")
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid input, must be y or n!");
                }
            }
        }

        //Return all winners in a list
        public List<Player> GetWinners(List<Player> winnerList)
        {
            //Find the max score on the game
            var maxPoints = 0;
            foreach (var winner in winnerList)
            {
                if (winner.Calculatepoints() > maxPoints)
                {
                    maxPoints = winner.Calculatepoints();
                }
            }

            //Get the winner or winners if it's a draw
            //Check if one or more players have the same score
            var allWinners = new List<Player>();
            foreach (var winner in winnerList)
            {
                if (winner.Calculatepoints() == maxPoints)
                {
                    allWinners.Add(winner);
                }
            }
            //return all winners/winner
            return allWinners;
        }

        public void AddPlayer(string name, bool isHuman)
        {
            Player playername = new Player(name, isHuman);

            playerList.Add(playername);
        }

        public void LineOutPut()
        {
            Console.WriteLine(new string('-', 80));
        }

        public void ColorText(ConsoleColor color, string text)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = currentColor;
        }

        public void Sleep()
        {
            Thread.Sleep(1000);
        }
    }
}
