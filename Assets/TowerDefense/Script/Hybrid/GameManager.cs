using Hybrid.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Hybrid
{
    public class GameManager : MonoBehaviour
    {
        public EntityManager manager;
        public Entity bulletPrefab;

        private void Start()
        {
            manager = World.Active.GetOrCreateManager<EntityManager>();
        }
    }
}
