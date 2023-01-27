using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace TimedTorchesStayLit.Patches
{
    internal class TimerPatch
    {

        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.IsBurning))]
        class FireplaceIsBurning_Patch
        {
            static void Postfix(Fireplace __instance, ref bool __result, ref GameObject ___m_enabledObject, ref ZNetView ___m_nview)
            {
                //check if should use fuel
                if (Configs.ConfigChecks(__instance.name))
                {
                    if ((int)Math.Ceiling(__instance.GetComponent<ZNetView>().GetZDO().GetFloat("fuel")) == 0 && Configs.configAllowAddingFuel.Value)

                    {
                        __result = false;
                        return;
                    }

                    // Calculate if the torch should currently be lit
                    bool shouldBeLit = false;
                    float dayFraction = (float)typeof(EnvMan).GetField("m_smoothDayFraction", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(EnvMan.instance);
                    if (Configs.timerOffFloatTime == Configs.timerOnFloatTime)
                    {
                        shouldBeLit = true;
                    }
                    else if (Configs.timerOffFloatTime < Configs.timerOnFloatTime)
                    {
                        if ((dayFraction >= Configs.timerOnFloatTime && dayFraction <= 1f) || dayFraction <= Configs.timerOffFloatTime)
                        {
                            shouldBeLit = true;
                        }
                    }
                    else if (dayFraction >= Configs.timerOnFloatTime && dayFraction <= Configs.timerOffFloatTime)
                    {
                        shouldBeLit = true;
                    }
                    //check for dark environment
                    EnvSetup currentEnvironment = EnvMan.instance.GetCurrentEnvironment();
                    bool isAlwaysDarkBiome = currentEnvironment != null && currentEnvironment.m_alwaysDark;

                    if (shouldBeLit || (isAlwaysDarkBiome && Configs.configAlwaysOnInDarkBiomes.Value))
                    {
                        __result = true;
                    }
                    else
                    {
                        __result = false;
                    }



                }
            }
        }




        //[HarmonyPatch(typeof(Fireplace), nameof(Fireplace.IsBurning))]
        //internal class FireplaceIsBurning_Patch
        //{
        //    private static void Postfix(Fireplace __instance, ref bool __result,
        //             ref GameObject ___m_enabledObjectHigh, ref ZNetView ___m_nview)
        //    {
        //        float dayFraction = (float)typeof(EnvMan).GetField("m_smoothDayFraction",
        //                 BindingFlags.Instance | BindingFlags.NonPublic).GetValue(EnvMan.instance);

        //        //make the torch / campfire go out if blocked.
                

        //        bool shouldBeLit = true;
        //        __result = true;

        //        //checks the fuel level and if fuel is configured.  If so and there is no fuel it turns off the torch

        //        if ((int)Math.Ceiling(__instance.GetComponent<ZNetView>().GetZDO().GetFloat("fuel")) == 0 && !Configs.ConfigChecks(__instance.name))

        //        {
        //            __result = false;
        //            return;
        //        }

        //        if (__instance.m_blocked)
        //        {
        //            __result = false;
        //            return;
        //        }

        //        if (Configs.ConfigChecks(__instance.name))
        //        {
                   
        //            EnvSetup currentEnvironment = EnvMan.instance.GetCurrentEnvironment();
        //            bool isAlwaysDarkBiome = currentEnvironment != null && currentEnvironment.m_alwaysDark;
        //            if (Configs.timerOffFloatTime > Configs.timerOnFloatTime)
        //            {
                       

        //                if ((dayFraction <= Configs.timerOnFloatTime && dayFraction >= 1f) || dayFraction >= Configs.timerOffFloatTime)
        //                {
        //                    if (!shouldBeLit || (isAlwaysDarkBiome && Configs.configAlwaysOnInDarkBiomes.Value))
        //                    {
        //                        __result = true;
        //                        return;
        //                    }
        //                    shouldBeLit = false;
        //                    __result = false;
        //                }
        //            }
        //            else if (dayFraction <= Configs.timerOnFloatTime && dayFraction >= Configs.timerOffFloatTime)
        //            {
                        

        //                if (!shouldBeLit || (isAlwaysDarkBiome && Configs.configAlwaysOnInDarkBiomes.Value))
        //                {
        //                    __result = true;
        //                    return;
        //                }
        //                shouldBeLit = false;
        //                __result = false;
        //            }
        //        }
        //    }
        //}
    }
}