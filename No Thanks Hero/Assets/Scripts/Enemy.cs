using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private bool blowingLeft = false;
    private bool blowingRight = false;
    private Rigidbody enemRb;
    public float moveSpeed = 0;
    public Vector3 moveBoundLeft;
    public Vector3 moveBoundRight;
    public float emass;
    private UnityEngine.Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = UnityEngine.Camera.main;
        player = GameObject.Find("Player");
        enemRb = GetComponent<Rigidbody>();
        enemRb.mass = emass;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
         if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0) {
            blowingLeft = player.GetComponent<Wind>().blowLeft;
            blowingRight = player.GetComponent<Wind>().blowRight;
            if(blowingLeft) {
                enemRb.AddForce(player.GetComponent<Wind>().windVectL * player.GetComponent<Wind>().windSpeed * player.GetComponent<Wind>().windMod * Time.deltaTime, ForceMode.Impulse);
            }
            if(blowingRight) {
                enemRb.AddForce(player.GetComponent<Wind>().windVect * player.GetComponent<Wind>().windSpeed * player.GetComponent<Wind>().windMod * Time.deltaTime, ForceMode.Impulse);
            }
            if(player.GetComponent<Wind>().raining) {
                enemRb.AddForce(Vector3.down * player.GetComponent<Wind>().rainForce * Time.deltaTime, ForceMode.Impulse);
            }

            if(player.GetComponent<Wind>().tornadoActive) {
                enemRb.AddForce(Vector3.up * (player.GetComponent<Wind>().windSpeed) * Time.deltaTime, ForceMode.Impulse);
            }

         }
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Lightning")) {
            Destroy(gameObject);
        }
    }
}
