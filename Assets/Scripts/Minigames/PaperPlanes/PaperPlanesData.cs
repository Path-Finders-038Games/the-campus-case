namespace Minigames.PaperPlanes
{
    public static class PaperPlanesData
    {
        public static int PlanesSpawned { get; set; }
        public static int PlanesHit { get; set; }
        public static int PlanesMissed { get; set; }
        
        public static void ResetData()
        {
            PlanesSpawned = 0;
            PlanesHit = 0;
            PlanesMissed = 0;
        }
    }
}