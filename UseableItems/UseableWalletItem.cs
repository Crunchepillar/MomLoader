using UnityEngine;

namespace UseableItems
{
    public class UseableWalletItem : WalletItem
    {
        public UseableWalletItem(BaseItem item)
        {
            //Generic copy
            UseableBaseItem.CopyItem(this, item);

            //Wallet copy
            WalletItem wallet = item as WalletItem;
            maxMoneyContained = wallet.maxMoneyContained;
            minMoneyContained = wallet.minMoneyContained;

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
