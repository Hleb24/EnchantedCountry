using System.Collections.Generic;
using System.Linq;
using Core.Main.Dice;
using Core.Main.GameRule;
using Core.Main.GameRule.Impact;
using Core.Main.GameRule.Points;
using Core.Main.NonPlayerCharacters;
using Core.SO.Impacts;
using Core.SO.Npc;
using Core.SO.NpcSet;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Mono.Scenes.Fight {
  public class NpcBuilder {
    private static SixSidedDice GetLifeDiceForRoll() {
      return new SixSidedDice(DiceType.SixEdges);
    }

    private static DiceBox GetDiceBox(Dices[] dices) {
      return new DiceBox(dices);
    }

    private INpcModelSet _npcModelSet;
    private readonly INpcWeaponSet _npcWeaponSet;
    private readonly IImpactsSet _impactsSet;

    public NpcBuilder([NotNull] INpcModelSet npcModelSet, [NotNull] INpcWeaponSet npcWeaponSet, [NotNull] IImpactsSet impactsSet) {
      Assert.IsNotNull(npcModelSet, nameof(npcModelSet));
      Assert.IsNotNull(npcWeaponSet, nameof(npcWeaponSet));
      Assert.IsNotNull(impactsSet, nameof(impactsSet));
      _npcModelSet = npcModelSet;
      _npcWeaponSet = npcWeaponSet;
      _impactsSet = impactsSet;
    }

    public void ChangeNpcModelSet([NotNull] INpcModelSet npcModelSet) {
      Assert.IsNotNull(npcModelSet, nameof(npcModelSet));
      _npcModelSet = npcModelSet;
    }

    [NotNull]
    public NonPlayerCharacter Build(int id) {
      INpcModel model = _npcModelSet.GetNpcModel(id);
      Assert.IsNotNull(model, nameof(model));

      NpcMetadataModel npcMetadataModel = model.GetNpcMetadataModel();
      NpcMoralityModel npcMoralityModel = model.GetNpcMoralityModel();
      NpcEquipmentsModel npcEquipmentsModel = model.GetNpcEquipmentModel();
      NpcCombatAttributesModel npcCombatAttributesModel = model.GetNpcCombatAttributesModel();

      var npcMetadata = new NpcMetadata(npcMetadataModel);
      var npcMorality = new NpcMorality(npcMoralityModel);
      var npcCombatAttributes = new NpcCombatAttributes(npcCombatAttributesModel);
      WeaponSet weaponSet = GetWeaponSet(npcEquipmentsModel.WeaponsIdList);
      ArmorClass armorClass = GetArmorClass(npcEquipmentsModel.ClassOfArmor);
      var npcEquipments = new NpcEquipments(weaponSet, armorClass);

      var npc = new NonPlayerCharacter(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments);

      return npc;
    }

    [NotNull]
    private ArmorClass GetArmorClass(int classOfArmor) {
      return new ArmorClass(classOfArmor);
    }

    [NotNull]
    private WeaponSet GetWeaponSet([CanBeNull] IEnumerable<int> weaponIdList) {
      return new WeaponSet(GetListOfWeapon(weaponIdList));
    }

    [CanBeNull]
    private List<Weapon> GetListOfWeapon([CanBeNull] IEnumerable<int> weaponIdList) {
      if (weaponIdList == null) {
        return new List<Weapon>();
      }

      var npcWeapons = new List<Weapon>();
      foreach (Weapon npcWeapon in weaponIdList.Select(weaponId => _npcWeaponSet.GetNpcWeapon(weaponId))) {
        Assert.IsNotNull(npcWeapon, nameof(npcWeapon));
        npcWeapons.Add(npcWeapon);
      }

      return npcWeapons;
    }

    private int GetRiskPointsAfterDiceRoll(int _riskPoints, int liveDices) {
      if (IsFixedValueOfNumberOfRiskPoints()) {
        return _riskPoints;
      }

      Dices[] dices = GetDices(liveDices);

      DiceBox diceBox = GetDiceBox(dices);
      return diceBox.SumRollsOfDice();

      bool IsFixedValueOfNumberOfRiskPoints() {
        return _riskPoints != 0;
      }
    }

    private Dices[] GetDices(int lifeDice) {
      var dices = new Dices[lifeDice];
      for (var i = 0; i < dices.Length; i++) {
        dices[i] = GetLifeDiceForRoll();
      }

      return dices;
    }

    private RiskPoints GetNpcRiskPoints(int riskPoints, int lifeDice) {
      var npcRiskPoints = new NpcRiskPoints();
      return new RiskPoints(npcRiskPoints, GetRiskPointsAfterDiceRoll(riskPoints, lifeDice));
    }

    private List<Impact<ImpactOnRiskPoints>> GetListOfImpacts([CanBeNull] IEnumerable<int> impactsId) {
      if (impactsId == null) {
        return new List<Impact<ImpactOnRiskPoints>>();
      }

      var impacts = new List<Impact<ImpactOnRiskPoints>>();
      foreach (Impact<ImpactOnRiskPoints> impact in impactsId.Select(impactId => _impactsSet.GetImpactOnRiskPoints(impactId))) {
        Assert.IsNotNull(impact, nameof(impact));
        impacts.Add(impact);
      }

      return impacts;
    }

    private int GetNumberOfAttacks(List<Weapon> listOfWeapons, List<Impact<ImpactOnRiskPoints>> listOfImpacts, bool deadlyAttack, bool _attackEveryAtOnce) {
      if (listOfWeapons.Count <= 0 || deadlyAttack) {
        return 1;
      }

      if (listOfWeapons.Count > 0 && _attackEveryAtOnce) {
        return listOfWeapons.Count;
      }

      if (listOfWeapons.Count <= 0 && listOfImpacts != null && listOfImpacts.Count != 0) {
        return listOfWeapons.Count;
      }

      return 0;
    }
  }
}