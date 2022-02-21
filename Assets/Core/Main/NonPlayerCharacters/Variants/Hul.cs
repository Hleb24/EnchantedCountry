using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Hul : NonPlayerCharacter {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId, WeaponType type, bool isSpell = false) {
      if ((type & WeaponType.LongSword) == WeaponType.LongSword || (type & WeaponType.ShortSword) == WeaponType.ShortSword ||
          (type & WeaponType.TwoHandedSword) == WeaponType.TwoHandedSword) {
        return false;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }

    public Hul([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}