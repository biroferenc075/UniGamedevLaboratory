using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _lifetime;
    [SerializeField] float _speed;

    private Vector3 _velocity;

    private bool disableUpdate = true;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11) // if we hit bounds
        {
            BulletPoolManager.instance.freeItem(gameObject);
            return;
        }

        var destructible = collision.gameObject.GetComponent<Destructible>();
        if (destructible != null)
        {
            destructible.TakeDamage(_damage);
        }
        BulletPoolManager.instance.freeItem(gameObject);
        AudioManager.Instance.PlayHit();
    }

    public void FixedUpdate()
    {
        if (!disableUpdate)
        {
            transform.position += _velocity * Time.fixedDeltaTime;
        }
    }

    public void Update()
    {
        if (disableUpdate)
            return;
        float dt = Time.deltaTime;
        OnUpdate(dt);
        TimeOut(dt);
    }

    protected virtual void OnUpdate(float dt) { }

    protected void TimeOut(float dt)
    {
        _lifetime -= dt;
        if (_lifetime < 0)
            BulletPoolManager.instance.freeItem(gameObject);
    }

    public void SetAttr(GameObject other, Transform firingPos, Quaternion aim, int bulletDamage)
    {
        var proj = other.GetComponent<Projectile>();
        var sp = other.GetComponent<SpriteRenderer>();

        _damage = bulletDamage;
        _lifetime = proj._lifetime;
        _speed = proj._speed;

        _velocity = aim * Vector3.forward * _speed;

        GetComponent<SpriteRenderer>().sprite = sp.sprite;

        transform.parent = null;

        transform.position = firingPos.position;
        transform.rotation = firingPos.rotation;
        transform.localScale = other.transform.localScale;

        disableUpdate = false;
        gameObject.SetActive(true);
        gameObject.layer = other.layer;
        gameObject.name = other.name;
    }
}

