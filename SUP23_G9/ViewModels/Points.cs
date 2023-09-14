using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP23_G9.ViewModels
{
    internal class Points
    {
        private int _score;

        public int Score
        {
            get => _score;
            private set => _score = value;
        }

        public void AddPoints(int pointsToAdd)
        {
            Score += pointsToAdd;
        }

        public void DeductPoints(int pointsToDeduct)
        {
            Score -= pointsToDeduct;
            // Om du inte vill ha negativa poäng kan du lägga till en check här:
            if (Score < 0)
                Score = 0;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
