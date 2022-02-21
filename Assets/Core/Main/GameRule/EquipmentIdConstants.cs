using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core.Main.GameRule {
  [PublicAPI]
  public static class EquipmentIdConstants {
    public static readonly List<int> Bags = new() { BAG, BIG_BAG };
    public static readonly List<int> Animals = new() { MULE, HORSE, WAR_HORSE };
    public static readonly List<int> Carriages = new() { CARRIAGE };
    public const int POCKETS = 10;
    public const int NO_ARMOR_ID = 100;
    public const int BAG = 600;
    public const int BIG_BAG = 610;
    public const int MULE = 660;
    public const int HORSE = 680;
    public const int CARRIAGE = 690;
    public const int WAR_HORSE = 710;
    public const int SILVER_DAGGER = 250;
    public const int STAKE = 620;
  }
}