using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> eventObservers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        eventObservers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        eventObservers.Remove(observer);

    }

    public void EventHasTriggered(EnemyActions action)
    {
        foreach (IObserver observer in eventObservers)
        {
            observer.OnNotify(action);
        }
    }
}
