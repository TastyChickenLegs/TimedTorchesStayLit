using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimedTorchesStayLit.Patches
{
    internal class InterfacePatch
    {
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.Awake))]

        //checks to see if items use fuel and configures the interface accordingly
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.GetHoverText))]
        class FireplaceGetHoverText_Patch
        {
            static void Postfix(Fireplace __instance, ref string __result, ref ZNetView ___m_nview, ref string ___m_name)
            {

                if  ((Configs.ConfigChecks(__instance.name)) && !Configs.configAllowAddingFuel.Value)
                {
                    __result = Localization.instance.Localize(___m_name + "\n <color=yellow>No Fuel Required</color>" + "\n Timed Torches Mod");
                }//"\n[<color=yellow><b>1-8</b></color>] Use Item");
            }
        }
        //if allow adding fuel show the interface with fuel left
        //fixed interface bug for fuel showing 0.6.4
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.Interact))]
        class FireplaceInteract_Patch
        {
            static bool Prefix(Fireplace __instance, ref bool __result)
            {

                if  (Configs.ConfigChecks(__instance.name))
                    {
                    if (!Configs.configAllowAddingFuel.Value)
                    {
                        __result = false;
                        return false;

                    }
                    else
                    {
                        __result = true;
                        return true;
                    }


                }
                __result = true;
                return true;
            }
        }
    }
}
