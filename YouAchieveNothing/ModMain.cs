// ModMain.cs
// By: mims
// On: 4/10/2020

using MomLoader;

namespace YouAchieveNothing
{
    public class ModMain : ModContainer
    {
        public ModMain() : base("You've Achieved Nothing", "Lovely Mama", "0.0.1")
        {
            QTLoader.RegisterForTickUpdate(this);
        }

        public override void ModUpdateTick()
        {
            //Get a handle the player character
            PlayerManager player = GameManager.instance.players[0];

            //Check if player exists
            if (player == null)
                return;

            //Clear achievements every heartbeat if needed
            if (player.achievementsManager.achievements.Count > 0)
            {
                //Debug Logging
                Utils.Log($"Clearing {player.achievementsManager.achievements.Count}");

                player.achievementsManager.achievements.Clear();
            }
        }
    }
}
