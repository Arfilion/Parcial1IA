using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hunter : SteeringAgent
{
    public float energy;
    [SerializeField] float maxEnergy;
    public SteeringAgent _target;
    public static Hunter instance;
    FiniteStateMachine _fsm;
    public Renderer renderer;
    public SteeringAgent prey;
    public List<SteeringAgent> boidsInScene;
    public List<GameObject> patrolPoints;


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
        new List<SteeringAgent>();
        new List<GameObject>();
    }
    void Start()
    {
        _fsm = new FiniteStateMachine(); 
        _fsm.AddState(EnemyActions.Rest, new Rest(this));
        _fsm.AddState(EnemyActions.Chase, new Chase(this));
        _fsm.AddState(EnemyActions.Patrol, new Patrol(this));
        _fsm.ChangeState(EnemyActions.Patrol);
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.Update();

    }
}
public enum EnemyActions
{
    Chase,
    Patrol,
    Rest
}

