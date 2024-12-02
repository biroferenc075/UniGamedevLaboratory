using System;
using UnityEngine;

[System.Serializable]
public class EnemyWave : ICloneable {
    [SerializeField] public float startTime;
    [SerializeField] public int repeat;
    [SerializeField] public float repeatDelay;
    [SerializeField] public int wavePrefabIdx;

    public EnemyWave(float start, int repeat, float delay, int idx)
    {
        this.startTime = start;
        this.repeat = repeat;
        this.repeatDelay = delay;
        this.wavePrefabIdx = idx;
    }
    public object Clone()
    {
        return new EnemyWave(this.startTime, this.repeat, this.repeatDelay, this.wavePrefabIdx);
    }
}
