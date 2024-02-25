using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject blackScene, enemy,canvas;
    private GameObject music;
    void Start()
    {
        music = GameObject.FindGameObjectWithTag ("gameMusic");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
        Destroy(enemy);
        Player.instance.moveSpeed = 0;
        Player.instance.runSpeed = 0;
        Player.instance.animSprite.SetBool("isDied", true);
        canvas.GetComponent<Animator>().Play("DeadPanel",0,0f);
        blackScene.GetComponent<Animator>().Play("End 1",0,0f);
        
        Player.instance.isDied.Play();

        Invoke("EndScene",1.5f);
        Destroy(music);
        }

    }
    void EndScene()
    {
        SceneManager.LoadScene(3);

    }
}
