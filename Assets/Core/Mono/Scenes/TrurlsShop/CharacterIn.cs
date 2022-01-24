using System;
using System.Collections.Generic;
using Core.Main.Character;
using Core.Mono.MainManagers;
using Core.Support.Data;
using UnityEngine;
using Zenject;
using static Core.Main.GameRule.Armor;
using static Core.Main.GameRule.Weapon;

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
    protected ClassType _classTypeEnum;
    [SerializeField]
    protected bool _testCharacterType;
    protected IStartGame _startGame;
    private IClassType _classType;

    private List<(ClassType, WeaponType)> _weaponKits = new List<(ClassType, WeaponType)> {
      (ClassType.Warrior, WeaponType.WarriorWeaponKit),
      (ClassType.Elf, WeaponType.ElfWeaponKit),
      (ClassType.Wizard, WeaponType.WizardWeaponKit),
      (ClassType.Kron, WeaponType.KronWeaponKit),
      (ClassType.Gnom, WeaponType.GnomWeaponKit)
    };
    
    [Inject]
    public void Constructor(IStartGame startGame, IClassType classType) {
      _startGame = startGame;
      _classType = classType;
    }


    protected virtual void Start() {
      TestCharacterType();
      LoadCharacterTypeWithInvoke();
    }

    public ArmorType GetArmorKit() {
      for (var i = 0; i < _armorKits.Count; i++) {
        if (_armorKits[i].Item1 == _classTypeEnum) {
          return _armorKits[i].Item2;
        }
      }

      return ArmorType.None;
    }

    public WeaponType GetWeaponKit() {
      for (var i = 0; i < _weaponKits.Count; i++) {
        if (_weaponKits[i].Item1 == _classTypeEnum) {
          return _weaponKits[i].Item2;
        }
      }

      return WeaponType.None;
    }

    private void LoadCharacterTypeWithInvoke() {
      if (_testCharacterType) {
        return;
      }

      if (_startGame.UseGameSave()) {
        SetClassTypeEnum();
      } else {
        // SaveSystem.LoadWithInvoke(_type, SaveSystem.Constants.ClassOfCharacter, (nameInvoke, time) => Invoke(nameInvoke, time), nameof(TrySetCharacterType), 0.3f);
      }
    }

    private void TestCharacterType() {
      if (_testCharacterType) {
        GetCharacterType?.Invoke();
      }
    }

    private void SetClassTypeEnum() {
      _classTypeEnum = _classType.GetClassType();
      GetCharacterType?.Invoke();
    }

    public ClassType ClassTypeEnum {
      get {
        return _classTypeEnum;
      }
    }
  }
}