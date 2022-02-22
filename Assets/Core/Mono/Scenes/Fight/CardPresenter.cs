using System.Collections.Generic;
using Core.Main.Character;
using Core.Main.NonPlayerCharacters;
using Core.Mono.Scenes.QualityDiceRoll;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Mono.Scenes.Fight {
  public class CardPresenter : MonoBehaviour {
    [SerializeField]
    private GameObject _cardPrefab;
    [SerializeField]
    private Transform _anchorForPlayer;
    [SerializeField]
    private Transform _anchorForNpc;
    [SerializeField]
    private PlayerBuilder _playerBuilder;
    [FormerlySerializedAs("_npcBuilder"), SerializeField]
    private NpcHolder _npcHolder;
    [SerializeField]
    private Button _createCards;
    [SerializeField]
    private List<Sprite> _sprites;
    [SerializeField]
    private List<CardView> _cardViews;

    private List<BaseCharacter> _playerCharacterList;
    private List<NonPlayerCharacter> _npcList;
    private BaseCharacter _baseCharacter;
    private NonPlayerCharacter _nonPlayerCharacter;

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    public void SpawnCards() {
      _baseCharacter = _playerBuilder.BaseCharacter;
      _nonPlayerCharacter = _npcHolder.NonPlayerCharacter;
      _cardViews = new List<CardView>();
      GameObject cardPlayer = Instantiate(_cardPrefab, _anchorForPlayer);
      _cardViews.Add(cardPlayer.GetComponent<CardView>());
      _cardViews[0].SetFieldsInCard(_sprites[0], _baseCharacter.Name, _baseCharacter.RiskPoints.GetPoints(), _baseCharacter.Armor.GetArmorClass());
      GameObject cardNpc = Instantiate(_cardPrefab, _anchorForNpc);
      _cardViews.Add(cardNpc.GetComponent<CardView>());
      _cardViews[1].SetFieldsInCard(_sprites[1], _nonPlayerCharacter.GetName(), _nonPlayerCharacter.GetPointsOfRisk(), _nonPlayerCharacter.GetClassOfArmor());
    }

    private void AddListeners() {
      _createCards.onClick.AddListener(SpawnCards);
    }

    private void RemoveListeners() {
      _createCards.onClick.RemoveListener(SpawnCards);
    }
  }
}