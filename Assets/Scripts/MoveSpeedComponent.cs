using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace Assets.Scripts
{
    public struct MoveSpeedComponent : IComponentData
    {
        public float moveSpeed;
    }
}
