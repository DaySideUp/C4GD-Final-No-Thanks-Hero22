using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class DialogueTriggerMod2 : MonoBehaviour
{
    public TextMeshProUGUI Dialogue;
    public TextMeshProUGUI NameBox;
    public float talkSpeed = .1f;
    public float speedCap = .1f;
    public string textToWrite = "";
    public string curText = "";
    private bool textDone = true;
    private bool sectDone = false;
    private  int index = 0;
    private int dialogueIndex = 0;
    private int maxDialogue = 0;
    private float skipCool = .5f;
    public List<string> NameList = new List<string>();
    public List<string> Script = new List<string>();
    public GameObject player;
    public AudioSource talkNoise;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameObject.SetActive(false);
        //StartDialogue(testScript, .1f, testScript.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if(textDone == false && talkSpeed > 0) {
            talkSpeed -= Time.deltaTime;
        }
        if(skipCool > 0) {
            skipCool -= Time.deltaTime;
        }
        if(textDone == true && talkSpeed <= 0) {
            gameObject.SetActive(false);
        }
        if(talkSpeed <= 0 && index < textToWrite.Length) {
            curText += textToWrite[index];
            index++;
            talkSpeed = speedCap;

        }  else if (talkSpeed <= 0 && index >= textToWrite.Length && sectDone == false) {
            sectDone = true;
        }
        if(sectDone && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))) {
            dialogueIndex++;
            if(dialogueIndex >= maxDialogue) {
                textDone = true;
                player.GetComponent<Wind>().state = 0;
                SceneManager.LoadScene("MainMenu");
                gameObject.SetActive(false);
            } else {
                talkNoise.Play();
                NameBox.text = NameList[dialogueIndex];
                curText = "";
                sectDone = false;
                index = 0;
                talkSpeed = speedCap;
                skipCool = .5f;

                textToWrite = Script[dialogueIndex];
            }
        }
        if(skipCool <= 0f && !sectDone && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))) {
            curText = textToWrite;
            index = textToWrite.Length;
            sectDone = true;
            talkSpeed = 2f;
            skipCool = .5f;
        }
        Dialogue.text = curText;
    }

    public void StartDialogue(List<string> ttw, float ts, int sL, List<string> names) {
        curText = "";
        player.GetComponent<Wind>().state = -1;
        player.GetComponent<Wind>().resetVelocity();
        Script.Clear();
        NameList.Clear();
        for(int i = 0; i < ttw.Count; i++) {
            Script.Add(ttw[i]);
            NameList.Add(names[i]);
        }
       
        textToWrite = Script[0];
        index = 0;
        dialogueIndex = 0;
        NameBox.text = names[dialogueIndex];
        maxDialogue = sL;
        talkSpeed = ts;
        speedCap = ts;
        textDone = false;
        talkNoise.Play();

    }
}
