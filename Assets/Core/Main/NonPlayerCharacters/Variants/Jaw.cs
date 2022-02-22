using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Jaw : NonPlayerCharacter {
    public Jaw([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) :
      base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, WeaponType type = WeaponType.None, bool isSpell = false) {
      if ((type & WeaponType.Spear) == WeaponType.Spear) {
        damage += 2f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}