using Pure.Components;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public float Speed;
    public Mesh Mesh;
    public Material Material;

    EntityManager entityManager;
    Entity bullet;

    // Start is called before the first frame update
    private void Start()
    {
        entityManager = World.Active.GetOrCreateManager<EntityManager>();

        bullet = entityManager.CreateEntity(
            ComponentType.Create<SpeedComponent>(),
            ComponentType.Create<Position>(),
            ComponentType.Create<RenderMesh>());

        entityManager.SetComponentData(bullet, new SpeedComponent { Value = Speed });
        entityManager.SetSharedComponentData(bullet, new RenderMesh
        {
            mesh = Mesh,
            material = Material
        });
    }
}
