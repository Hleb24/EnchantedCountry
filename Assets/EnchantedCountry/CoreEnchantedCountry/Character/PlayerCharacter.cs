using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.CoreEnchantedCountry.Dice;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.ArmorClass;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Impact;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Initiative;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.RiskPoints;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using Core.EnchantedCountry.SupportSystems.Data;
using UnityEngine;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character {
  public enum CharacterType {
    Warrior,
    Elf,
    Wizard,
    Kron,
    Gnom
  }

  [Serializable]
  public class PlayerCharacter : ImpactOnRiskPoints, IInitiative {
    #region FIELDS
    public event Action<string> IsDead;
    public string Name = "Kell";
    private CharacterQualities _characterQualities;
    private CharacterType _characterType;
    private Levels.Levels _levels;
    private GamePoints.GamePoints _gamePoints;
    private RiskPoints _riskPoints;
    private IWallet _wallet;
    private ArmorClass _armorClassOfCharacter;
    private EquipmentsOfCharacter _equipmentsOfCharacter;
    private EquipmentsUsed _equipmentsUsed;
    private Armor _armor;
    private Armor _shield;
    private Weapon _meleeWeapon;
    private Weapon _rangeWeapon;
    private Weapon _projectiles;
    #endregion
    #region COMPARE_TO
    public int CompareTo(IInitiative other) {
      if (other != null) {
        return Initiative.CompareTo(other.Initiative);
      }

      return -1;
    }
    #endregion
    #region IMPLEMENTATIONS_SET_RISK_POINTS
    void ImpactOnRiskPoints.SetRiskPoints(ImpactType impactType, int points, int protectiveThrow) {
      int playerLuckRoll = KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice();
      if (playerLuckRoll >= protectiveThrow) {
        return;
      }

      switch (impactType) {
        case ImpactType.Poison:
          _riskPoints.Points -= points;
          Debug.Log($"<color=red>Poison</color> {points}, luck roll {playerLuckRoll}, protective throw {protectiveThrow}"); 
          break;
        case ImpactType.Paralysis:
          _riskPoints.Points -= points;
          Debug.Log($"<color=red>Paralysis</color> {points}"); 
          break;
        case ImpactType.Petrification:
          _riskPoints.Points -= points;
          Debug.Log($"<color=red>Petrification</color> {points}"); 
          break;
        case ImpactType.Acid:
          _riskPoints.Points -= points;
          Debug.Log($"<color=red>Acid</color> {points}"); 
          break;
        case ImpactType.Gas:
          _riskPoints.Points -= points;
          Debug.Log($"<color=red>Gas</color> {points}"); 
          break;
        case ImpactType.Fire:
          _riskPoints.Points -= points;
          Debug.Log($"<color=red>Fire</color> {points}"); 
          break;
        case ImpactType.DragonBreath:
          _riskPoints.Points -= points;
          Debug.Log($"<color=red>DragonBreath</color> {points}"); 
          break;
      }
    }
    #endregion
    #region BATTLE_METHODS
    public int GetMeleeAccuracy() {
      if (_meleeWeapon == null) {
        return 0;
      }

      return _meleeWeapon.Attack.Accuracy + _characterQualities[Quality.QualityType.Strength].Modifier;
    }

    public int GetRangeAccuracy() {
      if (_rangeWeapon == null) {
        return 0;
      }

      if (_projectiles != null) {
        return _rangeWeapon.Attack.Accuracy + _projectiles.Attack.Accuracy + _characterQualities[Quality.QualityType.Agility].Modifier;
      }

      return _rangeWeapon.Attack.Accuracy + _characterQualities[Quality.QualityType.Agility].Modifier;
    }

    public float GetMeleeDamage() {
      if (_meleeWeapon == null) {
        return 0;
      }

      return _meleeWeapon.ToDamage();
    }

    public float GetRangeDamage() {
      if (_rangeWeapon == null) {
        return 0;
      }

      if (_projectiles != null) {
        return _rangeWeapon.ToDamage() + _projectiles.ToDamage();
      }

      return _rangeWeapon.ToDamage();
    }

    public virtual bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.WeaponType type = Weapon.WeaponType.None) {
      if (!IsHit(diceRoll)) {
        return false;
      }
      _riskPoints.Points -= damage;
      if (_riskPoints.isDead) {
        IsDead?.Invoke(Name);
      }

      return _riskPoints.isDead;
    }

    public bool IsHit(int hit) {
      return _armorClassOfCharacter.IsHit(hit);
    }
    #endregion
    #region CONSTRUCTORS
    public PlayerCharacter(CharacterQualities characterQualities, CharacterType characterType, Levels.Levels levels, GamePoints.GamePoints gamePoints, RiskPoints riskPoints,
      IWallet wallet, EquipmentsOfCharacter equipmentsOfCharacter, EquipmentsUsed equipmentsUsed, Armor armor, Armor shield, Weapon rangeWeapon, Weapon meleeWeapon,
      Weapon projectiles) {
      _characterQualities = characterQualities;
      _characterType = characterType;
      _levels = levels;
      _gamePoints = gamePoints;
      _riskPoints = riskPoints;
      _wallet = wallet;
      _equipmentsOfCharacter = equipmentsOfCharacter;
      _equipmentsUsed = equipmentsUsed;
      _armor = armor;
      _shield = shield;
      _meleeWeapon = meleeWeapon;
      _rangeWeapon = rangeWeapon;
      _projectiles = projectiles;
      _armorClassOfCharacter = new ArmorClass(_armor.ArmorClass.ClassOfArmor + _characterQualities[Quality.QualityType.Agility].Modifier);
      if (_shield != null) {
        _armorClassOfCharacter = new ArmorClass(_armor.ArmorClass.ClassOfArmor + _shield.ArmorClass.ClassOfArmor + _characterQualities[Quality.QualityType.Agility].Modifier);
      }
    }

    public PlayerCharacter() { }
    #endregion
    #region PROPERTIES
    public CharacterQualities CharacterQualities {
      get {
        return _characterQualities;
      }
      set {
        _characterQualities = value;
      }
    }

    public CharacterType CharacterType {
      get {
        return _characterType;
      }
      set {
        _characterType = value;
      }
    }

    public Levels.Levels Levels {
      get {
        return _levels;
      }
      set {
        _levels = value;
      }
    }

    public GamePoints.GamePoints GamePoints {
      get {
        return _gamePoints;
      }
      set {
        _gamePoints = value;
      }
    }

    public RiskPoints RiskPoints {
      get {
        return _riskPoints;
      }
      set {
        _riskPoints = value;
      }
    }

    public IWallet Wallet {
      get {
        return _wallet;
      }
      set {
        _wallet = value;
      }
    }

    public EquipmentsOfCharacter EquipmentsOfCharacter {
      get {
        return _equipmentsOfCharacter;
      }
      set {
        _equipmentsOfCharacter = value;
      }
    }

    public EquipmentsUsed EquipmentsUsed {
      get {
        return _equipmentsUsed;
      }
      set {
        _equipmentsUsed = value;
      }
    }

    public Armor Armor {
      get {
        return _armor;
      }
      set {
        _armor = value;
      }
    }

    public Armor Shield {
      get {
        return _shield;
      }
      set {
        _shield = value;
      }
    }

    public Weapon MeleeWeapon {
      get {
        return _meleeWeapon;
      }
      set {
        _meleeWeapon = value;
      }
    }

    public Weapon RangeWeapon {
      get {
        return _rangeWeapon;
      }
      set {
        _rangeWeapon = value;
      }
    }

    public Weapon Projectiles {
      get {
        return _projectiles;
      }
      set {
        _projectiles = value;
      }
    }

    public int Initiative { get; set; }
    #endregion
  }
}