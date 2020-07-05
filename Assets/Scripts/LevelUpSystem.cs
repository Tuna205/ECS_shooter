using UnityEngine;
using Unity.Entities;

public class LevelUpSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities.ForEach((ref LevelComponent levelComponent) =>
        {
            levelComponent.level += 1f * deltaTime;
        }).ScheduleParallel();
    }
}
