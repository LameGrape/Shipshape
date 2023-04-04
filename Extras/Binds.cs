using UnityEngine;
using BepInEx;
using HarmonyLib;

namespace Shipshape.Binds;

// Custom keybinds and other input
public class BindManager : MonoBehaviour
{
    public Builder builder;

    void Start()
    {
        builder = GameObject.Find("Player").GetComponent<Builder>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Patches.UIPatches.ToggleUI();
        }
    }
}