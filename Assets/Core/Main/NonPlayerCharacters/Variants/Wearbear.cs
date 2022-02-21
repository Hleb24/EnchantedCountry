using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Wearbear : NonPlayerCharacter {
    private const float AdditionalDamageFromSilverDagger = 3.0f;
    public override bool GetDamaged(int diceRoll, float damage, int weaponId = 100, WeaponType type = WeaponType.None, bool isSpell = false) {
      if (Weapon.Is(WeaponType.SilverDagger, type)) {
        damage += AdditionalDamageFromSilverDagger;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }

    public Wearbear([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}