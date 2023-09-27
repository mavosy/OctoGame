using SUP23_G9.ViewModels.Base;

namespace SUP23_G9.ViewModels
{
    public class Points : BaseViewModel
    {
        /// <summary>
        /// Representerar den aktuella poängen.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Returnerar den aktuella poängen som en sträng.
        /// </summary>
        public string DisplayScore => $"{Score} Poäng";

        /// <summary>
        /// Returnerar den aktuella poängen.
        /// </summary>
        /// <returns>Aktuell poäng.</returns>
        public int GetScore()
        {
            return Score;
        }

        /// <summary>
        /// Lägger till poäng till den totala poängen.
        /// </summary>
        public void AddPoints(int pointsToAdd)
        {
            Score += pointsToAdd;
        }

        /// <summary>
        /// Drar av poäng från den totala poängen.
        /// </summary>
        public void DeductPoints(int pointsToDeduct)
        {
            Score -= pointsToDeduct;
            if (Score < 0)
                Score = 0; 
        }

        /// <summary>
        /// Återställer den totala poängen till noll.
        /// </summary>
        public void ResetScore()
        {
            Score = 0;
        }
    }
}