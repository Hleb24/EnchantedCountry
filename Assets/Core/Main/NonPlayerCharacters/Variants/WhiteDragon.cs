using Core.Main.GameRule.Impact;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class WhiteDragon : NonPlayerCharacter {
    private const int MaxNumberOfDragonBreath = 3;
    private int _numberOfDragonBreath;
    
    public override void ToDamagedOfImpact(int diceRoll, ImpactOnRiskPoints character, int indexOfImpact) {
      if (_numberOfDragonBreath == MaxNumberOfDragonBreath) {
        return;
      }
      if (_npcCombatAttributes.CanUseImpact(diceRoll, indexOfImpact)) {
        _npcCombatAttributes.UseImpact(character, indexOfImpact);
        _numberOfDragonBreath++;
      }
    }

    public WhiteDragon([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) : base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }
  }
}