using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Animations;

public class CanBo : MonoBehaviour
{
    public AIPath aIPath;
    public GameObject canvasDialogue, panel, dialogue, goal, bubbleChat;
    TriggerDialogue triggerDialogue;
    [SerializeField] GameObject enemy;
    public bool isActive, atEnd = false;
    public GameObject NPC1;

    bool isTalking, isInColiider;
    public static CanBo instance;
    Animator animPanel, anim;
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        triggerDialogue = dialogue.GetComponent<TriggerDialogue>();
        animPanel = panel.GetComponent<Animator>();
        isTalking = false;
        anim = gameObject.GetComponent<Animator>();
        isInColiider = false;
        instance = this;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                aIPath.canMove = !aIPath.canMove;
            }
            if (triggerDialogue.isEndDialogue == true && triggerDialogue.isTalking == false && canvasDialogue.activeSelf == true && isTalking == true)
            {
                animPanel.Play("End");
                Invoke("SetFalseCanvasDialogue", 1f);
                isTalking = false;
                enemy.SetActive(true);
                goal.SetActive(true);
                Player.instance.savepoint = gameObject.transform.position;
                GameController.instance.NextRoom(GameController.instance.arrow4);

                gameObject.GetComponent<AIDestinationSetter>().target = NPC1.transform;
                aIPath.canMove = true;
                atEnd = true;
                isActive = false;

            }
        }
        if (aIPath.canMove && !atEnd)
        {
            bubbleChat.SetActive(true);
            anim.SetFloat("Yspeed", (Player.instance.transform.position.y - transform.position.y));
        }
        else
        {
            anim.SetFloat("Yspeed", 0);
            bubbleChat.SetActive(false);
        }
        if (atEnd)
        {
            anim.SetFloat("Yspeed", (NPC1.transform.position.y - transform.position.y));
        }

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "MissionEvent")
        {
            Destroy(coll.gameObject);
            isTalking = true;
            canvasDialogue.SetActive(true);
            animPanel.Play("Start");
            triggerDialogue.ResetStart();
            print("Talk");
            Debug.Log("ENDMISSION");
            Time.timeScale = 0;
        }
        if (coll.tag == "UndefeatEnemy")
        {
            Player.instance.isDie = true;
            Player.instance.Die();
            Die();
        }
        if (coll.tag == "SavePoint")
        {
            Player.instance.savepoint = coll.gameObject.transform.position;
            Destroy(coll.gameObject);
        }
        if (coll.tag == "Water")
        {
            // enterWaterPos = transform.position - new Vector3(Mathf.Clamp(rb.velocity.x, 0, 1), Mathf.Clamp(rb.velocity.y, 0, 1), 0);
            anim.SetBool("isDrown", true);
        }

    }
    void SetFalseCanvasDialogue()
    {
        canvasDialogue.SetActive(false);
    }
    public void Die()
    {
        if (isActive) transform.position = Player.instance.savepoint;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mushroom")
        {
            collision.GetComponent<Mushroom>().Bounce();
        }
        if (collision.tag == "Water")
        {

            anim.SetBool("isDrown", true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            anim.SetBool("isDrown", false);
        }
    }

}
