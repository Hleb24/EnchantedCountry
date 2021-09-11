using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;
using static Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor.Armor;
using static Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon.Weapon;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.TrurlsShop {

	public class CharacterIn : MonoBehaviour {
		#region FIELDS
		// [Inject]
		protected ClassOfCharacterData classOfCharacterData;
		[SerializeField]
		protected CharacterType _characterType;
		[SerializeField]
		protected bool _testCharacterType;
		[SerializeField]
		protected bool _useGameSave;
		protected List<(CharacterType, ArmorType)> _armorKits = new List<(CharacterType, ArmorType)>() {
		 (CharacterType.Warrior, ArmorType.WarriorArmorKit),
		 (CharacterType.Elf, ArmorType.ElfArmorKit),
		 (CharacterType.Wizard, ArmorType.WizardArmorKit),
		 (CharacterType.Kron, ArmorType.KronArmorKit),
		(CharacterType.Gnom, ArmorType.GnomArmorKit),
	};

		protected List<(CharacterType, WeaponType)> _weaponKits = new List<(CharacterType, WeaponType)>() {
		 (CharacterType.Warrior, WeaponType.WarriorWeaponKit),
		 (CharacterType.Elf, WeaponType.ElfWeaponKit),
		 (CharacterType.Wizard, WeaponType.WizardWeaponKit),
		 (CharacterType.Kron, WeaponType.KronWeaponKit),
		(CharacterType.Gnom, WeaponType.GnomWeaponKit),
	};
		public event Action GetCharacterType;
		#endregion
		#region MONBEHAVIOUR_METHODS
		protected virtual void Start() {
			TestCharacterType();
			LoadCharacterTypeWithInvoke();
		}
		#endregion
		#region LOAD_AND_TRY_SET_CHARACTER_TYPE
		private void LoadCharacterTypeWithInvoke() {
			if (_testCharacterType)
				return;
			if (_useGameSave) {
				classOfCharacterData = GSSSingleton.Instance;
				Invoke(nameof(TrySetCharacterType), 0.3f);
			} else {
				SaveSystem.LoadWithInvoke(classOfCharacterData, SaveSystem.Constants.ClassOfCharacter,
				(nameInvoke, time) => Invoke(nameInvoke, time), nameof(TrySetCharacterType), 0.3f);
			}
		}

		private void TestCharacterType() {
			if (_testCharacterType) {
				GetCharacterType?.Invoke();
			}
		}

		private void TrySetCharacterType() {
			if (Enum.TryParse(classOfCharacterData.nameOfClass, out CharacterType characterType)) {
				_characterType = characterType;
			}
			GetCharacterType?.Invoke();
		}
		#endregion
		#region GET_KITS
		public ArmorType GetArmorKit() {
			for (int i = 0; i < _armorKits.Count; i++) {
				if (_armorKits[i].Item1 == _characterType) {
					return _armorKits[i].Item2;
				}
			}
			return ArmorType.None;
		}

		public WeaponType GetWeaponKit() {
			for (int i = 0; i < _weaponKits.Count; i++) {
				if (_weaponKits[i].Item1 == _characterType) {
					return _weaponKits[i].Item2;
				}
			}
			return WeaponType.None;
		}
		#endregion
		#region PROPERTIES
		public CharacterType CharacterType {
			get {
				return _characterType;
			}
		}
		#endregion
	}
}
