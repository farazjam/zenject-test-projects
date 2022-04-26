using UnityEngine;
using Zenject;

namespace DI2
{
    public class MessageInstallerDI2 : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMessage>().To<MessageA>().AsSingle();
            //Container.Bind<IMessage>().To<MessageB>().AsSingle();
        }
    }
}