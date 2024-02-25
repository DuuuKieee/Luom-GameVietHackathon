using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject obj;
    static Animator anim;
    private Transform player;
    public float maxX = 0, minX = 0, maxY = 0, minY = 0;
    public float moveSpeed;
    public bool isZoomOut =false;

    static bool isFollowPlayer = true;
    //bool isScoll = false;

    Rigidbody2D rigi;

    static public GameObject targetObj;
    Vector3 target;
    // Start is called before the first frame update
    void Awake()
    {
        obj = gameObject;
        anim = gameObject.GetComponent<Animator>();
        rigi = obj.GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("PlayerAnim").transform;
    }

    private void Start()
    {
        isFollowPlayer = true;
        target = obj.transform.position;
        targetObj = player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null /*&& isFollowPlayer*//*&& isScoll == false*/)
        {
            // Tao camera di chuyen theo targetObj
            target.x = targetObj.transform.position.x;
            target.y = targetObj.transform.position.y;

            if (target.x < minX) target.x = minX;
            if (target.x > maxX) target.x = maxX;
            if (target.y < minY) target.y = minY;
            if (target.y > maxY) target.y = maxY;
            if(gameObject.transform.position.y >= 55 && !isZoomOut) 
            {
                ZoomOut();
                isZoomOut = true;
            }
            else if (gameObject.transform.position.y < 55 && isZoomOut)
            {
                ZoomIn();
                isZoomOut = false;
            }


            obj.transform.position = Vector3.Lerp(obj.transform.position, target, moveSpeed);
        }

        //if (isFollowPlayer == false) Invoke("FollowPlayer", 0.5f);
    }

    public Vector2 CameraPos
    {
        get { return transform.position; }
        set { 
                transform.position = value;
            transform.position -= new Vector3(0, 0, 10);
            }
    }

    public static void Shake() //rung
    {
        anim.Play("Shake");
    }

    public static void LightShake() //rung nhe
    {
        anim.Play("LightShake");
    }

    public static void SlideRight() //cuon phai
    {        
        anim.Play("SlideRight");
        isFollowPlayer = false;
    }

    public static void SlideLeft()
    {
        anim.Play("SlideLeft");
        isFollowPlayer = false;
    }

    public static void ZoomIn()
    {
        anim.Play("ZoomIn");
    }

    public static void ZoomOut()
    {
        anim.Play("ZoomOut");
    }

    public static void SuperZoomIn()
    {
        anim.Play("SuperZoomIn");
    }

    void FollowPlayer()
    {
        isFollowPlayer = true;
    }

    //public void Scoll()
    //{
    //    isScoll = true;
    //}
}
