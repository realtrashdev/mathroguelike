using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NumberInfo
{
    public Vector2 position;
    public int value;

    public NumberInfo(Vector2 position, int value)
    {
        this.position = position;
        this.value = value;
    }
}

public class NumberManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> numbers = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            numbers.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNumber(NumberInfo info)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            if (!numbers[i].activeSelf)
            {
                GameObject obj = numbers[i];
                obj.SetActive(true);
                obj.transform.position = info.position;
                obj.GetComponent<Number>().value = info.value;
                break;
            }
        }
    }

    /*public void SpawnNumber()
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            if (!numbers[i].activeSelf)
            {
                numbers[i].SetActive(true);
                break;
            }
        }
    }*/
}