using UnityEngine;

namespace UseableItems
{
    public class UseableDrugItem : DrugItem
    {
        public UseableDrugItem(BaseItem item)
        {
            //Generic copy
            UseableBaseItem.CopyItem(this, item);

            DrugItem drug = item as DrugItem;
            smokable = drug.smokable;

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
