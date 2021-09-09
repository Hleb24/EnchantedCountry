using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using UnityEngine;

namespace Core.EnchantedCountry.ScriptableObject.WeaponObjects {
	[Serializable]
	[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 52)]
	public class WeaponObject : UnityEngine.ScriptableObject {
		#region Fields
		public Weapon.WeaponType weaponType = Weapon.WeaponType.None;
		public string weaponName = "";
		public float minDamage;
		public float maxDamage;
		public List<float> damageList;
		public int accurancy;
		public string effectName = "";
		public Weapon weapon;
		public int id;
		#endregion
		#region Methods
		public void OnEnable() {
			InitWeapon();
		}

		public void OnValidate() {
			if (damageList.Count != 0 || damageList != null) {
				InitWeaponWithDamageList();
			} else {
				InitWeapon();
			}

			weaponName = name;
		}

		public Weapon InitWeapon() {
			if (weapon == null) {
				weapon = new Weapon(maxDamage, weaponType, weaponName, minDamage, accurancy, effectName, id);
			} else {
				weapon.Init(maxDamage, weaponType, weaponName, minDamage, accurancy, effectName, id);
			}

			return weapon;
		}
		
		public Weapon InitWeaponWithDamageList() {
			if (weapon == null) {
				weapon = new Weapon(maxDamage,weaponType, weaponName , minDamage, accurancy, effectName, id);
			} else {
				weapon.Init(damageList, weaponType, weaponName,accurancy, effectName, id);
			}
			return weapon;
		}
		#endregion
	}
}
