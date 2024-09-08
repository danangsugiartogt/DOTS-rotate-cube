using Unity.Entities;
using UnityEngine;

public class StunnedMono : MonoBehaviour
{
    public class Baker : Baker<StunnedMono>
    {
        public override void Bake(StunnedMono authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            AddComponent(entity, new StunnedComponent());
            SetComponentEnabled<StunnedComponent>(entity, false);
        }
    }
}

public struct StunnedComponent: IComponentData, IEnableableComponent
{

}
