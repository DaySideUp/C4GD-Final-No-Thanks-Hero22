using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        Destroy(camera.GetComponent<FollowPlayerX>());
        player.GetComponent<Wind>().state = 2;
        player.GetComponent<Wind>().StartCoroutine("LevelClearText");
        Destroy(gameObject);

    }

}
