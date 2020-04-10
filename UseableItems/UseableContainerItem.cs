using UnityEngine;

namespace UseableItems
{
    public class UseableContainerItem : ContainerItem
    {
        public UseableContainerItem(BaseItem item)
        {
            //Copy Generic stuff first
            UseableBaseItem.CopyItem(this, item);

            ContainerItem copyFrom = item as ContainerItem;

            //Container specific data
            inventoryCapacity = copyFrom.inventoryCapacity;
            inventory = copyFrom.inventory;
            containerInventoryTypeFilter = copyFrom.containerInventoryTypeFilter;
            singleItemAllowed = copyFrom.singleItemAllowed;
    }

        public override void OpenItemPanel(PlayerManager playerManager)
        {
            //This override will let the player use Food items by clicking on them in the inventory

            if (UseableBaseItem.IsItemUseable(inventoryContext))
            {
                UseItem(playerManager);
                return;
            }

            base.OpenItemPanel(playerManager);
        }
    }
}
