using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Cyclops : NonPlayerCharacter {
    private bool _isBlind;

    public Cyclops([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) :
      base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    public override bool GetDamaged(int diceRoll, float damage, int weaponId, WeaponType type, bool isSpell = false) {
      if ((type & WeaponType.LongBow) == WeaponType.LongBow && diceRoll >= 16 || (type & WeaponType.ShortBow) == WeaponType.ShortBow && diceRoll >= 16) {
        _isBlind = true;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }

    public override int Accuracy(int index = 0) {
      var accuracy = 0;
      if (_npcEquipments.IsHasWeapon()) {
        accuracy = _npcEquipments.GetAccuracy();
        if (_isBlind) {
          accuracy -= 2;
        }
      }

      return accuracy;
    }
  }
}