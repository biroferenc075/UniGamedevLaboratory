using UnityEngine;

[CreateAssetMenu(fileName = "ParallelStrategy", menuName = "Shmup/FiringStrategy/Parallel")]
public class ParallelStrategy : FiringStrategy
{
    private GameStateManager gameState;

    [SerializeField]
    float offset;
    [SerializeField]
    int num;

    public override void Init()
    {
        gameState = GameStateManager.Instance;
    }

    public override void Fire(Transform firingPos, Quaternion aim, GameObject projectile, int bulletDamage)
    {
        Vector3 axis = gameState.isTopView() ? Vector3.right : Vector3.up;
        if (num % 2 == 0)
        {
            for (int i = 0; i < num; i++)
            {
                Vector3 newPos;
                if (i%2 == 0)
                {
                    newPos = firingPos.position + (0.5f + (i / 2)) * axis * offset;
                } else
                {
                    newPos = firingPos.position - (0.5f + (i / 2)) * axis * offset;
                }
                var go = BulletPoolManager.instance.getItemFromPool();
                go.GetComponent<Projectile>().SetAttr(projectile, firingPos, aim, bulletDamage);
                go.transform.position = newPos;
            }
        } else
        {
            var go = BulletPoolManager.instance.getItemFromPool();
            go.GetComponent<Projectile>().SetAttr(projectile, firingPos, aim, bulletDamage);
            for (int i = 0; i < num - 1; i++)
            {
                Vector3 newPos;
                if (i % 2 == 0)
                {
                    newPos = firingPos.position + (1 + i / 2) * axis * offset;
                }
                else
                {
                    newPos = firingPos.position - (1 + i / 2) * axis * offset;
                }
                var obj = BulletPoolManager.instance.getItemFromPool();
                obj.GetComponent<Projectile>().SetAttr(projectile, firingPos, aim, bulletDamage);
                obj.transform.position = newPos;
            }
        }
            
    }
}

