using System.Collections.Generic;
using Core.Main.GameRule;
using Core.Main.NonPlayerCharacters;
using Core.Mono.Scenes.Fight;
using Core.SO.ImpactObjects;
using Core.SO.WeaponObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.Npc {
  [CreateAssetMenu(menuName = "NPC", fileName = "NPC", order = 58)]
  public class NpcSO : ScriptableObject, INpcModel {
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
    [FormerlySerializedAs("_riskPoints"), FormerlySerializedAs("RiskPoints"), SerializeField]
    private int _defaultRiskPoints;
    [FormerlySerializedAs("Morality"), SerializeField]
    private int _morality;
    [SerializeField]
    private List<WeaponSO> _weaponObjects;
    [FormerlySerializedAs("_weaponsIdList"), FormerlySerializedAs("_weaponId"), SerializeField]
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
    [FormerlySerializedAs("_attackEveryAtOnce"), FormerlySerializedAs("AttackEveryAtOnce"), SerializeField]
    private bool _attackWithAllWeapons;
    [FormerlySerializedAs("Id"), SerializeField]
    private int _id;
    private IRiskPoints _npcRiskPoints;

    public NpcEquipmentsModel GetNpcEquipmentModel() {
      return new NpcEquipmentsModel(_weaponsId, _classOfArmor);
    }

    public NpcCombatAttributesModel GetNpcCombatAttributesModel() {
      return new NpcCombatAttributesModel(_impactId, _defaultRiskPoints, _lifeDice, _attackWithAllWeapons, _deadlyAttack, _immortal);
    }

    public NpcMetadataModel GetNpcMetadataModel() {
      return new NpcMetadataModel(_id, _name, _description, _property, _experience, _alignment, _npcType);
    }

    public NpcMoralityModel GetNpcMoralityModel() {
      return new NpcMoralityModel(_morality, _immoral, _escapePossibility);
    }

    public int GetId() {
      return _id;
    }
  }
}