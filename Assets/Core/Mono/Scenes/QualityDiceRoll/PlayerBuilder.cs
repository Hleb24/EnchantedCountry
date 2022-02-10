using System.Threading.Tasks;
using Aberrance.Extensions;
using Core.Main.Character;
using Core.Main.Character.Equipment;
using Core.Main.Character.Levels;
using Core.Main.Character.Qualities;
using Core.Main.GameRule;
using Core.Main.GameRule.EquipmentIdConstants;
using Core.Main.GameRule.Points;
using Core.Mono.MainManagers;
using Core.SO.Product;
using Core.SO.Storage;
using Core.Support.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.QualityDiceRoll {
  public class PlayerBuilder : MonoBehaviour {
    [FormerlySerializedAs("_storageObject"),FormerlySerializedAs("_storageSo"), SerializeField]
    private StorageSO _storageSO;
    [SerializeField]
    private PlayerCharacter _playerCharacter;
    [SerializeField]
    private Button _createPlayer;
    [SerializeField]
    private bool _buildOnStart;
    private IStartGame _startGame;
    private IEquipment _equipments;
    private IEquipmentUsed _equipmentUsed;
    private IGamePoints _gamePoints;
    private IRiskPoints _riskPoints;
    private IQualityPoints _qualityPoints;
    private IClassType _classType;
    private IWallet _wallet;

    private void Start() {
      WaitLoad();
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    [Inject]
    public void Constructor(IStartGame startGame, IEquipment equipment, IEquipmentUsed equipmentUsed, IGamePoints gamePoints, IRiskPoints riskPoints, IQualityPoints qualityPoints,
      IClassType classType, IWallet wallet) {
      _startGame = startGame;
      _equipments = equipment;
      _equipmentUsed = equipmentUsed;
      _gamePoints = gamePoints;
      _riskPoints = riskPoints;
      _qualityPoints = qualityPoints;
      _classType = classType;
      _wallet = wallet;
    }

    public void BuildPlayer() {
      _playerCharacter = new PlayerCharacter(GetCharacterQualities(), GetCharacterType(), GetLevels(), GetGamePoints(), GetRiskPoints(), GetWallet(), GetEquipmentsOfCharacter(),
        GetEquipmentsUsed(), GetArmor(), GetShield(), GetRangeWeapon(), GetMeleeWeapon(), GetProjectiles());
      Debug.LogWarning("Player create " + (_playerCharacter.NotNull()));
    }

    private async void WaitLoad() {
      while (_startGame.StillInitializing()) {
        await Task.Yield();
      }

      if (_buildOnStart) {
       BuildPlayer();
      }
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
      return _wallet;
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
        ProductSO armorSO = _storageSO.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
        Armor armor = armorSO.GetArmor();
        return armor;
      }

      ProductSO noArmorProduct = _storageSO.GetArmorFromList(EquipmentIdConstants.NO_ARMOR_ID);
      Armor noArmor = noArmorProduct.GetArmor();
      return noArmor;
    }

    private Armor GetShield() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId) != 0) {
        ProductSO shieldSO = _storageSO.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId));
        Armor shield = shieldSO.GetArmor();
        return shield;
      }

      return null;
    }

    private Weapon GetMeleeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId) != 0) {
        ProductSO weaponSO = _storageSO.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId));
        Weapon weapon = weaponSO.GetWeapon();
        return weapon;
      }

      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId) != 0) {
        ProductSO weaponSO = _storageSO.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
        Weapon weapon = weaponSO.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetRangeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId) != 0) {
        ProductSO weaponSO = _storageSO.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
        Weapon weapon = weaponSO.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetProjectiles() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId) != 0) {
        ProductSO weaponSO = _storageSO.GetProjectilesFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
        Weapon weapon = weaponSO.GetWeapon();
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
      return _classType.GetClassType();
    }

    private CharacterQualities GetCharacterQualities() {
      var characterQualities = new CharacterQualities(QualityType.Strength, _qualityPoints.GetQualityPoints(QualityType.Strength), QualityType.Agility,
        _qualityPoints.GetQualityPoints(QualityType.Agility), QualityType.Constitution, _qualityPoints.GetQualityPoints(QualityType.Constitution), QualityType.Wisdom,
        _qualityPoints.GetQualityPoints(QualityType.Wisdom), QualityType.Courage, _qualityPoints.GetQualityPoints(QualityType.Courage));
      return characterQualities;
    }

    public PlayerCharacter PlayerCharacter {
      get {
        return _playerCharacter;
      }
    }
  }
}