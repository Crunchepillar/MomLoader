// ModMain.cs
// By: mims
// On: 4/9/2020
using MomLoader;

namespace ShopManager
{
    public class ModMain : ModContainer
    {
        public ModMain() : base("Shop Manager", "Lovely Mama", "0.0.1")
        {
            QTLoader.RegisterShopOverrideService(this);
        }

        public override Shop ShopOverride(Shop shop)
        {
            ModShop newShop = new ModShop();
            newShop.InitShop(shop);

            return newShop;
        }
    }
}
