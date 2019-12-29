using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CactusDamage
{
	public class CactusDamage : Mod
	{
		public CactusDamage()
		{
		}
	}

	public class CDPlayer : ModPlayer
	{
		public override void PostUpdate()
		{
			if (player.IsPlayerTouchingTile(TileID.Cactus))
			{
				player.Hurt(PlayerDeathReason.ByCustomReason(" got spiked to death by a cactus. Ouch!"), Main.rand.Next(6, 8), player.direction);
			}
		}
	}

	internal static class Extensions
	{
		//Thanks Corinna
		public static bool IsPlayerTouchingTile(this Player P, int TileID)
		{
			Point16 curTilePos = P.position.ToTileCoordinates16();
			for (int curX = curTilePos.X; curX < curTilePos.X + 3; curX++)
			{
				for (int curY = curTilePos.Y; curY < curTilePos.Y + 4; curY++)
				{
					Rectangle tileRect = new Rectangle(curX * 16, curY * 16, 16, 16);
					if (P.getRect().Intersects(tileRect) && Main.tile[curX, curY].type == TileID)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}