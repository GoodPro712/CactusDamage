using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using GoodProLib.GData;

namespace CactusDamage
{
	public class CactusDamage : Mod
	{
	}

	public class CDPlayer : ModPlayer
	{
		public override void PostUpdate()
		{
			if (PlayerData.IsPlayeIntersectingTile(PlayerData.LPlayer, TileID.Cactus))
				player.Hurt(PlayerDeathReason.ByCustomReason(" got spiked to death by a cactus. Ouch!"), Main.rand.Next(6, 8), PlayerData.GetDirection(PlayerData.LPlayer));
		}
	}
}