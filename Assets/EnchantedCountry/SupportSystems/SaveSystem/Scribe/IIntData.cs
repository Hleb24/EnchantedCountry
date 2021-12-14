using System;

namespace Core {
  public interface IIntData: IScribe{
    public int GetInt(Enum eEnum);
    public void SetInt(Enum eEnum, int value);
    public void IncreaseInt(Enum eEnum, int value);
  }
}