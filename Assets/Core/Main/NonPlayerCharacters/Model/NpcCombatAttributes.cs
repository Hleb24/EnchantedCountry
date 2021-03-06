using System.Collections.Generic;
using Core.Main.GameRule.Impact;
using Core.Main.GameRule.Point;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.NonPlayerCharacters.Model {
  public class NpcCombatAttributes {
    private readonly List<Impact<IImpactOnRiskPoints>> _impacts;
    private readonly RiskPoints _riskPoints;
    private readonly bool _attackWithAllWeapons;
    private readonly bool _deadlyAttack;
    private readonly bool _isImmortal;
    private int _numberOfAttack;

    public NpcCombatAttributes([NotNull] List<Impact<IImpactOnRiskPoints>> impacts, [NotNull] RiskPoints riskPoints, bool attackWithAllWeapons, bool deadlyAttack,
      bool isImmortal) {
      Assert.IsNotNull(impacts, nameof(impacts));
      Assert.IsNotNull(riskPoints, nameof(riskPoints));
      _impacts = impacts;
      _riskPoints = riskPoints;
      _attackWithAllWeapons = attackWithAllWeapons;
      _deadlyAttack = deadlyAttack;
      _isImmortal = isImmortal;
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

    public bool IsAttackWithAllWeapons() {
      return _attackWithAllWeapons;
    }

    public bool IsDeadlyAttack() {
      return _deadlyAttack;
    }

    public bool IsHasImpact() {
      return _impacts.Count > 0;
    }

    public int GetNumberOfImpacts() {
      return _impacts.Count;
    }

    public bool CanUseImpact(int diceRollValue, int impactIndex) {
      return diceRollValue >= _impacts[impactIndex].GetDiceRollValueForInvokeImpact();
    }

    public void UseImpact(IImpactOnRiskPoints target, int impactIndex) {
      _impacts[impactIndex].ImpactAction(target);
    }

    public float GetPointsOfRisk() {
      return _riskPoints.GetPoints();
    }

    public bool IsImmortal() {
      return _isImmortal;
    }
  }
}