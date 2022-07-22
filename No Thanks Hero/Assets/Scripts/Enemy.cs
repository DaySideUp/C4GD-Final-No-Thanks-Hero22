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
    public float emass;
    public bool moveable = false;
    public int xDirec = 0;
    public float rightBound;
    public float leftBound;
    private UnityEngine.Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rightBound = transform.position.x + 5;
        leftBound = transform.position.x - 5;
        cam = UnityEngine.Camera.main;
        player = GameObject.Find("Player");
        enemRb = GetComponent<Rigidbody>();
        enemRb.mass = emass;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveable){
            switch(xDirec){
            case 0: enemRb.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode.Impulse);
            if(transform.position.x >= rightBound) {
                xDirec = 1;
            }
            break;
            case 1: enemRb.AddForce(Vector3.left * moveSpeed * Time.deltaTime, ForceMode.Impulse);
            if(transform.position.x <= leftBound) {
                xDirec = 0;
            }
            break;
        }
        }
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
         if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0) {
            blowingLeft = player.GetComponent<Wind>().blowLeft;
            blowingRight = player.GetComponent<Wind>().blowRight;
            if(blowingLeft) {
                enemRb.AddForce(player.GetComponent<Wind>().windVectL * player.GetComponent<Wind>().windSpeed / 100 * player.GetComponent<Wind>().windMod * Time.deltaTime, ForceMode.Impulse);
            }
            if(blowingRight) {
                enemRb.AddForce(player.GetComponent<Wind>().windVect * player.GetComponent<Wind>().windSpeed / 100 * player.GetComponent<Wind>().windMod * Time.deltaTime, ForceMode.Impulse);
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
