using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public int energy;
    [SerializeField] int maxEnergy;
    [SerializeField] float _radius;
    public static Hunter instance;
    FiniteStateMachine _fsm; 

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        energy = maxEnergy;
        
    }
    void Start()
    {
        _fsm = new FiniteStateMachine(); 
        _fsm.AddState(EnemyActions.Rest, new Rest(this));
        _fsm.AddState(EnemyActions.Chase, new Chase(this));
        _fsm.ChangeState(EnemyActions.Chase);
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.Update();
        GetTired();

    }
    public void GetTired()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            energy -= 1;
        if (energy <= 0) _fsm.ChangeState(EnemyActions.Rest);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
public enum EnemyActions
{
    Chase,
    Patrol,
    Rest
}

