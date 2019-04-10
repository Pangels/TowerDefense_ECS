using System;
using Unity.Entities;

namespace TowerDefense
{
    [Serializable]
    public struct TowerData : IComponentData
    {
        public float Attack;
        public string Name;
    }

    public class TowerComponent : ComponentDataProxy<TowerData> { }
}