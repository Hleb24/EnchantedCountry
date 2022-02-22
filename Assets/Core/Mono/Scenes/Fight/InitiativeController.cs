using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.Character;
using Core.Main.Dice;
using Core.Main.GameRule;
using Core.Main.NonPlayerCharacters;
using Core.Mono.Scenes.QualityDiceRoll;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Mono.Scenes.Fight {
  public class InitiativeController : MonoBehaviour {
    private static void SetInitiative(IInitiative initiative, int value) {
      initiative.Initiative = value;
    }

    public event Action InitiativeDiceRollComplete;
    [SerializeField]
    private PlayerBuilder _playerBuilder;
    [FormerlySerializedAs("_npcBuilder"), SerializeField]
    private NpcHolder _npcHolder;
    [SerializeField]
    private Button _diceRollForInitiative;

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    public void MoveIndexToBack() {
      if (CurrentIndexOfInitiativeList == 0) {
        CurrentIndexOfInitiativeList = InitiativeList.LastIndex();
        return;
      }

      CurrentIndexOfInitiativeList--;
    }

    public IInitiative GetIInitiative() {
      return InitiativeList[CurrentIndexOfInitiativeList];
    }

    private void AddListeners() {
      _diceRollForInitiative.onClick.AddListener(CreateListOfInitiative);
    }

    private void RemoveListeners() {
      _diceRollForInitiative.onClick.RemoveListener(CreateListOfInitiative);
    }

    private int DiceRollForInitiative(int bonus = 0) {
      return KitOfDice.DicesKit[KitOfDice.SET_WITH_ONE_TWELVE_SIDED_AND_ONE_SIX_SIDED_DICE].GetSumRollOfBoxDices() + bonus;
    }

    private void GetPlayerCharacter() {
      BaseCharacter = _playerBuilder.BaseCharacter;
    }

    private void GetNpc() {
      NonPlayerCharacter = _npcHolder.NonPlayerCharacter;
    }

    private void CreateListOfInitiative() {
      GetPlayerCharacter();
      GetNpc();
      SetInitiative(BaseCharacter, DiceRollForInitiative());
      SetInitiative(NonPlayerCharacter, DiceRollForInitiative());
      InitiativeList = new List<IInitiative> {
        BaseCharacter,
        NonPlayerCharacter
      };
      InitiativeList.Sort();
      Debug.Log("Initiative player " + BaseCharacter.Initiative);
      Debug.Log("Initiative npc " + NonPlayerCharacter.Initiative);
      foreach (IInitiative initiative in InitiativeList) {
        Debug.Log($"Initiative {initiative.Initiative}");
      }

      CurrentIndexOfInitiativeList = InitiativeList.LastIndex();
      InitiativeDiceRollComplete?.Invoke();
    }

    private int CurrentIndexOfInitiativeList { get; set; }

    private List<IInitiative> InitiativeList { get; set; }

    public BaseCharacter BaseCharacter { get; private set; }

    public NonPlayerCharacter NonPlayerCharacter { get; private set; }
  }
}