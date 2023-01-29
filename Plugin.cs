using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ServerSync;
using System;
using System.IO;
using TimedTorchesStayLit.Patches;

namespace TimedTorchesStayLit
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class TimedTorchesStayLitMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("Tastychickenlegs.TimedTorchesStayLit");
        internal const string ModName = "TimedTorchesStayLit";
        internal const string ModVersion = "1.3.0";
        internal const string Author = "Tastychickenlegs";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;
        public static ConfigEntry<string> toggleKey;
        public static ConfigEntry<string> toggleString;
        public static ConfigEntry<bool> isOn;
        public static bool configVerifyClient => _configVerifyClient.Value;
        internal static string ConnectionError = "";
        public static ConfigEntry<bool> _configEnabled;
        public static ConfigEntry<bool> _configVerifyClient;
        private static ConfigEntry<Toggle> _serverConfigLocked = null!;
        public static TimedTorchesStayLitMain context;
        public static ConfigEntry<string> configAffectedSources;
        public static float lastFuel;
        public static int fuelCount;

        private static readonly ConfigSync ConfigSync = new(ModGUID)
        { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };

        public static readonly ManualLogSource TastyLogger =
            BepInEx.Logging.Logger.CreateLogSource(ModName);

        public enum Toggle
        {
            On = 1,
            Off = 0
        }

        public void Awake()
        {
            
            _configEnabled = config("", "Mod Enabled", true, "Sets the mod to be enabled or not.");
            if (!_configEnabled.Value)
                return;

            context = this;
            _serverConfigLocked = TimedTorchesStayLitMain.context.config("", "Lock Configuration", Toggle.On,
                "If on, the configuration is locked and can be changed by server admins only.");
            _ = ConfigSync.AddLockingConfigEntry(_serverConfigLocked);

            _configVerifyClient = config("", "Verify Clients", true, "Enable this to turn on the client verification and version checks.");
    
            
            
            //Generate the Configs
            Configs.Generate();
            GogetTime();
            harmony.PatchAll();

            SetupWatcher();
            
        }

        private void Update()
        {
        }

        private void OnDestroy()
        {
            Config.Save();
            harmony.UnpatchSelf();
        }

        private void SetupWatcher()
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, ConfigFileName);
            watcher.Changed += ReadConfigValues;
            watcher.Created += ReadConfigValues;
            watcher.Renamed += ReadConfigValues;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadConfigValues(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(ConfigFileFullPath)) return;
            try
            {
                TastyLogger.LogDebug("ReadConfigValues called");

                Config.Reload();
                GogetTime();
            }
            catch
            {
                TastyLogger.LogError($"There was an issue loading your {ConfigFileName}");
                TastyLogger.LogError("Please check your config entries for spelling and format!");
            }
        }

        #region ConfigOptions

        internal ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description,
            bool synchronizedSetting = true)
        {
            ConfigDescription extendedDescription =
                new(
                    description.Description +
                    (synchronizedSetting ? " [Synced with Server]" : " [Not Synced with Server]"),
                    description.AcceptableValues, description.Tags);
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, extendedDescription);
            //var configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = ConfigSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        internal ConfigEntry<T> config<T>(string group, string name, T value, string description,
            bool synchronizedSetting = true)
        {
            return config(group, name, value, new ConfigDescription(description), synchronizedSetting);
        }

        private class ConfigurationManagerAttributes
        {
            public bool? Browsable = false;
        }

        private class AcceptableShortcuts : AcceptableValueBase
        {
            public AcceptableShortcuts() : base(typeof(KeyboardShortcut))
            {
            }

            public override object Clamp(object value) => value;

            public override bool IsValid(object value) => true;

            public override string ToDescriptionString() =>
                "# Acceptable values: " + string.Join(", ", KeyboardShortcut.AllKeyCodes);
        }

        #endregion ConfigOptions

        private static void GogetTime()
        {
            float timerValOnHours = Convert.ToSingle(Configs.timerOnHours.Value);
            timerValOnHours = timerValOnHours / 24;
            TastyLogger.LogDebug(timerValOnHours);

            float timerValOnMins = Convert.ToSingle(Configs.timerOnMinutes.Value);
            timerValOnMins = (timerValOnMins / 60) / 24;
            TastyLogger.LogDebug(timerValOnMins);

            Configs.timerOnFloatTime = (timerValOnMins + timerValOnHours);
            TastyLogger.LogDebug(Configs.timerOnFloatTime);

            float timerValOffHours = Convert.ToSingle(Configs.timerOffHours.Value);
            timerValOffHours = timerValOffHours / 24;
            TastyLogger.LogDebug(timerValOffHours);

            float timerValOffMins = Convert.ToSingle(Configs.timerOffMinutes.Value);
            timerValOffMins = (timerValOffMins / 60) / 24;
            TastyLogger.LogDebug(timerValOffMins);

            Configs.timerOffFloatTime = (timerValOffMins + timerValOffHours);
            TastyLogger.LogDebug(Configs.timerOffFloatTime);
        }
    }
}