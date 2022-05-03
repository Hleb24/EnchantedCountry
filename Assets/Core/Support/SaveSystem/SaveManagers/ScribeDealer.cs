using System;
using System.Collections.Generic;
using Core.Support.SaveSystem.Scribe;
using UnityEngine;

namespace Core.Support.SaveSystem.SaveManagers {
  /// <summary>
  ///   Класс для разрешения зависимостей с использованием классов реалзизующих интерфейс <see cref="IDealer" />.
  /// </summary>
  public class Dealers {
    /// <summary>
    ///   Получить метод для разрешения зависимость с использованием конкретной реализацией диллера.
    /// </summary>
    /// <typeparam name="T">Тип зависимости.</typeparam>
    /// <returns>Метод для разрешения зависимости.</returns>
    public static Func<IDealer, T> Resolve<T>() {
      return d => d.Peek<T>();
    }

    /// <summary>
    ///   Получить метод для разрешения клона зависимости с использованием конкретной реализацией диллера.
    /// </summary>
    /// <typeparam name="T">Тип зависимости.</typeparam>
    /// <returns>Метод для разрешения клона зависимости.</returns>
    public static Func<IDealer, T> ResolveClone<T>() {
      return d => d.PeekClone<T>();
    }

    /// <summary>
    ///   Получить метод для разрешения отслеживаемого клона зависимости с использованием конкретной реализацией диллера.
    /// </summary>
    /// <typeparam name="T">Тип зависимости.</typeparam>
    /// <returns>Метод для разрешения отслеживаемого клона зависимости.</returns>
    public static Func<IDealer, T> ResolveCloneWithTracking<T>() {
      return d => d.PeekCloneWithTracking<T>();
    }
  }

  /// <summary>
  ///   Интерфейс для диллеров зависимостей.
  /// </summary>
  public interface IDealer {
    /// <summary>
    ///   Получить зависимость согласно типу.
    /// </summary>
    /// <typeparam name="T">Тип зависимости.</typeparam>
    /// <returns>Требуемый экземпляр класса зависимости.</returns>
    public T Peek<T>();

    /// <summary>
    ///   Получить клон зависимости согласно типу.
    /// </summary>
    /// <typeparam name="T">Тип зависимости.</typeparam>
    /// <returns>Требуемый отслеживаемый клон экземпляра класса зависимости.</returns>
    public T PeekClone<T>();

    /// <summary>
    ///   Получить отслеживаемый клон зависимости согласно типу.
    /// </summary>
    /// <typeparam name="T">Тип зависимости.</typeparam>
    /// <returns>Требуемый отслеживаемый клон экземпляра класса зависимости.</returns>
    public T PeekCloneWithTracking<T>();
  }

  /// <summary>
  ///   Класс доступа к данным сохранений игры.
  /// </summary>
  public class ScribeDealer : IDealer {
    private static Dictionary<Type, IScribe> _scribes;

    /// <summary>
    ///   Инициализировать коллекцию сохранений.
    /// </summary>
    /// <param name="scribes">Коллекция разрозненных данных, без подвязки к типам данных.</param>
    internal static void Init(Dictionary<Type, IScribe> scribes) {
      _scribes = scribes;
    }

    /// <summary>
    ///   Достать экземпляр класса сохранененных данных.
    /// </summary>
    /// <typeparam name="T">Тип класса сохранения данных.</typeparam>
    /// <returns>Экземпляр класса сохранененных данных</returns>
    private static T Peek<T>() {
      Type type = typeof(T);

      if (_scribes.ContainsKey(type)) {
        return (T)_scribes[type];
      }

      Debug.LogWarning("Тип данных не найден!");
      return default;
    }

    /// <summary>
    ///   Достать клон экземпляра класса сохранененных данных.
    /// </summary>
    /// <typeparam name="T">Тип класса сохранения данных.</typeparam>
    /// <returns>Клон экземпляра класса сохранененных данных</returns>
    private static T PeekClone<T>() {
      Type type = typeof(T);

      if (_scribes.ContainsKey(type)) {
        return _scribes[type].Clone<T>();
      }

      Debug.LogWarning("Тип данных не найден!");
      return default;
    }

    /// <summary>
    ///   Достать отслеживаемый клон экземпляр класса сохранененных данных.
    /// </summary>
    /// <typeparam name="T">Тип класса сохранения данных.</typeparam>
    /// <returns>Отслеживаемый клон экземпляра класса сохранененных данных</returns>
    private static T PeekCloneWithTracking<T>() {
      Type type = typeof(T);

      if (_scribes.ContainsKey(type)) {
        return _scribes[type].CloneWithTracking<T>();
      }

      Debug.LogWarning("Тип данных не найден!");
      return default;
    }

    T IDealer.PeekClone<T>() {
      return PeekClone<T>();
    }

    T IDealer.Peek<T>() {
      return Peek<T>();
    }

    T IDealer.PeekCloneWithTracking<T>() {
      return PeekCloneWithTracking<T>();
    }
  }
}