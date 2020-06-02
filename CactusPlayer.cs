using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CactusDamage
{
	public class CactusPlayer : ModPlayer
	{
		public override void PostUpdate()
		{
			if (IsPlayerIntersectingTile(player, TileID.Cactus))
			{
				CactusConfig cactusConfig = ModContent.GetInstance<CactusConfig>();

				player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " got spiked to death by a cactus. Ouch!"), 
					cactusConfig.damage.enabled ? Main.rand.Next(cactusConfig.damage.Min, cactusConfig.damage.Max) : cactusConfig.damage.damage,
					cactusConfig.knockback ? player.direction : 0);
			}
		}

		public static bool IsPlayerIntersectingTile(Player player, int tileId)
		{
			if (Main.netMode == NetmodeID.Server || player.active == false || player == null)
				return false;

			Point16 curTilePos = player.position.ToTileCoordinates16();

			for (int curX = curTilePos.X; curX < curTilePos.X + 3; curX++)
			{
				for (int curY = curTilePos.Y; curY < curTilePos.Y + 4; curY++)
				{
					if (player.getRect().Intersects(GetTileRectangle(curX, curY)) && Main.tile[curX, curY].type == tileId)
						return true;
				}
			}

			return false;
		}

		public static Rectangle GetTileRectangle(int x, int y) => new Rectangle(x * 16, y * 16, 16, 16);
	}
}