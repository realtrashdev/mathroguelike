using UnityEngine;
using UnityEngine.UIElements;

public class Number : MonoBehaviour
{
    SpriteRenderer render;
    Rigidbody2D rb;
    TrailRenderer trail;

    public int value;

    [Header("Rendering")]
    [SerializeField] Sprite[] numbers;

    [Header("Movement")]
    [SerializeField] bool followMouse;
    [SerializeField] int forceHz;
    [SerializeField] int forceVt;
    [SerializeField] float followSpeed;
    [SerializeField] float maxRotation = 0;
    [SerializeField] float rotationSmoothing;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        GrabChange(false);

        //if 0, disable
        if (value == 0) { gameObject.SetActive(false); }

        //set number and layer
        //bigger digit = higher layer
        render.sprite = numbers[value];
        render.sortingOrder = value;

        //remove these
        transform.position = Vector2.zero;

        //bounce up
        rb.AddForce(new Vector2 (Random.Range(-forceHz, forceHz + 1), forceVt));
    }

    void Update()
    {
        Rotate();

        if (followMouse)
        { 
            transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), followSpeed);
        }

        //if off screen, disable
        if (transform.position.y < -10) { gameObject.SetActive(false); }
    }

    public void GrabChange(bool grab)
    {
        Debug.Log("Grab Change");
        followMouse = grab;

        //movement
        rb.gravityScale = grab ? 0 : 0.5f;
        rb.linearVelocity = Vector2.zero;

        //juice
        trail.emitting = grab;
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
        GrabChange(true);
    }

    private void OnMouseUp()
    {
        GrabChange(false);
    }
}
