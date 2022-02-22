using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Wearwolf : NonPlayerCharacter {
    public Wearwolf([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments)
      : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    public override bool GetDamaged(int diceRoll, float damage, int weaponId, WeaponType type, bool isSpell = false) {
      if ((type & WeaponType.SilverDagger) == WeaponType.SilverDagger && diceRoll >= 8) {
        damage += DEADLY_DAMAGE;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}