using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace StackTweaksNS
{
    public class StackTweaks : Mod
    {
        public override void Ready()
        {
            PatchMarket();
            Logger.Log("Stack Tweaks Ready!");
        }

        void PatchMarket()
        {
            Harmony.Patch(typeof(Market).GetMethod("UpdateCard", BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance), new HarmonyMethod(typeof(InjectedPatches), nameof(InjectedPatches.MarketUpdateCardPatch)));
        }

        public override object Call(object[] args)
        {
            return base.Call(args);
        }

    }
}