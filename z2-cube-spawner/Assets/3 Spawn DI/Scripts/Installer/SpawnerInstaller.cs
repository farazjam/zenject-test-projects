using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

/// <summary>
/// Factory and Spawner doesn't know the kind of object they are going to produce/spawn
/// Any prefab with component Transform can be assigned through inspector and spawner will spawn it
/// Ideally primitive types should not be used, custom class with a unified interface should be used to make it plug and play
/// e.g Shape interface, Cube and Sphere prefabs that can be plugged in with just 1 line
/// It is made this way to keep the example dumb-simple
/// </summary>

namespace Cube.Spawner.DI1
{
    public class SpawnerInstaller : MonoInstaller
    {
        [SerializeField] GameObject cubePrefab;

        private void Awake() => Assert.IsNotNull(cubePrefab);

        public override void InstallBindings()
        {
            Container.BindFactory<Transform, FactoryDI1>().FromComponentInNewPrefab(cubePrefab);
        }
    }
}