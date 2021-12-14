using System;

namespace Core {
  public interface IFloatData : IScribe{
    public float GetFloat(Enum eEnum);
    public void SetFloat(Enum eEnum, float value);
    public void IncreaseFloat(Enum eEnum, float value);
  }
}