using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Vulin : NonPlayerCharacter {
    private const int KillStake = 8;
    public override bool GetDamaged(int diceRoll, float damage, int weaponId, WeaponType type = WeaponType.None, bool isSpell = false) {
      if (weaponId == GameRule.EquipmentIdConstants.EquipmentIdConstants.STAKE && diceRoll >= KillStake) {
        damage += 10000f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }

    public Vulin([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}