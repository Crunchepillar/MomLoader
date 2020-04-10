# Project Overview:
MomLog is a modloader for the game Hard Times on steam. It patches some functions in the main Assembly-CSharp to add in an event-driven modding framework that can dynamically load assemblies from the mod directory without interfering with the existing data-driven mod structure.

# Installation

Installing MomLoader is a quick process.

Grab a release from the releases page and then follow these short instructions

1. Right click on Hard Times in your steam library and then manage -> Local files. From there navigate to HardTimes_Data and then Managed.
2. Copy the contents of the MomLoader\Managed folder into here. It will ask to overwrite Assembly-CSharp.dll. This is to add the event system into the base game and load MomLoader.dll when the game starts. You could backup your old DLL in case you ever decide to uninstall MomLoader or you could tell steam to verify the game cache to retrieve the old one for you.
3. Now launch Hard Times and click the Mods button on the main menu. Click the button to open the mods folder and then create a new folder named Assemblies. For posix users the casing of the letters is important but windows users likely don't need to mind the capitol A.
4. Finally copy the DLLs you want to load from the release into your Assemblies folder. You should be familiar with what each of the DLLs in this folder does and if you are not read the different changelogs in the github repo.

Okay you're done. Close Hard Times and relaunch to let MomLoader do her thing. You should see a different version number displayed in the top left. Instead of saying "Hard Times v x.x.x: Build xxxxx" it will say "Hard Times v x.x.x+QT-x.x.x" to indicate the mod has been loaded.

# F.A.Q

Nothing here :[

# MomLoader ChangeLog

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
