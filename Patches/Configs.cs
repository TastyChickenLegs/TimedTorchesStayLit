using BepInEx.Configuration;
using System.Linq;
using TimedTorchesStayLit;


namespace TimedTorchesStayLit.Patches;

public class Configs
{
    public static ConfigEntry<bool> configAlwaysOnInDarkBiomes;
    public static ConfigEntry<bool> fe_keepOnInRain;

    public static ConfigEntry<bool> fe_piece_walltorch_timer;
    public static ConfigEntry<bool> fe_piece_groundtorch_timer;
    public static ConfigEntry<bool> fe_piece_groundtorch_wood_timer;
    public static ConfigEntry<bool> fe_piece_groundtorch_green_timer;
    public static ConfigEntry<bool> fe_piece_groundtorch_blue_timer;
    public static ConfigEntry<bool> fe_piece_brazierfloor01_timer;
    public static ConfigEntry<bool> fe_piece_brazierceiling01_timer;
    public static ConfigEntry<bool> fe_piece_jackoturnip_timer;
    private static ConfigEntry<string> fe_custom_instance_timer;
    public static ConfigEntry<bool> fe_piece_walltorch_lit;
    public static ConfigEntry<bool> fe_piece_groundtorch_lit;
    public static ConfigEntry<bool> fe_piece_groundtorch_wood_lit;
    public static ConfigEntry<bool> fe_piece_groundtorch_green_lit;
    public static ConfigEntry<bool> fe_piece_groundtorch_blue_lit;
    public static ConfigEntry<bool> fe_piece_brazierfloor01_lit;
    public static ConfigEntry<bool> fe_piece_brazierceiling01_lit;
    public static ConfigEntry<bool> fe_piece_jackoturnip_lit;
    private static ConfigEntry<string> fe_custom_instance_lit;



    public static ConfigEntry<int> timerOnMinutes;
    public static ConfigEntry<int> timerOnHours;
    public static ConfigEntry<int> timerOffMinutes;
    public static ConfigEntry<int> timerOffHours;
    public static float timerOnFloatTime;
    public static float timerOffFloatTime;

    internal static void Generate()
    {
        //A portion of this code provided in Fuel Eternal by Markinator  all credit
        //    //This code provided in Fuel Eternal by Markinator  all credit
        //  used here with permission.
        //Items//

        configAlwaysOnInDarkBiomes = TimedTorchesStayLitMain.context.config("Basic", "Always On In Dark Biomes", true, "If true, torches will always burn in areas that Valheim considers 'always dark'. E.g Mistlands or any biome during a storm");
        fe_piece_walltorch_timer = TimedTorchesStayLitMain.context.config("Use Timers", "Wall Torch on Timer", true, "Allow timer for Sconce");
        fe_piece_groundtorch_timer = TimedTorchesStayLitMain.context.config("Use Timers", "Metal Torch On Timer", true, "Allow timer for Standing iron torch");
        fe_piece_groundtorch_wood_timer = TimedTorchesStayLitMain.context.config("Use Timers", "Wood Torch on Timer", true, "Allow timer for Standing wood torch");
        fe_piece_groundtorch_green_timer = TimedTorchesStayLitMain.context.config("Use Timers", "Green Torch on Timer", true, "Allow timer for Standing green-burning iron torch");
        fe_piece_groundtorch_blue_timer = TimedTorchesStayLitMain.context.config("Use Timers", "Blue Torch on Timer", true, "Allow timer for Standing blue-burning iron torch");
        fe_piece_brazierfloor01_timer = TimedTorchesStayLitMain.context.config("Use Timers", "Floor Brazier on Timer", true, "Allow timer for Standing brazier");
        fe_piece_brazierceiling01_timer = TimedTorchesStayLitMain.context.config("Use Timers", "Ceiling Brazier on Timer", true, "Allow timer for Hanging brazier");
        fe_piece_jackoturnip_timer = TimedTorchesStayLitMain.context.config("Use Timers", "Jackoturnip on Timer", true, "Allow timer for Jack-o-turnip");
        fe_custom_instance_timer = TimedTorchesStayLitMain.context.config("Custom", "Custom Items on Timers", "", "Enable Timers for items added by other mods, " +
            "comma-separated no spaces (e.g. \"rk_campfire,rk_hearth,rk_brazier\" )");
        fe_piece_walltorch_lit = TimedTorchesStayLitMain.context.config("Stay Lit", "Wall Torch Stay Lit", true, "Sconce Stay Lit");
        fe_piece_groundtorch_lit = TimedTorchesStayLitMain.context.config("Stay Lit", "Metal Torch Stay Lit", true, "iron torch Stay Lit");
        fe_piece_groundtorch_wood_lit = TimedTorchesStayLitMain.context.config("Stay Lit", "Wood Torch Stay Lit", true, "Standing wood torch Stay Lit");
        fe_piece_groundtorch_green_lit = TimedTorchesStayLitMain.context.config("Stay Lit", "Green Torch Stay Lit", true, "Standing green-burning iron torch Stay Lit");
        fe_piece_groundtorch_blue_lit = TimedTorchesStayLitMain.context.config("Stay Lit", "Blue Torch Stay Lit", true, "Standing blue-burning iron torch Stay Lit");
        fe_piece_brazierfloor01_lit = TimedTorchesStayLitMain.context.config("Stay Lit", "Floor Brazier Stay Lit", true, "Standing brazier Stay Lit");
        fe_piece_brazierceiling01_lit = TimedTorchesStayLitMain.context.config("Stay Lit", "Ceiling Brazier Stay Lit", true, "Hanging brazier Stay Lit");
        fe_piece_jackoturnip_lit = TimedTorchesStayLitMain.context.config("Stay Lit", "Jackoturnip Stay Lit", true, "Jack-o-turnip Stay Lit");
        fe_custom_instance_lit = TimedTorchesStayLitMain.context.config("Custom", "Custom Items Stay Lit", "", "Stay Lit items added by other mods, " +
            "comma-separated no spaces (e.g. \"rk_campfire,rk_hearth,rk_brazier\" )");
        fe_keepOnInRain = TimedTorchesStayLitMain.context.config("Basic", "Keep on when wet", true, "Keep fires lit even when raining and wet");

        //Timer Settings//
        timerOnHours = TimedTorchesStayLitMain.context.config("Timer Settings", "Timer On Hours (Night Time)", 17,
            new ConfigDescription("Time to Turn on at night in 24 hour time.  Example 7pm is 19 hours.",
            new AcceptableValueRange<int>(0, 24)));
        timerOnMinutes = TimedTorchesStayLitMain.context.config("Timer Settings", "Timer On Mins (Night Time)", 0,
            new ConfigDescription("Minutes in the hour.  This will be added to the hours above.",
            new AcceptableValueRange<int>(0, 60)));
        timerOffHours = TimedTorchesStayLitMain.context.config("Timer Settings", "Timer Off Hours (Day Time)", 5,
            new ConfigDescription("Time to Turn off the monring in 24 hour time.  Example 7pm is 19 hours.",
            new AcceptableValueRange<int>(0, 24)));
        timerOffMinutes = TimedTorchesStayLitMain.context.config("Timer Settings", "Timer Off Mins (Day Time)", 30,
            new ConfigDescription("Minutes in the hour.  This will be added to the hours above.",
            new AcceptableValueRange<int>(0, 60)));



    }
    public static bool ConfigChecks(string instanceName)
    {
        bool TimerOne = false;
        switch (instanceName)
        {
            case "piece_walltorch(Clone)":
                TimerOne = fe_piece_walltorch_timer.Value;
                break;

            case "piece_groundtorch(Clone)":
                TimerOne = fe_piece_groundtorch_timer.Value;
                break;

            case "piece_groundtorch_wood(Clone)":
                TimerOne = fe_piece_groundtorch_wood_timer.Value;
                break;

            case "piece_groundtorch_green(Clone)":
                TimerOne = fe_piece_groundtorch_green_timer.Value;
                break;

            case "piece_groundtorch_blue(Clone)":
                TimerOne = fe_piece_groundtorch_blue_timer.Value;
                break;

            case "piece_brazierfloor01(Clone)":
                TimerOne = fe_piece_brazierfloor01_timer.Value;
                break;

            case "piece_brazierceiling01(Clone)":
                TimerOne = fe_piece_brazierceiling01_timer.Value;
                break;

            case "piece_jackoturnip(Clone)":
                TimerOne = fe_piece_jackoturnip_timer.Value;
                break;

        }
        if (fe_custom_instance_timer.Value.Split(',').Contains(instanceName.Remove(instanceName.Length - 7)))
            TimerOne = true;
        return TimerOne;
    }
    public static bool ConfigFuel(string instanceName)
    {
        bool TimerFuel = false;
        switch (instanceName)
        {
            case "piece_walltorch(Clone)":
                TimerFuel = fe_piece_walltorch_lit.Value;
                break;

            case "piece_groundtorch(Clone)":
                TimerFuel = fe_piece_groundtorch_lit.Value;
                break;

            case "piece_groundtorch_wood(Clone)":
                TimerFuel = fe_piece_groundtorch_wood_lit.Value;
                break;

            case "piece_groundtorch_green(Clone)":
                TimerFuel = fe_piece_groundtorch_green_lit.Value;
                break;

            case "piece_groundtorch_blue(Clone)":
                TimerFuel = fe_piece_groundtorch_blue_lit.Value;
                break;

            case "piece_brazierfloor01(Clone)":
                TimerFuel = fe_piece_brazierfloor01_lit.Value;
                break;

            case "piece_brazierceiling01(Clone)":
                TimerFuel = fe_piece_brazierceiling01_lit.Value;
                break;

            case "piece_jackoturnip(Clone)":
                TimerFuel = fe_piece_jackoturnip_lit.Value;
                break;

        }
        if (fe_custom_instance_lit.Value.Split(',').Contains(instanceName.Remove(instanceName.Length - 7)))
            TimerFuel = true;
        return TimerFuel;
    }
}

