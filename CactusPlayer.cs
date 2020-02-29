using GoodProLib.GData;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CactusDamage
{
	public class CactusPlayer : ModPlayer
	{
		public override void PostUpdate()
		{
			if (PlayerData.IsPlayeIntersectingTile(player, TileID.Cactus))
			{
				player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " got spiked to death by a cactus. Ouch!"),

					ModContent.GetInstance<CactusConfig>().damage.enabled
					? Main.rand.Next(ModContent.GetInstance<CactusConfig>().damage.Min, ModContent.GetInstance<CactusConfig>().damage.Max)
					: ModContent.GetInstance<CactusConfig>().damage.damage,

					ModContent.GetInstance<CactusConfig>().knockback
					? (int)PlayerData.GetPlayerDirection(player)
					: 0);
			}
		}
	}
}