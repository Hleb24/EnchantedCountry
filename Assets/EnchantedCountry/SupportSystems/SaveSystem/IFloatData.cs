using System;

namespace Core {
  public interface IFloatData : IScribe{
    public float GetFloat(Enum eEnum);
    public void SetFloat(Enum eEnum, float value);
    public void IncreaseFloat(Enum eEnum, float value);
  }

  public interface IScribe {
    public void Init(Save save = null);
    public void Save(Save save);
    public void Loaded(Save save);
  }

  public interface IData: IFloatData, IIntData { }
}