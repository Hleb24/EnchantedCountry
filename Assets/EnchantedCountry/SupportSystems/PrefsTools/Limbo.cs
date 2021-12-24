using UnityEngine;

namespace Core.EnchantedCountry.SupportSystems.PrefsTools {
  /// <summary>
  ///   Класс для работы с душами в Лимбо.
  /// </summary>
  /// <remarks>Лимбо как абстракция для префс, душы - абстакция данных.</remarks>
  public static class Limbo {
    /// <summary>
    ///   Войти в Лимбо.
    /// </summary>
    public static void Enter(string gateKey, int soul, bool change = false) {
      if (change) {
        PlayerPrefs.SetInt(gateKey, soul + PlayerPrefs.GetInt(gateKey, default));
        PlayerPrefs.Save();
      } else {
        PlayerPrefs.SetInt(gateKey, soul);
        PlayerPrefs.Save();
      }
    }

    /// <summary>
    ///   Войти в Лимбо.
    /// </summary>
    public static void Enter(string gateKey, float soul, bool change = false) {
      if (change) {
        PlayerPrefs.SetFloat(gateKey, soul + PlayerPrefs.GetFloat(gateKey, default));
        PlayerPrefs.Save();
      } else {
        PlayerPrefs.SetFloat(gateKey, soul);
        PlayerPrefs.Save();
      }
    }

    /// <summary>
    ///   Войти в Лимбо.
    /// </summary>
    public static void Enter(string gateKey, string soul, bool change = false, bool whiteSpace = false) {
      if (change) {
        string oldSoul = PlayerPrefs.GetString(gateKey, string.Empty);
        if (whiteSpace) {
          PlayerPrefs.SetString(gateKey, oldSoul + " " + soul);
          PlayerPrefs.Save();
          return;
        }

        PlayerPrefs.SetString(gateKey, oldSoul + soul);
        PlayerPrefs.Save();
      } else {
        PlayerPrefs.SetString(gateKey, soul);
        PlayerPrefs.Save();
      }
    }

    /// <summary>
    ///   Выйти с Лимбо.
    /// </summary>
    public static void GetOff(string gateKey, out int soul) {
      soul = PlayerPrefs.GetInt(gateKey, default);
    }

    /// <summary>
    ///   Выйти с Лимбо.
    /// </summary>
    public static void GetOff(string gateKey, out float soul) {
      soul = PlayerPrefs.GetFloat(gateKey, default);
    }

    /// <summary>
    ///   Выйти с Лимбо.
    /// </summary>
    public static void GetOff(string gateKey, out string soul) {
      soul = PlayerPrefs.GetString(gateKey, string.Empty);
    }
  }
}