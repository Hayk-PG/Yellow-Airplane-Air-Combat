public static class Messages
{
    public static string[] LeaveRoomMessages { get; private set; } = new string[2]
    {
        "LEAVE ROOM",
        "You will leave your current room. Do you wish to continue?"
    };
    public static string[] ActionInOfflineModeErrorMessage { get; private set; } = new string[2]
    {
        "FAILED TO CONNECT TO THE SERVER",
        "Sorry, this action is not available in offline mode. Return to the Main Menu?"
    };
}
