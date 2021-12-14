﻿using System;
using System.Collections.Generic;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
  [Serializable]
  public  class GameSaveSystem {
    public  List<OldSave> gameSaves;
    public int isNewGame;
    public GameSaveSystem() {
      gameSaves = new List<OldSave>(){new OldSave()};
    }
  }
}