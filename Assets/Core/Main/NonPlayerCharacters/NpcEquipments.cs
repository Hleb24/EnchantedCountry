using System;
using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters {
  public class NpcEquipments {
    private readonly ArmorClass _armorClass;
    private readonly WeaponSet _weaponSet;

    public NpcEquipments([NotNull] NpcEquipmentsModel model) {
      _armorClass = model.ArmorClass ?? throw new ArgumentNullException(nameof(model.ArmorClass));
      _weaponSet = model.WeaponSet ?? throw new ArgumentNullException(nameof(model.WeaponSet));
    }

    public bool IsHit(int hit) {
      return _armorClass.IsHit(hit);
    }

    public bool IsKillOnlySpell() {
      return _armorClass.isKillOnlySpell;
    }
      
    public bool HasWeapon() {
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
    
    
  }
  
  public class NpcEquipmentsModel {
    public NpcEquipmentsModel([NotNull] ArmorClass armorClass,[NotNull] WeaponSet weaponSet) {
      ArmorClass = armorClass ?? throw new ArgumentNullException(nameof(armorClass));
      WeaponSet = weaponSet ?? throw new ArgumentNullException(nameof(weaponSet));
    }

    public ArmorClass ArmorClass { get; }
    public WeaponSet WeaponSet { get; }
  }
}