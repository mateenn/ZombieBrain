﻿using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace thesyedmateen.Dots.Zombies
{
    public struct ZombieSpawnPoints : IComponentData
    {
        public BlobAssetReference<ZombieSpawnPointsBlob> Value;
    }
    public struct ZombieSpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}