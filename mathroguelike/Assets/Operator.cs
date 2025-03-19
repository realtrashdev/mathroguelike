using DG.Tweening;
using UnityEngine;

public class Operator : MonoBehaviour
{
    public enum OperatorType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public OperatorType type;
    SpriteRenderer render;
    [SerializeField] Sprite[] operatorSprites;

    public void UpdateOperator(OperatorType newType)
    {
        type = newType;

        switch (type)
        {
            case OperatorType.Add:
                render.sprite = operatorSprites[0];
                break;
            case OperatorType.Subtract:
                render.sprite = operatorSprites[1];
                break;
            case OperatorType.Multiply:
                render.sprite = operatorSprites[2];
                break;
            case OperatorType.Divide:
                render.sprite = operatorSprites[3];
                break;
        }

        render.transform.localScale = Vector2.zero;
        render.transform.DOScale(1, 0.5f);
    }
}
