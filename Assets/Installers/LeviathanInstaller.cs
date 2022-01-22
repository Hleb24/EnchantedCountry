using System.Collections;
using System.Collections.Generic;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace Aberrance
{
    public class LeviathanInstaller : MonoInstaller
    {
        public override void InstallBindings() {
            // Container.Bind<Memento>().FromMethod(GetMemento).AsSingle();
        }
        
        private Memento GetMemento() {
            Memento memento = new Memento();
            

            memento.Init(out bool _);
            return memento;
        }
    }
}
