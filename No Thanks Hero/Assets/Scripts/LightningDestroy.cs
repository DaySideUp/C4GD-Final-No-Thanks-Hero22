using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDestroy : MonoBehaviour
{
    private float lifespan = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if(lifespan <= 0) {
            Destroy(gameObject);
        }
    }
}
