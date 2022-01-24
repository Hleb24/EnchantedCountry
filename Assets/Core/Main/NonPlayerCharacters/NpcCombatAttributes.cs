using System;
using System.Collections.Generic;
using Core.Main.GameRule.Impact;
using Core.Main.GameRule.Points;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters {
  public class NpcCombatAttributes {
    private readonly List<Impact<ImpactOnRiskPoints>> _impacts;
    private readonly RiskPoints _riskPoints;
    private readonly bool _attacksEveryoneAtOnce;
    private int _numberOfAttack;
    private readonly bool _deadlyAttack;

    public NpcCombatAttributes([NotNull] NpcCombatAttributesModel model) {
      _impacts = model.Impacts ?? throw new ArgumentNullException(nameof(model.Impacts));
      _riskPoints = model.RiskPoints ?? throw new ArgumentNullException(nameof(model.RiskPoints));
      _attacksEveryoneAtOnce = model.AttacksEveryoneAtOnce;
      _numberOfAttack = model.NumberOfAttack;
      _deadlyAttack = model.DeadlyAttack;
    }

    public void GetDamaged(float damage) {
      _riskPoints.ChangeRiskPoints(-damage);
    }

    public void Heal(float healPoints) {
      _riskPoints.ChangeRiskPoints(healPoints);
    }

    public void NeutralImpact(float points) {
      _riskPoints.SetRiskPoints(points);
    }

    public bool IsDead() {
      return _riskPoints.IsDead();
    }

    public int GetNumberOfAttack() {
      return _numberOfAttack;
    }

    public void SetNumberOfAttack(int numberOfAttack) {
      _numberOfAttack = numberOfAttack;
    }

    public bool IsAttackEveryAtOne() {
      return _attacksEveryoneAtOnce;
    }

    public bool IsDeadlyAttack() {
      return _deadlyAttack;
    }

    public bool HasImpacts() {
      return _impacts.Count > 0;
    }

    public int GetNumberOfImpacts() {
      return _impacts.Count;
    }

    public bool CanUseImpact(int diceRollValue, int impactIndex) {
      return diceRollValue >= _impacts[impactIndex].DiceRollValueForInvokeImpact;
    }

    public void UseImpact(ImpactOnRiskPoints target, int impactIndex) {
      _impacts[impactIndex].ImpactAction(target);
    }

    public RiskPoints GetRiskPoints() {
      return _riskPoints;
    }
  }

  public class NpcCombatAttributesModel {
    public NpcCombatAttributesModel([NotNull] List<Impact<ImpactOnRiskPoints>> impacts, [NotNull] RiskPoints riskPoints, bool attacksEveryoneAtOnce, int numberOfAttack,
      bool deadlyAttack) {
      Impacts = impacts ?? throw new ArgumentNullException(nameof(impacts));
      RiskPoints = riskPoints ?? throw new ArgumentNullException(nameof(riskPoints));
      AttacksEveryoneAtOnce = attacksEveryoneAtOnce;
      NumberOfAttack = numberOfAttack >= 0 ? numberOfAttack : throw new ArgumentException(nameof(numberOfAttack));
      DeadlyAttack = deadlyAttack;
    }

    public List<Impact<ImpactOnRiskPoints>> Impacts { get; }

    public RiskPoints RiskPoints { get; }

    public bool AttacksEveryoneAtOnce { get; }

    public int NumberOfAttack { get; }

    public bool DeadlyAttack { get; }
  }
}