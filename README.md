# Project Overview:
MomLoader is an assembly loader for the game Hard Times on steam. It patches some functions in the main Assembly-CSharp to add in an event-driven framework that can dynamically load assemblies from the mod directory without interfering with the existing data-driven mod structure.

https://github.com/Crunchepillar/MomLoader

# Installation

Installing MomLoader is a quick process.

Grab a release from the releases page and then follow these short instructions

1. Right click on Hard Times in your steam library and then manage -> Local files. From there navigate to HardTimes_Data and then Managed.
2. Copy the contents of the MomLoader/Managed folder into here. It will ask to overwrite Assembly-CSharp.dll. This is to add the event system into the base game and load MomLoader.dll when the game starts. You could backup your old DLL in case you ever decide to uninstall MomLoader or you could tell steam to verify the game cache to retrieve the old one for you.
3. Now launch Hard Times and click the Mods button on the main menu. Click the button to open the mods folder and then create a new folder named Assemblies. For posix users the casing of the letters is important but windows users likely don't need to mind the capitol A.
4. Finally copy the DLLs you want to load from the release folder MomLoader/Assemblies into your Assemblies folder. You should be familiar with what each of the DLLs in this folder does and if you are not read the different changelogs that come with each one to decide which ones you want to load.

Okay you're done. Close Hard Times and relaunch to let MomLoader do her thing. You should see a different version number displayed in the top left. Instead of saying "Hard Times v x.x.x: Build xxxxx" it will say "Hard Times v x.x.x+QT-x.x.x" to indicate that MomLoade is active in memory.

# F.A.Q

Q: I actually don't like (Whatever Mod) How do I stop MomLoader from loading that specific file?

A: MomLoader doesn't search for DLLs recursively so you can just move them inside another folder in your Mod/Assemblies folder and it won't be loaded. If you're sure you won't want a specific DLL loaded ever again you can also just delete it.

# MomLoader ChangeLog

## MomLoader Version 0.0.4
* Added AssemblyName field to ModInfo which contains the name of the loaded assembly without the ".dll"
* Added function GetModLoaded to get if a specific mod is loaded
    * Pass the AssemblyName of a Mod returns the Mod's ModInfo or null
* Added functions to Remove a mod's registration from any of the update types

## MomLoader Version 0.0.3
* Added two new types of Events: Workers and Services
    * Services take over the creation of base classes (eg: BaseItem or Shop) to modify its properties or even return a totally new object
    * Workers modify any new instances of a base class but cannot replace it while working
    * Supported Services: BaseItem, Shop
    * Supported Workers: BaseItem, Shop, BaseNPCManager, BaseCopManager
        * Note: Mods can request to be Workers for any type and emit a request for Workers of any type

## MomLoader Version 0.0.2
* Loads all Assemblies in the Mods/Assemblies folder and activates a new instance of (dllName).ModMain
* Mods register for which updates they want to recieve from the base game and are passed data relevant to that update
    * UpdateOnNewGame Added - Updates when a new game is started either from "New Game" or "Load Game"
	* UpdateOnPlayerSpawn Added - Updates when a player is spawned and runs MonoBehavior:Start(), provides a PlayerManager instance to the player
    * UpdateOnTick Added - Updates every time the player calls CheckDay(), which is a Unity Invoke() run about every 6 seconds
