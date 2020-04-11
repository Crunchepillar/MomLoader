// ModContainer.cs
// By: mims
// On: 4/8/2020

namespace MomLoader
{

    public abstract class ModContainer
    {
        /// <summary>
        /// Contains all the mod meta info. Use QualifiedModeName to pretty print
        /// a mod's data to the log instead of typing it all out.
        /// </summary>
        public class ModInfo
        {
            public ModInfo(string name, string author, string version)
            {
                ModName = name;
                ModAuthor = author;
                ModVersion = version;
            }

            public readonly string ModName;
            public readonly string ModAuthor;
            public readonly string ModVersion;
            public string AssemblyName;
            public string QualifiedModName
            {
                get => $"{ModName} [{ModVersion}] by {ModAuthor}";
            }
        }

        public ModInfo GetModInfo;

        protected ModContainer(string name, string author, string version)
        {
            GetModInfo = new ModInfo(name, author, version);
        }

        /// <summary>
        /// This is called once every 6 seconds and controlled by player.checkday
        /// </summary>
        public virtual void ModUpdateTick()
        {

        }

        /// <summary>
        /// This is called when the player object is initialized but not awake.
        /// Some player components will be initialized once the player wakes up.
        /// Example: achievement manager will not yet have loaded any achievements
        /// </summary>
        /// <param name="player">PlayerManager</param>
        public virtual void ModUpdatePlayerCreated(PlayerManager player)
        {

        }

        /// <summary>
        /// This is called when a new game starts. This also includes loading a
        /// savegame.
        /// </summary>
        public virtual void ModUpdateNewGameStarted()
        {

        }

        /// <summary>
        /// This is called when all mods are loaded and we are at the main menu
        /// It should be safe to do most game-altering setup in this function
        /// </summary>
        public virtual void SafeSetup()
        {

        }

        /// <summary>
        /// This is called each time a new shop is added during the mod loading
        /// process. Note: Only one mod can override shops at a time. The player.log
        /// file will contain a warning if multiple mods try to register an override
        /// and only the most recently loaded mod will be called.
        /// </summary>
        /// <returns>Modified <paramref name="shop"/></returns>
        /// <param name="shop">A Shop object</param>
        public virtual Shop ShopOverride(Shop shop)
        {
            return shop;
        }

        /// <summary>
        /// This is called each time a new item is added during the mod loading
        /// process. Note: Only one mod can override shops at a time. The player.log
        /// file will contain a warning if multiple mods try to register an override
        /// and only the most recently loaded mod will be called.
        /// </summary>
        /// <returns>A Modified <paramref name="item"/></returns>
        /// <param name="item">A BaseItem object</param>
        public virtual BaseItem ItemOverride(BaseItem item)
        {
            return item;
        }

        /// <summary>
        /// This is called each time the worker group(s) the mod is subscribed
        /// to are activated. If subscribed to multiple groups make sure to use
        /// if (workItem is desiredItem) blocks to do work per item type
        /// </summary>
        /// <param name="workItem">Work Item as object</param>
        public virtual void PerformWorkerUpdate(object workItem)
        {
            Utils.Log($"Unfilled work order from {this.GetModInfo.ModName}", "WorkOrder");
        }
    }

}
