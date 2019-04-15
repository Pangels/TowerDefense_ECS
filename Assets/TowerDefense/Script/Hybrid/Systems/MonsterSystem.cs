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
        }

        protected override void OnUpdate()
        {

        }
    }
}