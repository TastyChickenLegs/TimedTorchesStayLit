using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TimedTorchesStayLit.Patches
{
    

    internal class TimerPatch
    {
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.IsBurning))]
        internal class FireplaceIsBurning_Patch
        {
            private static void Postfix(Fireplace __instance, ref bool __result,
                     ref GameObject ___m_enabledObjectHigh, ref ZNetView ___m_nview)
            {
                float dayFraction = (float)typeof(EnvMan).GetField("m_smoothDayFraction",
                         BindingFlags.Instance | BindingFlags.NonPublic).GetValue(EnvMan.instance);

                bool shouldBeLit = true;
                __result = true;

                //checks the fuel level and if fuel is configured.  If so and there is no fuel it turns off the torch

                if ((int)Math.Ceiling(__instance.GetComponent<ZNetView>().GetZDO().GetFloat("fuel")) == 0 && !Configs.ConfigChecks(__instance.name))

                {
                    __result = false;
                    return;
                }

                if (Configs.ConfigChecks(__instance.name))
                {
                    //NoSmokeStayLit.TastyUtilsLogger.LogInfo(Configs.ConfigCheckTimerOne(__instance.name));
                    EnvSetup currentEnvironment = EnvMan.instance.GetCurrentEnvironment();
                    bool isAlwaysDarkBiome = currentEnvironment != null && currentEnvironment.m_alwaysDark;
                    if (Configs.timerOffFloatTime > Configs.timerOnFloatTime)
                    {
                        //NoSmokeStayLit.TastyUtilsLogger.LogInfo(NoSmokeStayLit.timerOffFloatTime);
                        //NoSmokeStayLit.TastyUtilsLogger.LogInfo(NoSmokeStayLit.timerOnFloatTime);

                        if ((dayFraction <= Configs.timerOnFloatTime && dayFraction >= 1f) || dayFraction >= Configs.timerOffFloatTime)
                        {

                            if (!shouldBeLit || (isAlwaysDarkBiome && Configs.configAlwaysOnInDarkBiomes.Value))
                            {
                                __result = true;
                                return;
                            }
                            shouldBeLit = false;
                            __result = false;
                        }
                    }
                    else if (dayFraction <= Configs.timerOnFloatTime && dayFraction >= Configs.timerOffFloatTime)
                    {
                        //NoSmokeStayLit.TastyUtilsLogger.LogInfo(dayFraction);

                        if (!shouldBeLit || (isAlwaysDarkBiome && Configs.configAlwaysOnInDarkBiomes.Value))
                        {
                            __result = true;
                            return;
                        }
                        shouldBeLit = false;
                        __result = false;
                    }


                }
            }
        }
    }
}
