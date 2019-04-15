using Hybrid.Components;
using Unity.Entities;
using UnityEngine;

namespace Hybrid.Systems
{
    public class FireBulletSystem : ComponentSystem
    {
        private struct GroupBullet
        {
            public BulletComponent BulletComponent;
            public Transform TransformBullet;
            public PositionComponent PositionBullet;
            public RotationComponent RotationBullet;
            public SpeedComponent SpeedBullet;
        }

        protected override void OnUpdate()
        {
            GroupBullet fireBulletJob = new GroupBullet { };

            foreach (var entityB in GetEntities<GroupBullet>())
            {
                if (entityB.BulletComponent.IsActive)
                {
                    var position = entityB.TransformBullet.position;
                    var tower = entityB.BulletComponent.Tower;

                    // Bullet collide with another object
                    if (Vector3.Distance(tower.TargetMonster.transform.position, entityB.TransformBullet.position) < 1f)
                    {
                        // Reduce the HP of the monster
                        --tower.TargetMonster.HitPoint;
                        // Reset the position of the bullet to fire a new one
                        position = entityB.PositionBullet.Value;

                        // When the monster life is down to '0'
                        if (tower.TargetMonster.HitPoint == 0)
                        {
                            tower.TargetMonster.HitPoint = 10;
                            tower.TargetMonster.transform.position = tower.TargetMonster.GetComponent<PositionComponent>().Value;
                        }
                    }

                    // Change the rotation of the bullet before the shot
                    if (Vector3.Distance(position, entityB.PositionBullet.Value) < tower.RestartDistance)
                    {
                        //entityB.TransformBullet.LookAt(tower.TargetMonster.transform);
                        var targetRotation = Quaternion.LookRotation(tower.TargetMonster.transform.position - entityB.TransformBullet.position) * Quaternion.Euler(90f, 0, 0);
                        entityB.TransformBullet.rotation = Quaternion.Slerp(entityB.TransformBullet.rotation, targetRotation, tower.GetComponent<SpeedComponent>().Value);

                        entityB.PositionBullet.Value = tower.transform.position;
                    }

                    // Move the bullet forward from the tower
                    position += entityB.SpeedBullet.Value * Time.deltaTime * entityB.TransformBullet.forward;

                    // Reset the position of the bullet if it reach the max fire range
                    if (Vector3.Distance(position, entityB.PositionBullet.Value) > tower.FireRange)
                    {
                        position = entityB.PositionBullet.Value;
                    }

                    entityB.TransformBullet.position = position;
                }
                else if (!entityB.BulletComponent.IsActive)
                {
                    // Hide the bullet by moving it to its starting point
                    entityB.BulletComponent.Tower.TargetMonster = null;
                    entityB.TransformBullet.rotation = Quaternion.identity;
                    entityB.TransformBullet.position = entityB.PositionBullet.Value;
                }
            }
        }
    }
}
