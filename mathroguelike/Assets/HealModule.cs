using System.Collections.Generic;
using UnityEngine;

public class HealModule : MonoBehaviour
{
    public List<NumberSlot> numberSlots = new List<NumberSlot>();
    public List<Operator> operators = new List<Operator>();

    public int total;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Calculate();
    }

    public void Calculate()
    {
        total = 0;

        int operations = -1;

        for (int i = 0; i < numberSlots.Count; i++)
        {
            if (numberSlots[i].isActiveAndEnabled)
            {
                operations++;
            }
        }

        Debug.Log("Number of Operations: " + operations);

        for (int i = 0; i < operations; i++)
        {
            switch (operators[i].type)
            {
                case Operator.OperatorType.Add:
                    total += numberSlots[i].value + numberSlots[i+1].value;
                    break;
                case Operator.OperatorType.Subtract:
                    total += numberSlots[i].value - numberSlots[i + 1].value;
                    break;
                case Operator.OperatorType.Multiply:
                    total += numberSlots[i].value * numberSlots[i + 1].value;
                    break;
                case Operator.OperatorType.Divide:
                    total += numberSlots[i].value / numberSlots[i + 1].value;
                    break;
            }
        }

        Debug.Log("Answer: " + total);
    }
}
