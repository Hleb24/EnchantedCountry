using System.Collections.Generic;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.EquipmentIdConstants {
	public static class EquipmentIdConstants {
		public static List<int> Bags = new List<int> { Bag, BigBag };
		public static List<int> Animals = new List<int> { Mule, Horse, WarHorse };
		public static List<int> Carriages = new List<int> { Carriage };
		public const int Pockets = 10;
		public const int NoArmorId = 100;
		public const int Bag = 600;
		public const int BigBag = 610;
		public const int Mule = 660;
		public const int Horse = 680;
		public const int Carriage = 690;
		public const int WarHorse = 710;

		public const int SilverDagger = 250;
		public const int Stake = 620;
	}
}
