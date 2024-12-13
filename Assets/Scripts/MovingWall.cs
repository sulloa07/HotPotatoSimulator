using UnityEngine;
using Alteruna;
public class MovingWall : AttributesSync
{
    [SerializeField]
    [SynchronizableField]
    private float moveSpeed = 1f; // Speed of the wall movement

    [SerializeField]
    [SynchronizableField]
    private float moveHeight = 2f; // Height the wall moves up and down

    [SerializeField]
    [SynchronizableField]
    private float[] waitTimes = { 10f, 5f, 8f, 5f, 12f, 10f, 5f, 6f}; // Array of wait times before moving again

    [SerializeField]
    private AudioClip openAudio; // Audio clip for opening
    [SerializeField]
    private AudioClip closeAudio; // Audio clip for closing

    [SynchronizableField]
    private Vector3 startPos;

    [SynchronizableField]
    private bool movingDown = true;

    [SynchronizableField]
    private int currentWaitIndex = 0;

    [SynchronizableField]
    private float timer;

    private AudioSource audioSource;

    void Start()
    {
        // Check if the wall is tagged as "MovingWalls"
        if (!gameObject.CompareTag("MovingWalls"))
        {
            // If not tagged as "MovingWalls", disable this script
            enabled = false;
            return;
        }

        startPos = transform.position;
        timer = waitTimes[currentWaitIndex];
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        MoveWall();
    }

    private void MoveWall()
    {
        float moveStep = moveSpeed * Time.deltaTime;
        if (movingDown)
        {
            transform.position -= new Vector3(0, moveStep, 0);
            if (transform.position.y <= startPos.y - moveHeight)
            {
                movingDown = false;
                currentWaitIndex = (currentWaitIndex + 1) % waitTimes.Length;
                timer = waitTimes[currentWaitIndex];
                PlayAudio(closeAudio);
            }
        }
        else
        {
            transform.position += new Vector3(0, moveStep, 0);
            if (transform.position.y >= startPos.y)
            {
                movingDown = true;
                currentWaitIndex = (currentWaitIndex + 1) % waitTimes.Length;
                timer = waitTimes[currentWaitIndex];
                PlayAudio(openAudio);
            }
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}