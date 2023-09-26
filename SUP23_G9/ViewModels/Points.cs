using SUP23_G9.ViewModels.Base;

namespace SUP23_G9.ViewModels
{
    public class Points : BaseViewModel
    {
        public int Score { get; private set; }
        public string DisplayScore => $"{Score} Points";

        public void AddPoints(int pointsToAdd) => Score += pointsToAdd;
        public void DeductPoints(int pointsToDeduct)
        {
            Score -= pointsToDeduct;
            // Om du inte vill ha negativa poäng kan du lägga till en check här:
            if (Score < 0)
                Score = 0;
        }
        public int GetScore() => Score;
        public void ResetScore() => Score = 0;
    }
}