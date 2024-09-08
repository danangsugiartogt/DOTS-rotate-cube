using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class RotateObjectMono : MonoBehaviour
{
    public float value;

    private class Baker: Baker<RotateObjectMono>
    {
        public override void Bake(RotateObjectMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateComponent
            {
                Value = authoring.value
            });
        }
    }
}

public struct RotateComponent : IComponentData
{
    public float Value;
}

public partial struct RotatingCubeSystem: ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        return;
        //foreach (var (localTransform, rotateSpeed) in
        //    SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateComponent>>().WithAll<RotateCubeComponent>())
        //{
        //    float power = 1f;
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        power *= 2f;
        //        power /= 2f;
        //    }

        //    localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.Value * SystemAPI.Time.DeltaTime * power);
        //}

        var rotatingCubeJob = new RotatingCubeJob()
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };

        rotatingCubeJob.ScheduleParallel();
    }
}

[BurstCompile]
[WithAll(typeof(RotateCubeComponent))]
public partial struct RotatingCubeJob: IJobEntity
{
    public float deltaTime;

    public void Execute(ref LocalTransform localTransform, in RotateComponent rotateSpeed)
    {
        float power = 1f;
        for (int i = 0; i < 100000; i++)
        {
            power *= 2f;
            power /= 2f;
        }

        localTransform = localTransform.RotateY(rotateSpeed.Value * deltaTime * power);
    }
}
