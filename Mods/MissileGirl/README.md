# <a href="https://steamcommunity.com/sharedfiles/filedetails/?id=2479389928&searchtext=">RimWorld-MissileGirl</a>

## Description:

MissileGirl, a fork of RocketMan, is a RimWorld mod that is designed to improve RimWorld performance.

<b>Important note: MissileGirl should be the last mod in your mod list.</b>

## Features:

XML Caching to increase load speed of RimWorld.
Adaptive Stat Caching - learns how often a stat is called/updated and will cache approriately
Alert Throttling / Disabling - throttled to lower performance cost on alerts with option to disable all together
Several vanilla RimWorld optimizations

## Notes:

1. Currently The Multiplayer mod is untested.
2. Bug reports with no logs will not get a response.

MissileGirl is OpenSource and for anyone to use, modify, update.

## Credits:

* Current developer: ViralReaction
* Previous developer: Karim
* Extra Optimizations: Wiri
* The Thumbnail: Smxrez

DISCLAIMER: I’m not responsible in any way for damage done by MissileGirl to your saves.

### RocketRules (Compatibility system)

MissileGirl support a new rule system to avoid compatibility issues.
This works by placing `RocketRules.xml` files in `YourModFolder/Extras/RocketRules.xml`

### Request and notification system

```xml
<?xml version="1.0" encoding="utf-8" ?>
<RocketRules>
     <Notify type="PawnDirty" packageId="vr.MissileGirl" method="ThingWithComps:Notify_Equipped"/>
</RocketRules>
```

Your mod can notify MissileGirl to clear the statCache by calling a function in your code (preferably not empty due to
patching limitations). You can follow this format

* `packageId` is your mod `packageId`. This is used only to keep track of the current rules.
* `method` (formated as `YourClass:Method`) is the method that you call to notify MissileGirl that your mod need the
  cache cleared.

**Note** This works by applying a `Prefix` patch on the destination/provided method (in this case
`ThingWithComps:Notify_Equipped`) thus every time you call `ThingWithComps:Notify_Equipped` in this example the prefix
is executed and the cache is cleared.
and that prefix notify MissileGirl to clear the cache

**Note** The destination/provided method should have something in it otherwise patching it may not be possible

**Notification types**

* `PawnDirty` The target/provided method for this need to have `Pawn pawn` as a parameter.

**Note on notification types** For now there is only one which is `PawnDirty`. This system is the new way forword for
your mod to call MissileGirl regardless of the load order.

## Special Thanks goes to:

* Karim for creating original RocketMan  - Github Link: [Original RocketMan](https://github.com/kbatbouta/RimWorld-RocketMan)

You can always ask questions on the Dubwise discord server: https://discord.gg/yKBVZRrRr6
 
