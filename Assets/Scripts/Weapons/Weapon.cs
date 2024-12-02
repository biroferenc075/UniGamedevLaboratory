using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


[Serializable]
public class Weapon
{
    [SerializeField] float cooldown;
    [SerializeField] int burstNum = 1;
    [SerializeField] float burstCooldown;

    private float internalCooldown = 0f;
    private IEnumerator handler;
       
    private bool firing = false;

    [SerializeField] GameObject projectile;
    [SerializeField] FiringStrategy firingStrategy;
    [SerializeField] int bulletDamage = 1;

    public void Init()
    {
        firingStrategy.Init();
    }

    IEnumerator HandleFire(Transform firingPoint, Quaternion aim)
    {
        firing = true;
        for(int i = 0; i < burstNum; i++)
        {
            if (firingPoint == null)
                break;
            firingStrategy.Fire(firingPoint, aim, projectile, bulletDamage);
            yield return new WaitForSeconds(burstCooldown);
            
        }
        firing = false;
    }

    

    public void Fire(Transform firingPoint, Quaternion aim, bool? sound = false)
    {


        if (firingPoint == null)
            return;

        if (!firing)
        {
            float dt = Time.deltaTime;
       
            if(internalCooldown < 0)
            {
                handler = HandleFire(firingPoint, aim);
                GameStateManager.Instance.StartCoroutine(handler);
                internalCooldown = cooldown;

                if(sound != null && (bool)sound)
                    AudioManager.Instance.PlayShoot();
            }
            internalCooldown -= dt;
        }
    }

    public void SetStrategy(FiringStrategy newStrategy)
    {
        firingStrategy = newStrategy;
        firingStrategy.Init();
    }

    public void Destroy()
    {
        GameStateManager.Instance.StopCoroutine(handler);
    }
}

