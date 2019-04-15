using System;
using Unity.Entities;

namespace Pure.Components
{
    [Serializable]
    public struct BulletData : IComponentData
    {
        public float Damage;
    }

    public class BulletComponent : ComponentDataProxy<BulletData> { }
}