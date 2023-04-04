using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace Shipshape.Patches;

// Bug fixes to various things
public class Fixes
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(ControllableRotate), "Start")]
    public static void RotatorScrollFix(ControllableRotate __instance) // Fixes Rotators not reacting to scrolling
    {
        __instance.transform.parent.gameObject.AddComponent<Scrollable>();
    }
}