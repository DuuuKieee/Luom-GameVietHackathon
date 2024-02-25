using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class Chasing : MonoBehaviour
{
    // Start is called before the first frame update
    public AIPath aIPath;
    public GameObject canvas;
    public Animator anim;
    void Start()
    {    
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // if((Player.instance.transform.position - gameObject.transform.position).magnitude == 2f )
        // {
        //     Player.instance.isDied.Play();
        //     canvas.GetComponent<Animator>().Play("DeadPanel",0,0f);
        //     Invoke("Reset",0.25f);

        // }
        gameObject.GetComponent<Animator>().SetFloat("moveY", (Player.instance.transform.position.y - transform.position.y));
        gameObject.GetComponent<Animator>().SetFloat("moveX", (Player.instance.transform.position.x - transform.position.x));
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            // enterWaterPos = transform.position - new Vector3(Mathf.Clamp(rb.velocity.x, 0, 1), Mathf.Clamp(rb.velocity.y, 0, 1), 0);
            anim.SetBool("isWater", true);
            aIPath.maxSpeed = 4f;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            anim.SetBool("isWater", false);
            // enterWaterPos = transform.position - new Vector3(Mathf.Clamp(rb.velocity.x, 0, 1), Mathf.Clamp(rb.velocity.y, 0, 1), 0);
            aIPath.maxSpeed = 5f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.instance.isDied.Play();
            canvas.GetComponent<Animator>().Play("DeadPanel",0,0f);
            Invoke("Reset",0.25f);
        }
    }
    
    void Reset()
    {
        SceneManager.LoadScene(2);
    }
}
