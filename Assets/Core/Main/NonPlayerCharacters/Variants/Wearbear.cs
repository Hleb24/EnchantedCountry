using Core.Main.GameRule;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Wearbear : NonPlayerCharacter {
    private const float ADDITIONAL_DAMAGE_FROM_SILVER_DAGGER = 3.0f;

    public Wearbear([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments)
      : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    public override bool GetDamaged(int diceRoll, float damage, int weaponId, WeaponType type, bool isSpell = false) {
      if (Weapon.Is(WeaponType.SilverDagger, type)) {
        damage += ADDITIONAL_DAMAGE_FROM_SILVER_DAGGER;
      }

      return base.GetDamaged(diceRoll, damage, weaponId, type, isSpell);
    }
  }
}