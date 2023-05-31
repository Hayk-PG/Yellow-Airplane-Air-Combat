
public static class EventInfo
{
    public static byte Code_InstantiatePlayers { get; private set; } = 0;
    public static object Content_InstantiatePlayers { get; private set; } = "InstantiatePlayers";

    public static byte Code_InstantiateWoodenBox { get; private set; } = 1;
    public static object[] Content_InstantiateWoodenBox { get; set; }

    public static byte Code_TornadoDamage { get; private set; } = 2;
    public static object[] Content_TornadoDamage { get; set; }

    public static byte Code_WoodBoxTriggerEntered { get; private set; } = 3;
    public static object[] Content_WoodBoxTriggerEntered { get; set; }

    public static byte Code_LaunchBarrel { get; private set; } = 4;
    public static object[] Content_LaunchBarrel { get; set; }

    public static byte Code_BarrelCollision { get; private set; } = 5;
    public static object[] Content_BarrelCollision { get; set; }
}
