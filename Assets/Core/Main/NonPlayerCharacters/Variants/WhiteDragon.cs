using Core.Main.GameRule.Impact;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class WhiteDragon : NonPlayerCharacter {
    private const int MAX_NUMBER_OF_DRAGON_BREATH = 3;
    private int _numberOfDragonBreath;

    public WhiteDragon([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes,
      [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    protected override void ToDamagedOfImpact(int diceRoll, ImpactOnRiskPoints character, int indexOfImpact) {
      if (_numberOfDragonBreath == MAX_NUMBER_OF_DRAGON_BREATH) {
        return;
      }

      if (_npcCombatAttributes.CanUseImpact(diceRoll, indexOfImpact)) {
        _npcCombatAttributes.UseImpact(character, indexOfImpact);
        _numberOfDragonBreath++;
      }
    }
  }
}