using UnityEngine;

namespace MamaMod
{
    public class ModShopsManager : ShopsManager
    {
        public ModShopsManager()
        {
            //Grab the list from the other shop
            shops = instance.shops;

            //Take over all shop manager work
            instance = this;
        }

        public override string ToString()
        {
            return "MamaMod.ModsShopManager";
        }

        public override Shop GetShop(string shopId)
        {
            if (shops.ContainsKey(shopId))
            {
                //Only return standard shops as ModShops
                if (shops[shopId].shopType == "Shop")
                    return new ModShop().InitShop(shops[shopId]);
                else
                    return shops[shopId].Clone();
            }
            Debug.LogWarning($"ModShopsManager.GetShop called with invalid shopId {shopId}, {shops.Count} records searched");
            return null;
        }
    }

}
