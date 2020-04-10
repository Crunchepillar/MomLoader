// ModMain.cs
// By: mims
// On: 4/10/2020

using MomLoader;

namespace WorkerDemo
{
    public class ModMain : ModContainer
    {
        public ModMain() : base("Worker Demo", "Lovely Mama", "0.0.1")
        {
            //Register as a worker on all items, shops and Civs
            QTLoader.RegisterWorker<BaseItem>(this);
            QTLoader.RegisterWorker<Shop>(this);
            QTLoader.RegisterWorker<BaseNpcManager>(this);
        }

        public override void PerformWorkerUpdate(object workItem)
        {

            if (workItem is BaseItem)
            {
                //Funny Meme
                ((BaseItem)workItem).itemName = "Funny Meme!";
                return;
            }

            if (workItem is Shop)
            {
                //Very Funny Meme
                Shop shop = (Shop)workItem;


                for (int i=0; i < shop.shoppables.Length; i++)
                {
                    shop.shoppables[i].itemId = "Eggs";
                    shop.shoppables[i].itemType = "FoodItem";
                    shop.shoppables[i].minQuality = 4;
                    shop.shoppables[i].maxQuality = 4;
                    shop.shoppables[i].quantity = 999;
                    shop.shoppables[i].price = 0f;
                }

                return;
            }

            if (workItem is BaseNpcManager)
            {
                //FUNNIEST MEME
                ((BaseNpcManager)workItem).figure.ChangeSex();
            }

            Utils.Log($"Possibly unexpected type in work order {workItem.GetType()}", "Warning");
        }
    }
}
