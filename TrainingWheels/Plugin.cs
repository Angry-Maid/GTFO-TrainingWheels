using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using TrainingWheels;

[assembly: AssemblyVersion(Plugin.VERSION)]
[assembly: AssemblyFileVersion(Plugin.VERSION)]
[assembly: AssemblyInformationalVersion(Plugin.VERSION)]

namespace TrainingWheels;

[BepInPlugin(GUID, MOD_NAME, VERSION)]
[BepInDependency("dev.gtfomodding.gtfo-api")]
public class Plugin : BasePlugin
{
    public const string GUID = "dev.Angry-Maid.gtfo.TrainingWheels";
    public const string MOD_NAME = ManifestInfo.TSName;
    public const string VERSION = ManifestInfo.TSVersion;

    internal static ManualLogSource L;

    private static readonly Harmony _harmony = new(GUID);

    public override void Load()
    {
        L = Log;

        ModConf.Init();

        _harmony.PatchAll(Assembly.GetExecutingAssembly());

        L.LogInfo("Plugin loaded!");
    }
}