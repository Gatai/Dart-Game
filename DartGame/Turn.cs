using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartGame
{
    public class Turn
    {
        //all throws should be added to the list
        private List<int> throwList;

        //constructor taking in a list of throws
        public Turn(List<int> throwList)
        {
            //The list of all throws that come from the constructor is then assigned to the variable that is in the turn class
            this.throwList = throwList;
        }

        //Returns score of all 3 throws for one turn
        public int GetScore()
        {
            //I use a sum method that comes from linq. The sum method should return a value of numbers from a list or a collection.
            return throwList.Sum();
        }

        //Override the ToString method
        public override string ToString()
        {
            //Overrides the default ToString() metod with this format
            return string.Format("First Throw: {0}, Second Throw: {1}, Third Throw: {2} ", throwList[0], throwList[1], throwList[2]);
        }
    }
}
