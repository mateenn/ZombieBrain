using Unity.Entities;

namespace thesyedmateen.Dots.Zombies
{
    public struct BrainHealth : IComponentData
    {
        public float Value;
        public float Max;
    }
}