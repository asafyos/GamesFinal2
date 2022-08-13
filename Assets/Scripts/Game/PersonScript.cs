using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum PersonState
{
    idle,
    walking,
    running,
    coughing,
    chasing
}

public class PersonScript : MonoBehaviour
{
    public int playerNoticeTreshold = 120;
    public float detectDistance = 30;
    public float detectStartDistance = 15;
    public int damage = 15;
    public float breakTime = 3f;
    public bool randomPoints = false;
    public float speed = 3.5f;
    public bool isSick = false;
    public bool isCrazy = false;
    public float coughInterval = 3f;

    public AudioClip coughSound;

    public GameObject waypointsHolder;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private PlayerScript player;
    private Camera playerCamera;

    private PersonState state = PersonState.walking;

    private int nextPos = 0;

    private PersonState lastState;
    private bool playerNoticing = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        playerCamera = GameObject.FindWithTag("Player").GetComponentInChildren<Camera>();

        agent.speed = speed;

        GotoNextPoint();

        if (isSick)
        {
            InvokeRepeating("Cough", coughInterval, coughInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isCrazy)
        {
            float dist = FindPlayerDistance();
            playerNoticing = dist > 0 && dist <= detectStartDistance;
        }
        else
        {
            playerNoticing = false;
        }

        switch (state)
        {
            case PersonState.idle:
                animator.Play("Stand");
                agent.speed = speed;

                if (playerNoticing)
                {
                    lastState = state;
                    state = PersonState.chasing;
                    agent.autoBraking = true;
                }

                break;
            case PersonState.walking:
                agent.speed = speed;

                if (playerNoticing)
                {
                    lastState = state;
                    state = PersonState.chasing;
                    agent.autoBraking = true;
                }
                else if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    setState(PersonState.idle);

                    StartCoroutine(WaitForNextPoint());
                }

                break;
            case PersonState.chasing:
                agent.destination = player.transform.position;
                agent.speed = 2f * speed;

                if (!playerNoticing)
                {
                    state = lastState;
                    agent.autoBraking = false;
                    GotoNextPoint();
                }
                break;
        }
    }

    float FindPlayerDistance()
    {
        const int mask = ~(1 << 6);

        bool isSneek = Input.GetAxisRaw("Sneek") != 0;

        if (Physics.Raycast(transform.position + Vector3.up * 2f, (playerCamera.transform.position - Vector3.up * 3) - transform.position, out RaycastHit hit, detectDistance, mask))
        {
            if (hit.collider.gameObject.layer == 7)
            {
                return isSneek ? -1 : hit.distance;
            }
        }

        return -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetAxisRaw("Sneek") == 0 || !isCrazy)
            {
                if (state != PersonState.coughing)
                {

                    lastState = state;
                    agent.isStopped = true;
                    setState(PersonState.coughing);

                    if (coughSound != null)
                    {
                        audioSource.clip = coughSound;
                        audioSource.Play();
                    }

                    player.TakeDamage(damage * (isCrazy ? 2 : 1));

                    StartCoroutine(WaitTillTimerEnd());
                }
            }
        }
        else if (other.gameObject.tag == "Duck")
        {
            Destroy(other.gameObject);

            lastState = state;
            agent.isStopped = true;
            setState(PersonState.idle);

            StartCoroutine(WaitTillTimerEnd(3f));
        }
    }

    void GotoNextPoint()
    {
        if (waypointsHolder.transform.childCount == 0)
            return;

        agent.destination = waypointsHolder.transform.GetChild(nextPos).position;

        if (randomPoints)
        {
            nextPos = Random.Range(0, waypointsHolder.transform.childCount - 1);
        }
        else
        {
            nextPos = (nextPos + 1) % waypointsHolder.transform.childCount;
        }

        setState(PersonState.walking);
        lastState = state;
    }

    IEnumerator WaitForNextPoint()
    {
        yield return new WaitForSeconds(breakTime);
        GotoNextPoint();
    }

    IEnumerator WaitTillTimerEnd(float seconds = 1.5f)
    {
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;

        setState(lastState);
    }

    void setState(PersonState state)
    {
        this.state = state;
        switch (state)
        {
            case PersonState.idle:
                animator.Play("Stand");
                break;
            case PersonState.walking:
                animator.Play("Walk");
                break;
            case PersonState.running:
                animator.Play("Run");
                break;
            case PersonState.coughing:
                animator.Play("Cough");
                break;
            default:
                animator.Play("Stand");
                break;
        }

    }

    public void Cough()
    {
        lastState = state;
        agent.isStopped = true;
        setState(PersonState.coughing);

        if (coughSound != null)
        {
            audioSource.clip = coughSound;
            audioSource.Play();
        }

        StartCoroutine(WaitTillTimerEnd());
    }


}
