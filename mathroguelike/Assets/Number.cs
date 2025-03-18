using UnityEngine;

public class Number : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] bool followMouse;

    [Header("Movement")]
    [SerializeField] int forceHz;
    [SerializeField] int forceVt;
    [SerializeField] float followSpeed;
    [SerializeField] float maxRotation = 0;
    [SerializeField] float rotationSmoothing;
    [SerializeField] float rotationMultiplier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        transform.position = Vector2.zero;

        //bounce up to give player time to catch
        if (!followMouse) rb.AddForce(new Vector2 (Random.Range(-forceHz, forceHz + 1), forceVt));

        rb.gravityScale = !followMouse ? 0.5f : 0;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        if (followMouse) { transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), followSpeed); }
    }

    public void Grabbed()
    {
        followMouse = true;
    }

    void Rotate()
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

        float rotate = Mathf.Lerp(transform.eulerAngles.z, rotAmount, rotationSmoothing * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, rotate);
        //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 0, -20), rotationSmoothing * Time.deltaTime);
    }
}
