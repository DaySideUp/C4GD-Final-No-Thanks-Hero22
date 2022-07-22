using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weakPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 10f);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Lightning")) {
            GameObject.Find("BigBoss").GetComponent<BossEnem>().Hit();
            gameObject.SetActive(false);
        }
    }
}
