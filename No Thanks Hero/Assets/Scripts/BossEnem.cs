using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnem : MonoBehaviour
{
    int yDirec = 0;
    int xDirec = 0;
    public float speed = 2f;
    public float lowerBound = 5f;
    public float upperBound = 6f;
    public float leftBound = -14f;
    public float rightBound = 14f;
    public int pattern = 0;
    public int HP = 5;
    public GameObject enemy;
    public GameObject weakPoint;
    public GameObject[] spikes;
    public bool aggro = false;
    public bool started = false;
    public AudioSource hitNoise;
    public List<string> scrip;
    public List<string> name;
    public GameObject dialogueBox;
    public GameObject deathDialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(true);
        Debug.Log("Calling the Box");
        dialogueBox.GetComponent<DialogueScript>().StartDialogue(scrip, .05f, scrip.Count, name);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(aggro)  {
            switch(yDirec) {
            case 0: transform.Translate(Vector3.up * speed * Time.deltaTime);
            if(transform.position.y >= upperBound) {
                yDirec = 1;
            }
            break;
            case 1: transform.Translate(Vector3.down * speed * Time.deltaTime);
            if(transform.position.y <= lowerBound) {
                yDirec = 0;
            }
            break;
        }
        switch(xDirec){
            case 0: transform.Translate(Vector3.right * speed * Time.deltaTime);
            if(transform.position.x >= rightBound) {
                xDirec = 1;
            }
            break;
            case 1: transform.Translate(Vector3.left * speed * Time.deltaTime);
            if(transform.position.x <= leftBound) {
                xDirec = 0;
            }
            break;
            }
        }
        if(!dialogueBox.activeInHierarchy && !started) {
            Debug.Log("Starting");
            StartCoroutine("bossAttacks");
            aggro = true;
            started = true;
        }
        
    }

    IEnumerator bossAttacks() {
        while(aggro) {
            yield return new WaitForSeconds(Random.Range(2, 3.5f));
            if(pattern < 2)  {
            Instantiate(enemy, new Vector3(Random.Range(-13, 13), 15f, -1f), enemy.transform.rotation);
            } else if(pattern < 9) {
                int atk = (int) (Random.Range(0, 3));
                switch(atk) {
                    case 0:
                    Instantiate(spikes[0], new Vector3(Random.Range(-13, 13), 15f, -1f), spikes[0].transform.rotation);
                    break;
                    case 1:
                    Instantiate(spikes[1], new Vector3(-24, Random.Range(-2, 13), -1f), spikes[1].transform.rotation);
                    break;
                    case 2:
                    Instantiate(spikes[2], new Vector3(24, Random.Range(0, 16), -1f), spikes[2].transform.rotation);
                    break;
                } 

            } else {
                weakPoint.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(0, 13), -1);
                weakPoint.SetActive(true);
                pattern = 0;
            }
            pattern++;
        }
    }

    public void Hit() {
        Debug.Log("HIT!");
        HP--;
        hitNoise.Play();
        weakPoint.SetActive(false);
        if(HP <= 0) {
            dialogueBox.GetComponent<DialogueScript>().enabled = false;
            dialogueBox.GetComponent<DialogueTriggerMod>().enabled = true;
            aggro = false;
            deathDialogue.SetActive(true);
        }

    }
}
