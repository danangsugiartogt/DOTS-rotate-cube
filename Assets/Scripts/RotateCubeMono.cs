using Unity.Entities;
using UnityEngine;

public class RotateCubeMono : MonoBehaviour
{
    public class Baker : Baker<RotateCubeMono>
    {
        public override void Bake(RotateCubeMono authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            AddComponent(entity, new RotateCubeComponent());
        }
    }
}

public struct RotateCubeComponent : IComponentData
{

}
