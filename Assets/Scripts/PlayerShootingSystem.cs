using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerShootingSystem : SystemBase
{
    public event EventHandler OnShoot;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerComponent>();
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
            EntityManager.SetComponentEnabled<StunnedComponent>(playerEntity, true);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
            EntityManager.SetComponentEnabled<StunnedComponent>(playerEntity, false);
        }

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        var cubeSpawner = SystemAPI.GetSingleton<CubeSpawnerComponent>();

        var entityCommandBuffer = new EntityCommandBuffer(WorldUpdateAllocator);

        foreach(var (localTransform, entity) in 
            SystemAPI.Query<RefRO<LocalTransform>>().WithAll<PlayerComponent>().WithDisabled<StunnedComponent>().WithEntityAccess())
        {
            var spawnedEntity = entityCommandBuffer.Instantiate(cubeSpawner.cubePrefabEntity);
            entityCommandBuffer.SetComponent(spawnedEntity, new LocalTransform()
            {
                Position = localTransform.ValueRO.Position,
                Rotation = quaternion.identity,
                Scale = 1f
            });

            OnShoot?.Invoke(entity, EventArgs.Empty);
        }

        entityCommandBuffer.Playback(EntityManager);
    }
}
