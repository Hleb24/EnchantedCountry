using Core.Support.Attributes;

namespace Core.Support.PrefsTools {
  public static class PrefsConstants {
    public const int INITIAL = 0;
    public const int COMPLETED = 1;
    [PrefsKeys]
    public const string COINS_IN_WALLET = "CoinsInWallet";
    [PrefsKeys]
    public const string DICE_ROLL_FOR_RISK_POINTS = "diceRollForRiskPoints";
  }
}