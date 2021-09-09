using System.Collections.Generic;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.GeneratingGameObjectsInScene {
  public class GeneratingGameObjectsInScene : MonoBehaviour {
    public GameObject UIObject;
    public List<GameObject> GameObjectsList = new List<GameObject>();
    public int Count;
    public Vector3 Scale = Vector3.one;
    public Transform Parent;
    public Vector2 StartPosition;
    public Vector2 Offset;
    public float EveryUIObjectOffset = 0.5f;
    public int Columns = 1;
    public int Rows = 1;
    public bool IsColumns;
    public bool IsRows;
    public float UIGameObjectGap = 3;

    [ContextMenu("Spawn Game Objects")]
    public void SpawnGameObjects() {
      if (GameObjectsList.Count >= Rows * Columns) {
        return;
      }

      for (var i = 0; i < Rows; ++i) {
        for (var j = 0; j < Columns; ++j) {
          GameObjectsList.Add(Instantiate(UIObject));
          GameObjectsList[GameObjectsList.Count - 1].transform.SetParent(Parent);
          GameObjectsList[GameObjectsList.Count - 1].transform.localScale = Scale;
        }
      }

      SetUIGameObjectVerticalOrHorizontalPositions();
    }

    private void SetUIGameObjectVerticalOrHorizontalPositions() {
      float x;
      float y;
      for (var i = 0; i < GameObjectsList.Count; i++) {
        var uiObject = GameObjectsList[i].GetComponent<RectTransform>();
        if (IsVerticalPositions()) {
          Offset.x = uiObject.rect.width * uiObject.transform.localScale.x;
          Offset.y = uiObject.rect.height * uiObject.transform.localScale.y - EveryUIObjectOffset * i;
          x = StartPosition.x;
          y = StartPosition.y - Offset.y;
          Vector3 anchoredAndLocalPosition = new Vector2(x, y);
          GameObjectsList[i].GetComponent<RectTransform>().anchoredPosition = anchoredAndLocalPosition;
          GameObjectsList[i].GetComponent<RectTransform>().localPosition = anchoredAndLocalPosition;
        }

        if (IsHorizontalPosition()) {
          Offset.x = uiObject.rect.width * uiObject.transform.localScale.x;
          Offset.y = uiObject.rect.height * uiObject.transform.localScale.y - EveryUIObjectOffset * i;
          x = StartPosition.x;
          y = StartPosition.y - Offset.y;
          Vector3 anchoredAndLocalPosition = new Vector2(x, y);
          GameObjectsList[i].GetComponent<RectTransform>().anchoredPosition = anchoredAndLocalPosition;
          GameObjectsList[i].GetComponent<RectTransform>().localPosition = anchoredAndLocalPosition;
        }
      }
    }

    private bool IsVerticalPositions() {
      return IsRows && !IsColumns;
    }

    private bool IsHorizontalPosition() {
      return IsColumns && !IsRows;
    }

    // ReSharper disable once UnusedMember.Local
    private void SetUIGameObjectVerticalPositions() {
      float x;
      float y;
      for (var i = 0; i < GameObjectsList.Count; i++) {
        var uiObject = GameObjectsList[i].GetComponent<RectTransform>();
        Offset.x = uiObject.rect.width * uiObject.transform.localScale.x;
        Offset.y = uiObject.rect.height * uiObject.transform.localScale.y - EveryUIObjectOffset * i;
        x = StartPosition.x;
        y = StartPosition.y - Offset.y;
        Vector3 anchoredAndLocalPosition = new Vector2(x, y);
        GameObjectsList[i].GetComponent<RectTransform>().anchoredPosition = anchoredAndLocalPosition;
        GameObjectsList[i].GetComponent<RectTransform>().localPosition = anchoredAndLocalPosition;
      }
    }

    // ReSharper disable once UnusedMember.Local
    private void SetUIGameObjectHorizontalPositions() {
      float x;
      float y;
      for (var i = 0; i < GameObjectsList.Count; i++) {
        var uiObject = GameObjectsList[i].GetComponent<RectTransform>();
        Offset.x = uiObject.rect.width * uiObject.transform.localScale.x + EveryUIObjectOffset * i;
        Offset.y = uiObject.rect.height * uiObject.transform.localScale.y;
        x = StartPosition.x + Offset.x;
        y = StartPosition.y;
        Vector3 anchoredAndLocalPosition = new Vector2(x, y);
        GameObjectsList[i].GetComponent<RectTransform>().anchoredPosition = anchoredAndLocalPosition;
        GameObjectsList[i].GetComponent<RectTransform>().localPosition = anchoredAndLocalPosition;
      }
    }

    // ReSharper disable once UnusedMember.Local
    private void SetUIGameObjectGridPositions() {
      var columnNumber = 0;
      var rowNumber = 0;
      Vector2 uiObjectGapNumber = Vector2.zero;
      var rowMoved = false;
      var uiObject = GameObjectsList[0].GetComponent<RectTransform>();
      Offset.x = uiObject.rect.width * uiObject.transform.localScale.x + EveryUIObjectOffset;
      Offset.y = uiObject.rect.height * uiObject.transform.localScale.y + EveryUIObjectOffset;
      foreach (GameObject uiGameObject in GameObjectsList) {
        if (columnNumber + 1 > Columns) {
          uiObjectGapNumber.x = 0;
          columnNumber = 0;
          rowNumber++;
          rowMoved = false;
        }

        float posXOffset = Offset.x + uiObjectGapNumber.x * UIGameObjectGap;
        float posYOffset = Offset.y;
        if (columnNumber > 0 && columnNumber % 3 == 0) {
          uiObjectGapNumber.x++;
          posXOffset += UIGameObjectGap;
        }

        if (rowNumber > 0 && rowNumber % 3 == 0 && rowMoved == false) {
          rowMoved = true;
          uiObjectGapNumber.y++;
          posYOffset += UIGameObjectGap;
        }

        float x = StartPosition.x + posXOffset;
        float y = StartPosition.y - posYOffset;
        Vector3 anchoredAndLocalPosition = new Vector2(x, y);
        uiGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredAndLocalPosition;
        uiGameObject.GetComponent<RectTransform>().localPosition = anchoredAndLocalPosition;
        columnNumber++;
      }
    }
  }
}