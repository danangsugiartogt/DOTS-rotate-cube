using Unity.Entities;
using Unity.Transforms;

public partial struct HandleCubeSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        // without aspect
        //foreach(var (localTransform, rotate, movement) in 
        //    SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateComponent>, RefRO<MovementComponent>>()
        //             .WithAll<RotateCubeComponent>())
        //{
        //    localTransform.ValueRW = localTransform.ValueRO.RotateY(rotate.ValueRO.Value * SystemAPI.Time.DeltaTime);
        //    localTransform.ValueRW = localTransform.ValueRO.Translate(movement.ValueRO.movementVector * SystemAPI.Time.DeltaTime);
        //}

        // using aspect
        foreach (var rotatingMovingCubeAspect in SystemAPI.Query<RotatingMovingCubeAspect>())
        {
            rotatingMovingCubeAspect.MoveAndRotate(SystemAPI.Time.DeltaTime);
        }
    }
}
