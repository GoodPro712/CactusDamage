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
	}

	public class CDPlayer : ModPlayer
	{

		public override void PostUpdate()
		{
			if (PlayerData.IsPlayeIntersectingTile(player, TileID.Cactus))
			{
				player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " got spiked to death by a cactus. Ouch!"),

					ModContent.GetInstance<CDConfig>().damage.enabled
					? Main.rand.Next(ModContent.GetInstance<CDConfig>().damage.Min, ModContent.GetInstance<CDConfig>().damage.Max)
					: ModContent.GetInstance<CDConfig>().damage.damage,

					ModContent.GetInstance<CDConfig>().knockback
					? (int)PlayerData.GetPlayerDirection(player)
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
			if (PlayerData.IsPlayerServerHost(PlayerData.LPlayer, out _))
				return true;
			else
			{
				message = "You are not the server host, and thus can't change global configs.";
				return false;
			}
		}
	}

	public class Damage
	{
		[Label("Random damage")]
		[DefaultValue(true)]
		public bool enabled;

		[Label("Damage")]
		[Range(0, int.MaxValue)]
		public int damage = 7;

		private int min = 6;

		[Label("Min value of random damage")]
		[Range(0, int.MaxValue)]
		public int Min
		{
			get
			{
				return min;
			}
			set
			{
				if (value > Max)
				{
					min = Max;
				}
				else
				{
					min = value;
				}
			}
		}

		private int max = 8;

		[Label("Max value of random damage")]
		[Range(0, int.MaxValue)]
		public int Max
		{
			get
			{
				return max;
			}
			set
			{
				if (value < Min)
				{
					max = Min;
				}
				else
				{
					max = value;
				}
			}
		}

		public override string ToString() => $"{(enabled ? $"random between {Min} and {Max}" : $"{damage}")}";
	}
}