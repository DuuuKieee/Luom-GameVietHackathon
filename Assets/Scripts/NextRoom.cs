using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    GameObject gameController, cameraObj;
    [SerializeField] GameObject enemyNextRoom, wallObj;

    [SerializeField] Collider2D coll;
    public bool isFinish = false;
    public bool isFinish1 = false;
    public bool isFinish2 = false;
    float determine;
    float determinex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        //coll = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.room == 1)
        {
            determine = 40;
            if (coll.isTrigger == false && isFinish) coll.isTrigger = true;
        }
        if(GameController.room ==2)
        {
            determine = 26;
            determinex=17;
            if (coll.isTrigger == false && isFinish1) coll.isTrigger = true;

        }
        if(GameController.room ==3)
        {
            determine = 21;
            determinex=23;
            if (coll.isTrigger == false && isFinish2) coll.isTrigger = true;
            cameraObj.GetComponent<CameraController>().maxX += 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 

            GameController.room++;
            cameraObj.GetComponent<CameraController>().maxY += determine;
            cameraObj.GetComponent<CameraController>().maxX += determinex;
            //Instantiate(wallObj, transform.position - new Vector3(0, 1.5f, 0), Quaternion.identity);
            collision.GetComponent<Player>().savepoint = gameObject.transform.position;
            Debug.Log(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
