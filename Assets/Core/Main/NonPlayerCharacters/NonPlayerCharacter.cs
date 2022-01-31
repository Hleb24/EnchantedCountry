using System;
using Core.Main.Dice;
using Core.Main.GameRule;
using Core.Main.GameRule.Impact;
using Core.Main.GameRule.Initiative;
using Core.Main.GameRule.Points;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.NonPlayerCharacters {
  [Serializable]
  public class NonPlayerCharacter : ImpactOnRiskPoints, IInitiative {
    public event Action<string> IsDead;
    protected NpcMetadata _npcMetadata;
    protected NpcMorality _npcMorality;
    protected NpcCombatAttributes _npcCombatAttributes;
    protected NpcEquipments _npcEquipments;

    public NonPlayerCharacter([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes,
      [NotNull] NpcEquipments npcEquipments) {
      Assert.IsNotNull(npcMetadata, nameof(npcMetadata));
      Assert.IsNotNull(npcMorality, nameof(npcMorality));
      Assert.IsNotNull(npcCombatAttributes, nameof(npcCombatAttributes));
      Assert.IsNotNull(npcEquipments, nameof(npcEquipments));
      _npcMetadata = npcMetadata;
      _npcMorality = npcMorality;
      _npcCombatAttributes = npcCombatAttributes;
      _npcEquipments = npcEquipments;
      NumberOfAttacks();
    }


    public int CompareTo([NotNull] IInitiative other) {
      return Initiative.CompareTo(other.Initiative);
    }

    void ImpactOnRiskPoints.SetRiskPoints(ImpactType impactType, int points, int protectiveThrow) {
      int playerLuckRoll = KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice();
      if (IsProtected()) {
        return;
      }

      switch (impactType) {
        case ImpactType.Positive:
          _npcCombatAttributes.Heal(points);
          break;
        case ImpactType.Negative:
          _npcCombatAttributes.GetDamaged(points);
          break;
        case ImpactType.Neutral:
          _npcCombatAttributes.NeutralImpact(points);
          break;
        default:
          throw new InvalidOperationException("Тип воздействия не найден.");
      }

      bool IsProtected() {
        return playerLuckRoll >= protectiveThrow;
      }
    }

    public virtual int Accuracy(int index = 0) {
      return _npcEquipments.GetAccuracy(index);
    }

    public virtual float Attack(int diceRoll, [NotNull] ImpactOnRiskPoints character, int weapon = 0) {
      return ToDamage(diceRoll, character, weapon);
    }

    public virtual float ToDamage(int diceRoll, [NotNull] ImpactOnRiskPoints character, int weapon = 0) {
      if (IsDeadlyAttack(out float deadlyDamage)) {
        return deadlyDamage;
      }

      float damage = WeaponsDamage(weapon);

      ImpactsDamage(diceRoll, character);

      return damage;
    }

    public virtual void ToDamagedOfImpact(int diceRoll, [NotNull] ImpactOnRiskPoints character, int indexOfImpact) {
      if (_npcCombatAttributes.CanUseImpact(diceRoll, indexOfImpact)) {
        _npcCombatAttributes.UseImpact(character, indexOfImpact);
      }
    }

    public virtual int GetIndexForImpact(int index = 0) {
      return index;
    }

    public virtual int GetIndexOfWeapon(int index = 0) {
      return index;
    }

    public virtual bool GetDamaged(int diceRoll, float damage, int weaponId = 100, Weapon.WeaponType type = Weapon.WeaponType.None, bool isSpell = false) {
      if (СanKillIfIsKillOnlySpell(isSpell)) {
        return false;
      }

      if (IsNotHit(diceRoll)) {
        return false;
      }

      _npcCombatAttributes.GetDamaged(damage);
      if (_npcCombatAttributes.IsDead()) {
        IsDead?.Invoke(_npcMetadata.GetName());
      }

      return _npcCombatAttributes.IsDead();
    }

    protected bool IsNotHit(int diceRoll) {
      return !IsHit(diceRoll);
    }

    public virtual bool IsHit(int hit) {
      return _npcEquipments.IsHit(hit);
    }

    public virtual bool GetDamaged(int diceRoll, float damage, bool isSpell = false) {
      if (_npcEquipments.IsKillOnlySpell() && !isSpell) {
        return false;
      }

      if (IsNotHit(diceRoll)) {
        return false;
      }

      _npcCombatAttributes.GetDamaged(damage);
      return _npcCombatAttributes.IsDead();
    }

    protected virtual void NumberOfAttacks() {
      int numberOfAttack;
      if (_npcEquipments.HasWeapon() || _npcCombatAttributes.IsDeadlyAttack()) {
        numberOfAttack = 1;
        _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
      }

      if (_npcEquipments.HasWeapon() && _npcCombatAttributes.IsAttackEveryAtOne()) {
        numberOfAttack = _npcEquipments.GetNumberOfWeapon();
        _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
        return;
      }

      if (IsNotDeadlyAttack() && HasNotWeapon() && _npcCombatAttributes.HasImpacts()) {
        numberOfAttack = _npcCombatAttributes.GetNumberOfImpacts();
        _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
      }
    }

    protected bool HasNotWeapon() {
      return !_npcEquipments.HasWeapon();
    }

    protected bool IsNotDeadlyAttack() {
      return !_npcCombatAttributes.IsDeadlyAttack();
    }

    protected virtual void ImpactsDamage(int diceRoll, [NotNull] ImpactOnRiskPoints character) {
      if (_npcCombatAttributes.HasImpacts()) {
        ToDamagedOfImpact(diceRoll, character, GetIndexForImpact());
      }
    }

    protected virtual float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      if (_npcEquipments.HasWeapon()) {
        damage = _npcEquipments.ToDamage(weapon);
      }

      return damage;
    }

    protected virtual bool IsDeadlyAttack(out float deadlyDamage) {
      deadlyDamage = 0;
      if (_npcCombatAttributes.IsDeadlyAttack()) {
        {
          deadlyDamage = float.MaxValue;
          return true;
        }
      }

      return false;
    }

    protected virtual bool СanKillIfIsKillOnlySpell(bool isSpell) {
      return _npcEquipments.IsKillOnlySpell() && !isSpell;
    }

    public int Initiative { get; set; }

    public RiskPoints RiskPoints {
      get {
        return _npcCombatAttributes.GetRiskPoints();
      }
    }

    public ArmorClass ArmorClass {
      get {
        return _npcEquipments.GetArmorClass();
      }
    }

    public int NumberOfAttack {
      get {
        return _npcCombatAttributes.GetNumberOfAttack();
      }
    }

    public int NumberOfWeapon {
      get {
        return _npcEquipments.GetNumberOfWeapon();
      }
    }

    public bool AttackEveryoneAtOnce {
      get {
        return _npcCombatAttributes.IsAttackEveryAtOne();
      }
    }

    public string Name {
      get {
        return _npcMetadata.GetName();
      }
    }
  }
}