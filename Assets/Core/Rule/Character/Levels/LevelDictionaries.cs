﻿using System.Collections.Generic;

namespace Core.Rule.Character.Levels {
  public class LevelDictionaries {
    public static readonly Dictionary<ClassType, List<int>> DefiningLevelsForСharacterTypes = 
      new Dictionary<ClassType, List<int>>() {
        [ClassType.Warrior] = new List<int>() {
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
        [ClassType.Elf] = new List<int>() {
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
        [ClassType.Wizard] = new List<int>() {
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
        [ClassType.Kron] = new List<int>() {
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
        [ClassType.Gnom] = new List<int>() {
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
        },
      };
    public static readonly Dictionary<ClassType, Dictionary<int, int>> DefiningSpellLevelsForСharacterTypes =
      new Dictionary<ClassType, Dictionary<int, int>>() {
        [ClassType.Warrior] = new Dictionary<int, int>() {
          [3] = 1,
          [6] = 2,
          [8] = 3,
          [10] = 4,
          [12] = 5
        },
        [ClassType.Elf] = new Dictionary<int, int>() {
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
        [ClassType.Wizard] = new Dictionary<int, int>() {
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
        [ClassType.Kron] = new Dictionary<int, int>() {
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
        [ClassType.Gnom] = new Dictionary<int, int>() {
          [1] = 1,
          [3] = 2,
          [5] = 3,
          [7] = 4,
          [8] = 5,
          [10] = 6,
          [12] = 7
        }
      };
  }
}