using System.Collections.Generic;
using Core.Main.Dice;
using Core.Main.GameRule;
using Core.Main.GameRule.Impact;
using Core.Main.GameRule.Points;
using Core.Main.NonPlayerCharacters;
using Core.Mono.Scenes.Fight;
using Core.SO.Impacts;
using Core.SO.Weapon;
using Core.Support.Data;
using UnityEngine;

namespace Core.SO.Npc {
  [CreateAssetMenu(menuName = "NPC", fileName = "NPC", order = 58)]
  public class NpcSO : ScriptableObject, INpcModel {
    private static SixSidedDice GetLifeDiceForRoll() {
      return new SixSidedDice(DiceType.SixEdges);
    }

    private static DiceBox GetDiceBox(Dices[] dices) {
      return new DiceBox(dices);
    }

    [SerializeField]
    public string Name;
    [SerializeField]
    public Alignment Alignment;
    [SerializeField]
    public NpcType NpcType;
    [SerializeField]
    public int ClassOfArmor;
    [SerializeField]
    public int LifeDice;
    [SerializeField]
    public int RiskPoints;
    [SerializeField]
    public int Morality;
    [SerializeField]
    public List<WeaponSO> _weaponObjects;
    [SerializeField]
    private List<int> _weaponId;
    [SerializeField]
    private List<int> _impactId;
    [SerializeField]
    public List<ImpactsSO> _impactObjects;
    [SerializeField]
    public int Experience;
    [SerializeField]
    public List<int> EscapePossibility;
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Property;
    [SerializeField]
    public bool Immoral;
    [SerializeField]
    public bool Immortal;
    [SerializeField]
    public bool DeadlyAttack;
    [SerializeField]
    public bool AttackEveryAtOnce;
    [SerializeField]
    public int Id;
    private NonPlayerCharacter _nonPlayerCharacter;
    private IRiskPoints _npcRiskPoints;

    public NpcEquipmentsModel GetNpcEquipmentModel() {
      return new NpcEquipmentsModel(GetArmorClass(), GetWeaponSet());
    }

    public NpcCombatAttributesModel GetNpcCombatAttributesModel() {
      RiskPoints riskPoints = GetNpcRiskPoints();
      return new NpcCombatAttributesModel(GetListOfImpacts(), riskPoints, AttackEveryAtOnce, GetNumberOfAttacks(), DeadlyAttack, Immortal);
    }

    public NpcMetadataModel GetNpcMetadataModel() {
      return new NpcMetadataModel(Id, Name, Description, Property, Experience, Alignment, NpcType);
    }

    public NpcMoralityModel GetNpcMoralityModel() {
      return new NpcMoralityModel(Morality, Immoral, EscapePossibility);
    }

    public void OnValidate() {
      Name = name;
      if (_weaponObjects != null && _weaponId.Count == 0) {
        for (var i = 0; i < _weaponObjects.Count; i++) {
          _weaponId.Add(_weaponObjects[i].id);
        }
      }

      if (_impactObjects != null && _impactId.Count == 0) {
        for (var i = 0; i < _impactObjects.Count; i++) {
          _impactId.Add(_impactObjects[i].GetId());
        }
      }
    }

    public NonPlayerCharacter GetNpc() {
      var npcMetadata = new NpcMetadata(GetNpcMetadataModel());
      var npcMorality = new NpcMorality(GetNpcMoralityModel());
      var npcCombatAttributes = new NpcCombatAttributes(GetNpcCombatAttributesModel());
      var npcEquipments = new NpcEquipments(GetNpcEquipmentModel());
      _nonPlayerCharacter = new NonPlayerCharacter(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments);
      return _nonPlayerCharacter;
    }

    public int GetRiskPointsAfterDiceRoll() {
      if (IsFixedValueOfNumberOfRiskPoints()) {
        return RiskPoints;
      }

      Dices[] dices = GetDices();

      DiceBox diceBox = GetDiceBox(dices);
      return diceBox.SumRollsOfDice();

      bool IsFixedValueOfNumberOfRiskPoints() {
        return RiskPoints != 0;
      }
    }

    private Dices[] GetDices() {
      var dices = new Dices[LifeDice];
      for (var i = 0; i < dices.Length; i++) {
        dices[i] = GetLifeDiceForRoll();
      }

      return dices;
    }

    private ArmorClass GetArmorClass() {
      return new ArmorClass(ClassOfArmor);
    }

    private WeaponSet GetWeaponSet() {
      return new WeaponSet(GetListOfWeapon());
    }

    private RiskPoints GetNpcRiskPoints() {
      _npcRiskPoints = new NpcRiskPoints();
      return new RiskPoints(_npcRiskPoints, GetRiskPointsAfterDiceRoll());
    }

    private List<Main.GameRule.Weapon> GetListOfWeapon() {
      if (_weaponObjects == null) {
        return new List<Main.GameRule.Weapon>();
      }

      var weapons = new List<Main.GameRule.Weapon>();
      foreach (WeaponSO weaponObject in _weaponObjects) {
        weapons.Add(weaponObject.weapon);
      }

      return weapons;
    }

    private List<Impact<ImpactOnRiskPoints>> GetListOfImpacts() {
      if (_impactObjects == null) {
        return new List<Impact<ImpactOnRiskPoints>>();
      }

      var impacts = new List<Impact<ImpactOnRiskPoints>>();
      foreach (ImpactsSO impactsSo in _impactObjects) {
        impacts.Add(impactsSo.GetImpact());
      }

      return impacts;
    }

    private int GetNumberOfAttacks() {
      if (GetListOfWeapon().Count <= 0 || DeadlyAttack) {
        return 1;
      }

      if (GetListOfWeapon().Count > 0 && AttackEveryAtOnce) {
        return GetListOfWeapon().Count;
      }

      if (!DeadlyAttack && GetListOfWeapon().Count <= 0 && GetListOfImpacts() != null && GetListOfImpacts().Count != 0) {
        return GetListOfImpacts().Count;
      }

      return 0;
    }
  }
}