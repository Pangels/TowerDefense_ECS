using System;
using Unity.Entities;

namespace Pure.Components
{
    [Serializable]
    public struct TowerData : IComponentData
    {
        public float Attack;
    }

    public class TowerComponent : ComponentDataProxy<TowerData> { }
}