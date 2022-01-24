using Core.Main.NonPlayerCharacters;
using Core.SO.NpcSet;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Mono.Scenes.Fight {
  public class NpcBuilder : MonoBehaviour {
    [FormerlySerializedAs("_nonPlayerCharacterSet"),FormerlySerializedAs("_setOfNpcSo"), FormerlySerializedAs("_setOfNpcSO"), SerializeField]
    private NpcSetSO _npcSetSO;
    [SerializeField]
    private Button _createNpc;
    [SerializeField]
    private int _npcId;
    [SerializeField]
    private bool _buildOnStart;

    private void Start() {
      if (_buildOnStart) {
        BuildNpc();
      }
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    private void AddListeners() {
      _createNpc.onClick.AddListener(BuildNpc);
    }

    private void RemoveListeners() {
      _createNpc.onClick.RemoveListener(BuildNpc);
    }

    private void BuildNpc() {
      NonPlayerCharacter = _npcSetSO.GetNpcFromList(_npcId);
    }

    private void BuildNpc(int id) {
      NonPlayerCharacter = _npcSetSO.GetNpcFromList(id);
    }

    public NonPlayerCharacter NonPlayerCharacter { get; private set; }
  }
}