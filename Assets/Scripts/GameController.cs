using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int numEnemy = 0;
    static public int room;

    [SerializeField] public GameObject arrow1, arrow2, arrow3, arrow4, canvas;
    public bool quest1, quest2, quest3;
    GameObject player;
    [SerializeField] GameObject[] confuseItem;
    public static GameController instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        player = GameObject.FindGameObjectWithTag("Player");
         arrow1.transform.localScale = UnityEngine.Vector3.zero;
         arrow2.transform.localScale = UnityEngine.Vector3.zero;
         arrow3.transform.localScale = UnityEngine.Vector3.zero;
         arrow4.transform.localScale = UnityEngine.Vector3.zero;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        room = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.K)) canvas.SetActive(false);
         if (Input.GetKeyDown(KeyCode.L)) NextLevel();
         if (Input.GetKeyDown(KeyCode.O)) Player.instance.transform.position = new UnityEngine.Vector3(43f, 64f, 0f);
         if (Input.GetKeyDown(KeyCode.P)) 
         {
            Player.instance.transform.position = new UnityEngine.Vector3(8f, 23f, 0f);
            CanBo.instance.transform.position = new UnityEngine.Vector3(8f, 23f, 0f);}
        // if (Input.GetKeyDown(KeyCode.J)) canvas.SetActive(true);

        // // if (quest1 && arrow1.transform.localScale == Vector3.zero && room == 1)
        // // {
        // //     NextRoom(arrow1);
        // //     Debug.Log("Da");
        // // }
        // // if (!quest2  && arrow2.transform.localScale == Vector3.zero && room == 2)
        // // {
        // //     NextRoom(arrow2);
        // // }
        // // if (!quest3  && goal.transform.localScale == Vector3.zero && room == 3)
        // // {
        // //     goal.SetActive(true);
        // //     NextRoom(goal);
        // // }
    }
   
    public void NextRoom(GameObject arrow)
    {
        Time.timeScale = 0.01f;
        arrow.transform.localScale = UnityEngine.Vector3.one;
        CameraController.targetObj = arrow;
        StartCoroutine(TargetPlayer(2));
    }
    
    IEnumerator TargetPlayer(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        Time.timeScale = 1;
        CameraController.targetObj = player;
    }
    public void SpawnItem()
    {
        confuseItem[0].SetActive(true);
        confuseItem[1].SetActive(true);
        confuseItem[2].SetActive(true);
    }

    public static void NextLevel()
    {
        if (Level.level == 2)
        {
            Level.level = 1;
            SceneManager.LoadScene(2);
        }
        else
        {
            Level.level++;
            SceneManager.LoadScene(2);
        }
    }

    public static void Restart(bool isGoal)
    {
        if (isGoal)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
