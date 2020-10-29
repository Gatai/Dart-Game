using System;
using System.Collections.Generic;
using System.Text;

namespace DartGame
{
    public class Player
    {
        public string Name { get; }

        //Property represented if a user is a human or not
        public bool IsHuman { get; }

        //List of object of turns.
        private List<Turn> turnsList = new List<Turn>();

        public Player(string name, bool isHuman)    
        {
            Name = name;
            IsHuman = isHuman;
        }

        // Write all throws
        public List<Turn> GetTurns()
        {
            return turnsList;
        }

        //Calculate all score 
        public int Calculatepoints()
        {
            var result = 0;
            foreach (var turn in turnsList)
            {
                result += turn.GetScore();
            }

            return result;
        }

        //Add turn
        public void AddTurn(List<int> throwList)
        {
            //Creates a turns object that has stored in the turnsList list
            turnsList.Add(new Turn(throwList));
        }

        //Override the ToString metod
        public override string ToString()
        {
            return string.Format(Name);
        }

    }
}
