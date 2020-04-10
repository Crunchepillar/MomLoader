namespace ShopManager
{
    public class ModShop : Shop
    {

        new public ModShop InitShop(Shop argShop)
        {
            shopSprite = argShop.shopSprite;
            base.InitShop(argShop);

            return this;
        }

        public override void SecondaryAction(PlayerManager playerManager)
        {
            bool canDoSecondary = true;

            //Clarify which shops cannot be robbed
            if (effectNeededToRob == "cantrobmebaby" || shopType == "Buyier")
            {
                canDoSecondary = false;
                playerManager.uiManager.infoPanel.ShowPanel("This shop cannot be robbed.");
                SoundManager.instance.PlayErrorSound();
            }
            //Stop robbing multiple shops at once
            //Will be obsoleted in HardTimes 0.1.4r3+
            else if (playerManager.GetComponent<PlayerEffectsManager>().GetPlayerEffect("robbery_effect", removeIt: false) != null)
            {
                canDoSecondary = false;
                playerManager.uiManager.infoPanel.ShowPanel("You are already robbing a shop.");
                SoundManager.instance.PlayErrorSound();
            }

            if (canDoSecondary)
                base.SecondaryAction(playerManager);

        }

        //Obsolete in HardTimes 0.1.4r2
        /*public override void RobShop(PlayerManager playerManage)
        {
            //Drop fewer items and suffer reduced chance for each item from the third onward
            lastRobbed = TimeManager.instance.AbsoluteMinutes;

            //Temp Vars
            int NumberOfRobbedItems = 0;
            int FreeItems = ModMain.Config.ShopConfig.StolenItemsMin;
            int BaseChance = ModMain.Config.ShopConfig.StolenItemBaseChance;
            int StepChance = ModMain.Config.ShopConfig.StolenItemChanceStep;
            PlayerManager player = GameManager.instance.players[0];
            System.Random RNG = new System.Random();

            foreach (BaseItem item in MyShopInventory.ReturnAllItemsAsList())
            {
                if ((NumberOfRobbedItems < FreeItems) || RNG.Next(NumberOfRobbedItems * StepChance + BaseChance) < 1)
                {
                    //We are stealing the item in question so remove it
                    MyShopInventory.DestroyItem(item);
                    NumberOfRobbedItems++;

                    //Player has room in inventory
                    if (player.inventory.HasFreeSlots())
                    {
                        ModMain.Log($"Player stealing into inventory {item.itemId}");
                        player.inventory.AddItem(item);
                        continue;
                    }

                    //No room so we gotta place it on the floor
                    //Do this near the player instead of the shop to stop items bugging into the walls
                    ModMain.Log($"Player stealing onto ground {item.itemType}/{item.itemName}");
                    ItemsProvider.instance.SpawnNewItem(item.itemType, item.itemId, player.transform.position);
                    continue;

                }
            }

            SoundManager.instance.PlaySound("inventory");
        }*/

        public override void OpenShopPanel(PlayerManager playerManager)
        {
            bool canDoShop = true;

            //Control weather or not the shop will open to the player
            //Should be a list of if .. elseif in order of need to show the player
            if (playerManager.GetComponent<PlayerEffectsManager>().GetPlayerEffect("caught_stealing_effect", removeIt: false) != null)
            {
                canDoShop = false;
                playerManager.uiManager.infoPanel.ShowPanel($"You were recently caught stealing!");
                SoundManager.instance.PlayErrorSound();
            }

            if (canDoShop)
            {
                base.OpenShopPanel(playerManager);
            }
        }

        public override void StealItem(BaseItem itemToSteal, PlayerManager playerManager)
        {
            base.StealItem(itemToSteal, playerManager);

            //Getting caught stealing boots you from the shop instead of leaving the panel open
            if (playerManager.GetComponent<PlayerEffectsManager>().GetPlayerEffect("caught_stealing_effect", removeIt: false) != null)
            {
                DisposeShopPanel();
                SoundManager.instance.PlayErrorSound();
            }

        }
    }
}