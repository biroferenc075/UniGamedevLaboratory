using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

public class Player : Destructible
{

    private Renderer[] renderers;
    private Collider[] hitboxes;

    [SerializeField]
    float invincibilityDuration = 2f;
    [SerializeField]
    float blinkPeriod = 0.25f;
    [SerializeField] 
    GameObject explosionPrefab;

    private bool invincible = false;
    private bool blinking = false;
    
    private float blinkTimer = 0f;
    private float invincTimer = 0f;

    public override void Awake()
    {
        base.Awake();
        renderers = GetComponentsInChildren<Renderer>();
        hitboxes = GetComponentsInChildren<Collider>();

    }

    public override void TakeDamage(int amount)
    {
        AudioManager.Instance.PlayHit();
        invincTimer = invincibilityDuration;
        blinkTimer = blinkPeriod;
        

        setRenderers(false);
        setHitboxes(false);

        GameStateManager.Instance.PlayerHit();

        base.TakeDamage(1);
    }

    private void setRenderers(bool active)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = active;
        }
        blinking = !active;
    }

    private void setHitboxes(bool active)
    {
        foreach (var coll in hitboxes)
        {
            coll.excludeLayers = active ? 0 : (1 << 8) | (1 << 9);
        }
        invincible = !active;
    }

    public void Update()
    {
        if (invincible)
        {
            blinkTimer -= Time.deltaTime;
            invincTimer -= Time.deltaTime;

            if (invincTimer < 0f)
            {
                setRenderers(true);
                setHitboxes(true);
            }
            else if (blinkTimer < 0f)
            {
                if (blinking)
                {
                    setRenderers(true);
                }
                else
                {
                    setRenderers(false);
                }
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "GroundEnemy" && collision.gameObject.tag != "Item")
        {
            TakeDamage(1);
        }
    }

    protected override void Die()
    {
        AudioManager.Instance.PlayDeath();
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<PlayerMovementController>().enabled = false;
        gameObject.GetComponent<PlayerWeaponController>().enabled = false;

        var expl = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(expl, expl.GetComponent<ParticleSystem>().main.duration);
    }

    protected override void LeaveBounds()
    {
        
    }
}
