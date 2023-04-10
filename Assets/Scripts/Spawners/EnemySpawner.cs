using Entities;

namespace Spawners
{
    public class EnemySpawner : Spawner<Enemy>
    {
        protected override void InitialiseEntity(Enemy enemy)
        {
            base.InitialiseEntity(enemy);
            enemy.Initialise(player);
        }
    }
}