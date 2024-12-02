using UnityEngine.Events;

using UnityEngine;
using System.Collections.Generic;
using System;

public class Enemy : Destructible
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] int scoreValue;

    public UnityEvent OnEnemyDestroyed;

    private List<Material> mats = new List<Material>();
    private List<Color> originalColors = new List<Color>();
    private List<Color> originalEmissions = new List<Color>();

    private bool isFlashing = false;
    private bool flashExpired = false;

    private float flashTimer = 0f;
    [SerializeField] float flashDuration = 0.15f;

    public override void Awake()
    {
        base.Awake();
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                if (mat.HasProperty("_BaseColor") && mat.HasProperty("_EmissionColor"))
                {
                    var em = mat.GetColor("_EmissionColor");
                    var col = mat.GetColor("_BaseColor");
                    originalColors.Add(col);
                    originalEmissions.Add(em);
                    
                    mats.Add(mat);
                }
            }
        }
    }
    public override void TakeDamage(int amount)
    {
        flashTimer = flashDuration;

        if (!isFlashing)
        {
            foreach (Material mat in mats)
            {
                mat.SetColor("_BaseColor", new Color(0.8f, 0.8f, 0.8f));
                mat.SetColor("_EmissionColor", new Color(0.8f, 0.8f, 0.8f));
            }
        }

        isFlashing = true;
        flashExpired = false;

        base.TakeDamage(amount);
    }

    public void Update()
    {
        flashTimer -= Time.deltaTime;
        if (flashTimer < 0 && !flashExpired)
        {

            int i = 0;
            foreach (Material mat in mats)
            {
                mat.SetColor("_BaseColor", originalColors[i]);
                mat.SetColor("_EmissionColor", originalEmissions[i]);
                i++;
            }
            flashExpired = true;
            isFlashing = false;
        }
    }
    protected override void Die()
    {
        AudioManager.Instance.PlayDeath();
        GameStateManager.Instance.EnemyKilled(scoreValue);
        var expl = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        OnEnemyDestroyed?.Invoke();
        Destroy(gameObject);
        Destroy(expl, expl.GetComponent<ParticleSystem>().main.duration);
    }
    protected override void LeaveBounds()
    {
        Destroy(gameObject);
    }
}
