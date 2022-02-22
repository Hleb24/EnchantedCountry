using System;
using Aberrance.Extensions;
using Core.Main.Character.Class;
using Core.Main.Character.Item;
using Core.Main.Character.Level;
using Core.Main.Character.Quality;
using Core.Main.Dice;
using Core.Main.GameRule.Battle;
using Core.Main.GameRule.Equipment;
using Core.Main.GameRule.Impact;
using Core.Main.GameRule.Item;
using Core.Main.GameRule.Point;
using UnityEngine;

namespace Core.Main.Character {
  public class BaseCharacter : IImpactOnRiskPoints, IInitiative {
    private readonly Qualities _qualities;
    private readonly ArmorClass _armorClassOfCharacter;
    private readonly Armor _shield;
    private readonly Weapon _rangeWeapon;
    private readonly Weapon _projectiles;
    public event Action<string> IsDead;
    public string Name = "Kell";
    private BaseLevel _baseLevel;
    private IGamePoints _gamePoints;
    private IWallet _wallet;
    private CharacterEquipments _characterEquipments;
    private IEquipmentUsed _equipmentsUsed;

    public BaseCharacter(Qualities qualities, ClassType classType, BaseLevel baseLevel, IGamePoints gamePoints, RiskPoints riskPoints, IWallet wallet,
      CharacterEquipments characterEquipments, IEquipmentUsed equipmentsUsed, Armor armor, Armor shield, Weapon rangeWeapon, Weapon meleeWeapon, Weapon projectiles) {
      _qualities = qualities;
      ClassType = classType;
      _baseLevel = baseLevel;
      _gamePoints = gamePoints;
      RiskPoints = riskPoints;
      _wallet = wallet;
      _characterEquipments = characterEquipments;
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

    void IImpactOnRiskPoints.SetRiskPoints(ImpactType impactType, int points, int protectiveThrow) {
      int playerLuckRoll = KitOfDice.DicesKit[KitOfDice.SET_WITH_ONE_TWELVE_SIDED_AND_ONE_SIX_SIDED_DICE].GetSumRollOfBoxDices();
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

    public int CompareTo(IInitiative other) {
      if (other.NotNull()) {
        return Initiative.CompareTo(other.Initiative);
      }

      return -1;
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