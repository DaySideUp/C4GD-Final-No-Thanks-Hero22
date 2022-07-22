using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScriptTriggerMOD : MonoBehaviour
{
    public List<string> dialogue = new List<string>();
    public List<string> names = new List<string>();
    public float talkSpeed;
    public GameObject dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        //dialogueBox = GameObject.Find("TextboxFrame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        dialogueBox.SetActive(true);
        dialogueBox.GetComponent<DialogueTriggerMod>().StartDialogueEll(dialogue, talkSpeed, dialogue.Count, names);
        Destroy(gameObject);
    }
}
