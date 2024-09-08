using Unity.Entities;
using Unity.Transforms;

public readonly partial struct RotatingMovingCubeAspect : IAspect
{
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<RotateComponent> rotateComponent;
    public readonly RefRO<MovementComponent> movementComponent;
    public readonly RefRO<RotateCubeComponent> rotateCubeComponent;

    public void MoveAndRotate(float deltaTime)
    {
        localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateComponent.ValueRO.Value * deltaTime);
        localTransform.ValueRW = localTransform.ValueRO.Translate(movementComponent.ValueRO.movementVector * deltaTime);
    }
}
