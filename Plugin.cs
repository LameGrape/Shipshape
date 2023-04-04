using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace Shipshape;

[BepInPlugin(GUID, NAME, VERSION)]
public class Plugin : BaseUnityPlugin
{
    private const string GUID = "raisin.airships2.shipshape";
    private const string NAME = "Shipshape";
    private const string VERSION = "0.2.0";

    private void Awake()
    {
        Logger.LogInfo($"We are Shipshape and ready to go!");
        Harmony.CreateAndPatchAll(typeof(Patches.Fixes));
        Harmony.CreateAndPatchAll(typeof(Patches.UIPatches));
        Harmony.CreateAndPatchAll(typeof(Patches.BuildPatches));

        Patches.UIPatches.Init();

        GameObject bindManager = new GameObject("Bind Manager");
        bindManager.AddComponent<Binds.BindManager>();
        DontDestroyOnLoad(bindManager);
    }
}