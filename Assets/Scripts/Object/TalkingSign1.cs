using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingSign1 : MonoBehaviour
{
    public GameObject bubbleChat;
    public GameObject canvasDialogue, panel, dialogue;

    TriggerDialogue triggerDialogue;

    bool isTalking, isInColiider;
    public AudioSource dialogueAudio;

    Animator animPanel, anim;

    private void Awake()
    {
        dialogueAudio = dialogueAudio.GetComponent<AudioSource>();
        triggerDialogue = dialogue.GetComponent<TriggerDialogue>();
        animPanel = panel.GetComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        isInColiider = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInColiider)
        {
            if (Input.GetKey(KeyCode.F) && isTalking == false && canvasDialogue.activeSelf == false)
            {
                isTalking = true;
                dialogueAudio.Play();

                canvasDialogue.SetActive(true);
                animPanel.Play("Start");
                triggerDialogue.ResetStart();
                print("Talk");
                Time.timeScale = 0;
            }
            if (triggerDialogue.isEndDialogue == true && triggerDialogue.isTalking == false && canvasDialogue.activeSelf == true && isTalking == true)
            {
                animPanel.Play("End");
                //Time.timeScale = 1;
                Invoke("SetFalseCanvasDialogue", 1f);
                isTalking = false;
            }
        }
    }

    void SetFalseCanvasDialogue()
    {
        canvasDialogue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInColiider = true;
            bubbleChat.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInColiider = false;
            bubbleChat.SetActive(false);
        }
    }
}
