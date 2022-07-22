using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpikeAtk : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        if(transform.position.y <= -3) {
            Destroy(gameObject);
        }
    }
}
