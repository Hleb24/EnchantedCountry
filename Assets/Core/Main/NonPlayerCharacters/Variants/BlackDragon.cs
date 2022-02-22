using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class BlackDragon : NonPlayerCharacter {
    public BlackDragon([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes,
      [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    public override bool GetDamaged(int diceRoll, float damage, int weaponId, WeaponType type, bool isSpell = false) {
      if ((type & WeaponType.GoldDagger) == WeaponType.GoldDagger) {
        damage += 2f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}