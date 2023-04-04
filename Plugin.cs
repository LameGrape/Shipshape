using BepInEx;
using HarmonyLib;

namespace Shipshape;

[BepInPlugin(GUID, NAME, VERSION)]
public class Plugin : BaseUnityPlugin
{
    private const string GUID = "raisin.airships2.shipshape";
    private const string NAME = "Shipshape";
    private const string VERSION = "0.0.1";

    private void Awake()
    {
        Logger.LogInfo($"We are Shipshape and ready to go!");
        Harmony.CreateAndPatchAll(typeof(Patches.Fixes));
        Harmony.CreateAndPatchAll(typeof(Patches.UIPatches));

        Patches.UIPatches.AddTextToCrosshair();
    }
}