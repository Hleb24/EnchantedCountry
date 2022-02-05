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
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Mono.Scenes.Fight {
  public class NpcBuilder {
    private readonly INpcWeaponSet _npcWeaponSet;
    private readonly IImpactsSet _impactsSet;
    private INpcModelSet _npcModelSet;

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

      NpcMetadata npcMetadata = GetNpcMetadata();
      NpcMorality npcMorality = GetNpcMorality();
      NpcCombatAttributes npcCombatAttributes = GetNpcCombatAttributes();
      NpcEquipments npcEquipments = GetNpcEquipments();

      var npc = new NonPlayerCharacter(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments);
      npc.PrepareNumberOfAttacks();
      return npc;

      NpcMetadata GetNpcMetadata() {
        Debug.LogWarning($"Имя npc {npcMetadataModel.Name}");
        return new NpcMetadata(npcMetadataModel);
      }

      NpcMorality GetNpcMorality() {
        return new NpcMorality(npcMoralityModel);
      }

      NpcCombatAttributes GetNpcCombatAttributes() {
        List<Impact<ImpactOnRiskPoints>> listOfImpacts = GetListOfImpacts(npcCombatAttributesModel.Impacts);

        int defaultRiskPoints = npcCombatAttributesModel.DefaultRiskPoints;
        int lifeDice = npcCombatAttributesModel.LifeDice;
        RiskPoints npcRiskPoints = NpcRiskPointsBuilder.Build(defaultRiskPoints, lifeDice);

        bool attackWithAllWeapons = npcCombatAttributesModel.AttackWithAllWeapons;
        bool deadlyAttack = npcCombatAttributesModel.DeadlyAttack;
        bool isImmortal = npcCombatAttributesModel.IsImmortal;
        Debug.LogWarning($"Количество очков риска npc {npcRiskPoints.GetPoints()}");
        Debug.LogWarning($"Количество воздействий npc {listOfImpacts.Count}");
        Debug.LogWarning($"Npc aтакует всех {attackWithAllWeapons}");
        Debug.LogWarning($"Смертельная атака у npc {deadlyAttack}");
        return new NpcCombatAttributes(listOfImpacts, npcRiskPoints, attackWithAllWeapons, deadlyAttack, isImmortal);
      }

      NpcEquipments GetNpcEquipments() {
        WeaponSet weaponSet = GetWeaponSet(npcEquipmentsModel.WeaponsIdList);
        ArmorClass armorClass = GetArmorClass(npcEquipmentsModel.ClassOfArmor);
        return new NpcEquipments(weaponSet, armorClass);
      }
    }

    [NotNull]
    private ArmorClass GetArmorClass(int classOfArmor) {
      return new ArmorClass(classOfArmor);
    }

    [NotNull]
    private WeaponSet GetWeaponSet([CanBeNull] IEnumerable<int> weaponIdList) {
      List<Weapon> weaponList = GetListOfWeapon(weaponIdList);
      return new WeaponSet(weaponList);
    }

    [NotNull, ItemNotNull]
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

    [NotNull, ItemNotNull]
    private List<Impact<ImpactOnRiskPoints>> GetListOfImpacts([NotNull] IEnumerable<int> impactsId) {
      var impacts = new List<Impact<ImpactOnRiskPoints>>();
      foreach (Impact<ImpactOnRiskPoints> impact in impactsId.Select(impactId => _impactsSet.GetImpactOnRiskPoints(impactId))) {
        Assert.IsNotNull(impact, nameof(impact));
        impacts.Add(impact);
      }

      return impacts;
    }

    private static class NpcRiskPointsBuilder {
      public static RiskPoints Build(int riskPoints, int lifeDice, DiceType diceType = DiceType.SixEdges) {
        Assert.IsTrue(riskPoints >= 0, nameof(riskPoints));
        Assert.IsTrue(lifeDice >= 0, nameof(lifeDice));
        var npcRiskPoints = new NpcRiskPoints();
        return new RiskPoints(npcRiskPoints, GetRiskPointsAfterDiceRoll(riskPoints, lifeDice, diceType));
      }

      private static int GetRiskPointsAfterDiceRoll(int riskPoints, int lifeDice, DiceType diceType) {
        if (IsFixedValueOfNumberOfRiskPoints()) {
          return riskPoints;
        }

        Dices[] dices = GetDices(lifeDice, diceType);

        DiceBox diceBox = GetDiceBox(dices);
        return diceBox.SumRollsOfDice();

        bool IsFixedValueOfNumberOfRiskPoints() {
          return riskPoints != 0;
        }
      }

      private static Dices[] GetDices(int lifeDice, DiceType diceType) {
        var dices = new Dices[lifeDice];
        for (var i = 0; i < dices.Length; i++) {
          dices[i] = GetLifeDiceForRoll(diceType);
        }

        return dices;
      }

      private static SixSidedDice GetLifeDiceForRoll(DiceType diceType) {
        return new SixSidedDice(diceType);
      }

      private static DiceBox GetDiceBox(Dices[] dices) {
        return new DiceBox(dices);
      }
    }
  }
}