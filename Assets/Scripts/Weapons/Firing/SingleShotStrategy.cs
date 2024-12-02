using UnityEngine;


[CreateAssetMenu(fileName = "SingleShotStrategy", menuName = "Shmup/FiringStrategy/SingleShot")]
public class SingleShotStrategy : FiringStrategy
{
    public override void Fire(Transform firingPos, Quaternion aim, GameObject projectile, int bulletDamage)
    {
        var go = BulletPoolManager.instance.getItemFromPool();
        go.GetComponent<Projectile>().SetAttr(projectile, firingPos, aim, bulletDamage);
    }
}

