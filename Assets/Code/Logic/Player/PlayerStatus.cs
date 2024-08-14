using System;
using System.Data.Common;
using System.Numerics;

namespace Logic.player
{
	public static class PlayerStatus
	{
		#region variables
		private static readonly object playerLocationLock = new object();
		private static readonly object levelLock = new object();
		private static readonly object healthLock = new object();
		private static readonly object equiptIDLock = new object();
		
		public static string PlayerName { get; set; }

        private static int health;
		private static int level;
		private static Godot.Vector2 playerLocation;
		#endregion

		#region Attributes
		#region Level Attributes
		public static PlayerAttribute agility = new PlayerAttribute("agility", 10);
		public static PlayerAttribute perception = new PlayerAttribute("perception", 10);
		public static PlayerAttribute endurance = new PlayerAttribute("endurance", 10);
		public static PlayerAttribute strength = new PlayerAttribute("strength", 10);
		public static PlayerAttribute intellegence = new PlayerAttribute("intellegence", 10);
		public static PlayerAttribute luck = new PlayerAttribute("luck", 10);
        private static int itemIdEquipt;

        #endregion
        #region Buff Attributes
        enum BuffType
		{
			Invisibility,
			TimeDialation,
			RunSpeed,
			Hacking,
		}
		#endregion
		#endregion

		public static void SetPlayerLocation(Godot.Vector2 location)
		{
			lock (playerLocationLock)
			{
				playerLocation = location;
			}
		}

		public static int GetHealth()
		{
			lock (playerLocationLock)
			{
				return health;
			}
		}
		public static void SetHealth(int value)
		{
			lock (playerLocationLock)
			{
				health = value;
			}
		}
		public static Godot.Vector2 PlayerLocation()
		{
			lock (playerLocationLock)
			{
				return playerLocation;
			}
		}
		public static void PlayerEquiptItem(int itemID)
		{
			lock(equiptIDLock){
				itemIdEquipt = itemID;
			}
		}
		public static int GetEquiptItemId()
		{
			lock(equiptIDLock){
				return itemIdEquipt;
			}
		}

		public static int Level()
		{
			lock (levelLock)
			{
				return level;
			}
		}


		public static void LevelUp()
		{
			lock(levelLock)
			{
				level++;
			}
		}


		public static void DamageTaken(int damage)
		{
			lock (healthLock)
			{
				health -= damage;
			}
		}


		public static void Heal(int heal)
		{
			lock (healthLock)
			{
				health += heal;
			}
		}
	}

	public class PlayerAttribute
	{
		public PlayerAttribute(string name, int maxLevel, int defaultLevel)
		{
			this.name = name;
			this.maxLevel = maxLevel;
			level = defaultLevel;
		}
		public PlayerAttribute(string name, int maxLevel)
		{
			this.name = name;
			this.maxLevel = maxLevel;
			level = 0;
		}
		public bool canSpend()
		{
			return this.maxLevel < level;
		}
		public void spend()
		{
			level++;
		}
		public int reset()
		{
			level = 0;
			return getLevel();
		}
		public int getMaxLevel()
		{
			return maxLevel;
		}
		public int getLevel()
		{
			return level;
		}
		private string name;
		private int maxLevel;
		private int level;
	}
}
