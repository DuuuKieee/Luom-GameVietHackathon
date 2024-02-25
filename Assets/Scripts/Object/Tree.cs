using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    GameObject obj;
    Animator anim;
    public AudioSource brush;
    private void Awake()
    {
        obj = gameObject;
        brush = brush.GetComponent<AudioSource>();
        anim = obj.GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            brush.Play();
            anim.Play("TreeTransparent");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            anim.Play("TreeEndTransparent");
    }
}
