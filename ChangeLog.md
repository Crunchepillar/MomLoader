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
