using System.Collections.Generic;

public class Indexes
{
    public static int Combat_Announcer_Male_Effect_Ready { get; private set; } = 0;
    public static int Combat_Announcer_Male_Effect_Go { get; private set; } = 1;  
    public static int Combat_Announcer_Male_Effect_You_Win_1 { get; private set; } = 2;
    public static int Combat_Announcer_Male_Effect_You_Lose { get; private set; } = 3;


    public static string[] PositiveFeedbacks = new string[]
    {
        "Good Hit!",
        "Good Shot!",
        "Great!",
        "Nice Shot!",
        "Great Shot!",
        "Amazing!",
        "Excellent!",
        "Brilliant!"
    };


    public static int Combat_Announcer_Male_Effect_Good_Hit { get; private set; } = 0;
    public static int Combat_Announcer_Male_Effect_Good_Shot { get; private set; } = 1;
    public static int Combat_Announcer_Male_Effect_Great { get; private set; } = 2;
    public static int Combat_Announcer_Male_Effect_Nice_Shot { get; private set; } = 3;
    public static int Combat_Announcer_Male_Effect_Great_Shot { get; private set; } = 4;
    public static int Combat_Announcer_Male_Effect_Amazing { get; private set; } = 5;
    public static int Combat_Announcer_Male_Effect_Excellent { get; private set; } = 6;
    public static int Combat_Announcer_Male_Effect_Brilliant { get; private set; } = 7;
}
