using System;
using Unity.Mathematics;
using UnityEngine;

namespace Hybrid.Components
{
    [Serializable]
    public class RotationComponent : MonoBehaviour
    {
        public quaternion Value;
    }
}