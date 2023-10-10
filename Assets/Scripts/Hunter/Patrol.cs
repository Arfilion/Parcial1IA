using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    public float cooldown;
    Hunter _hunter;
    Renderer _rend;
    public Transform _target;
    public int wayPointObjective =0 ; //prueba

    public Patrol(Hunter p)
    {
        _hunter = p;
    }

    public override void OnEnter()
    {
        Hunter.instance.renderer.material.color = Color.blue;

    }

    public override void OnUpdate()
    {
        
        Hunter.instance.AddForce(Hunter.instance.Seek(Hunter.instance.patrolPoints[wayPointObjective].transform.position));//se mueve hacia la posicion
        Hunter.instance.Move();
        if (Vector3.Distance(Hunter.instance.transform.position, Hunter.instance.patrolPoints[wayPointObjective].transform.position) < 0.15f)
        {
            wayPointObjective++;
            Debug.Log(wayPointObjective);
        }
        else
        {
            Debug.Log("nosequepasa");
        }
        if(wayPointObjective== Hunter.instance.patrolPoints.Count)
        {
            wayPointObjective = 0;
        }

        Vector2 dist = Hunter.instance.transform.position - Hunter.instance._target.transform.position; 
        Hunter.instance.energy -= Time.deltaTime;
        if (Hunter.instance.energy > 0)
        {
            if (dist.magnitude <= Hunter.instance._viewRadius)
            {
                fsm.ChangeState(EnemyActions.Chase);
            }
        }
        else
        {
            fsm.ChangeState(EnemyActions.Rest);
        }
        
    }

    public override void OnExit()
    {
    }


}
