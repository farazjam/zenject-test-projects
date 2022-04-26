using UnityEngine;
using Zenject;

namespace DI1
{
    public class MessageInstallerDI1 : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMessage>().To<Message>().AsSingle();
        }
    }
}