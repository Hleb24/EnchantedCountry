using System;
using System.Collections.Generic;
using Core.Main.GameRule;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.NonPlayerCharacters {
  public class NpcEquipments {
    private readonly ArmorClass _armorClass;
    private readonly WeaponSet _weaponSet;

    public NpcEquipments([NotNull] WeaponSet weaponSet, [NotNull] ArmorClass armorClass) {
      Assert.IsNotNull(weaponSet, nameof(weaponSet));
      Assert.IsNotNull(armorClass, nameof(armorClass));
      _weaponSet = weaponSet;
      _armorClass = armorClass;
    }

    public bool IsHit(int hit) {
      return _armorClass.IsHit(hit);
    }

    public bool IsKillOnlySpell() {
      return _armorClass.isKillOnlySpell;
    }
      
    public bool IsHasWeapon() {
      return _weaponSet.HasWeapon();
    }

    public float ToDamage(int weaponIndex) {
      return _weaponSet.ToDamage(weaponIndex);

        
    }
      
    public int GetAccuracy(int weaponIndex = 0) {
      return _weaponSet.GetAccuracy(weaponIndex);
    }

    public int GetNumberOfWeapon() {
      return _weaponSet.GetNumberOfWeapon();
    }

    public ArmorClass GetArmorClass() {
      return _armorClass;
    }

    public int GetClassOfArmor() {
      return _armorClass.ClassOfArmor;
    }
  }
  
  public class NpcEquipmentsModel {
    public NpcEquipmentsModel([NotNull] IEnumerable<int> weaponsIdList,int classOfArmor) {
      ClassOfArmor = classOfArmor;
      WeaponsIdList = weaponsIdList;
    }

    public int ClassOfArmor { get; }
    public IEnumerable<int> WeaponsIdList { get; }
  }
}