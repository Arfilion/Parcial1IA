using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hunter : SteeringAgent
{
    public float energy;
    [SerializeField] float maxEnergy;
    public float _radius; //revisar y coincidir
    public SteeringAgent _target;
    public static Hunter instance;
    FiniteStateMachine _fsm;
    public Renderer renderer;
    public float _maxSpeed, _maxForce;
    //public float _viewRadius;
    public Vector3 _velocity;
    public SteeringAgent prey;
    [SerializeField] List<SteeringAgent> boidsInScene = new List<SteeringAgent>();

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
        _fsm.AddState(EnemyActions.Patrol, new Patrol(this));
        _fsm.ChangeState(EnemyActions.Patrol);
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.Update();

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

