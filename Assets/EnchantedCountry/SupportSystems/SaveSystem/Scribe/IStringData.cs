using System;

namespace Core {
  public interface IStringData: IScribe{
    public string GetString(Enum eEnum);
    public void SetString(Enum eEnum, string value);
    public void IncreaseString(Enum eEnum, string value);
  }
}