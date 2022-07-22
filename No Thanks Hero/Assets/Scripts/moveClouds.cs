using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveClouds : MonoBehaviour
{
    private bool direction = false;
    public float speed = 2f;
    private GameObject player;
    private float playerMovementPrev;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = new Vector3(player.transform.position.x * .05f * Random.Range(1, 2), transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float playerX = player.transform.position.x;
        if(playerX > playerMovementPrev) {
            transform.Translate(Vector3.right * (playerX- playerMovementPrev));
        } else if( playerX < playerMovementPrev) {
            transform.Translate(Vector3.right * (playerX - playerMovementPrev));
        }
        if(direction) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        } else {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if(transform.position.x <= player.transform.position.x - 14f) {
            direction = false;
        }

        if(transform.position.x >= player.transform.position.x + 12f) {
            direction = true;
        }

        playerMovementPrev = player.transform.position.x;

        
    }
}
