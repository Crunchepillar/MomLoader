# Project Overview:
MomLoader is an assembly loader for the game Hard Times on steam. It patches some functions in the main Assembly-CSharp to add in an event-driven framework that can dynamically load assemblies from the mod directory without interfering with the existing data-driven mod structure.

Note: MomLoader was written before I learned a number of best practices. The code here isn't really even good for educational purposes. A noteable example would be that I exposed an empty class to be the ModMain class rather than an interface which would work MUCH better. A second example is how the main AssemblyCsharp.dll needed to be replaced with one compiled to work with MomLoader rather than patched in with something like harmony to keep it more compatible.

https://github.com/Crunchepillar/MomLoader

# Installation

# Warning: MomLoader is extremely out of date. Do not use.

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

# MomLoader Latest:

* Added AssemblyName field to ModInfo which contains the name of the loaded assembly without the ".dll"
* Added function GetModLoaded to get if a specific mod is loaded
    * Pass the AssemblyName of a Mod returns the Mod's ModInfo or null
* Added functions to Remove a mod's registration from any of the update types
