using System;
using System.Collections.Generic;

namespace Core {
  public static class DataDealer {
    private static Dictionary<Type, IData> _dataDict;
    private static Dictionary<Type, IFloatData> _floatDict;
    private static Dictionary<Type, IIntData> _intDict;

    public static void Init(Dictionary<Type, IData> data, Dictionary<Type, IFloatData> floatDict, Dictionary<Type, IIntData> intDict) {
      _dataDict = data;
      _floatDict = floatDict;
      _intDict = intDict;
    }

    public static T Peek<T>() {
      Type type = typeof(T);
      if (_dataDict.ContainsKey(type)) {
        return (T)_dataDict[type];
      }

      if (_floatDict.ContainsKey(type)) {
        return (T)_floatDict[type];
      }

      if (_intDict.ContainsKey(type)) {
        return (T)_intDict[type];
      }

      return default;
    }

    public static float GetFloat<T>(Enum dataType) where T : IFloatData {
      return Peek<T>().GetFloat(dataType);
    }

    public static int GetInt<T>(Enum dataType) where T : IIntData {
      return Peek<T>().GetInt(dataType);
    }

    public static void SetFloat<T>(Enum dataType, float value) where T : IFloatData {
      Peek<T>().SetFloat(dataType, value);
    }

    public static void SetInt<T>(Enum dataType, int value) where T : IIntData {
      Peek<T>().SetInt(dataType, value);
    }

    public static void IncreaseFloat<T>(Enum dataType, int value) where T : IFloatData {
      Peek<T>().IncreaseFloat(dataType, value);
    }

    public static void IncreaseInt<T>(Enum dataType, int value) where T : IIntData {
      Peek<T>().IncreaseInt(dataType, value);
    }
  }
}