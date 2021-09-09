using System;
using System.Collections.Generic;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
  [Serializable]
  public  class GameSaveSystem {
    public  List<GameSave> gameSaves;
    public int isNewGame;
    public GameSaveSystem() {
      gameSaves = new List<GameSave>(){new GameSave()};
    }
  }
}