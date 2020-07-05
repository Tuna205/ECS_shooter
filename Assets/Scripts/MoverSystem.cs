using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts
{
    public class MoverSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities.ForEach((ref Translation translation, in MoveSpeedComponent moveSpeedComponent) =>
            {
                translation.Value.y += moveSpeedComponent.moveSpeed * deltaTime;
            }).ScheduleParallel();
        }
    }
}
