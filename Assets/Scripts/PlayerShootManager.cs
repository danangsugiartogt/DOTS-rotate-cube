using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{
    private void Start()
    {
        var playerShootingSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();
        playerShootingSystem.OnShoot += PlayerShootingSystem_OnShoot;
    }

    private void PlayerShootingSystem_OnShoot(object sender, System.EventArgs e)
    {
        var playerEntity = (Entity)sender;
        var localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
        Debug.Log($"Shoot position: {localTransform.Position}");
    }
}
