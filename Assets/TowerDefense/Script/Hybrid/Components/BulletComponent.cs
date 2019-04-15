using System;
using UnityEngine;

namespace Hybrid.Components
{
    [Serializable]
    public class BulletComponent : MonoBehaviour
    {
        public TowerComponent Tower;
        public bool IsActive;
    }
}
