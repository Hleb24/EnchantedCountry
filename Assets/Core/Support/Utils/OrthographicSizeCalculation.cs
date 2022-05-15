using System;
using UnityEngine;

namespace Core.Support.Utils {
  [RequireComponent(typeof(Camera))]
  public class OrthographicSizeCalculation : MonoBehaviour {
    [SerializeField]
    private int _pixelToWorldUnits = 100;
    private Camera _camera;

    private void Awake() {
      _camera = GetComponent<Camera>();
    }

    private void OnEnable() {
      CalculateOrthographicSize();
    }

    private void CalculateOrthographicSize() {
      _camera.orthographicSize = Screen.height / 2f / _pixelToWorldUnits;
    }
  }
}