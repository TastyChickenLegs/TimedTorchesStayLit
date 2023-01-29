using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace TimedTorchesStayLit.Patches
{
    internal class TimerPatch
    {
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.IsBurning))]
        private class FireplaceIsBurning_Patch
        {
            private static void Postfix(Fireplace __instance, ref bool __result, ref GameObject ___m_enabledObject, ref ZNetView ___m_nview)
            {
                //check if should use fuel
                if (Configs.ConfigChecks(__instance.name))
                {
                    if ((int)Math.Ceiling(__instance.GetComponent<ZNetView>().GetZDO().GetFloat("fuel")) == 0 && !Configs.ConfigFuel(__instance.name))

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
                    //keep on in rain if configured
                    if (shouldBeLit && (Configs.fe_keepOnInRain.Value))
                    {
                        // shouldBeLit = true;
                        __instance.m_wet = false;
                        return;
                    }

                }

                //if set to always lit but not on timers
                //check for wet
                if ((Configs.ConfigFuel(__instance.name)) && (!Configs.ConfigChecks(__instance.name)))
                {
                    __result = true;

                    if (__result && Configs.fe_keepOnInRain.Value)
                    {
                        __instance.m_wet = false;
                    }

                    return;
                }
            }
        }
    }
}