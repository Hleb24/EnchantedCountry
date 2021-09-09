using System;
using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.CoreEnchantedCountry.Dice;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Initiative;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC;
using Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.FightScene {
  public class InitiativeController : MonoBehaviour {
    public void MoveIndexToBack() {
      if (CurrentIndexOfInitiativeList == 0) {
        CurrentIndexOfInitiativeList = InitiativeList.Count - 1;
        return;
      }

      CurrentIndexOfInitiativeList--;
    }

    #region FIELDS
    public  event Action InitiativeDiceRollComplete;
    [SerializeField]
    private PlayerBuilder _playerBuilder;
    [SerializeField]
    private NpcBuilder _npcBuilder;
    [SerializeField]
    private Button _diceRollForInitiative;
    #endregion

    #region MONOBEHAVIOR_METHODS
    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }
    #endregion

    #region HANDLERS
    private void AddListeners() {
      _diceRollForInitiative.onClick.AddListener(CreateListOfInitiative);
    }

    private void RemoveListeners() {
      _diceRollForInitiative.onClick.RemoveListener(CreateListOfInitiative);
    }
    #endregion

    #region GET_INITIATIVE
    public IInitiative GetIInitiative() {
      return InitiativeList[CurrentIndexOfInitiativeList];
    }
    
    private int DiceRollForInitiative(int bonus = 0) {
      return KitOfDice.diceKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].SumRollsOfDice() + bonus;
    }

    private void GetPlayerCharacter() {
      PlayerCharacter = _playerBuilder.PlayerCharacter;
    }

    private void GetNpc() {
      Npc = _npcBuilder.Npc;
    }

    private void SetInitiative(IInitiative initiative, int value) {
      initiative.Initiative = value;
    }

    private void CreateListOfInitiative() {
      GetPlayerCharacter();
      GetNpc();
      SetInitiative(PlayerCharacter, DiceRollForInitiative());
      SetInitiative(Npc, DiceRollForInitiative());
      InitiativeList = new List<IInitiative>();
      InitiativeList.Add(PlayerCharacter);
      InitiativeList.Add(Npc);
      InitiativeList.Sort();
      Debug.Log("Initiative player " + PlayerCharacter.Initiative);
      Debug.Log("Initiative npc " + Npc.Initiative);
      foreach (IInitiative initiative in InitiativeList) {
        Debug.Log($"Initiative {initiative.Initiative}");
      }

      CurrentIndexOfInitiativeList = InitiativeList.Count - 1;
      InitiativeDiceRollComplete?.Invoke();
    }
    #endregion

    #region PROPERTIES
    public int CurrentIndexOfInitiativeList { get; private set; }

    public List<IInitiative> InitiativeList { get; private set; }

    public  PlayerCharacter PlayerCharacter { get; private set; }

    public  Npc Npc { get; private set; }
    #endregion
  }
}