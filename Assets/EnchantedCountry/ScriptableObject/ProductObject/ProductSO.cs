using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using Core.EnchantedCountry.ScriptableObject.WeaponObjects;
using UnityEngine;
using static Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor.Armor;
using static Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon.Weapon;

namespace Core.EnchantedCountry.ScriptableObject.ProductObject {
	[CreateAssetMenu(fileName = "New Products", menuName = "Product", order = 54)]
	public class ProductSO : UnityEngine.ScriptableObject {
		#region Emums
		public enum ProductType {
			None,
			Weapon,
			Armor,
			Item
		}
		#endregion
		#region Fields
		public ProductType productType;
		public UnityEngine.ScriptableObject item;
		public string productName = "";
		public string description = "";
		public string Property;
		public int price;
		public int id;
		public Sprite icon;
		#endregion
		#region Methods
		public void OnValidate() {
			GetNameWithItem();
		}

		public void OnEnable() {
			GetNameWithItem();
		}

		private void GetNameWithItem() {
			IsWeaponObject();
			IsArmorObject();
		}

		private void IsWeaponObject() {
			if (item is WeaponObject w) {
				if (w.effectName != string.Empty) {
					string tempName = w.effectName + " " + w.weaponName;
					productName = tempName;
				} else {
					productName = w.weaponName;
				}
				Property = w.minDamage.ToString() + " - " + w.maxDamage.ToString();
				id = w.id;
			}
		}

		private void IsArmorObject() {
			if (item is ArmorObject.ArmorObject a) {
				if (a.effectName != string.Empty) {
					string tempName = a.effectName + " " + a.armorName;
					productName = tempName;
				} else {
					productName = a.armorName;
				}
				Property = a.classOfArmor.ToString();
				id = a.id;
			}
		}

		public WeaponType GetWeaponType() {
			WeaponObject weapon = item as WeaponObject;
			return weapon.weaponType;
		}

		public ArmorType GetArmorType() {
			ArmorObject.ArmorObject armor = item as ArmorObject.ArmorObject;
			return armor.armorType;
		}

		public Armor GetArmor() {
			ArmorObject.ArmorObject armorObject = item as ArmorObject.ArmorObject;
			Armor armor = armorObject.InitArmor();
			return armor;
		}
		
		public Weapon GetWeapon() {
			WeaponObject weaponObject = item as WeaponObject;
			Weapon weapon = weaponObject.InitWeapon();
			return weapon;
		}
		#endregion

		#region OPERATIONS
		public static implicit operator Armor(ProductSO productSo) {
			return productSo.GetArmor();
		}
		public static implicit operator Weapon(ProductSO productSo) {
			return productSo.GetWeapon();
		}
		#endregion
	}
}