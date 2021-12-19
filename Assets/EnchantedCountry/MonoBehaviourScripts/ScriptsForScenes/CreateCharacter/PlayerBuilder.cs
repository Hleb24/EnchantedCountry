using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Levels;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.RiskPoints;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using Core.EnchantedCountry.ScriptableObject.ProductObject;
using Core.EnchantedCountry.ScriptableObject.Storage;
using Core.EnchantedCountry.SupportSystems.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter {
  public class PlayerBuilder : MonoBehaviour {
    [FormerlySerializedAs("_storageSO"), SerializeField]
    private StorageSO _storageSo;
    [SerializeField]
    private PlayerCharacter _playerCharacter;
    [SerializeField]
    private Button _createPlayer;
    // ReSharper disable once Unity.RedundantSerializeFieldAttribute
    [SerializeField]
    // ReSharper disable once NotAccessedField.Local
    private IEquipment _equipments;
    [SerializeField]
    private bool _buildOnStart;
    private IEquipmentUsed _equipmentUsed;
    private IGamePoints _gamePoints;
    private IRiskPoints _riskPoints;
    private IClassType _type;

    private void Awake() {
      _gamePoints = ScribeDealer.Peek<GamePointsScribe>();
      _riskPoints = ScribeDealer.Peek<RiskPointsScribe>();
      _type = ScribeDealer.Peek<ClassTypeScribe>();
    }

    private void Start() {
      if (_buildOnStart) {
        Invoke(nameof(BuildPlayer), 0.5f);
      }
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    public void BuildPlayer() {
      _equipments = ScribeDealer.Peek<EquipmentsScribe>();
      _equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      _playerCharacter = new PlayerCharacter(GetCharacterQualities(), GetCharacterType(), GetLevels(), GetGamePoints(), GetRiskPoints(), GetWallet(), GetEquipmentsOfCharacter(),
        GetEquipmentsUsed(), GetArmor(), GetShield(), GetRangeWeapon(), GetMeleeWeapon(), GetProjectiles());
    }

    private IGamePoints GetGamePoints() {
      return _gamePoints;
    }

    private Levels GetLevels() {
      var levels = new Levels(_gamePoints.GetPoints());
      return levels;
    }

    private RiskPoints GetRiskPoints() {
      return new RiskPoints(_riskPoints);
    }

    private IWallet GetWallet() {
      return ScribeDealer.Peek<WalletScribe>();
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      var equipmentsOfCharacter = new EquipmentsOfCharacter(_equipments.GetEquipmentCards());
      return equipmentsOfCharacter;
    }

    private IEquipmentUsed GetEquipmentsUsed() {
      return _equipmentUsed;
    }

    private Armor GetArmor() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId) != 0) {
        ProductSO armorSo = _storageSo.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
        Armor armor = armorSo.GetArmor();
        return armor;
      }

      return null;
    }

    private Armor GetShield() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId) != 0) {
        ProductSO shieldSo = _storageSo.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId));
        Armor shield = shieldSo.GetArmor();
        return shield;
      }

      return null;
    }

    private Weapon GetMeleeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetRangeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetProjectiles() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private void AddListeners() {
      _createPlayer.onClick.AddListener(BuildPlayer);
    }

    private void RemoveListeners() {
      _createPlayer.onClick.RemoveListener(BuildPlayer);
    }

    private ClassType GetCharacterType() {
      return _type.GetClassType();
    }

    private CharacterQualities GetCharacterQualities() {
      IQualityPoints qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      var characterQualities = new CharacterQualities(QualityType.Strength, qualityPoints.GetQualityPoints(QualityType.Strength), QualityType.Agility,
        qualityPoints.GetQualityPoints(QualityType.Agility), QualityType.Constitution, qualityPoints.GetQualityPoints(QualityType.Constitution), QualityType.Wisdom,
        qualityPoints.GetQualityPoints(QualityType.Wisdom), QualityType.Courage, qualityPoints.GetQualityPoints(QualityType.Courage));
      return characterQualities;
    }

    public PlayerCharacter PlayerCharacter {
      get {
        return _playerCharacter;
      }
    }
  }

  public class Builder {
    private const string StoragePath = "ForBuilder/Storage";
    private readonly StorageSO _storageSo;
    private readonly IGamePoints _gamePoints;
    private readonly IEquipmentUsed _equipmentUsed;
    private readonly IRiskPoints _riskPoints;
    private readonly IClassType _type;

    public Builder() {
      _storageSo = Resources.Load<StorageSO>(StoragePath);
      _gamePoints = ScribeDealer.Peek<GamePointsScribe>();
      _equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      _riskPoints = ScribeDealer.Peek<RiskPointsScribe>();
      _type = ScribeDealer.Peek<ClassTypeScribe>();
    }

    public PlayerCharacter BuildPlayer() {
      return new PlayerCharacter(GetCharacterQualities(), GetCharacterType(), GetLevels(), GetGamePoints(), GetRiskPoints(), GetWallet(), GetEquipmentsOfCharacter(),
        GetEquipmentsUsed(), GetArmor(), GetShield(), GetRangeWeapon(), GetMeleeWeapon(), GetProjectiles());
    }

    private IGamePoints GetGamePoints() {
      return _gamePoints;
    }

    private Levels GetLevels() {
      var levels = new Levels(_gamePoints.GetPoints());
      return levels;
    }

    private RiskPoints GetRiskPoints() {
      return new RiskPoints(_riskPoints);
    }

    private IWallet GetWallet() {
      return ScribeDealer.Peek<WalletScribe>();
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      IEquipment equipments = ScribeDealer.Peek<EquipmentsScribe>();
      var equipmentsOfCharacter = new EquipmentsOfCharacter(equipments.GetEquipmentCards());
      return equipmentsOfCharacter;
    }

    private IEquipmentUsed GetEquipmentsUsed() {
      return _equipmentUsed;
    }

    private Armor GetArmor() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId) != 0) {
        ProductSO armorSo = _storageSo.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
        Armor armor = armorSo.GetArmor();
        return armor;
      }

      return null;
    }

    private Armor GetShield() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId) != 0) {
        ProductSO shieldSo = _storageSo.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId));
        Armor shield = shieldSo.GetArmor();
        return shield;
      }

      return null;
    }

    private Weapon GetMeleeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetRangeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetProjectiles() {
      IEquipmentUsed equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private ClassType GetCharacterType() {
      return _type.GetClassType();
    }

    private CharacterQualities GetCharacterQualities() {
      IQualityPoints qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      var characterQualities = new CharacterQualities(QualityType.Strength, qualityPoints.GetQualityPoints(QualityType.Strength), QualityType.Agility,
        qualityPoints.GetQualityPoints(QualityType.Agility), QualityType.Constitution, qualityPoints.GetQualityPoints(QualityType.Constitution), QualityType.Wisdom,
        qualityPoints.GetQualityPoints(QualityType.Wisdom), QualityType.Courage, qualityPoints.GetQualityPoints(QualityType.Courage));
      return characterQualities;
    }

    public PlayerCharacter PlayerCharacter { get; }
  }
}