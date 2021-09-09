﻿using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Dice;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Impact;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Initiative;
using UnityEngine;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC {
  [Serializable]
  public class Npc : ImpactOnRiskPoints, IInitiative {
    #region IMPLEMENTATIONS_SET_RISK_POINTS
    void ImpactOnRiskPoints.SetRiskPoints(ImpactType impactType, int points, int protectiveThrow) {
      int playerLuckRoll = KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice();
      if (playerLuckRoll >= protectiveThrow) {
        return;
      }

      switch (impactType) {
        case ImpactType.Positive:
          _riskPoints.Points += points;
          break;
        case ImpactType.Negative:
          _riskPoints.Points -= points;
          break;
        case ImpactType.Neutral:
          _riskPoints.Points = points;
          break;
        default:
          throw new InvalidOperationException("Impact type is invalid");
      }
    }
    #endregion

    #region FIELDS
    public event Action<string> IsDead;
    protected string _name;
    protected Alignment _alignment;
    protected NpcType _npcType;
    protected RiskPoints.RiskPoints _riskPoints;
    protected ArmorClass.ArmorClass _armorClass;
    protected int _morality;
    protected List<Weapon.Weapon> _weapons;
    protected List<Impact<ImpactOnRiskPoints>> _impacts;
    protected Weapon.Weapon _meleeWeapon;
    protected Weapon.Weapon _rangeWeapon;
    protected Weapon.Weapon _projectilies;
    protected bool _immoral;
    protected bool _immortal;
    protected bool _deadlyAttack;
    protected bool _dontRunAway;
    protected int _experience;
    protected List<int> _escapePossibility;
    protected string _description;
    protected string _property;
    protected int _id;
    protected bool _hasWeapon;
    protected bool _attacksEveryoneAtOnce;
    protected int _numberOfAttack;
    protected int _numberOfWeapon;
    #endregion

    #region CONSTRUCTORS
    public Npc(string name, Alignment alignment, NpcType npcType, RiskPoints.RiskPoints riskPoints, ArmorClass.ArmorClass armorClass, int morality, bool immoral, bool immortal, bool deadlyAttack,
      bool attacksEveryoneAtOnce, int experience, List<int> escapePosibility, string description, string property, int id, List<Weapon.Weapon> weapons = null,
      List<Impact<ImpactOnRiskPoints>> impacts = null) {
      _name = name;
      _alignment = alignment;
      _npcType = npcType;
      _riskPoints = riskPoints;
      _armorClass = armorClass;
      _morality = morality;

      _immoral = immoral;
      _immortal = immortal;
      _deadlyAttack = deadlyAttack;
      _attacksEveryoneAtOnce = attacksEveryoneAtOnce;
      _experience = experience;
      _escapePossibility = new List<int>();
      if (escapePosibility.Count == 0) {
        _dontRunAway = true;
      } else {
        _escapePossibility.AddRange(escapePosibility);
      }

      _description = description;
      _property = property;
      _id = id;

      _weapons = new List<Weapon.Weapon>();
      if (weapons != null && weapons.Count != 0) {
        _weapons.AddRange(weapons);
        _hasWeapon = true;
        _numberOfWeapon = _weapons.Count;
      }

      _impacts = new List<Impact<ImpactOnRiskPoints>>();
      if (impacts != null && impacts.Count != 0) {
        _impacts.AddRange(impacts);
      }

      NumberOfAttacks();
    }

    public Npc() { }
    #endregion

    #region ATTACK
    protected virtual void NumberOfAttacks() {
      if (_hasWeapon || _deadlyAttack) {
        _numberOfAttack = 1;
      }

      if (_hasWeapon && _attacksEveryoneAtOnce) {
        _numberOfAttack = _weapons.Count;
        return;
      }

      if (!_deadlyAttack && !_hasWeapon && _impacts != null && _impacts.Count != 0) {
        _numberOfAttack = _impacts.Count;
      }
    }

    public virtual int Accuracy(int index = 0) {
      var accuracy = 0;
      if (_weapons != null && _weapons.Count > 0) {
        accuracy = _weapons[GetIndexOfWeapon(index)].Attack.Accuracy;
      }

      return accuracy;
    }

    public virtual float Attack(int diceRoll, ImpactOnRiskPoints character, int weapon = 0) {
      return ToDamage(diceRoll, character, weapon);
    }

    public virtual float ToDamage(int diceRoll, ImpactOnRiskPoints character, int weapon = 0) {
      var damage = 0f;
      if (IsDeadlyAttack(out float deadlyDamage)) {
        return deadlyDamage;
      }

      damage = WeaponsDamage(weapon);

      ImpactsDamage(diceRoll, character);

      return damage;
    }

    protected virtual void ImpactsDamage(int diceRoll, ImpactOnRiskPoints character) {
      if (_impacts != null && _impacts.Count > 0 && character != null) {
        ToDamagedOfImpact(diceRoll, character, GetIndexForImpact());
      }
    }

    protected virtual float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      if (_weapons != null && _weapons.Count > 0) {
        damage = _weapons[weapon].ToDamage();
      }

      return damage;
    }

    protected virtual bool IsDeadlyAttack(out float deadlyDamage) {
      deadlyDamage = 0;
      if (_deadlyAttack) {
        {
          deadlyDamage = 10000f;
          return true;
        }
      }

      return false;
    }

    public virtual void ToDamagedOfImpact(int diceRoll, ImpactOnRiskPoints character, int indexOfImpact) {
      if (diceRoll >= _impacts[indexOfImpact].DiceRollValueForInvokeImpact) {
        _impacts[indexOfImpact].ImpactAction(character);
      }
    }

    public virtual int GetIndexForImpact(int index = 0) {
      return index;
    }

    public virtual int GetIndexOfWeapon(int index = 0) {
      return index;
    }
    #endregion

    #region GET_DAMAGED
    public virtual bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.Weapon.WeaponType type = Weapon.Weapon.WeaponType.None, bool isSpell = false) {
      if (СanKillIfIsKillOnlySpell(isSpell)) {
        return false;
      }

      if (!IsHit(diceRoll)) {
        return false;
      }

      _riskPoints.Points -= damage;
      if (_riskPoints.isDead) {
        IsDead?.Invoke(_name);
      }
      return _riskPoints.isDead;
    }

    protected virtual bool СanKillIfIsKillOnlySpell(bool isSpell) {
      return _armorClass.isKillOnlySpell && !isSpell;
    }

    public virtual bool IsHit(int hit) {
      return _armorClass.IsHit(hit);
    }

    public virtual bool GetDamaged(int diceRoll, float damage, bool isSpell = false) {
      if (_armorClass.isKillOnlySpell && !isSpell) {
        return false;
      }

      if (!_armorClass.IsHit(diceRoll)) {
        return false;
      }

      _riskPoints.Points -= damage;
      return _riskPoints.isDead;
    }
    #endregion

    #region PROPERTIES
    public RiskPoints.RiskPoints RiskPoints {
      get {
        return _riskPoints;
      }
      set {
        _riskPoints = value;
      }
    }

    public ArmorClass.ArmorClass ArmorClass {
      get {
        return _armorClass;
      }
      set {
        _armorClass = value;
      }
    }

    public Weapon.Weapon MeleeWeapon {
      get {
        return _meleeWeapon;
      }
      set {
        _meleeWeapon = value;
      }
    }

    public Weapon.Weapon RangeWeapon {
      get {
        return _rangeWeapon;
      }
      set {
        _rangeWeapon = value;
      }
    }

    public int NumberOfAttack {
      get {
        return _numberOfAttack;
      }
    }
    
    public int NumberOfWeapon {
      get {
        return _numberOfWeapon;
      }
    }

    public bool AttackEveryoneAtOnce {
      get {
        return _attacksEveryoneAtOnce;
      }
    }

    public Weapon.Weapon Projectilies {
      get {
        return _projectilies;
      }
      set {
        _projectilies = value;
      }
    }

    public string Name {
      get {
        return _name;
      }
    }
    #endregion

    public int CompareTo(IInitiative other) {
      if (other != null)
        return this.Initiative.CompareTo(other.Initiative);
      else
        throw new Exception("Can not compare 2 object");
    }
    public int Initiative { get; set; }
  }
}