using System;
using UnityEngine;

namespace Hybrid.Components
{
    [Serializable]
    public class TowerComponent : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public BulletComponent Bullet;
        public float FireRange;
        public float RestartDistance;
        public GameObject TowerBody;
        public GameObject TowerWeapon;
        public MonsterComponent TargetMonster;
    }
}