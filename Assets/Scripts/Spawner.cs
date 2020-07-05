using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Assets.Scripts;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using Unity.Physics;
using Material = UnityEngine.Material;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Mesh mesh = null;
    [SerializeField] private Material material = null;

    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(LevelComponent), 
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(RenderBounds),
            typeof(MoveSpeedComponent),
            typeof(Scale),
            typeof(PhysicsCollider)
            );
        NativeArray<Entity> entityArray = new NativeArray<Entity>(100000, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);

        SphereGeometry sg = new SphereGeometry();
        float radius = 0.1f;
        sg.Radius = radius;
        sg.Center = new float3(0, 0, 0);

        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];

            float3 position = new float3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), Random.Range(-8f, 8f));

            entityManager.SetComponentData(entity, new LevelComponent { level = 10 });
            entityManager.SetComponentData(entity, new MoveSpeedComponent { moveSpeed = Random.Range(0.1f, 0.2f) });
            entityManager.SetComponentData(entity, new Translation { Value = position});
            entityManager.SetComponentData(entity, new Scale {Value = radius });
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material
            });

            
            entityManager.SetComponentData(entity, new PhysicsCollider
            {
                Value = Unity.Physics.SphereCollider.Create(sg)
            });
        }

        entityArray.Dispose();
    }
}
