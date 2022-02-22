using System;
using Core.Main.Dice;
using Core.Main.GameRule;
using Core.Main.GameRule.Impact;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.NonPlayerCharacters {
  public class NonPlayerCharacter : IImpactOnRiskPoints, IInitiative {
    protected const float DEADLY_DAMAGE = 10000;
    protected readonly NpcCombatAttributes _npcCombatAttributes;
    protected readonly NpcEquipments _npcEquipments;
    private readonly NpcMetadata _npcMetadata;
    public event Action<string> IsDead;
    private NpcMorality _npcMorality;

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
    }

    void IImpactOnRiskPoints.SetRiskPoints(ImpactType impactType, int points, int protectiveThrow) {
      int playerLuckRoll = KitOfDice.DicesKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].GetSumRollOfBoxDices();
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

    public int CompareTo([NotNull] IInitiative other) {
      return Initiative.CompareTo(other.Initiative);
    }

    public virtual int Accuracy(int index = 0) {
      return _npcEquipments.GetAccuracy(index);
    }

    public virtual float Attack(int diceRoll, [NotNull] IImpactOnRiskPoints character, int weapon = 0) {
      return ToDamage(diceRoll, character, weapon);
    }

    public virtual float ToDamage(int diceRoll, [NotNull] IImpactOnRiskPoints character, int weapon = 0) {
      if (IsDeadlyAttack(out float deadlyDamage)) {
        return deadlyDamage;
      }

      float damage = WeaponsDamage(weapon);

      ImpactsDamage(diceRoll, character);

      return damage;
    }

    public virtual bool GetDamaged(int diceRoll, float damage, int weaponId, WeaponType type, bool isSpell = false) {
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

    public bool GetDamaged(int diceRoll, float damage, bool isSpell = false) {
      if (_npcEquipments.IsKillOnlySpell() && !isSpell) {
        return false;
      }

      if (IsNotHit(diceRoll)) {
        return false;
      }

      _npcCombatAttributes.GetDamaged(damage);
      return _npcCombatAttributes.IsDead();
    }

    public float GetPointsOfRisk() {
      return _npcCombatAttributes.GetPointsOfRisk();
    }

    public int GetClassOfArmor() {
      return _npcEquipments.GetClassOfArmor();
    }

    public int GetNumberOfAttack() {
      return _npcCombatAttributes.GetNumberOfAttack();
    }

    public bool IsHasNoNumberOfAttack() {
      const int noAttack = 0;
      return _npcCombatAttributes.GetNumberOfAttack() == noAttack;
    }

    public int GetNumberOfWeapon() {
      return _npcEquipments.GetNumberOfWeapon();
    }

    public bool IsAttackWithAllWeapons() {
      return _npcCombatAttributes.IsAttackWithAllWeapons();
    }

    public bool IsAttackWithOneWeapon() {
      return !_npcCombatAttributes.IsAttackWithAllWeapons();
    }

    public string GetName() {
      return _npcMetadata.GetName();
    }

    protected virtual void ToDamagedOfImpact(int diceRoll, [NotNull] IImpactOnRiskPoints character, int indexOfImpact) {
      if (_npcCombatAttributes.CanUseImpact(diceRoll, indexOfImpact)) {
        _npcCombatAttributes.UseImpact(character, indexOfImpact);
      }
    }

    protected int GetIndexOfWeapon(int index = 0) {
      return index;
    }

    protected virtual bool IsHit(int hit) {
      return _npcEquipments.IsHit(hit);
    }

    protected bool IsNotDeadlyAttack() {
      return !_npcCombatAttributes.IsDeadlyAttack();
    }

    protected virtual void ImpactsDamage(int diceRoll, [NotNull] IImpactOnRiskPoints character) {
      if (_npcCombatAttributes.IsHasImpact()) {
        ToDamagedOfImpact(diceRoll, character, GetIndexForImpact());
      }
    }

    protected virtual float WeaponsDamage(int weapon = 0) {
      float damage = 0;
      if (_npcEquipments.IsHasWeapon()) {
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

    /// <summary>
    ///   Подготовить количество атак для npc.
    /// </summary>
    /// <remarks>Вызывать всегда после создания npc.</remarks>
    internal virtual void PrepareNumberOfAttacks() {
      const int oneAttack = 1;
      var numberOfAttack = 0;
      if (_npcCombatAttributes.IsDeadlyAttack()) {
        _npcCombatAttributes.SetNumberOfAttack(oneAttack);
        return;
      }

      if (_npcEquipments.IsHasWeapon() && _npcCombatAttributes.IsAttackWithAllWeapons()) {
        numberOfAttack = _npcEquipments.GetNumberOfWeapon();
        _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
        return;
      }

      if (IsHasNotWeapon() && _npcCombatAttributes.IsHasImpact()) {
        numberOfAttack = _npcCombatAttributes.GetNumberOfImpacts();
        _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
        return;
      }

      if (_npcEquipments.IsHasWeapon()) {
        _npcCombatAttributes.SetNumberOfAttack(oneAttack);
        return;
      }

      _npcCombatAttributes.SetNumberOfAttack(numberOfAttack);
    }

    private int GetIndexForImpact(int index = 0) {
      return index;
    }

    private bool IsNotHit(int diceRoll) {
      return !IsHit(diceRoll);
    }

    private bool IsHasNotWeapon() {
      return !_npcEquipments.IsHasWeapon();
    }

    public int Initiative { get; set; }
  }
}