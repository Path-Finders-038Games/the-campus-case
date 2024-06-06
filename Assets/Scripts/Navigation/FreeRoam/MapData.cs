namespace Navigation.FreeRoam
{
    public static class MapData
    {
        public static MapName CurrentMap;

        static MapData()
        {
            // CurrentMap = DataManager.CurrentMapV2;
            CurrentMap = MapName.T0;
        }
    }
}