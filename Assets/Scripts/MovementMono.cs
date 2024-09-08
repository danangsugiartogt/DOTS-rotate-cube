using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MovementMono : MonoBehaviour
{
    public class Baker : Baker<MovementMono>
    {
        public override void Bake(MovementMono authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            AddComponent(entity, new MovementComponent()
            {
                movementVector = new float3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f))
            });
        }
    }
}

public struct MovementComponent: IComponentData
{
    public float3 movementVector; 
}
