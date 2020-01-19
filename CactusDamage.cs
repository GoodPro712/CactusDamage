using GoodProLib.GData;
using System.ComponentModel;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace CactusDamage
{
	public class CactusDamage : Mod
	{
		public override void Unload()
		{
			CDPlayer.deathReason = null;
		}
	}

	public class CDPlayer : ModPlayer
	{
		public static string deathReason = " got spiked to death by a cactus. Ouch!";

		public override void PostUpdate()
		{
			if (PlayerData.IsPlayeIntersectingTile(player, TileID.Cactus))
			{
				player.Hurt(PlayerDeathReason.ByCustomReason(deathReason),

					ModContent.GetInstance<CDConfig>().damage.enabled
					? Main.rand.Next(ModContent.GetInstance<CDConfig>().damage.min, ModContent.GetInstance<CDConfig>().damage.max)
					: ModContent.GetInstance<CDConfig>().damage.value,

					ModContent.GetInstance<CDConfig>().knockback
					? PlayerData.GetDirection(player)
					: 0);
			}
		}
	}

	[Label("Cactus Damage Global Config")]
	public class CDConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Label("Cacti deal knockback")]
		[DefaultValue(true)]
		public bool knockback;

		[Label("Amount of damage cacti deal")]
		public Damage damage = new Damage();

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			if (PlayerData.IsPlayerServerOwner(PlayerData.LPlayer))
				return true;
			else
			{
				message = "You are not the server host, and thus can't change global configs.";
				return false;
			}
		}
	}

	[Label("Damage")]
	public class Damage
	{
		[Label("Random damage")]
		[DefaultValue(true)]
		public bool enabled;

		[Label("Damage")]
		public int value = 7;

		[Label("Min value of random damage")]
		public int min = 6;

		[Label("Max value of random damage")]
		public int max = 8;

		public override string ToString() => $"{(enabled ? $"random between {min} and {max}" : $"{value}")}";
	}
}