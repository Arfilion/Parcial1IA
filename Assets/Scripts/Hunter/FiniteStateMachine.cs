using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour, IObserver
{
    [SerializeField] Subject _hunter;

    // Start is called before the first frame update

    public void OnNotify(EnemyActions action)
    {
        switch (action)
        {
            case EnemyActions.Rest:
                print("sanguchito");
                //StartCoroutine(Rest());
                return;
        }

    }
    private void OnEnable()
    {
        _hunter.AddObserver(this);
    }
    private void OnDisable()
    {
        _hunter.RemoveObserver(this);

    }
    
}
