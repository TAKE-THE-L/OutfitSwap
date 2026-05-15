using BepInEx;
using GorillaTagScripts;
using UnityEngine;
using static GorillaNetworking.CosmeticsController;

namespace OutfitSwap;

[BepInPlugin(Constants.Guid, Constants.ModName, Constants.Version)]
public class Plugin : BaseUnityPlugin
{
    private float LastPress;

    private void Update()
    {
        if (ControllerInputPoller.instance.rightControllerPrimaryButton)
        {
            if (Time.time - LastPress < 0.3f)
            {
                Wear(
                    (SelectedOutfit + 1) >= (SubscriptionManager.IsLocalSubscribed() ? 10 : 4) // just checks if you have vim and gets the outfit cap based on that 
                        ? 0
                        : SelectedOutfit + 1
                );
            }
            LastPress = Time.time;
        }
    }

    private static void Wear(int outfitIndex)
    {
        GorillaTagger.Instance.StartVibration(false, 0.08f, 0.1f);
        instance.LoadSavedOutfit(outfitIndex);
    }
}