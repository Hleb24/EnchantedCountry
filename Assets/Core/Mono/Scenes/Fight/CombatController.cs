using System;
using Aberrance.Extensions;
using Core.Main.Character;
using Core.Main.Dice;
using Core.Main.GameRule.Initiative;
using Core.Main.NonPlayerCharacters;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Core.Mono.Scenes.Fight {
  public class CombatController : MonoBehaviour {
    public static event Action<string, float> AttackButtonClicked;
    [SerializeField]
    private Button _attack;
    [SerializeField]
    private InitiativeController _initiativeController;
    private PlayerCharacter _playerCharacter;
    private NonPlayerCharacter _nonPlayerCharacter;

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    private void AddListeners() {
      _initiativeController.InitiativeDiceRollComplete += OnInitiativeDiceRollComplete;
      _attack.onClick.AddListener(OnAttackButtonClicked);
    }

    private void RemoveListeners() {
      _initiativeController.InitiativeDiceRollComplete -= OnInitiativeDiceRollComplete;
      _attack.onClick.RemoveListener(OnAttackButtonClicked);
    }

    private void OnInitiativeDiceRollComplete() {
      _playerCharacter = _initiativeController.PlayerCharacter;
      _nonPlayerCharacter = _initiativeController.NonPlayerCharacter;
    }

    private int GetDiceRollValue(int accuracy = 0) {
      return KitOfDice.DiceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice() + accuracy;
    }

    private bool IsPlayerCharacter(IInitiative initiative) {
      return _playerCharacter == initiative;
    }

    private bool IsNpc(IInitiative initiative) {
      return _nonPlayerCharacter == initiative;
    }

    private void OnAttackButtonClicked() {
      IInitiative initiative = _initiativeController.GetIInitiative();
      if (IsPlayerCharacter(initiative)) {
        PlayerTurn();
      } else if (IsNpc(initiative)) {
        NpcTurn();
      }

      _initiativeController.MoveIndexToBack();
    }

    private void PlayerTurn() {
      if (_playerCharacter.MeleeWeapon.NotNull()) {
        int diceRollValue = GetDiceRollValue(_playerCharacter.GetMeleeAccuracy());
        Debug.Log($"<color=orange>{_playerCharacter.ClassType}</color>: атака ближним оружием.");
        float meleeDamage = _playerCharacter.GetMeleeDamage();
        if (_nonPlayerCharacter.GetDamaged(diceRollValue, meleeDamage, _playerCharacter.MeleeWeapon.Id, _playerCharacter.MeleeWeapon.weaponType)) {
          Debug.Log(
            $"<color=orange>{_playerCharacter.ClassType}</color>: значения бросков кубика <color=orange>{diceRollValue}</color> - <color=orange>{meleeDamage}</color> урон(а), тип оружия - <color=orange>{_playerCharacter.MeleeWeapon.weaponType}</color>.");
          AttackButtonClicked?.Invoke(_nonPlayerCharacter.GetName(), _nonPlayerCharacter.GetPointsOfRisk());
        }
      }
    }

    private void NpcTurn() {
      if (_nonPlayerCharacter.IsHasNoNumberOfAttack()) {
        return;
      }

      if (_nonPlayerCharacter.IsAttackWithAllWeapons()) {
        for (var i = 0; i < _nonPlayerCharacter.GetNumberOfAttack(); i++) {
          int index = i;
          int diceRollValue = GetDiceRollValue(_nonPlayerCharacter.Accuracy());
          float damage = _nonPlayerCharacter.ToDamage(diceRollValue, _playerCharacter, index);
          Debug.Log($"<color=magenta>{_nonPlayerCharacter.GetName()}</color>: атакует всем одновременно.");
          if (_playerCharacter.GetDamaged(diceRollValue, damage)) {
            Debug.Log(
              $"<color=magenta>{_nonPlayerCharacter.GetName()}</color>: значение броска кубиков <color=magenta>{diceRollValue}</color> - <color=magenta>{damage}</color> урон(а).");
            AttackButtonClicked?.Invoke(_playerCharacter.Name, _playerCharacter.RiskPoints.GetPoints());
          }
        }
      } else if (_nonPlayerCharacter.IsAttackWithOneWeapon()) {
        int weapon = Random.Range(0, _nonPlayerCharacter.GetNumberOfWeapon());
        int diceRollValue = GetDiceRollValue(_nonPlayerCharacter.Accuracy());
        float damage = _nonPlayerCharacter.ToDamage(diceRollValue, _playerCharacter, weapon);
        Debug.Log($"<color=magenta>{_nonPlayerCharacter.GetName()}</color>: одна атака.");
        if (_playerCharacter.GetDamaged(diceRollValue, damage)) {
          Debug.Log(
            $"<color=magenta>{_nonPlayerCharacter.GetName()}</color>: значение броска кубиков <color=magenta>{diceRollValue}</color> - <color=magenta>{damage}</color> урон(а).");
          AttackButtonClicked?.Invoke(_playerCharacter.Name, _playerCharacter.RiskPoints.GetPoints());
        }
      }
    }
  }
}