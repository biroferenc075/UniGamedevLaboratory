using Assets.Scripts.Weapons;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    AimingStrategy aimingStrategy;

    [SerializeField]
    MovementStrategy movementStrategy;
    
    [SerializeField] 
    Weapon weapon;

    [SerializeField]
    Transform firingPoint;

    private void Start()
    {
        weapon.Init();
        aimingStrategy.Init();
    }
    public void Update()
    {
        movementStrategy.Move(transform);
        var a = aimingStrategy.Aim(firingPoint);
        weapon.Fire(firingPoint, a);
    }
}