using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Wearwolf : NonPlayerCharacter {
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, WeaponType type = WeaponType.None, bool isSpell = false) {
      if ((type & WeaponType.SilverDagger) == WeaponType.SilverDagger && diceRoll >= 8) {
        damage += DeadlyDamage;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }

    public Wearwolf([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}