using System.Collections.Generic;
using Core.Main.Dice;
using Core.Main.GameRule;
using Core.Main.GameRule.Impact;
using Core.Main.GameRule.Points;
using Core.Main.NonPlayerCharacters;
using Core.SO.Impacts;
using Core.SO.Weapon;
using Core.Support.Data;
using UnityEngine;

namespace Core.SO.Npc {
  [CreateAssetMenu(menuName = "NPC", fileName = "NPC", order = 58)]
  public class NpcSO : ScriptableObject {
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

    public void OnValidate() {
      Name = name;
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

    private NpcEquipmentsModel GetNpcEquipmentModel() {
      return new NpcEquipmentsModel(GetArmorClass(), GetWeaponSet());
    }

    private ArmorClass GetArmorClass() {
      return new ArmorClass(ClassOfArmor);
    }

    private WeaponSet GetWeaponSet() {
      return new WeaponSet(GetListOfWeapon());
    }

    private NpcCombatAttributesModel GetNpcCombatAttributesModel() {
      RiskPoints riskPoints = GetNpcRiskPoints();
      return new NpcCombatAttributesModel(GetListOfImpacts(), riskPoints, AttackEveryAtOnce, GetNumberOfAttacks(), DeadlyAttack);
    }

    private RiskPoints GetNpcRiskPoints() {
      _npcRiskPoints = new NpcRiskPoints();
      return new RiskPoints(_npcRiskPoints, GetRiskPointsAfterDiceRoll());
    }

    private NpcMetadataModel GetNpcMetadataModel() {
      return new NpcMetadataModel(Id, Name, Description, Property, Experience, Alignment, NpcType);
    }

    private NpcMoralityModel GetNpcMoralityModel() {
      return new NpcMoralityModel(Morality, Immoral, EscapePossibility);
    }

    private NpcModel GetNpcDTO() {
      return new NpcModel(GetNpcMoralityModel(), GetNpcMetadataModel(), GetNpcCombatAttributesModel(), GetNpcEquipmentModel());
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