// ModMain.cs
// By: mims
// On: 4/9/2020
using MomLoader;

namespace UseableItems
{
    public class ModMain : ModContainer
    {
        public ModMain() : base("Shop Manager", "Lovely Mama", "0.0.1")
        {

            QTLoader.RegisterItemOverrideService(this);

        }

        public override BaseItem ItemOverride(BaseItem item)
        {
            return UseableBaseItem.ConvertToUseable(item);
        }
    }
}
