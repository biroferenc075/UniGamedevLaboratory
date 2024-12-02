using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxItem : MonoBehaviour
{
    public double speed = 100f;
    public bool tile = true;
    public bool vertical = true;
    public bool isTile { get; set; } = false;

    public double loopDelay = 0f;
    
    protected double period = 0f;
    private float current = 0f;

    public Vector3 startPos
    {
        get;
        set;
    }

    public Vector3 calcOffset()
    {
        return new Vector3(0, 0, (float)(period / 2.0 * speed));
    }


    private void Awake()
    {
        if (vertical)
        {
            period = 20.0 * transform.localScale.z / speed;
        } else
        {
            period = 20.0 * transform.localScale.x / speed;
        }

    }
    private void Start()
    {
        if (tile && !isTile) // this object is already ahead of the another tile
        {

            float timeOffset = (float) period / 2.0f;

            startPos = transform.position + calcOffset();
            current = timeOffset;
        }
        else
        {
            startPos = transform.position;
        }
    }

    void Update()
    {
        current += Time.deltaTime;
        if (tile && current > period + loopDelay)
        {
            current = 0;
            transform.position = startPos;
        }
        else
        {
            transform.position += new Vector3(0,0, -Time.deltaTime * (float)speed);
        }
    }

    public void CopyValues(ParallaxItem other)
    {
        speed = other.speed;
        tile = other.tile;
        period = other.period;
    }
}