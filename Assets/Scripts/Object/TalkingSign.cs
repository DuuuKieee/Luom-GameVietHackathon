using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingSign : MonoBehaviour
{
    public GameObject bubbleChat, menuSkill;
    public GameObject canvasDialogue, panel, dialogue;
    public GameObject NextRoom1;
    public GameObject CanBo, NPC1, NPC2, NPC3;
    public GameObject arrow1, arrow2, arrow3;

    TriggerDialogue triggerDialogue;

    bool isTalking, isInColiider;
    public AudioSource dialogueAudio;

    Animator animPanel;

    private void Awake()
    {
        dialogueAudio = dialogueAudio.GetComponent<AudioSource>();
        triggerDialogue = dialogue.GetComponent<TriggerDialogue>();
        animPanel = panel.GetComponent<Animator>();
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
                if (gameObject.CompareTag("NPC1"))
                {
                    Quest1Manager.instance.Quest1.SetActive(true);
                    NextRoom1.GetComponent<NextRoom>().isFinish = true;
                    Quest1Manager.instance.OnQuestClick();
                    gameObject.GetComponent<TalkingSign>().enabled = false;
                    GameController.instance.NextRoom(GameController.instance.arrow1);

                }
                if (gameObject.CompareTag("NPC2"))
                {
                    Quest1Manager.instance.Quest2.SetActive(true);
                    Quest1Manager.instance.FinishQuest1();
                    Quest1Manager.instance.StartQuest();
                }
                if (gameObject.CompareTag("NPC3"))
                {
                    Quest1Manager.instance.FinishQuest3();
                    GameController.instance.NextRoom(GameController.instance.arrow3);
                    Player.instance.canSmoke = true;
                    Player.instance.isActivevSkill = true;
                    GameController.instance.SpawnItem();
                    menuSkill.SetActive(true);

                    Quest1Manager.instance.Quest2.SetActive(true);


                }
                if (gameObject.CompareTag("NPC4"))
                {
                    CanBo.GetComponent<SpriteRenderer>().enabled = true;
                    CanBo.GetComponent<CanBo>().isActive = true;
                    Destroy(gameObject);
                    Destroy(NPC2);
                    Destroy(NPC3);
                    Quest1Manager.instance.OnQuestClick();
                    arrow1.transform.localScale = new Vector2(1f, -1f);
                    arrow2.transform.localScale = new Vector2(1f, -1f);
                    arrow3.transform.localScale = new Vector2(1f, -1f);
                    GameController.instance.NextRoom(GameController.instance.arrow1);

                    NPC1.GetComponent<TalkingSign>().enabled = false;
                    Player.instance.savepoint = gameObject.transform.position;



                }
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
            // bubbleChat.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInColiider = false;
            // bubbleChat.SetActive(false);
        }
    }
}
