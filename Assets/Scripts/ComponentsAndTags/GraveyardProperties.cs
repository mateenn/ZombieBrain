﻿using Unity.Entities;
using Unity.Mathematics;

namespace thesyedmateen.Dots.Zombies
{
    public struct GraveyardProperties: IComponentData
    {
        public float2 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public Entity TombstonePrefab;
        public Entity ZombiePrefab;
        public float ZombieSpawnRate;
    }

    public struct ZombieSpawnTimer : IComponentData
    {
        public float Value;
    }
}