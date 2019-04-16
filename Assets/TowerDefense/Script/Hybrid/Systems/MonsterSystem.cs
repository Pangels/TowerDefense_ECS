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

        }
    }
}