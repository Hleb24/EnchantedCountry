using System;
using System.Collections.Generic;
using Core.Support.SaveSystem.Scribe;
using UnityEngine;

namespace Core.Support.SaveSystem.SaveManagers {
  /// <summary>
  /// Класс для разрешения зависимостей с использованием классов реалзизующих интерфейс <see cref="IDealer"/>.
  /// </summary>
  public class Dealers {
    /// <summary>
    /// Получить метод для разрешения зависимость с использованием конкретной реализацией диллера.
    /// </summary>
    /// <typeparam name="T">Тип зависимости.</typeparam>
    /// <returns>Метод для разрешения зависимости.</returns>
    public static Func<IDealer, T>Resolve<T>() {
      return d => d.Peek<T>();
    }
  }
  
  /// <summary>
  /// Интерфейс для диллеров зависимостей.
  /// </summary>
  public interface IDealer {
    /// <summary>
    /// Получить зависимость согласно типу.
    /// </summary>
    /// <typeparam name="T">Тип зависимости.</typeparam>
    /// <returns>Требуемый экземпляр класса зависимости.</returns>
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