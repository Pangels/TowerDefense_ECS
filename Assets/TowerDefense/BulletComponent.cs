using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public struct BulletData : IComponentData
    {
        public float damage;
        public float speed;

    }

    public class Bullet : ComponentDataProxy<BulletData> { }
}