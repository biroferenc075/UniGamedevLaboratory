using UnityEngine;


[CreateAssetMenu(fileName = "SpitfireStrategy", menuName = "Shmup/FiringStrategy/Spitfire")]
public class SpitfireStrategy : FiringStrategy
{
    private GameStateManager gameState;

    [SerializeField]
    float spread;
    [SerializeField]
    int num;

    private int counter = 0;
    private bool asc = true;

    public override void Init()
    {
        gameState = GameStateManager.Instance;
        counter = 0;
        asc = true;
    }

    public override void Fire(Transform firingPos, Quaternion aim, GameObject projectile, int bulletDamage)
    {
        Vector3 axis = gameState.isTopView() ? Vector3.up : Vector3.right;
    
        Quaternion newAim = aim * Quaternion.AngleAxis(-spread / 2.0f + counter * spread / (num - 1), axis);
        var go = BulletPoolManager.instance.getItemFromPool();
        go.GetComponent<Projectile>().SetAttr(projectile, firingPos, newAim, bulletDamage);

        if (asc)
        {
            counter++;
            if (counter >= num-1)
                asc = false;
        }
        else
        {
            counter--;
            if (counter <= 0)
                asc = true;
        }
    }
}

