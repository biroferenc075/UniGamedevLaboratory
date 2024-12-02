using Assets.Scripts.Weapons;
using UnityEngine;

public class PowerUpItem : Item
{
    protected override void ItemEffect(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerWeaponController>().UpgradeWeapon();
    }
}
