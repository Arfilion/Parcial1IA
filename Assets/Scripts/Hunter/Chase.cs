using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    Renderer _rend;
    Hunter _hunter;


    
    public Chase(Hunter p)
    {
        _rend = p.GetComponent<Renderer>();

    }

    public override void OnEnter()
    {
        Hunter.instance.renderer.material.color = Color.red;
    }

    public override void OnUpdate()
    {
        Vector3 avoidanceObs = Hunter.instance.obstacleAvoidance();

        if (avoidanceObs != Vector3.zero)
        {
            Hunter.instance.AddForce(avoidanceObs);
        }
        else if (Vector3.Distance(Hunter.instance.transform.position, Hunter.instance._fleeTarget.position) <= Hunter.instance._viewRadius)
        {
        }
        else
        {
            Hunter.instance.AddForce(Hunter.instance.Arrive(Hunter.instance._seekTarget.position));
        }

        /*transform.position += _velocity * Time.deltaTime;
        if (_velocity != Vector3.zero) transform.right = _velocity;*/












        Hunter.instance.Arrive(Hunter.instance._target.transform.position);
        Hunter.instance.Move();
        
        
        
        
        
        
        Hunter.instance.energy -= Time.deltaTime * 2;
        Vector2 dist = Hunter.instance.transform.position - Hunter.instance._target.transform.position;
        if (Hunter.instance.energy <= 0)
        {
            fsm.ChangeState(EnemyActions.Rest);
        }
        else
        {
            if (dist.magnitude > Hunter.instance._viewRadius)
            {
                fsm.ChangeState(EnemyActions.Patrol);
            }
        }
        Debug.Log(Hunter.instance.obstacleAvoidance());

    }

    public override void OnExit()
    {
    }     
}
