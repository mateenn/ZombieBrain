 using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace thesyedmateen.Dots.Zombies.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardProperties>();
        }
        
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
            var graveyard = SystemAPI.GetAspect<GraveyardAspect>(graveyardEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            //var spawnPoint = new NativeList<float3>(Allocator.Temp);
            var builder = new BlobBuilder(Allocator.Temp);
            ref var spawnPoints = ref builder.ConstructRoot<ZombieSpawnPointsBlob>();
            var arrayBuilder = builder.Allocate(ref spawnPoints.Value, graveyard.NumberTombstonesToSpawn);
            var tombStoneOffset = new float3(0f, -2f, 1f);
            
            for (var i = 0; i < graveyard.NumberTombstonesToSpawn; i++)
            {
                var newTombstone = ecb.Instantiate(graveyard.TombstonePrefab);
                var newTombstoneTransform = graveyard.GetRandomTombstoneTransform();
                ecb.SetComponent(newTombstone, newTombstoneTransform);
                
                var newZombieSpawnPoint = newTombstoneTransform.Position + tombStoneOffset;
                arrayBuilder[i] = newZombieSpawnPoint;
                /*var newZombieSpawnPoint = newTombstoneTransform.Position + tombStoneOffset;
                spawnPoint.Add(newZombieSpawnPoint);*/
            }

            var blobAsset = builder.CreateBlobAssetReference<ZombieSpawnPointsBlob>(Allocator.Persistent);
            ecb.SetComponent(graveyardEntity, new ZombieSpawnPoints{Value = blobAsset});
            builder.Dispose();
            
            ecb.Playback(state.EntityManager);
            /*graveyard.ZombieSpawnPoints = spawnPoint.ToArray(Allocator.Persistent);
            ecb.Playback(state.EntityManager);*/
        }
    }
}