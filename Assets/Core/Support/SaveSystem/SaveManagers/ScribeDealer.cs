using System;
using System.Collections.Generic;
using Core.Support.SaveSystem.Scribe;
using UnityEngine;

namespace Core.Support.SaveSystem.SaveManagers {
  public interface IDealer {
    public T Peek<T>();
  }

  /// <summary>
  ///   Класс доступа к данным сохранений игры.
  /// </summary>
  public class ScribeDealer: IDealer {
    private static Dictionary<Type, IScribe> _scribes;

    /// <summary>
    ///   Инициализировать коллекцию сохранений.
    /// </summary>
    /// <param name="scribes">Коллекция разрозненных данных, без подвязки к типам данных.</param>
    public static void Init(Dictionary<Type, IScribe> scribes) {
      _scribes = scribes;
    }

    /// <summary>
    ///   Достать экземпляр класса сохранененных данных.
    /// </summary>
    /// <typeparam name="T">Тип класса сохранения данных.</typeparam>
    /// <returns>Экземпляр класса сохранененных данных</returns>
    public static T Peek<T>() {
      Type type = typeof(T);

      if (_scribes.ContainsKey(type)) {
        return (T)_scribes[type];
      }

      Debug.LogWarning("Тип данных не найден!");
      return default;
    }

    T IDealer.Peek<T>() {
      return Peek<T>();
    }
  }
}