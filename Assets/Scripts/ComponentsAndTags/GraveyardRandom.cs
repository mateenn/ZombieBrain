using Unity.Entities;
using Unity.Mathematics;

namespace thesyedmateen.Dots.Zombies
{
    public struct GraveyardRandom: IComponentData
    {
        public Random Value;
    }
}