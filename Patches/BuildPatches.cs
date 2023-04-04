using UnityEngine;
using BepInEx;
using HarmonyLib;
using System.Collections.Generic;

namespace Shipshape.Patches;

public class BuildPatches
{
    public static float rotation;

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Builder), "MovePieces")]
    public static void MovePiecesPostfix(Builder __instance)
    {
        __instance.currentBuildPiece.transform.localEulerAngles += new Vector3(0, 0, Mathf.Round(rotation / 11.25f) * 11.25f);
        __instance.currentBuildPieceMirror.transform.localEulerAngles += new Vector3(0, 0, Mathf.Round(-rotation / 11.25f) * 11.25f);
        if (Input.GetKey(KeyCode.Q)) rotation += Input.GetAxis("Mouse ScrollWheel") * 100;

        if (Input.GetKeyDown(KeyCode.M)) __instance.mirror = !__instance.mirror;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Builder), "ScrollPieces")]
    public static bool ScrollPiecesPrefix(ShipPart[] __0)
    {
        for (int i = 0; i < __0.Length; i++)
        {
            if (__0[i])
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    __0[i].scrollTarget3 += Input.GetAxis("Mouse ScrollWheel") * 0.5f;
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    __0[i].scrollTarget1 += -Input.GetAxis("Mouse ScrollWheel") * 0.5f;
                }
                else if (!Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.Q))
                {
                    __0[i].scrollTarget2 += Input.GetAxis("Mouse ScrollWheel") * 0.5f;
                }
                __0[i].UpdateScroll();
            }
        }
        return false;
    }
}