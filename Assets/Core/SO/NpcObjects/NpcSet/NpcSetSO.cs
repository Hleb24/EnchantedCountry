using System.Collections.Generic;
using Core.Mono.Scenes.Fight;
using UnityEngine;

namespace Core.SO.NpcObjects.NpcSet {
  [CreateAssetMenu(menuName = "SetOfNpcSO", fileName = "SetOfNpcSO", order = 61)]
  public class NpcSetSO : ScriptableObject, INpcModelSet {
    [SerializeField]
    private List<NpcSO> _npcSoList;

    public INpcModel GetNpcModel(int id) {
      foreach (NpcSO npcSO in _npcSoList) {
        if (npcSO.GetId() == id) {
          return npcSO;
        }
      }

      Debug.LogWarning("Модель Npc не найдена!");
      return null;
    }
  }
}