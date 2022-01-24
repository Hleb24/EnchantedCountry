using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Cyclops : NonPlayerCharacter {
    private bool _isBlind;
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if (((type & Weapon.WeaponType.LongBow) == Weapon.WeaponType.LongBow && diceRoll >= 16)
      ||((type & Weapon.WeaponType.ShortBow) == Weapon.WeaponType.ShortBow && diceRoll >= 16)
      ) {
        _isBlind = true;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
    
    public override int Accuracy(int index = 0) {
      int accuracy = 0;
      if (_npcEquipments.HasWeapon()) {
        accuracy = _npcEquipments.GetAccuracy();
        if (_isBlind) {
          accuracy -= 2;
        }
      }

      return accuracy;
    }

    public Cyclops([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}