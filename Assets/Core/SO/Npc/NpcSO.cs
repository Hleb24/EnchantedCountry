using System.Collections.Generic;
using Core.Main.Dice;
using Core.Main.GameRule;
using Core.Main.GameRule.Impact;
using Core.Main.GameRule.Points;
using Core.Main.NonPlayerCharacters;
using Core.Mono.Scenes.Fight;
using Core.SO.Impacts;
using Core.SO.WeaponObjects;
using Core.Support.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.Npc {
  [CreateAssetMenu(menuName = "NPC", fileName = "NPC", order = 58)]
  public class NpcSO : ScriptableObject, INpcModel {
    private static SixSidedDice GetLifeDiceForRoll() {
      return new SixSidedDice(DiceType.SixEdges);
    }

    private static DiceBox GetDiceBox(Dices[] dices) {
      return new DiceBox(dices);
    }

    [FormerlySerializedAs("Name"), SerializeField]
    private string _name;
    [FormerlySerializedAs("Alignment"), SerializeField]
    private Alignment _alignment;
    [FormerlySerializedAs("NpcType"), SerializeField]
    private NpcType _npcType;
    [FormerlySerializedAs("ClassOfArmor"), SerializeField]
    private int _classOfArmor;
    [FormerlySerializedAs("LifeDice"), SerializeField]
    private int _lifeDice;
    [FormerlySerializedAs("_riskPoints"),FormerlySerializedAs("RiskPoints"), SerializeField]
    private int _defaultRiskPoints;
    [FormerlySerializedAs("Morality"), SerializeField]
    private int _morality;
    [SerializeField]
    private List<WeaponSO> _weaponObjects;
    [FormerlySerializedAs("_weaponsIdList"),FormerlySerializedAs("_weaponId"), SerializeField]
    private List<int> _weaponsId;
    [SerializeField]
    private List<int> _impactId;
    [SerializeField]
    private List<ImpactsSO> _impactObjects;
    [FormerlySerializedAs("Experience"), SerializeField]
    private int _experience;
    [FormerlySerializedAs("EscapePossibility"), SerializeField]
    private List<int> _escapePossibility;
    [FormerlySerializedAs("Description"), SerializeField]
    private string _description;
    [FormerlySerializedAs("Property"), SerializeField]
    private string _property;
    [FormerlySerializedAs("Immoral"), SerializeField]
    private bool _immoral;
    [FormerlySerializedAs("Immortal"), SerializeField]
    private bool _immortal;
    [FormerlySerializedAs("DeadlyAttack"), SerializeField]
    private bool _deadlyAttack;
    [FormerlySerializedAs("_attackEveryAtOnce"),FormerlySerializedAs("AttackEveryAtOnce"), SerializeField]
    private bool _attackWithAllWeapons;
    [FormerlySerializedAs("Id"), SerializeField]
    private int _id;
    private IRiskPoints _npcRiskPoints;

    public NpcEquipmentsModel GetNpcEquipmentModel() {
      return new NpcEquipmentsModel(_weaponsId, _classOfArmor);
    }

    public NpcCombatAttributesModel GetNpcCombatAttributesModel() {
      RiskPoints riskPoints = GetNpcRiskPoints();
      return new NpcCombatAttributesModel(_impactId, _defaultRiskPoints, _lifeDice,_attackWithAllWeapons, _deadlyAttack, _immortal);
    }

    public NpcMetadataModel GetNpcMetadataModel() {
      return new NpcMetadataModel(_id, _name, _description, _property, _experience, _alignment, _npcType);
    }

    public NpcMoralityModel GetNpcMoralityModel() {
      return new NpcMoralityModel(_morality, _immoral, _escapePossibility);
    }

    public void OnValidate() {
      // _name = name;
      // if (_weaponObjects != null && _weaponsId.Count == 0) {
      //   for (var i = 0; i < _weaponObjects.Count; i++) {
      //     _weaponsId.Add(_weaponObjects[i].id);
      //   }
      // }
      //
      // if (_impactObjects != null && _impactId.Count == 0) {
      //   for (var i = 0; i < _impactObjects.Count; i++) {
      //     _impactId.Add(_impactObjects[i].GetId());
      //   }
      // }
    }

    public int GetId() {
      return _id;
    }

    private int GetRiskPointsAfterDiceRoll() {
      if (IsFixedValueOfNumberOfRiskPoints()) {
        return _defaultRiskPoints;
      }

      Dices[] dices = GetDices();

      DiceBox diceBox = GetDiceBox(dices);
      return diceBox.SumRollsOfDice();

      bool IsFixedValueOfNumberOfRiskPoints() {
        return _defaultRiskPoints != 0;
      }
    }

    private Dices[] GetDices() {
      var dices = new Dices[_lifeDice];
      for (var i = 0; i < dices.Length; i++) {
        dices[i] = GetLifeDiceForRoll();
      }

      return dices;
    }

    private RiskPoints GetNpcRiskPoints() {
      _npcRiskPoints = new NpcRiskPoints();
      return new RiskPoints(_npcRiskPoints, GetRiskPointsAfterDiceRoll());
    }

    private List<Weapon> GetListOfWeapon() {
      if (_weaponObjects == null) {
        return new List<Weapon>();
      }

      var weapons = new List<Weapon>();
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
      if (GetListOfWeapon().Count <= 0 || _deadlyAttack) {
        return 1;
      }

      if (GetListOfWeapon().Count > 0 && _attackWithAllWeapons) {
        return GetListOfWeapon().Count;
      }

      if (!_deadlyAttack && GetListOfWeapon().Count <= 0 && GetListOfImpacts() != null && GetListOfImpacts().Count != 0) {
        return GetListOfImpacts().Count;
      }

      return 0;
    }
  }
}