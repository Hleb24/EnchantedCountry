using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.NonPlayerCharacters {
  public class NpcCombatAttributesModel {
    public NpcCombatAttributesModel([NotNull] List<int> impacts, int defaultRiskPoints, int lifeDice, bool attackWithAllWeapons, bool deadlyAttack, bool isImmortal) {
      Assert.IsNotNull(impacts, nameof(impacts));
      Impacts = impacts;
      DefaultRiskPoints = defaultRiskPoints;
      LifeDice = lifeDice;
      AttackWithAllWeapons = attackWithAllWeapons;
      DeadlyAttack = deadlyAttack;
      IsImmortal = isImmortal;
    }

    public List<int> Impacts { get; }

    public int DefaultRiskPoints { get; }
    public int LifeDice { get; }

    public bool AttackWithAllWeapons { get; }

    public bool DeadlyAttack { get; }

    public bool IsImmortal { get; }
  }
}