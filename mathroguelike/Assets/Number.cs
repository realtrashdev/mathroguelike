using UnityEngine;

public class Number : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] bool followMouse;

    [Header("Settings")]
    [SerializeField] int forceHz;
    [SerializeField] int forceVt;

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
        if (followMouse) { transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1); }
    }

    public void Grabbed()
    {
        followMouse = true;
    }

    void Rotate()
    {
        float rotAmount = rb.linearVelocity.x;

        transform.eulerAngles = new Vector3(0, 0, rotAmount);
    }
}
