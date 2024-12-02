using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform[] backgrounds;
    private ParallaxItem[] items;

    private void Start()
    {
        foreach (Transform t in backgrounds)
        {
            var item = t.GetComponent<ParallaxItem>();
            if (item != null && item.tile)
            {
                var go = Instantiate(t);
                var par = go.GetComponent<ParallaxItem>();

                par.isTile = true;
                par.CopyValues(item);
                go.transform.position = t.position + par.calcOffset();            
            }
        }
    }
}
