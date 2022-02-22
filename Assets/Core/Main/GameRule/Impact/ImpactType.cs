using System;

namespace Core.Main.GameRule.Impact {
  [Flags]
  public enum ImpactType {
    None = 0,
    Poison = 1 << 0,
    MagicianSpell = 1 << 1,
    MagesStaff = 1 << 2,
    DragonBreath = 1 << 3,
    Paralysis = 1 << 4,
    Petrification = 1 << 5,
    Fire = 1 << 6,
    Gas = 1 << 7,
    Acid = 1 << 8,
    Healing = 1 << 9,
    Light = 1 << 10,
    Lightning = 1 << 11,
    Positive = Healing,
    Negative = Poison | MagicianSpell | MagesStaff | DragonBreath | Paralysis | Petrification | Fire | Gas | Acid | Lightning,
    Neutral = Light
  }
}