namespace Navigation.FreeRoam
{
    public static class Converter
    {
        public static string MapNameToString(MapName mapName) => mapName.ToString();
        public static MapName StringToMapName(string mapName) => (MapName)System.Enum.Parse(typeof(MapName), mapName.ToUpper());
    }
}