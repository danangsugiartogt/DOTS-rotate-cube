using Unity.Entities;
using UnityEngine;

public class PlayerMono : MonoBehaviour
{
    public class Baker: Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new PlayerComponent());
        }
    }
}

public struct PlayerComponent : IComponentData
{

}
