using System;
using System.Collections.Generic;
using Core.Rule.Character;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using static Core.Rule.GameRule.Armor;
using static Core.Rule.GameRule.Weapon;

namespace Core.Mono.Scenes.TrurlsShop {
  public class CharacterIn : MonoBehaviour {
    private readonly List<(ClassType, ArmorType)> _armorKits = new List<(ClassType, ArmorType)> {
      (ClassType.Warrior, ArmorType.WarriorArmorKit),
      (ClassType.Elf, ArmorType.ElfArmorKit),
      (ClassType.Wizard, ArmorType.WizardArmorKit),
      (ClassType.Kron, ArmorType.KronArmorKit),
      (ClassType.Gnom, ArmorType.GnomArmorKit)
    };
    public event Action GetCharacterType;
    [SerializeField]
    protected ClassType _classType;
    [SerializeField]
    protected bool _testCharacterType;
    [SerializeField]
    protected bool _useGameSave;

    private List<(ClassType, WeaponType)> _weaponKits = new List<(ClassType, WeaponType)> {
      (ClassType.Warrior, WeaponType.WarriorWeaponKit),
      (ClassType.Elf, WeaponType.ElfWeaponKit),
      (ClassType.Wizard, WeaponType.WizardWeaponKit),
      (ClassType.Kron, WeaponType.KronWeaponKit),
      (ClassType.Gnom, WeaponType.GnomWeaponKit)
    };

    private IClassType _type;

    protected virtual void Start() {
      TestCharacterType();
      LoadCharacterTypeWithInvoke();
    }

    public ArmorType GetArmorKit() {
      for (var i = 0; i < _armorKits.Count; i++) {
        if (_armorKits[i].Item1 == _classType) {
          return _armorKits[i].Item2;
        }
      }

      return ArmorType.None;
    }

    public WeaponType GetWeaponKit() {
      for (var i = 0; i < _weaponKits.Count; i++) {
        if (_weaponKits[i].Item1 == _classType) {
          return _weaponKits[i].Item2;
        }
      }

      return WeaponType.None;
    }

    private void LoadCharacterTypeWithInvoke() {
      if (_testCharacterType) {
        return;
      }

      if (_useGameSave) {
        _type = ScribeDealer.Peek<ClassTypeScribe>();
        Invoke(nameof(TrySetCharacterType), 0.3f);
      } else {
        // SaveSystem.LoadWithInvoke(_type, SaveSystem.Constants.ClassOfCharacter, (nameInvoke, time) => Invoke(nameInvoke, time), nameof(TrySetCharacterType), 0.3f);
      }
    }

    private void TestCharacterType() {
      if (_testCharacterType) {
        GetCharacterType?.Invoke();
      }
    }

    private void TrySetCharacterType() {
      _classType = _type.GetClassType();
      GetCharacterType?.Invoke();
    }

    public ClassType ClassType {
      get {
        return _classType;
      }
    }
  }
}