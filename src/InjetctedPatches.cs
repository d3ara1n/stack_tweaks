using System.Reflection;

namespace StackTweaksNS;

static class InjectedPatches
{
    internal static bool MarketUpdateCardPatch(Market __instance)
    {
        var marketStack = 0;
        var current = __instance as CardData;
        while (current.MyGameCard.HasParent)
        {
            marketStack++;
            current = current.MyGameCard.Parent.CardData;
        }
        if (__instance.MyGameCard.HasChild && __instance.MyGameCard.Child.CardData is not Market && WorldManager.instance.CardCanBeSold(__instance.MyGameCard.Child, false) && (!__instance.MyGameCard.HasParent || __instance.MyGameCard.Parent.CardData is HeavyFoundation || (__instance.MyGameCard.Parent.CardData is Market && marketStack < 12)))
            __instance.MyGameCard.StartTimer(60f / (marketStack + 1), new TimerAction(__instance.SellWithMarket), SokLoc.Translate("new_selling_card", LocParam.Create("card", __instance.MyGameCard.Child.CardData.FullName)), __instance.GetActionId("SellWithMarket"));
        else
            __instance.MyGameCard.CancelTimer(__instance.GetActionId("SellWithMarket"));
        // dirty call to base.UpdateCard()
        var baseUpdateCard = typeof(CardData).GetMethod(nameof(CardData.UpdateCard), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        var ptr = baseUpdateCard.MethodHandle.GetFunctionPointer();
        var updateCard = (Action)Activator.CreateInstance(typeof(Action), __instance, ptr);
        updateCard();
        return false;
    }
}