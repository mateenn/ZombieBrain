 using thesyedmateen.Dots.Zombies.Static;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace thesyedmateen.Dots.Zombies.Systems
{
    [BurstCompile]
    public partial struct SpawnZombieSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }
        
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            new SpawnZombieJob
            {
                DeltaTime = deltaTime,
                Ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule();
        }
    }
    
    [BurstCompile]
    public partial struct SpawnZombieJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        private void Execute(GraveyardAspect graveyard)
        {
            graveyard.ZombieSpawnTimer -= DeltaTime;
            if(!graveyard.TimeToSpawnZombie) return;
            if(!graveyard.ZombieSpawnPointInitialized()) return;
            graveyard.ZombieSpawnTimer = graveyard.ZombieSpawnRate;
            var newZombie = Ecb.Instantiate(graveyard.ZombiePrefab);

            var newZombieTransform = graveyard.GetZombieSpawnPoint();
            Ecb.SetComponent(newZombie,newZombieTransform);

            var zombieHeading = MathHelpers.GetHeading(newZombieTransform.Position, graveyard.Position);
            Ecb.SetComponent(newZombie, new ZombieHeading{Value = zombieHeading});
        }
    }
}