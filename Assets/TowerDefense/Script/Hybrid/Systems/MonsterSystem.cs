using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Hybrid.Components
{
    public class MonsterSystem : ComponentSystem
    {
        private struct GroupMonster
        {
            public MonsterComponent MonsterComponent;
            public Transform TransformMonster;
            public PositionComponent PositionMonster;
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            for (int i = 0; i < GetEntities<GroupMonster>().Length; i++)
            {
                var entity = GetEntities<GroupMonster>()[i];

                entity.PositionMonster.Value = entity.TransformMonster.position;
            }
        }

        protected override void OnUpdate()
        {
            foreach (var entity in GetEntities<GroupMonster>())
            {
                // When the monster life is down to '0'
                if (entity.MonsterComponent.HitPoint == 0)
                {
                    entity.MonsterComponent.HitPoint = 10;
                    entity.TransformMonster.position = entity.PositionMonster.Value;
                    entity.TransformMonster.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = 0f;
                } 
            }
        }
    }
}