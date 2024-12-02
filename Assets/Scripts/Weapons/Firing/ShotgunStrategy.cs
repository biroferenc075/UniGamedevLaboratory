using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunStrategy", menuName = "Shmup/FiringStrategy/Shotgun")]
public class ShotgunStrategy : FiringStrategy
{
    private GameStateManager gameState;

    [SerializeField]
    float spread;
    [SerializeField]
    int num;

    public override void Init()
    {
        gameState = GameStateManager.Instance;
    }

    public override void Fire(Transform firingPos, Quaternion aim, GameObject projectile, int bulletDamage)
    {          
        Vector3 axis = gameState.isTopView() ? Vector3.up : Vector3.right;
             
        for(int i = 0; i < num; i++)
        {
            Quaternion newAim = aim * Quaternion.AngleAxis(-spread/2.0f + i * spread / (num - 1), axis);
            var go = BulletPoolManager.instance.getItemFromPool();
            go.GetComponent<Projectile>().SetAttr(projectile, firingPos, newAim, bulletDamage);
        }
    }
}

