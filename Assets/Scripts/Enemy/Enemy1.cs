using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy1 : MonoBehaviour
{
    private float dirX;
    public float moveSpeed;

    //private bool facingRight = false;
    private Vector3 localScale;
    public Transform[] patrolPoints;
    public int targetPoint;

    protected Rigidbody2D rb;
    private bool AtEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //moveSpeed = 3f;
        targetPoint = 0;
    }


    void FixedUpdate()
    {
        if(transform.position == patrolPoints[targetPoint].position)
        {
            IncreaseTargetInt();
        }
        

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, moveSpeed * Time.deltaTime);
    }
    void IncreaseTargetInt()
    {
        targetPoint++;
        if(targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
        
    }



    //void LateUpdate()
    //{
    //    CheckWhereToFace();
    //}

    //void CheckWhereToFace()
    //{
    //    if (dirX > 0)
    //        facingRight = true;
    //    else if (dirX < 0)
    //        facingRight = false;

    //    if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
    //        localScale.x *= -1;

    //    transform.localScale = localScale;
    //}
}
