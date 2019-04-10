using System;
using UnityEngine;
using Unity.Entities;

namespace ECS
{
    #region Hybrid ECS
    public class RotateSpeed : MonoBehaviour
    {
        public float speed;
    }

    class RotateSpeedSystem : ComponentSystem
    {
        struct Components
        {
            public RotateSpeed rotateSpeed;
            public Transform transform;
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;

            foreach (var e in GetEntities<Components>())
            {
                e.transform.Rotate(0f, e.rotateSpeed.speed * deltaTime, 0f);
            }
        }
    }
    #endregion
}