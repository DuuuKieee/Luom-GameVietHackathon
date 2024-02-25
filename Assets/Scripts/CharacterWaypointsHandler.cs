using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

/*
 * Character moves between waypoints
 * */
public class CharacterWaypointsHandler : MonoBehaviour {
        
    private float speed = 4f;
    [SerializeField] GameObject confuseObj;

    [SerializeField] private List<Vector3> waypointList;
    [SerializeField] private List<float> waitTimeList;
    private int waypointIndex;

    [SerializeField] private Vector3 aimDirection;

    [SerializeField] private Player player;
    public bool isActive = false;
    [SerializeField] private Transform pfFieldOfView;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] GameObject enemySprite;

    private FieldOfView fieldOfView;
    private Animator myanim;
    [SerializeField] private LayerMask layerMask;
    Animator animSprite;



    private enum State {
        Waiting,
        Moving,
        Busy,
    }

    private State state;
    private float waitTimer;
    private Vector3 lastMoveDir;

    private void Start() {
        Transform bodyTransform = transform.Find("Body");
        animSprite = confuseObj.GetComponent<Animator>();
        state = State.Waiting;
        waitTimer = waitTimeList[0];
        lastMoveDir = aimDirection;

        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);
        myanim = enemySprite.GetComponent<Animator>();
    }

    private void Update() {
        switch (state) {
        default:
        case State.Waiting:
        case State.Moving:
            HandleMovement();
            FindTargetPlayer();
            FindTargetCanbo();
            break;
        case State.Busy:
            break;
        }

        if (fieldOfView != null) {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(GetAimDir());
        }

        Debug.DrawLine(transform.position, transform.position + GetAimDir() * 10f);
    }
    private void FindTargetCanbo() {
        if (Vector3.Distance(GetPosition(), CanBo.instance.GetPosition()) < viewDistance)
         {
            // Player inside viewDistance
            Vector3 dirToCanbo = (CanBo.instance.GetPosition() - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToCanbo) < fov / 2f) {
                // Player inside Field of View

                RaycastHit2D RaycastHit2D = Physics2D.Raycast(GetPosition(), dirToCanbo, viewDistance, layerMask);
                if (RaycastHit2D.collider != null) {
                    // Hit something
                    if (RaycastHit2D.collider.gameObject.GetComponent<CanBo>() != null) {
                        // Hit Player
                        Invoke("PlayerTarget",0.1f);
                        
                    } else {
                        // Hit something else
                    }
                }
            }
        }
    }

    private void FindTargetPlayer() {
        if (Vector3.Distance(GetPosition(), player.GetPosition()) < viewDistance)
         {
            // Player inside viewDistance
            Vector3 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 2f) {
                // Player inside Field of View
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance, layerMask);
                if (raycastHit2D.collider != null) {
                    // Hit something
                    if (raycastHit2D.collider.gameObject.GetComponent<Player>() != null) {
                        // Hit Player
                        Invoke("PlayerTarget",0.1f);
                        
                    } else {

                    }

                }
            }
        }
    }
   

    private void HandleMovement() {
        switch (state) {
        case State.Waiting:
            waitTimer -= Time.deltaTime;
            myanim.SetBool("waiting", false);
            if (waitTimer <= 0f) {
                state = State.Moving;
            }
            break;
        case State.Moving:
            Vector3 waypoint = waypointList[waypointIndex];

            Vector3 waypointDir = (waypoint - transform.position).normalized;
            lastMoveDir = waypointDir;

            float distanceBefore = Vector3.Distance(transform.position, waypoint);
            myanim.SetBool("waiting", true);
            myanim.SetFloat("moveX", (waypoint.x - transform.position.x));
            myanim.SetFloat("moveY", (waypoint.y - transform.position.y));

            transform.position = transform.position + waypointDir * speed * Time.deltaTime;
            float distanceAfter = Vector3.Distance(transform.position, waypoint);

            float arriveDistance = .1f;
            if (distanceAfter < arriveDistance || distanceBefore <= distanceAfter) {
                // Go to next waypoint
                waitTimer = waitTimeList[waypointIndex];
                waypointIndex = (waypointIndex + 1) % waypointList.Count;
                state = State.Waiting;
            }
            break;
        }
    }
    public void PlayerTarget()
    {
        player.isDie = true;
        player.Die();
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public Vector3 GetAimDir() {
        return lastMoveDir;
    }
    private void SlowDown()
    {
        speed = 1;
        confuseObj.SetActive(true);
        animSprite.SetBool("isConfuse",true);
        StartCoroutine(SlowCount(3.5f));
    }
    IEnumerator SlowCount(float sec)
    {
        yield return new WaitForSeconds(sec);
        confuseObj.SetActive(false);
        animSprite.SetBool("isConfuse",false);
        speed = 4;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Smoke")
        {
            SlowDown();
        }
    }

}
