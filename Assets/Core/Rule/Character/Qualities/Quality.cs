using System;
using Core.SupportSystems.Data;

namespace Core.Rule.Character.Qualities {
	[Serializable]

	public class Quality {
		#region Fields
		private const int BOTTOM_BORDER = 0;
		private const int TOP_BORDER = 24;
		private int _valueOfQuality;
		#endregion
		#region Constructors
		public Quality(QualityType qualityType) {
			this.qualityType = qualityType;
		}
		public Quality(QualityType qualityType, int valueOfQuality) {
			this.qualityType = qualityType;
			ValueOfQuality = valueOfQuality;
		}
		#endregion
		#region Properties
		public QualityType qualityType { get; private set; }

		public int Modifier { get; private set; }

		public int ValueOfQuality {
			get {
				return _valueOfQuality;
			}
			set {
				if (IsWithinBorders(value)) {
					_valueOfQuality = value;
					GetModifier();
				} else {
					throw new InvalidOperationException("Value is invalid");
				}
			}
		}
		#endregion
		#region Methods
		private static bool IsWithinBorders(int value) {
			return value >= BOTTOM_BORDER && value <= TOP_BORDER;
		}

		private void GetModifier() {
			if (qualityType == QualityType.Wisdom) {
				switch (ValueOfQuality) {
					case 0:
					case 1:
					case 2:
					case 3:
						Modifier = -3;
						break;
					case 4:
					case 5:
						Modifier = -2;
						break;
					case 6:
					case 7:
					case 8:
						Modifier = -1;
						break;
					case 9:
					case 10:
					case 11:
					case 12:
						Modifier = 0;
						break;
					case 13:
					case 14:
					case 15:
						Modifier = 1;
						break;
					case 16:
					case 17:
						Modifier = 2;
						break;
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
					case 23:
					case 24:
						Modifier = 4;
						break;
					default:
						throw new ArgumentException("Parameters invalid");

				}
			} else if (qualityType == QualityType.Courage) {
				Modifier = 0;
			} else {
				switch (ValueOfQuality) {
					case 0:
					case 1:
					case 2:
					case 3:
						Modifier = -3;
						break;
					case 4:
					case 5:
						Modifier = -2;
						break;
					case 6:
					case 7:
					case 8:
						Modifier = -1;
						break;
					case 9:
					case 10:
					case 11:
					case 12:
						Modifier = 0;
						break;
					case 13:
					case 14:
					case 15:
						Modifier = 1;
						break;
					case 16:
					case 17:
						Modifier = 2;
						break;
					case 18:
					case 19:
					case 20:
					case 21:
					case 22:
					case 23:
					case 24:
						Modifier = 3;
						break;
					default:
						throw new ArgumentException("Parameters invalid");

				}
			}
		}
		#endregion
	}
}