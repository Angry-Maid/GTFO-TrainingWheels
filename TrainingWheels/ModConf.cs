using BepInEx.Configuration;
using BepInEx;
using System.IO;
using GTFO.API.Utilities;

namespace TrainingWheels
{
    internal static class ModConf
    {
        private readonly static ConfigEntry<int> _modMainStartingAmmo;
        public static int ModMainStartingAmmo => _modMainStartingAmmo.Value;
        private readonly static ConfigEntry<int> _modSpecialStartingAmmo;
        public static int ModSpecialStartingAmmo => _modSpecialStartingAmmo.Value;
        private readonly static ConfigEntry<int> _modToolStartingAmmo;
        public static int ModToolStartingAmmo => _modToolStartingAmmo.Value;

        private readonly static ConfigEntry<bool> _regenDisabled;
        public static bool RegenDisabled => _regenDisabled.Value;
        private readonly static ConfigEntry<int> _startingHealth;
        public static int StartingHealth => _startingHealth.Value;
        private readonly static ConfigEntry<int> _startingInfection;
        public static int StartingInfection => _startingInfection.Value;

        private readonly static ConfigFile configFile;

        static ModConf()
        {
            configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, Plugin.MOD_NAME + ".cfg"), saveOnInit: true);
            string ammoSection = "Ammo Percentages";
            _modMainStartingAmmo = configFile.Bind(ammoSection, "Main Ammo Percentage", 17, "Percentage from the starting ammo of Main (0-65535)");
            _modSpecialStartingAmmo = configFile.Bind(ammoSection, "Special Ammo Percentage", 17, "Percentage from the starting ammo of Special (0-65535)");
            _modToolStartingAmmo = configFile.Bind(ammoSection, "Tool Ammo Percentage", 17, "Percentage from the starting ammo of Tool (0-65535)");

            string healthSection = "Health";
            _regenDisabled = configFile.Bind(healthSection, "Regen Disabled", true, "Enables/Disables passive regeneration");
            _startingHealth = configFile.Bind(healthSection, "Starting Health", 1, "Set starting health upon drop");
            _startingInfection = configFile.Bind(healthSection, "Starting Infection", 100, "Set starting infection upon drop");
        }

        internal static void Init()
        {
            LiveEditListener listener = LiveEdit.CreateListener(Paths.ConfigPath, Plugin.MOD_NAME + ".cfg", false);
            listener.FileChanged += OnFileChanged;
        }

        private static void OnFileChanged(LiveEditEventArgs _)
        {
            configFile.Reload();
        }
    }
}
