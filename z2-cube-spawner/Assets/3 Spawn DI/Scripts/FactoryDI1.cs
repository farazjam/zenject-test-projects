using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Cube.Spawner.DI1
{
    /// <summary>
    /// - Either create a new script with Monobehavior and use that type or use an existing
    /// component on Instantiated Prefab to have it as that type in Factory.
    /// 
    /// - This is not a Monobehavior, it's Zenject provided factory pattern and can be placed
    /// in spawner class. Doesn't need it's own script file
    /// </summary>
    public class FactoryDI1 : PlaceholderFactory<Transform> { }
}
