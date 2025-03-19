using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/NumberInfo Event Channel")]
public class NumberInfoEventChannel : GenericEventChannel<NumberInfoEvent> {}

[System.Serializable]
public struct NumberInfoEvent
{
    public NumberInfo Info;

    public NumberInfoEvent(NumberInfo inf) => Info = inf;
}
