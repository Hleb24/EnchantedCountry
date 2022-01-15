using Core.Rule.GameRule.NPC;
using Core.ScriptableObject.NpcSet;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Mono.Scenes.Fight {
  public class NpcBuilder : MonoBehaviour {
    [FormerlySerializedAs("_setOfNpcSo"), FormerlySerializedAs("_setOfNpcSO"), SerializeField]
    private NonPlayerCharacterSet _nonPlayerCharacterSet;
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
      Npc = _nonPlayerCharacterSet.GetNpcFromList(_npcId);
    }

    private void BuildNpc(int id) {
      Npc = _nonPlayerCharacterSet.GetNpcFromList(id);
    }

    public Npc Npc { get; private set; }
  }
}