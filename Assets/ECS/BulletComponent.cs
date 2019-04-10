using Unity.Entities;
using System;

namespace TowerDefense
{
    [Serializable]
    public struct BulletData : IComponentData
    {
        public float Damage;
        public float Speed;
    }

    public class BulletComponent : ComponentDataProxy<BulletData> { }
}