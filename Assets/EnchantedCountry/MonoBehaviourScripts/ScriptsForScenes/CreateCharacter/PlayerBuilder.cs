using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.GamePoints;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Levels;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.CoreEnchantedCountry.Character.Wallet;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.RiskPoints;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
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

    private GamePoints GetGamePoints() {
      var gamePoints = new GamePoints(GSSSingleton.Instance.GetGamePointsData().Points);
      return gamePoints;
    }

    private Levels GetLevels() {
      var levels = new Levels(GSSSingleton.Instance.GetGamePointsData().Points);
      return levels;
    }

    private RiskPoints GetRiskPoints() {
      var riskPoints = new RiskPoints(GSSSingleton.Instance.GetRiskPointsData().riskPoints);
      return riskPoints;
    }

    private IWallet GetWallet() {
      return ScribeDealer.Peek<WalletScribe>();
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      var equipmentsOfCharacter = new EquipmentsOfCharacter(_equipments.GetEquipmentCards());
      return equipmentsOfCharacter;
    }

    private EquipmentsUsed GetEquipmentsUsed() {
      var equipmentsUsed = new EquipmentsUsed(_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId), _equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId),
        _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId), _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId), _equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId),
        _equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId), _equipmentUsed.GetEquipment(EquipmentsUsedId.BagId), _equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId),
        _equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId));
      return equipmentsUsed;
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

    private CharacterType GetCharacterType() {
      if (Enum.TryParse(GSSSingleton.Instance.GetClassOfCharacterData().nameOfClass, out CharacterType characterType)) {
        return characterType;
      }

      return default;
    }

    private CharacterQualities GetCharacterQualities() {
      QualitiesData qualitiesData = GSSSingleton.Instance;
      var characterQualities = new CharacterQualities(Quality.QualityType.Strength, qualitiesData.strength, Quality.QualityType.Agility, qualitiesData.agility,
        Quality.QualityType.Constitution, qualitiesData.constitution, Quality.QualityType.Wisdom, qualitiesData.wisdom, Quality.QualityType.Courage, qualitiesData.courage);
      return characterQualities;
    }

    public PlayerCharacter PlayerCharacter {
      get {
        return _playerCharacter;
      }
    }
  }

  public class Builder {
    private const string STORAGE_PATH = "ForBuilder/Storage";
    private readonly StorageSO _storageSo;

    public Builder() {
      _storageSo = Resources.Load<StorageSO>(STORAGE_PATH);
    }

    public PlayerCharacter BuildPlayer() {
      return new PlayerCharacter(GetCharacterQualities(), GetCharacterType(), GetLevels(), GetGamePoints(), GetRiskPoints(), GetWallet(), GetEquipmentsOfCharacter(),
        GetEquipmentsUsed(), GetArmor(), GetShield(), GetRangeWeapon(), GetMeleeWeapon(), GetProjectiles());
    }

    private GamePoints GetGamePoints() {
      var gamePoints = new GamePoints(GSSSingleton.Instance.GetGamePointsData().Points);
      return gamePoints;
    }

    private Levels GetLevels() {
      var levels = new Levels(GSSSingleton.Instance.GetGamePointsData().Points);
      return levels;
    }

    private RiskPoints GetRiskPoints() {
      var riskPoints = new RiskPoints(GSSSingleton.Instance.GetRiskPointsData().riskPoints);
      return riskPoints;
    }

    private IWallet GetWallet() {
      return ScribeDealer.Peek<WalletScribe>();
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      IEquipment equipments = ScribeDealer.Peek<EquipmentsScribe>();
      var equipmentsOfCharacter = new EquipmentsOfCharacter(equipments.GetEquipmentCards());
      return equipmentsOfCharacter;
    }

    private EquipmentsUsed GetEquipmentsUsed() {
      IEquipmentUsed equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      var equipmentsUsed = new EquipmentsUsed(equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId), equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId),
        equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId), equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId), equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId),
        equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId), equipmentUsed.GetEquipment(EquipmentsUsedId.BagId), equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId),
        equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId));
      return equipmentsUsed;
    }

    private Armor GetArmor() {
      IEquipmentUsed equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      if (equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId) != 0) {
        ProductSO armorSo = _storageSo.GetArmorFromList(equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
        Armor armor = armorSo.GetArmor();
        return armor;
      }

      return null;
    }

    private Armor GetShield() {
      IEquipmentUsed equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      if (equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId) != 0) {
        ProductSO shieldSo = _storageSo.GetArmorFromList(equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId));
        Armor shield = shieldSo.GetArmor();
        return shield;
      }

      return null;
    }

    private Weapon GetMeleeWeapon() {
      IEquipmentUsed equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();

      if (equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      if (equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetRangeWeapon() {
      IEquipmentUsed equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      if (equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetProjectiles() {
      IEquipmentUsed equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      if (equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId) != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private CharacterType GetCharacterType() {
      if (Enum.TryParse(GSSSingleton.Instance.GetClassOfCharacterData().nameOfClass, out CharacterType characterType)) {
        return characterType;
      }

      return default;
    }

    private CharacterQualities GetCharacterQualities() {
      QualitiesData qualitiesData = GSSSingleton.Instance;
      var characterQualities = new CharacterQualities(Quality.QualityType.Strength, qualitiesData.strength, Quality.QualityType.Agility, qualitiesData.agility,
        Quality.QualityType.Constitution, qualitiesData.constitution, Quality.QualityType.Wisdom, qualitiesData.wisdom, Quality.QualityType.Courage, qualitiesData.courage);
      return characterQualities;
    }

    public PlayerCharacter PlayerCharacter { get; }
  }
}