using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queslog : MonoBehaviour
{
    public Transform target;
    public float buffer;
    public float maxDistance;
    private SpriteRenderer rend;
    public static Queslog instance;
    // Start is called before the first frame update
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
    }
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 difference = transform.position - target.position;
            float Angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, Angle + buffer);
        }
    }
    float DistanceToQuest()
    {
        return Mathf.Clamp01(Vector2.Distance(transform.position, target.position) / maxDistance);
    }

}
