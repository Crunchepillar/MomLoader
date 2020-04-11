// Main.cs
// By: mims
// On: 4/9/2020

using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;

namespace MomLoader
{

    public static class QTLoader
    {
        //***** All Mods List *****//
        private static List<ModContainer> AllMods = new List<ModContainer>();

        //Used to lock out unsafe functions during constructors
        private static bool SafeUpdate;

        /// <summary>
        /// Returns the modinfo string for a loaded mod, assuming it is loaded.
        /// returns null if the mod isn't loaded. Do not use this function in
        /// a mod constructor. Restrict checking for mods until after SafeSetup
        /// is called.
        /// </summary>
        /// <returns>ModInfo container</returns>
        /// <param name="assemblyName">Case-Sensitive name of the dll</param>
        public static ModContainer.ModInfo GetModLoaded(string assemblyName)
        {

            if (!SafeUpdate)
            {
                Utils.Log($"Unsafe call to GetModLoaded", "Warning");
                return null;
            }

            foreach (ModContainer mod in AllMods)
            {
                if (!(mod.GetModInfo.AssemblyName == assemblyName))
                    continue;

                return mod.GetModInfo;
            }

            return null;
        }

        //***** Begin Mod Registration *****//
        private static List<ModContainer> UpdateOnTick = new List<ModContainer>();
        private static List<ModContainer> UpdateOnNewGame = new List<ModContainer>();
        private static List<ModContainer> UpdateOnPlayerSpawn = new List<ModContainer>();

        public static void RegisterForTickUpdate(ModContainer mod) { UpdateOnTick.Add(mod); }
        public static void RegisterForNewGameUpdate(ModContainer mod) { UpdateOnNewGame.Add(mod); }
        public static void RegisterForPlayerSpawnUpdate(ModContainer mod) { UpdateOnPlayerSpawn.Add(mod); }

        public static void RemoveFromTickUpdate(ModContainer mod) { UpdateOnTick.Remove(mod); }
        public static void RemoveFromNewGameUpdate(ModContainer mod) { UpdateOnNewGame.Remove(mod); }
        public static void RemoveFromPlayerSpawnUpdate(ModContainer mod) { UpdateOnPlayerSpawn.Remove(mod); }


        //***** Begin Override Services *****//
        private static Dictionary<string, ModContainer> OverrideServices = new Dictionary<string, ModContainer>();

        private static void RegisterOverrideService(string service, ModContainer mod)
        {
            if (OverrideServices.ContainsKey(service))
                Utils.Log($"Competing services of type {service} from {mod.GetModInfo.ModName} and {OverrideServices[service].GetModInfo.ModName}");

            OverrideServices[service] = mod;
        }

        public static void RegisterShopOverrideService(ModContainer mod) => RegisterOverrideService("shops", mod);
        public static void RegisterItemOverrideService(ModContainer mod) => RegisterOverrideService("items", mod);

        //***** End Override Services *****//

        //***** Begin Worker Registration *****//
        private static Dictionary<Type, List<ModContainer>> Workers = new Dictionary<Type, List<ModContainer>>
        {
            {typeof(BaseItem), new List<ModContainer>() },
            {typeof(Shop), new List<ModContainer>() },
            {typeof(BaseNpcManager), new List<ModContainer>() }
        };

        /// <summary>
        /// Registers a mod as a worker for the given <typeparamref name="workType"/>
        /// Creates a new worktype when passed a type not yet in the list.
        /// </summary>
        /// <param name="mod">Mod.</param>
        /// <typeparam name="workType">Exported from HardTimes: BaseItem, Shop, BaseNPCManager</typeparam>
        public static void RegisterWorker<workType>(ModContainer mod)
        {
            if (!Workers.ContainsKey(typeof(workType)))
            {
                Utils.Log($"New worktype {typeof(workType)} being sideloaded.", "Experimental");
                Workers.Add(typeof(workType), new List<ModContainer>());
            }

            List<ModContainer> typeWorkers = Workers[typeof(workType)];

            if (typeWorkers.Contains(mod))
            {
                Utils.Log($"{mod.GetModInfo.ModName} has registered as workerType {typeof(workType)} multiple times.");
                return;
            }

            Utils.Log($"Registered new worker {mod.GetModInfo.ModName}|{typeof(workType)}", "WorkerReg");

            typeWorkers.Add(mod);
        }

        //***** End Worker Registration *****//
        //***** End Mod Registration *****//

        public static string LoaderVersion
        {
            get => "0.0.4";
        }

        private static bool OneTimeSetup;

        /// <summary>
        /// Exported hook. Do not use.
        /// </summary>
        public static void SetupMomLoader()
        {
            Utils.Log("Setup MomLoader");

            if (OneTimeSetup)
                return;
                
            Utils.Log("Starting to load assemblies");

            //Get a list of all the Assemblies to load in mods/assemblies
            string fileDir = ModsManager.instance.ModsFolder + "/Assemblies";
            string[] filePaths = Directory.GetFiles(fileDir, "*.dll", SearchOption.TopDirectoryOnly);

            Utils.Log($"MomLoader loading Assemblies: {filePaths.Length}");

            OneTimeSetup = true;

            if (filePaths == null)
                return;

            if (filePaths.Length == 0)
                return;

            object ModInstance;
            Assembly ModAssembly;
            Type ModType;
            string typeName;

            foreach (string dll in filePaths)
            {
                Utils.Log($"Loading and instancing {dll}");
                ModAssembly = Assembly.LoadFile(dll);

                //Trim and retrieve typename from filename
                typeName = dll.Replace(fileDir, "");
                typeName = typeName.Substring(1);
                typeName = typeName.Remove(typeName.Length - 4) + ".ModMain";
                Utils.Log($"TypeName for this assembly: {typeName}");

                ModType = ModAssembly.GetType(typeName);

                if (ModType == null)
                {
                    Utils.Log("Skipping this DLL. No ModMain");
                    continue;
                }

                ModInstance = Activator.CreateInstance(ModType);

                if (ModInstance == null)
                    continue;

                //Update the ModList
                ((ModContainer)ModInstance).GetModInfo.AssemblyName = typeName.Replace(".ModMain", "");
                AllMods.Add((ModContainer)ModInstance);
            }

        }

        //***** Game Hooks *****//

        /// <summary>
        /// Exported hook. Do not use.
        /// </summary>
        public static void OnNewGameStart()
        {
            foreach (ModContainer mod in UpdateOnNewGame)
            {
                mod.ModUpdateNewGameStarted();
            }
        }

        /// <summary>
        /// Exported hook. Do not use.
        /// </summary>
        public static void OnHeartBeat()
        {
            foreach (ModContainer mod in UpdateOnTick)
            {
                mod.ModUpdateTick();
            }
        }

        /// <summary>
        /// Exported hook. Do not use.
        /// </summary>
        /// <param name="player">Player.</param>
        public static void OnPlayerSpawn(PlayerManager player)
        {
            foreach (ModContainer mod in UpdateOnPlayerSpawn)
            {
                mod.ModUpdatePlayerCreated(player);
            }

        }

        /// <summary>
        /// Exported hook. Do not use.
        /// </summary>
        public static void OnBaseModsDoneLoading()
        {
            //Unlock unsafe functions now that everything is loaded up
            SafeUpdate = true;

            foreach (ModContainer mod in AllMods)
            {
                mod.SafeSetup();
            }
        }

        //***** Services *****//

        private static bool ServiceAvailable(string service)
        {
            return (OverrideServices.ContainsKey(service) && OverrideServices[service] != null);
        }

        public static Shop OverrideShopResource(Shop shopResource)
        {
            if (ServiceAvailable("shops"))
            {
                //Query service for a new shop
                shopResource =  OverrideServices["shops"].ShopOverride(shopResource);
            }

            //Removed worker group. Added safer operation to ShopsManager.cs in AssemblyCSharp
            return shopResource;
        }

        public static BaseItem OverrideItemResource(BaseItem itemResource)
        {
            if (ServiceAvailable("items"))
            {
                //Query service for a new item
                itemResource = OverrideServices["items"].ItemOverride(itemResource);
            }

            //Safe to operate item workers here because we over-ride items at creation
            //Not in storage
            RunWorkerGroup(itemResource);

            return itemResource;
        }

        //***** Workers *****//

        /// <summary>
        /// Exported hook. Do not use.
        /// </summary>
        public static void RunWorkerGroup<workType>(workType workItem)
        {
            if (!Workers.ContainsKey(typeof(workType)))
                return;

            Utils.Log($"Running worker group for <{typeof(workType)}>", "Workers");

            List<ModContainer> typeWorkers = Workers[typeof(workType)];

            foreach(ModContainer worker in typeWorkers)
            {
                Utils.Log($"Handing off to {worker.GetModInfo.ModName}", "GroupWorker");
                worker.PerformWorkerUpdate(workItem);
            }
        }
    }
}
