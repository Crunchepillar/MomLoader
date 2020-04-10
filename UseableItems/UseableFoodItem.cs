using UnityEngine;

namespace UseableItems
{
    public class UseableFoodItem : FoodItem
    {
        public UseableFoodItem(BaseItem item)
        {
            UseableBaseItem.CopyItem(this, item);
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
