using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject ballSpriteObj, afterImageObj, dieObj, blackScene;
    [SerializeField] Slider dashSlider, confuseSlider;


    [SerializeField] public float moveSpeed, runSpeed, dashStopSpeed;
    [SerializeField] float timeDashing, timeHurting;
    [SerializeField] StaminaManager staminaVar;

    public Animator animSprite, anim, dashAnim;

    Rigidbody2D rb;
    public Image menuSkill;

    float xdir, ydir, xdirRaw, ydirRaw, xdirRawDash, ydirRawDash; //2 bien cuoi: bien luu tru gia tri cuar Input.getAxitRaw khi bat dau Dash
    public bool isPressMoveKey, canRun, isCanConotrol, isJumping, isHurting, isCanBeHurted, isConfuse, isDrown, isDie = false, isGoal, canSmoke, isSmoking = false, isActivevSkill;

    static public float life;
    public Vector2 savepoint;

    Color color;
    public AudioSource isDied, onWater;
    Collider2D collider2D;

    public ParticleSystem obtainEff, dushEffect, smokeEff;
    public static Player instance;
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        animSprite = ballSpriteObj.GetComponent<Animator>();
        isDied = isDied.GetComponent<AudioSource>();
        onWater = onWater.GetComponent<AudioSource>();
        dashAnim = dashSlider.GetComponentInChildren<Animator>();
        collider2D = gameObject.GetComponent<Collider2D>();
        staminaVar.value = staminaVar.maxValue;


        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        color = ballSpriteObj.GetComponent<SpriteRenderer>().material.color;
        dashSlider.gameObject.SetActive(true);
        isCanConotrol = true;
        isCanBeHurted = true;
        isConfuse = false;
        isDie = false;
    }

    private void FixedUpdate()
    {

        xdir = Input.GetAxis("Horizontal");
        ydir = Input.GetAxis("Vertical");
        xdirRaw = Input.GetAxisRaw("Horizontal");
        ydirRaw = Input.GetAxisRaw("Vertical");
        if (staminaVar.value == staminaVar.maxValue)
        {
            canRun = true;
        }
        else if (staminaVar.value <= 5)
        {
            canRun = false;
        }
        if (xdirRaw != 0 || ydirRaw != 0) isPressMoveKey = true;
        else isPressMoveKey = false;
        if (Input.GetKey(KeyCode.Space) && isCanConotrol && isDrown == false && canRun)
        {
            Running();
            staminaVar.value -= 4;
        }
        if (Input.GetMouseButton(1))
        {
            if (canSmoke)
            {
                animSprite.SetBool("isSkill", true);
                Invoke("Smoking", 0.25f);
            }
        }



        if (isCanConotrol && isDrown == false) Walking();
        // if (isConfuse && confuseSlider.value >= confuseSlider.maxValue) EndConfuse();
    }

    void Walking()
    {
        if (Time.timeScale != 0)
        {

            transform.position += new Vector3(xdir * moveSpeed * Time.deltaTime, ydir * moveSpeed * Time.deltaTime);

            animSprite.SetFloat("xSpeed", xdirRaw);
            animSprite.SetFloat("ySpeed", ydirRaw);
        }
    }
    void Smoking()
    {
        if (Time.timeScale != 0)
        {
            Instantiate(smokeEff, transform.position, Quaternion.identity);
            animSprite.SetBool("isSkill", false);
            menuSkill.color = new Color32(255, 255, 255, 120);
            canSmoke = false;
        }
    }

    void Running()
    {
        transform.position += new Vector3(xdir * runSpeed * Time.deltaTime, ydir * runSpeed * Time.deltaTime);
        CameraController.LightShake();
        animSprite.SetFloat("xSpeed", xdir);
        animSprite.SetFloat("ySpeed", ydir);
        Instantiate(dushEffect, transform.position, Quaternion.identity);
        // isCanConotrol = false;
        // if (isConfuse) rb.velocity = - new Vector2(xdirRawDash, ydirRawDash).normalized * dashSpeed;
        // else rb.velocity = new Vector2(xdirRawDash, ydirRawDash).normalized * dashSpeed;

        // animSprite.speed = 4;
        // anim.SetBool("canRun", canRun);
    }

    IEnumerator StopDashing(float sec)
    {
        yield return new WaitForSeconds(sec);

        isCanConotrol = true;

        if (canRun)
        {
            if (isConfuse) rb.velocity = -new Vector2(xdirRawDash, ydirRawDash).normalized * dashStopSpeed;
            else
                rb.velocity = new Vector2(xdirRawDash, ydirRawDash).normalized * dashStopSpeed;
        }
        canRun = false;
        animSprite.speed = 1;
        anim.SetBool("canRun", canRun);

    }

    IEnumerator AppearAfterImage(float sec)
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(afterImageObj, ballSpriteObj.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator EndHurt(float sec)
    {
        yield return new WaitForSeconds(sec);

        isHurting = false;
        isCanBeHurted = true;

        //animSprite.Play("MoveBlendTree");
        anim.SetBool("isHurting", isHurting);
    }

    public void Land()
    {
        isJumping = false;
        isCanBeHurted = true;
        Invoke("CreateDustEffect", 0.05f);
    }

    void CreateDustEffect()
    {
        Instantiate(dushEffect, transform.position, Quaternion.identity);
    }

    void Jump()
    {
        if (!isHurting)
            anim.Play("Jump");
        //Ham InAir duoc lam o trong animation
    }

    void InAir()
    {
        isJumping = true;
        isCanBeHurted = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mushroom")
        {
            collision.GetComponent<Mushroom>().Bounce();
        }
        if (collision.gameObject.tag == "Water")
        {
            // enterWaterPos = transform.position - new Vector3(Mathf.Clamp(rb.velocity.x, 0, 1), Mathf.Clamp(rb.velocity.y, 0, 1), 0);
            moveSpeed = 1.2f;
            runSpeed = 4;
            animSprite.SetBool("isDrown", true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            // enterWaterPos = transform.position - new Vector3(Mathf.Clamp(rb.velocity.x, 0, 1), Mathf.Clamp(rb.velocity.y, 0, 1), 0);
            moveSpeed = 4;
            runSpeed = 4;
            animSprite.SetBool("isDrown", false);
        }
    }

    Vector3 enterWaterPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Water")
        {
            // enterWaterPos = transform.position - new Vector3(Mathf.Clamp(rb.velocity.x, 0, 1), Mathf.Clamp(rb.velocity.y, 0, 1), 0);
            onWater.Play();
            moveSpeed = 1.2f;
            animSprite.SetBool("isDrown", true);
        }
        if (collision.tag == "UndefeatEnemy")
        {
            isDie = true;
            Invoke("Die", 0.5f);
        }
        if (collision.tag == "ConfuseItem")
        {
            canSmoke = true;
            menuSkill.color = new Color32(255, 255, 255, 255);
            if (isActivevSkill) collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canRun) Instantiate(dushEffect, transform.position, Quaternion.identity);
        if (collision.gameObject.tag == "PatrolEnemy")
        {
            Invoke("Die", 0.5f);
        }
        if (collision.gameObject.tag == "UndefeatEnemy")
        {
            isDie = true;
            Invoke("Die", 0.5f);
        }
        if (collision.gameObject.tag == "Water" && isCanBeHurted)
        {
            // print("hit water");
            // Vector3 landPos = transform.position + new Vector3(collision.contacts[0].normal.x, collision.contacts[0].normal.y, 0);
            // if (collision.contacts[0].normal.x > 0.9 || collision.contacts[0].normal.x < -0.9)
            //     transform.Translate(new Vector2(-collision.contacts[0].normal.x, 0));
            // if (collision.contacts[0].normal.y > 0.9)
            //     transform.Translate(new Vector2(0, -collision.contacts[0].normal.y));
            // if (collision.contacts[0].normal.y < -0.9)
            //     transform.Translate(new Vector2(0, -collision.contacts[0].normal.y * 0.3f));
            // //transform.Translate(new Vector2(-collision.contacts[0].normal.x*1.2f, -collision.contacts[0].normal.y*1.2f));

            // rb.velocity = Vector2.zero;
            // isCanConotrol = false;
            // isDrown = true;
            // anim.Play("Drown");
            // animSprite.speed = 1;
            // animSprite.Play("Drown");
            // StartCoroutine(LenBo(1, landPos));
        }

        //canRun = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water" && isCanBeHurted)
        {
            // print("hit water");
            // Vector3 landPos = enterWaterPos;

            // rb.velocity = Vector2.zero;
            // isCanConotrol = false;
            // isDrown = true;
            // anim.Play("Drown");
            // animSprite.speed = 1;
            // animSprite.Play("Drown");
            // StartCoroutine(LenBo(1, landPos));
        }
    }

    void EndConfuse()
    {
        isConfuse = false;
        color.r = 1;
        ballSpriteObj.GetComponent<SpriteRenderer>().material.color = color;
    }

    // IEnumerator LenBo(float sec, Vector3 landPos)
    // {
    //     yield return new WaitForSeconds(sec);
    //     transform.position = landPos;
    //     isDrown = false;
    //     isCanConotrol = true;

    //     //Mot cai giong ham Hurt() danh rieng cho viec roi xuong nuoc
    //     animSprite.speed = 0;
    //     anim.Play("Blink");
    //     anim.SetBool("isHurting", isHurting);

    //     animSprite.Play("MoveBlendTree");
    //     isHurting = true;
    //     isCanBeHurted = false;
    //     StartCoroutine(EndHurt(timeHurting));

    //     print("Player Hurt");
    // }

    void Hurt()
    {
        if (isHurting == false && isCanBeHurted)
        {
            //if (isConfuse) EndConfuse();

            canRun = false;
            isHurting = true;
            isCanBeHurted = false;

            CameraController.Shake();

            anim.Play("Blink");
            anim.SetBool("isHurting", isHurting);

            animSprite.speed = 1;
            animSprite.Play("HurtBlendTree");

            StartCoroutine(EndHurt(timeHurting));

            print("Player Hurt");
        }
    }

    public void Die()
    {
        // anim.Play("Die");

        collider2D.enabled = false;
        isDied.Play();
        GameObject dieObject;
        dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        CanBo.instance.Die();
        Destroy(dieObject, 0.5f);
        ballSpriteObj.SetActive(false);
        Invoke("Respawn", 0.8f);
        Invoke("BlackSceneEnd", 0.2f);
        if (isActivevSkill)
        {
            GameController.instance.SpawnItem();
            menuSkill.color = new Color32(255, 255, 255, 255);
            canSmoke = true;
        }

    }
    public void Respawn()
    {
        ballSpriteObj.SetActive(true);
        gameObject.transform.position = savepoint;
        collider2D.enabled = true;
    }

    public void BlackSceneEnd()
    {
        blackScene.GetComponent<Animator>().Play("End", 0, 0f);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
