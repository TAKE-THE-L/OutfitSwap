using BepInEx;
using GorillaTagScripts;
using UnityEngine;
using static GorillaNetworking.CosmeticsController;

namespace OutfitSwap;

[BepInPlugin(Constants.Guid, Constants.ModName, Constants.Version)]
public class Plugin : BaseUnityPlugin
{
    private bool changed;

    private void Update()
    {
        var IsPressed = ControllerInputPoller.instance.rightControllerSecondary2DAxisClick;

        if (IsPressed)
        {
            if (!changed)
            {
                Wear(
                    (SelectedOutfit + 1) >= (SubscriptionManager.IsLocalSubscribed() ? 10 : 4) // just checks if you have vim and gets the outfit cap based on that 
                        ? 0
                        : SelectedOutfit + 1
                );

                changed = true;
            }
        else
        {
            changed = false;
        }
        
    }

    private static void Wear(int outfitIndex)
    {
        GorillaTagger.Instance.StartVibration(false, 0.08f, 0.1f);
        instance.LoadSavedOutfit(outfitIndex);
    }
}
