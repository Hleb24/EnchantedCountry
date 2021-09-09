using System;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor;
using UnityEngine;

namespace Core.EnchantedCountry.ScriptableObject.ArmorObject {
	[Serializable]
	[CreateAssetMenu(fileName = "New Armor", menuName = "Armor", order = 53)]
	public class ArmorObject : UnityEngine.ScriptableObject {
		#region Fields
		public Armor.ArmorType armorType = Armor.ArmorType.None;
		public string armorName = "";
		public int classOfArmor;
		public string effectName = "";
		public Armor armor;
		public int id;
		#endregion
		#region Methods
		public void OnEnable() {
			InitArmor();
		}

		public void OnValidate() {
			InitArmor();
		}

		public Armor InitArmor() {
			if (armor == null) {
				armor = new Armor(armorName, classOfArmor, armorType, effectName);
			} else {
				armor.Init(armorName, classOfArmor, armorType, effectName);
			}

			return armor;
		}
		#endregion
	}
}

