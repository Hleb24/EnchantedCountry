using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Hul : NonPlayerCharacter {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if ((type & Weapon.WeaponType.LongSword) == Weapon.WeaponType.LongSword || (type & Weapon.WeaponType.ShortSword) == Weapon.WeaponType.ShortSword ||
          (type & Weapon.WeaponType.TwoHandedSword) == Weapon.WeaponType.TwoHandedSword) {
        return false;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }

    public Hul([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}