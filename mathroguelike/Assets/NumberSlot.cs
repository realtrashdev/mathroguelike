using DG.Tweening;
using UnityEngine;

public class NumberSlot : MonoBehaviour
{
    [SerializeField] SpriteRenderer numberRenderer;

    public int value = 0;
    public bool hasValue = false;
    [SerializeField] float holdTime = 1;
    
    public float countdown;
    bool doCount = false;

    Number script;

    void Start()
    {
        countdown = holdTime;
    }

    void Update()
    {
        if (doCount)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            AddSlotValue();
            countdown = holdTime;
            doCount = false;
        }
    }

    public void AddSlotValue()
    {
        hasValue = true;

        value = script.value;

        numberRenderer.sprite = script.render.sprite;
        numberRenderer.transform.DOScale(1, 0.5f).SetEase(Ease.OutQuint);

        script.gameObject.SetActive(false);
        script = null;
    }

    public void ResetValue()
    {
        hasValue = false;
        numberRenderer.transform.DOScale(1, 0.5f).SetEase(Ease.OutQuint);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasValue) return;

        if (collision.CompareTag("Number"))
        {
            script = collision.GetComponent<Number>();
            doCount = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Number"))
        {
            doCount = false;
            countdown = holdTime;
        }
    }
}
