using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;

namespace CactusDamage
{
	[Label("Cactus Damage Global Config")]
	public class CactusConfig : ModConfig
	{
		public class Damage
		{
			[Label("Damage")]
			[Range(0, int.MaxValue)]
			public int damage = 7;

			[Label("Random damage")]
			[DefaultValue(true)]
			public bool enabled;

			private int max = 8;
			private int min = 6;

			[Label("Max value of random damage")]
			[Range(0, int.MaxValue)]
			public int Max { get => max; set => max = value < Min ? Min : value; }

			[Label("Min value of random damage")]
			[Range(0, int.MaxValue)]
			public int Min { get => min; set => min = value > Max ? Max : value; }

			public override string ToString() => $"{(enabled ? $"random between {Min} and {Max}" : $"{damage}")}";
		}

		[Label("Amount of damage cacti deal")]
		public Damage damage = new Damage();

		[Label("Cacti deal knockback")]
		[DefaultValue(true)]
		public bool knockback;

		public override ConfigScope Mode => ConfigScope.ServerSide;

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			if (!IsPlayerServerHost(Main.player[whoAmI]))
			{
				message = "You are not the server host, and thus can't change global configs.";
				return false;
			}
			else
				return true;
		}

		public static bool IsPlayerServerHost(Player player)
		{
			//iriterates through every player
			for (int plr = 0; plr < Main.maxPlayers; plr++)
			{
				//checks if the player is the one provided in the parameters and check if the player its iriterating though is the local host. checking for the state is an edge case when there are situations, maybe, where the Player check is incorrect, while joining the world
				if (Netplay.Clients[plr].State == 10 && plr == player.whoAmI && Netplay.Clients[plr].Socket.GetRemoteAddress().IsLocalHost())
					return true;
			}

			return false;
		}

	}
}