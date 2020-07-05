using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine.InputSystem;
using Unity.Rendering;

namespace Assets.Scripts.Player
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Mesh mesh = null;
        [SerializeField] private UnityEngine.Material material = null;

        public void Shoot()
        {
            Camera mainCam = Camera.main;
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            UnityEngine.Ray ray = mainCam.ScreenPointToRay(mousePosition);
            float rayDistance = 100f;
            Entity hit = Raycast(ray.origin, ray.direction * rayDistance);
            Debug.Log(hit);
            if(hit != Entity.Null){
                ChangeEntityMesh(hit, mesh, material);
            }
        }

        private void ChangeEntityMesh(Entity entity, Mesh mesh, UnityEngine.Material material)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.AddComponent(entity, typeof(ChangeColorComponent));
            entityManager.SetComponentData(entity, new ChangeColorComponent { material = material });
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material
            });
        }

        private Entity Raycast(float3 fromPosition, float3 toPosition)
        {
            BuildPhysicsWorld buildPhysicsWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
            CollisionWorld collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;

            RaycastInput raycastInput = new RaycastInput
            {
                Start = fromPosition,
                End = toPosition,
                Filter = new CollisionFilter
                {
                    BelongsTo = ~0u,
                    CollidesWith = ~0u,
                    GroupIndex = 0,
                }
            };

            Unity.Physics.RaycastHit raycastHit = new Unity.Physics.RaycastHit();

            if(collisionWorld.CastRay(raycastInput, out raycastHit))
            {
                Entity hitEntity = buildPhysicsWorld.PhysicsWorld.Bodies[raycastHit.RigidBodyIndex].Entity;
                return hitEntity;
            }
            else
            {
                return Entity.Null;
            }
        }
    }
}
