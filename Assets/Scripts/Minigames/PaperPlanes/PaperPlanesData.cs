namespace Minigames.PaperPlanes
{
    public static class PaperPlanesData
    {
        public const int WinScore = 10;
        public const int LoseScore = 10;

        public static int PlanesSpawned { get; set; }
        public static int PlanesHit { get; set; }
        public static int PlanesMissed { get; set; }
        
        public static bool IsRunning { get; set; }
        
        /// <summary>
        /// Reset the data for the paper planes minigame.
        /// </summary>
        public static void ResetData()
        {
            PlanesSpawned = 0;
            PlanesHit = 0;
            PlanesMissed = 0;
            IsRunning = false;
        }
    }
}