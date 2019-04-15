using Pure.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

namespace Pure.Systems
{
    public class BulletSystem : JobComponentSystem
    {
        private struct BulletGroup : IJobProcessComponentData<SpeedComponent, Position>
        {
            public float DeltaTime;
            //public Transform Transform;
            //public SpeedComponent Speed;

            public void Execute(ref SpeedComponent speed, ref Position position)
            {
                position.Value.x += speed.Value * DeltaTime;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new BulletGroup
            {
                DeltaTime = Time.deltaTime
            };

            return job.Schedule(this, inputDeps);
        }
    }
}
