// UseableMedicItem.cs
// By: mims
// On: 4/8/2020

namespace UseableItems
{
    public class UseableMedicItem : MedicItem
    {
        public UseableMedicItem(BaseItem item)
        {
            UseableBaseItem.CopyItem(this, item);

            MedicItem medic = item as MedicItem;
            isDisposable = medic.isDisposable;
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
