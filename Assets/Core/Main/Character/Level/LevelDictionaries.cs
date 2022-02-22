using System.Collections.Generic;
using Core.Main.Character.Class;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Main.Character.Level {
  public class LevelDictionaries {
    private static readonly Dictionary<ClassType, List<int>> CharacterLevels = new() {
      [ClassType.Human] = new List<int> {
        0,
        1_000,
        2_000,
        3_000,
        4_000,
        5_000,
        6_000,
        7_000,
        8_500,
        9_000,
        10_000,
        11_000,
        12_000
      },
      [ClassType.Warrior] = new List<int> {
        0,
        2_000,
        4_000,
        5_500,
        7_000,
        9_000,
        11_000,
        12_000,
        13_500,
        15_000,
        17_000,
        20_000,
        25_000
      },
      [ClassType.Elf] = new List<int> {
        0,
        2_000,
        4_000,
        6_000,
        8_000,
        9_000,
        10_000,
        12_000,
        13_500,
        15_000,
        18_000,
        21_000,
        26_000
      },
      [ClassType.Wizard] = new List<int> {
        0,
        500,
        1_000,
        1_500,
        2_000,
        3_000,
        4_000,
        5_000,
        6_500,
        8_000,
        10_000,
        12_000,
        15_000
      },
      [ClassType.Kron] = new List<int> {
        0,
        1_000,
        2_000,
        3_000,
        4_000,
        5_500,
        7_000,
        8_500,
        10_000,
        12_000,
        14_000,
        16_000,
        19_000
      },
      [ClassType.Gnom] = new List<int> {
        0,
        1_400,
        2_300,
        4_000,
        7_000,
        10_000,
        12_000,
        13_000,
        14_800,
        16_700,
        18_500,
        20_000,
        23_000
      }
    };
    private static readonly Dictionary<ClassType, Dictionary<int, int>> CharacterSpellLevels = new() {
      [ClassType.Human] = new Dictionary<int, int> {
        [3] = 1,
        [6] = 2,
        [8] = 3,
        [10] = 4,
        [12] = 5
      },
      [ClassType.Warrior] = new Dictionary<int, int> {
        [3] = 1,
        [6] = 2,
        [8] = 3,
        [10] = 4,
        [12] = 5
      },
      [ClassType.Elf] = new Dictionary<int, int> {
        [0] = 1,
        [1] = 2,
        [2] = 3,
        [3] = 4,
        [4] = 5,
        [5] = 6,
        [6] = 7,
        [7] = 8,
        [8] = 9,
        [9] = 9,
        [10] = 10,
        [11] = 11,
        [12] = 12
      },
      [ClassType.Wizard] = new Dictionary<int, int> {
        [1] = 1,
        [2] = 2,
        [3] = 3,
        [4] = 4,
        [5] = 5,
        [6] = 6,
        [7] = 7,
        [8] = 8,
        [9] = 9,
        [10] = 10,
        [11] = 11,
        [12] = 12
      },
      [ClassType.Kron] = new Dictionary<int, int> {
        [1] = 1,
        [2] = 2,
        [3] = 3,
        [4] = 4,
        [5] = 5,
        [6] = 6,
        [7] = 7,
        [8] = 8,
        [9] = 9,
        [10] = 10,
        [11] = 11,
        [12] = 12
      },
      [ClassType.Gnom] = new Dictionary<int, int> {
        [1] = 1,
        [3] = 2,
        [5] = 3,
        [7] = 4,
        [8] = 5,
        [10] = 6,
        [12] = 7
      }
    };

    public static List<int> GetCharacterLevelsProgress(ClassType classType) {
      return CharacterLevels[classType];
    }

    [MustUseReturnValue]
    public static int GetSpellLevelByCharacterLevel(ClassType classType, int level) {
      if (HasSpellLevelInCharacterLevel(classType, level)) {
        return CharacterSpellLevels[classType][level];
      }

      return -1;
    }

    [MustUseReturnValue]
    public static bool HasSpellLevelInCharacterLevel(ClassType classType, int level) {
      if (CharacterSpellLevels[classType].ContainsKey(level)) {
        return true;
      }

      Debug.LogWarning($"Игровые очки в соответствии с уровнем персонажа не найден: класс персонажа {classType}, уровень персонажа {level}.");
      return false;
    }

    [MustUseReturnValue]
    public static int GetGamePointsByCharacterLevel(ClassType classType, int level) {
      if (CharacterLevels[classType].Contains(level)) {
        return CharacterLevels[classType][level];
      }

      Debug.LogWarning($"Игровые очки в соответствии с уровнем персонажа не найден: класс персонажа {classType}, уровень персонажа {level}.");
      return -1;
    }

    [MustUseReturnValue]
    public static int GetNumberOfLevelsByClassType(ClassType classType) {
      if (CharacterLevels.ContainsKey(classType)) {
        return CharacterLevels[classType].Count;
      }

      Debug.LogWarning($"Класс персонажа не найден: класс персонажа {classType}");
      return -1;
    }
  }
}