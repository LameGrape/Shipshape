using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Shipshape.Patches;

// UI additions and patches
public class UIPatches
{
    public static TextMeshProUGUI crosshairText;
    public static CrosshairManager crosshairManager;

    public static void AddTextToCrosshair()
    {
        GameObject crosshairTextObject = new GameObject("Crosshair Text");
        Canvas canvas = crosshairTextObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        crosshairText = crosshairTextObject.AddComponent<TextMeshProUGUI>();
        crosshairText.text = "XXX<br>XXX     XXX<br>XXX";
        crosshairText.alignment = TextAlignmentOptions.Center;
        crosshairText.fontSize = 20;
        crosshairText.richText = true;
        crosshairManager = crosshairTextObject.AddComponent<CrosshairManager>();
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Interact), "LateUpdate")]
    public static void InteractPostfix(Interact __instance)
    {
        Camera cam = __instance.GetComponentInChildren<Camera>();
        Builder builder = __instance.GetComponent<Builder>();
        if (MenuHandler.inMenu || TextInputHandler.isTyping)
        {
            return;
        }
        float num = 100000f;
        Controllable controllable = null;
        RaycastHit[] array = Physics.SphereCastAll(cam.transform.position, 0.1f, cam.transform.forward, 5f);
        Ship ship = null;
        for (int i = 0; i < array.Length; i++)
        {
            if (!ship)
            {
                ship = array[i].transform.root.GetComponent<Ship>();
            }
            Controllable componentInParent = array[i].collider.transform.GetComponentInParent<Controllable>();
            if (componentInParent && componentInParent)
            {
                float num2 = Vector3.Angle(cam.transform.forward, componentInParent.transform.position - cam.transform.position) + Vector3.Distance(cam.transform.position, componentInParent.transform.position) * 5f;
                if (num2 < num)
                {
                    num = num2;
                    controllable = componentInParent;
                }
            }
        }
        if (!builder.isBuilding && controllable)
        {
            __instance.isInteracting = true;
            Scrollable component = controllable.GetComponent<Scrollable>();
            Clickable component2 = controllable.GetComponent<Clickable>();
            if (component2 && Input.GetKeyDown(KeyCode.F))
            {
                component2.Click();
            }
            if (component)
            {
                component.ChangeValue(Input.GetAxis("Mouse ScrollWheel") * 0.25f);
                string extra = "   ";
                if (controllable.GetComponentInChildren<ControllableRotate>())
                {
                    extra = ((int)(controllable.GetComponentInChildren<ControllableRotate>().transform.localEulerAngles.z)).ToString();
                    extra = new string('0', 3 - extra.Length) + extra;
                }
                crosshairManager.ChangeText($"{controllable.CurrentValueTarget.ToString()}<br>          {extra}<br>{controllable.CurrentValue.ToString()}");
            }
        }
        else
        {
            __instance.isInteracting = false;
            crosshairManager.ChangeText("");
        }
        return;
    }
}

public class CrosshairManager : MonoBehaviour
{
    GameObject crosshair;

    public void Start()
    {
        crosshair = GameObject.Find("CrossHair");
    }

    public void ChangeText(string newText)
    {
        UIPatches.crosshairText.text = newText;
        // crosshair.SetActive(newText.Length == 0);
    }
}