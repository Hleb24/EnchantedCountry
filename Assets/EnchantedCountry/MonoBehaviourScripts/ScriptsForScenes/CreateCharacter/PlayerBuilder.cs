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
      _equipments = DataDealer.Peek<EquipmentsScribe>();
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

    private Wallet GetWallet() {
      var wallet = new Wallet(GSSSingleton.Instance.GetWalletData().NumberOfCoins);
      return wallet;
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      var equipmentsOfCharacter = new EquipmentsOfCharacter(_equipments.GetEquipmentCards());
      return equipmentsOfCharacter;
    }

    private EquipmentsUsed GetEquipmentsUsed() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      var equipmentsUsed = new EquipmentsUsed(equipmentUsedData.armorId, equipmentUsedData.shieldId, equipmentUsedData.oneHandedId, equipmentUsedData.twoHandedId,
        equipmentUsedData.rangeId, equipmentUsedData.projectiliesId, equipmentUsedData.bagId, equipmentUsedData.animalId, equipmentUsedData.carriageId);
      return equipmentsUsed;
    }

    private Armor GetArmor() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.armorId != 0) {
        ProductSO armorSo = _storageSo.GetArmorFromList(equipmentUsedData.armorId);
        Armor armor = armorSo.GetArmor();
        return armor;
      }

      return null;
    }

    private Armor GetShield() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.shieldId != 0) {
        ProductSO shieldSo = _storageSo.GetArmorFromList(equipmentUsedData.animalId);
        Armor shield = shieldSo.GetArmor();
        return shield;
      }

      return null;
    }

    private Weapon GetMeleeWeapon() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.oneHandedId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.oneHandedId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      if (equipmentUsedData.twoHandedId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.twoHandedId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetRangeWeapon() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.rangeId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.rangeId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetProjectiles() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.projectiliesId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.projectiliesId);
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

    private Wallet GetWallet() {
      var wallet = new Wallet(GSSSingleton.Instance.GetWalletData().NumberOfCoins);
      return wallet;
    }

    private EquipmentsOfCharacter GetEquipmentsOfCharacter() {
      IEquipment equipments = DataDealer.Peek<EquipmentsScribe>();
      var equipmentsOfCharacter = new EquipmentsOfCharacter(equipments.GetEquipmentCards());
      return equipmentsOfCharacter;
    }

    private EquipmentsUsed GetEquipmentsUsed() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      var equipmentsUsed = new EquipmentsUsed(equipmentUsedData.armorId, equipmentUsedData.shieldId, equipmentUsedData.oneHandedId, equipmentUsedData.twoHandedId,
        equipmentUsedData.rangeId, equipmentUsedData.projectiliesId, equipmentUsedData.bagId, equipmentUsedData.animalId, equipmentUsedData.carriageId);
      return equipmentsUsed;
    }

    private Armor GetArmor() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.armorId != 0) {
        ProductSO armorSo = _storageSo.GetArmorFromList(equipmentUsedData.armorId);
        Armor armor = armorSo.GetArmor();
        return armor;
      }

      return null;
    }

    private Armor GetShield() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.shieldId != 0) {
        ProductSO shieldSo = _storageSo.GetArmorFromList(equipmentUsedData.animalId);
        Armor shield = shieldSo.GetArmor();
        return shield;
      }

      return null;
    }

    private Weapon GetMeleeWeapon() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.oneHandedId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.oneHandedId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      if (equipmentUsedData.twoHandedId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.twoHandedId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetRangeWeapon() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.rangeId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.rangeId);
        Weapon weapon = weaponSo.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetProjectiles() {
      EquipmentUsedData equipmentUsedData = GSSSingleton.Instance;
      if (equipmentUsedData.projectiliesId != 0) {
        ProductSO weaponSo = _storageSo.GetWeaponFromList(equipmentUsedData.projectiliesId);
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