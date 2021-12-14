using System;
using System.Collections.Generic;

namespace Core {
  /// <summary>
  ///   Класс доступа к данным сохранений игры.
  /// </summary>
  public static class DataDealer {
    private static Dictionary<Type, ICommonData> _commonDict;
    private static Dictionary<Type, IFloatData> _floatDict;
    private static Dictionary<Type, IIntData> _intDict;
    private static Dictionary<Type, IStringData> _stringDict;
    private static Dictionary<Type, IScribe> _hollowDict;

    /// <summary>
    ///   Инициализировать коллекцию сохранений.
    /// </summary>
    /// <param name="commonDict">Коллекция целочисленных, с плавающей точкой чисел и строковых данных.</param>
    /// <param name="floatDict">Коллекция данных с плавающей точкой.</param>
    /// <param name="intDict">Коллекция целочисленных данных.</param>
    /// <param name="stringDict">Коллекция строковых данных.</param>
    /// <param name="hollowDict">Коллекция разрозненных данных, без подвязки к типам данных.</param>
    public static void Init(Dictionary<Type, ICommonData> commonDict, Dictionary<Type, IFloatData> floatDict, Dictionary<Type, IIntData> intDict, Dictionary<Type, IStringData> stringDict, Dictionary<Type, IScribe> hollowDict) {
      _commonDict = commonDict;
      _floatDict = floatDict;
      _intDict = intDict;
      _stringDict = stringDict;
      _hollowDict = hollowDict;
    }

    /// <summary>
    ///   Достать экземпляр класса сохранененных данных.
    /// </summary>
    /// <typeparam name="T">Тип класса сохранения данных.</typeparam>
    /// <returns>Экземпляр класса сохранененных данных</returns>
    public static T Peek<T>() {
      Type type = typeof(T);
      if (_commonDict.ContainsKey(type)) {
        return (T)_commonDict[type];
      }

      if (_floatDict.ContainsKey(type)) {
        return (T)_floatDict[type];
      }

      if (_intDict.ContainsKey(type)) {
        return (T)_intDict[type];
      }
      
      if (_stringDict.ContainsKey(type)) {
        return (T)_stringDict[type];
      }
      
      if (_hollowDict.ContainsKey(type)) {
        return (T)_hollowDict[type];
      }

      return default;
    }

    /// <summary>
    ///   Получить значение сохраненных данных.
    /// </summary>
    public static float GetFloat<T>(Enum dataType) where T : IFloatData {
      return Peek<T>().GetFloat(dataType);
    }

    /// <summary>
    ///   Получить значение сохраненных данных.
    /// </summary>
    public static int GetInt<T>(Enum dataType) where T : IIntData {
      return Peek<T>().GetInt(dataType);
    }
    
    /// <summary>
    ///   Получить значение сохраненных данных.
    /// </summary>
    public static string GetString<T>(Enum dataType) where T : IStringData {
      return Peek<T>().GetString(dataType);
    }

    /// <summary>
    ///   Установить значение сохраненных данных.
    /// </summary>
    public static void SetFloat<T>(Enum dataType, float value) where T : IFloatData {
      Peek<T>().SetFloat(dataType, value);
    }

    /// <summary>
    ///   Установить значение сохраненных данных.
    /// </summary>
    public static void SetInt<T>(Enum dataType, int value) where T : IIntData {
      Peek<T>().SetInt(dataType, value);
    }
    
    /// <summary>
    ///   Установить значение сохраненных данных.
    /// </summary>
    public static void SetString<T>(Enum dataType, string value) where T : IStringData {
      Peek<T>().SetString(dataType, value);
    }

    /// <summary>
    ///   Увеличить/уменьштиь значение сохраненных данных.
    /// </summary>
    public static void IncreaseFloat<T>(Enum dataType, int value) where T : IFloatData {
      Peek<T>().IncreaseFloat(dataType, value);
    }

    /// <summary>
    ///   Увеличить/уменьштиь значение сохраненных данных.
    /// </summary>
    public static void IncreaseInt<T>(Enum dataType, int value) where T : IIntData {
      Peek<T>().IncreaseInt(dataType, value);
    }
    
    /// <summary>
    ///   Установить значение сохраненных данных.
    /// </summary>
    public static void IncreaseString<T>(Enum dataType, string value) where T : IStringData {
      Peek<T>().IncreaseString(dataType, value);
    }
  }
}