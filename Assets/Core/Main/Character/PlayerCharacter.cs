using System;
using Aberrance.Extensions;
using Core.Main.Dice;
using Core.Main.GameRule;
using Core.Main.GameRule.Impact;
using Core.Support.Data;
using UnityEngine;

namespace Core.Main.Character {
  public enum ClassType {
    Human,
    Warrior,
    Elf,
    Wizard,
    Kron,
    Gnom
  }

  public class PlayerCharacter : ImpactOnRiskPoints, IInitiative {
    private readonly Qualities _qualities;
    private readonly ArmorClass _armorClassOfCharacter;
    private readonly Armor _shield;
    private readonly Weapon _rangeWeapon;
    private readonly Weapon _projectiles;
    public event Action<string> IsDead;
    public string Name = "Kell";
    private Level _level;
    private IGamePoints _gamePoints;
    private IWallet _wallet;
    private EquipmentsOfCharacter _equipmentsOfCharacter;
    private IEquipmentUsed _equipmentsUsed;

    public PlayerCharacter(Qualities qualities, ClassType classType, Level level, IGamePoints gamePoints, RiskPoints riskPoints, IWallet wallet,
      EquipmentsOfCharacter equipmentsOfCharacter, IEquipmentUsed equipmentsUsed, Armor armor, Armor shield, Weapon rangeWeapon, Weapon meleeWeapon, Weapon projectiles) {
      _qualities = qualities;
      ClassType = classType;
      _level = level;
      _gamePoints = gamePoints;
      RiskPoints = riskPoints;
      _wallet = wallet;
      _equipmentsOfCharacter = equipmentsOfCharacter;
      _equipmentsUsed = equipmentsUsed;
      Armor = armor;
      _shield = shield;
      MeleeWeapon = meleeWeapon;
      _rangeWeapon = rangeWeapon;
      _projectiles = projectiles;
      _armorClassOfCharacter = new ArmorClass(Armor.GetArmorClass() + _qualities.GetModifierOf(QualityType.Agility));
      if (_shield.NotNull()) {
        _armorClassOfCharacter = new ArmorClass(Armor.GetArmorClass() + _shield.GetArmorClass() + _qualities.GetModifierOf(QualityType.Agility));
      }
    }

    public int CompareTo(IInitiative other) {
      if (other.NotNull()) {
        return Initiative.CompareTo(other.Initiative);
      }

      return -1;
    }

    void ImpactOnRiskPoints.SetRiskPoints(ImpactType impactType, int points, int protectiveThrow) {
      int playerLuckRoll = KitOfDice.DicesKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].GetSumRollOfBoxDices();
      if (playerLuckRoll >= protectiveThrow) {
        return;
      }

      switch (impactType) {
        case ImpactType.Poison:
          RiskPoints.ChangeRiskPoints(-points);
          Debug.Log($"<color=magenta>Яд</color>: {points} урон(а), спасброскок - {protectiveThrow} и больше, бросок удачи - {playerLuckRoll}.");
          break;
        case ImpactType.Paralysis:
          RiskPoints.ChangeRiskPoints(-points);

          Debug.Log($"<color=magenta>Паралич</color>: {points} урон(а), спасброскок - {protectiveThrow} и больше, бросок удачи - {playerLuckRoll}.");
          break;
        case ImpactType.Petrification:
          RiskPoints.ChangeRiskPoints(-points);

          Debug.Log($"<color=magenta>Окаменение</color>: {points} урон(а), спасброскок - {protectiveThrow} и больше, бросок удачи - {playerLuckRoll}.");
          break;
        case ImpactType.Acid:
          RiskPoints.ChangeRiskPoints(-points);

          Debug.Log($"<color=magenta>Acid</color> {points} урон(а), спасброскок - {protectiveThrow} и больше, бросок удачи - {playerLuckRoll}.");
          break;
        case ImpactType.Gas:
          RiskPoints.ChangeRiskPoints(-points);

          Debug.Log($"<color=magenta>Газ</color>: {points} урон(а), спасброскок - {protectiveThrow} и больше, бросок удачи - {playerLuckRoll}.");
          break;
        case ImpactType.Fire:
          RiskPoints.ChangeRiskPoints(-points);

          Debug.Log($"<color=magenta>Огонь</color>: {points} урон(а), спасброскок - {protectiveThrow} и больше, бросок удачи - {playerLuckRoll}.");
          break;
        case ImpactType.DragonBreath:
          RiskPoints.ChangeRiskPoints(-points);
          Debug.Log($"<color=magenta>Дух дракона</color>: {points} урон(а), спасброскок - {protectiveThrow} и больше, бросок удачи - {playerLuckRoll}.");
          break;
      }
    }

    public int GetMeleeAccuracy() {
      if (MeleeWeapon.Null()) {
        return 0;
      }

      int meleeAccuracy = MeleeWeapon.GetAccuracy();
      return meleeAccuracy + _qualities.GetModifierOf(QualityType.Strength);
    }

    public int GetRangeAccuracy() {
      if (_rangeWeapon.Null()) {
        return 0;
      }

      int rangeAccuracy = _rangeWeapon.GetAccuracy();
      if (_projectiles.Null()) {
        return rangeAccuracy + _qualities.GetModifierOf(QualityType.Agility);
      }

      int projectilesAccuracy = _projectiles.GetAccuracy();
      return rangeAccuracy + projectilesAccuracy + _qualities.GetModifierOf(QualityType.Agility);
    }

    public float GetMeleeDamage() {
      if (MeleeWeapon.Null()) {
        return 0;
      }

      return MeleeWeapon.ToDamage();
    }

    public float GetRangeDamage() {
      if (_rangeWeapon.Null()) {
        return 0;
      }

      if (_projectiles.NotNull()) {
        return _rangeWeapon.ToDamage() + _projectiles.ToDamage();
      }

      return _rangeWeapon.ToDamage();
    }

    public virtual bool GetDamaged(int diceRoll, float damage, int weaponId = 100, WeaponType type = WeaponType.None) {
      if (IsHit(diceRoll).False()) {
        return false;
      }

      RiskPoints.ChangeRiskPoints(-damage);
      if (RiskPoints.IsDead()) {
        IsDead?.Invoke(Name);
      }

      return RiskPoints.IsDead();
    }

    private bool IsHit(int hit) {
      return _armorClassOfCharacter.IsHit(hit);
    }

    public int Initiative { get; set; }

    public ClassType ClassType { get; set; }

    public RiskPoints RiskPoints { get; set; }

    public Armor Armor { get; set; }

    public Weapon MeleeWeapon { get; set; }
  }
}