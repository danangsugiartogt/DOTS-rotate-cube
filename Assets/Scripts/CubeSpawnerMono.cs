using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CubeSpawnerMono : MonoBehaviour
{
    public GameObject cubePrefab;
    public int amountToSpawn;

    public class Baker : Baker<CubeSpawnerMono>
    {
        public override void Bake(CubeSpawnerMono authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None);

            AddComponent(entity, new CubeSpawnerComponent()
            {
                cubePrefabEntity = GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                amountToSpawn = authoring.amountToSpawn
            });
        }
    }
}

public partial class CubeSpawnerSystem: SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<CubeSpawnerComponent>();
    }

    protected override void OnUpdate()
    {
        this.Enabled = false;

        var cubeSpawner = SystemAPI.GetSingleton<CubeSpawnerComponent>();

        for (int i = 0; i < cubeSpawner.amountToSpawn; i++)
        {
            var spawnedEntity = EntityManager.Instantiate(cubeSpawner.cubePrefabEntity);
            EntityManager.SetComponentData(spawnedEntity, new LocalTransform()
            {
                Position = new float3(UnityEngine.Random.Range(-10f, 5), .6f, UnityEngine.Random.Range(-4f, 7)),
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }
    }
}

public struct CubeSpawnerComponent : IComponentData
{
    public Entity cubePrefabEntity;
    public int amountToSpawn;
}