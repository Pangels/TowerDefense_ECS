using Hybrid.Components;
using Unity.Entities;
using UnityEngine;

namespace Hybrid.Systems
{
    public class TowerSystem : ComponentSystem
    {
        private struct GroupTower
        {
            public TowerComponent TowerComponent;
            public Transform TransformTower;
            public PositionComponent PositionTower;
            public RotationComponent RotationTower;
            public SpeedComponent SpeedTower;
        }

        private struct GroupMonster
        {
            public MonsterComponent MonsterComponent;
            public Transform TransformMonster;
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            GroupTower TowerJob = new GroupTower { };

            foreach (var entity in GetEntities<GroupTower>())
            {
                var bullet = Object.Instantiate(entity.TowerComponent.BulletPrefab);
                bullet.GetComponent<BulletComponent>().Tower = entity.TowerComponent;
                entity.TowerComponent.Bullet = bullet.GetComponent<BulletComponent>();

                // Initialize the component variable
                bullet.GetComponent<PositionComponent>().Value = entity.TransformTower.position;
                bullet.GetComponent<PositionComponent>().Value.y = 2f;
                bullet.GetComponent<PositionComponent>().Value.z += 0.5f;
                bullet.GetComponent<RotationComponent>().Value = entity.TransformTower.rotation;
                bullet.GetComponent<BulletComponent>().IsActive = false;

                // Update the prefab position for the first shot
                bullet.transform.position = bullet.GetComponent<PositionComponent>().Value;
                bullet.transform.rotation = bullet.GetComponent<RotationComponent>().Value;
            }
        }

        protected override void OnUpdate()
        {
            foreach (var entityT in GetEntities<GroupTower>())
            {
                foreach (var entityM in GetEntities<GroupMonster>())
                {
                    BulletComponent bullet = entityT.TowerComponent.Bullet;
                    // If the monster is within the fire range of the tower
                    // The tower rotate to look at the monster and open fire
                    if (Vector3.Distance(entityT.TransformTower.position, entityM.TransformMonster.position) < entityT.TowerComponent.FireRange)
                    {
                        Quaternion targetRotation;

                        // The tower look at the monster
                        targetRotation = Quaternion.LookRotation(entityM.TransformMonster.position - entityT.TransformTower.position);
                        entityT.TransformTower.rotation = Quaternion.Slerp(entityT.TransformTower.rotation, new Quaternion(0f, targetRotation.y, 0f, targetRotation.w), entityT.SpeedTower.Value);

                        // The weapon look at the monster
                        targetRotation = Quaternion.LookRotation(entityM.TransformMonster.position - entityT.TowerComponent.TowerWeapon.transform.position) * Quaternion.Euler(90f, 0, 0);
                        entityT.TowerComponent.TowerWeapon.transform.rotation = Quaternion.Slerp(entityT.TowerComponent.TowerWeapon.transform.rotation, targetRotation, entityT.SpeedTower.Value);

                        entityT.TowerComponent.TargetMonster = entityM.MonsterComponent;
                        bullet.IsActive = true;
                        break;
                    }
                    // If a bullet is shot and no monster is within range, reset the bullet after it arrive in the tower max fire range
                    else if (Vector3.Distance(bullet.transform.position, bullet.GetComponent<PositionComponent>().Value) < 0.1f)
                    {
                        // Smooth animation and return the weapon to its initial state
                        Quaternion.Slerp(entityT.TowerComponent.TowerWeapon.transform.rotation, Quaternion.Euler(90f, 0, 0), entityT.SpeedTower.Value);
                        entityT.TowerComponent.TowerWeapon.transform.rotation = /*new Quaternion(0.7071068f, 0f, 0f, 0.7071068f);*/ Quaternion.Euler(90f, 0f, 0f);
                        bullet.IsActive = false;
                    }
                }
            }
        }
    }
}
