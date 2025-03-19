using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Collections;

public class Number : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer render;
    Rigidbody2D rb;
    TrailRenderer trail;

    public int value;
    bool canGrab;

    [Header("Rendering")]
    [SerializeField] Sprite[] numbers;

    [Header("Grabbing")]
    [SerializeField] bool followMouse;
    [SerializeField] float followSpeed;
    [SerializeField] float maxRotation = 0;
    [SerializeField] float rotationSmoothing;

    [Header("Spawning")]
    [SerializeField] int forceHz;
    [SerializeField] int forceVt;
    [SerializeField] float gravity;

    void Awake()
    {
        render = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        canGrab = false;
        StartCoroutine(SpawnTimer());
        GrabChange(false);

        //remove these
        transform.position = Vector2.zero;
        value = Random.Range(0, 10);

        //gravity
        rb.gravityScale = gravity;

        //visuals
        render.sprite = numbers[value];
        render.sortingOrder = value;

        //scaling
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.33f).SetEase(Ease.OutQuint);

        //bounce up
        rb.AddForce(new Vector2 (Random.Range(-forceHz, forceHz + 1), forceVt));
    }

    void Update()
    {
        Rotate();

        if (rb.linearVelocityY < 0 && rb.gravityScale < gravity * 2)
        {
            rb.gravityScale = gravity * 2;
        }

        //if off screen, disable
        if (transform.position.y < -15) { gameObject.SetActive(false); }
    }

    private void FixedUpdate()
    {
        if (followMouse)
        {
            if (!canGrab) GrabChange(false);
            transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), followSpeed);
        }
    }

    public void GrabChange(bool grab)
    {
        Debug.Log("Grab Change");
        followMouse = grab;

        //movement
        rb.gravityScale = grab ? 0 : gravity * 4;
        rb.linearVelocity = Vector2.zero;

        //juice
        trail.emitting = grab;

        switch (grab)
        {
            case true:
                render.transform.DOScale(1.5f, 0.1f);
                break;
            case false:
                render.transform.DOScale(1f, 0.1f);
                break;
        }
    }

    void Rotate()
    {
        if (!followMouse)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(rb.linearVelocityX * 20, -maxRotation, maxRotation));
        }

        if (followMouse)
        {
            float rotAmount = 0;

            if (transform.position.x + 0.05f < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                rotAmount = maxRotation;
            }

            else if (transform.position.x - 0.05f > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                rotAmount = -maxRotation;
            }

            float rotate = Mathf.LerpAngle(transform.eulerAngles.z, rotAmount, rotationSmoothing * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, rotate);
        }
    }

    private void OnMouseDown()
    {
        if (!followMouse)
        {
            GrabChange(true);
        }
    }

    private void OnMouseUp()
    {
        if (followMouse)
        {
            GrabChange(false);
        }
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(0.33f);
        ToggleGrabbable(true);
    }

    void ToggleGrabbable(bool state)
    {
        canGrab = state;

        if (followMouse && !canGrab)
        {
            GrabChange(false);
        }
    }
}