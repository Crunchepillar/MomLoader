using System;
using System.Collections.Generic;

namespace UseableItems
{
    public static class UseableBaseItem
    {
        public static void CopyItem(BaseItem toItem, BaseItem fromItem)
        {
            toItem.itemArchetipus = "BaseItemArchetipus";
            toItem.inventoryContext = fromItem.inventoryContext;
            toItem.itemType = fromItem.itemType;
            toItem.itemId = fromItem.itemId;
            toItem.itemSpriteName = fromItem.itemSpriteName;
            toItem.rootItemPath = fromItem.rootItemPath;
            toItem.maxStackable = fromItem.maxStackable;
            toItem.itemSprite = fromItem.itemSprite;
            toItem.itemProxySpriteName = fromItem.itemProxySpriteName;
            toItem.itemEffectOnEquip = fromItem.itemEffectOnEquip;
            toItem.itemsLeftOnUse = fromItem.itemsLeftOnUse;
            toItem.itemEffectOnUse = fromItem.itemEffectOnUse;
            toItem.itemExpiration = fromItem.itemExpiration;
            toItem.effectModifiers = fromItem.effectModifiers;
            toItem.achievementsOnCraft = fromItem.achievementsOnCraft;
            toItem.achievementsOnUse = fromItem.achievementsOnUse;
            toItem.achievementsOnSteal = fromItem.achievementsOnSteal;
            toItem.achievementsOnBuy = fromItem.achievementsOnBuy;
            toItem.creationTime = fromItem.creationTime;
            toItem.itemName = fromItem.itemName;
            toItem.itemDescription = fromItem.itemDescription;
            toItem.itemsContainedInItemInventory = fromItem.itemsContainedInItemInventory;
            toItem.itemInteractionDistance = fromItem.itemInteractionDistance;
            toItem.quality = fromItem.quality;

            if (toItem.extras != null)
            {
                fromItem.extras.CopyTo(toItem.extras, 0);
            }
        }

        public static BaseItem ConvertToUseable(BaseItem item)
        {
            Dictionary<string, Type> itemTable = new Dictionary<string, Type>
            {
                { "ContainerItem", typeof(UseableContainerItem) },
                { "CleanItem", typeof(UseableCleaningItem) },
                { "DrugItem", typeof(UseableDrugItem) },
                { "FoodItem", typeof(UseableFoodItem) },
                { "MedicItem", typeof(UseableMedicItem)},
                { "WalletItem", typeof(UseableWalletItem) }
            };

            if (itemTable.ContainsKey(item.itemType))
            {
                BaseItem returnItem = (BaseItem)Activator.CreateInstance(itemTable[item.itemType], item);

                //Process inventory of containers inside inventory
                if (returnItem.itemType == "ContainerItem")
                {
                    //Maybe check if the inventory has any items in it at all to save overhead
                    UseableContainerItem newBox = (UseableContainerItem)returnItem;
                    ConvertInventory(newBox.inventory);
                }

                return returnItem;
            }

            return item;
        }

        public static void ConvertInventory(BaseInventory inventory)
        {
            //Replaces each item in each slot with a useable version
            for (int i = 0; i < inventory.slots.Count; i++)
            {
                for (int itemNo = 0; itemNo < inventory.slots[i].items.Count; itemNo++)
                {
                    BaseItem item = inventory.slots[i].items[itemNo];
                    inventory.slots[i].items[itemNo] = ConvertToUseable(item);
                }
            }
        }

        public static bool IsItemUseable(BaseInventory inventoryContext)
        {
            //Never auto-use from the ground
            if (inventoryContext == null)
                return false;

            //List of Contexts where it is not okay to auto-use
            List<int> invalidContexts = new List<int>
            {
                BaseInventory.INVENTORY_TYPE_BUYIER,
                BaseInventory.INVENTORY_TYPE_CRAFTING,
                BaseInventory.INVENTORY_TYPE_NONE,
                BaseInventory.INVENTORY_TYPE_NPC,
                BaseInventory.INVENTORY_TYPE_SHOP
            };

            if (invalidContexts.Contains(inventoryContext.inventoryType))
            {
                return false;
            }

            //Other selection may happen here

            return true;
        }

        //Obsolete as heck
        /*public static bool IsItemUseableOld(BaseInventory inventoryContext, bool inContainer, bool inFurniture)
        {
            //Never auto-use if the item is on the ground
            if (inventoryContext == null)
                return false;

            //Inside player inventory
            if (inventoryContext.inventoryType == BaseInventory.INVENTORY_TYPE_PLAYER)
                return true;

            //Just Inside furniture
            if (inventoryContext.inventoryType == BaseInventory.INVENTORY_TYPE_CONTAINER && inFurniture)
                return true;

            //If we are allowed to be inside a container whether in or out of the player inventory
            if (inContainer && inventoryContext.inventoryType == BaseInventory.INVENTORY_TYPE_ITEM_CONTAINER)
            {
                ContainerItemInventory container = inventoryContext as ContainerItemInventory;

                //If we are in a container itself inside the player inventory or a furniture and are allowed to be there
                if (container.parentContainerItem.inventoryContext.inventoryType == BaseInventory.INVENTORY_TYPE_PLAYER)
                    return true;
                if (container.parentContainerItem.inventoryContext.inventoryType == BaseInventory.INVENTORY_TYPE_CONTAINER && inFurniture)
                    return true;
            }

            return false;
        }*/
    }
}
