using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Necrophage : NonPlayerCharacter {
    private const int KillStake = 5;

    public override bool GetDamaged(int diceRoll, float damage, int weaponId, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if (weaponId == GameRule.EquipmentIdConstants.EquipmentIdConstants.STAKE && diceRoll >= KillStake) {
        damage += 10000f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }

    public Necrophage([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}