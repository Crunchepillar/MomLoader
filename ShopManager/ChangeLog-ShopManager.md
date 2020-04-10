# ShopManager Plugin ChangeLog


## ShopManager Version 0.0.1
* Getting caught stealing closes the shop window
* Shops won't open the shop window for players with caught_stealing_effect
* Robbing a shop with a crowbar no longer calls ShopInventory.DropAllItemsToTheGround.
* Robbed items go directly into the player's inventory if there is a free slot, otherwise to the ground near the player
    * This function disabled for newest steam update
* Shops of type 'buyier' can't be robbed. It was a new player trap because they are always empty
* Shops that can never be robbed warn the player instead of asking for a non-existant item