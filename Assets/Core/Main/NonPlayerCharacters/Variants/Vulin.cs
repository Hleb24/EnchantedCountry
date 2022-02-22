using Core.Main.GameRule.Equipment;
using Core.Main.GameRule.Item;
using Core.Main.NonPlayerCharacters.Model;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Vulin : NonPlayerCharacter {
    private const int KILL_STAKE = 8;

    public Vulin([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) :
      base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    public override bool GetDamaged(int diceRoll, float damage, int weaponId, WeaponType type, bool isSpell = false) {
      if (weaponId == EquipmentIdConstants.STAKE && diceRoll >= KILL_STAKE) {
        damage += 10000f;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}