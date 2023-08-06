using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace StackTweaksNS
{
    public class StackTweaks : Mod
    {
        public void Awake(){
            Harmony.PatchAll();
        }

        public override void Ready()
        {
            Logger.Log("Stack Tweaks Ready!");
        }

        public override object Call(object[] args)
        {
            return base.Call(args);
        }
    }
}
