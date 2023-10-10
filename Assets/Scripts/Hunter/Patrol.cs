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
        Vector3 _patrolPoint = Vector3.zero;
        Vector3 distProximity = (Hunter.instance.transform.position - Hunter.instance.patrolPoints[0].transform.position);
        //Vector3 distProximity = (Hunter.instance.transform.position - Hunter.instance.patrolPoints[0].transform.position);
        Hunter.instance.AddForce(Hunter.instance.Seek(Hunter.instance.patrolPoints[0].transform.position));
        //Hunter.instance.AddForce(Hunter.instance.Arrive(Hunter.instance.patrolPoints[0].transform.position));//se mueve hacia la posicion
        Hunter.instance.Move();

        if ( Vector3.Distance(Hunter.instance.transform.position, Hunter.instance.patrolPoints[0].transform.position) <0.15f)  /*distProximity.magnitude < 0.15f && _patrolPoint==Hunter.instance.patrolPoints[0].transform.position)*/
        {
            Debug.Log("Holi");

            
            wayPointObjective ++;
            if (wayPointObjective ==1)
            {
                  Debug.Log(wayPointObjective);
              //  distProximity= Hunter.instance.transform.position - Hunter.instance.patrolPoints[wayPointObjective].transform.position;
              //
               Hunter.instance.AddForce(Hunter.instance.Seek(Hunter.instance.patrolPoints[1].transform.position));//se mueve hacia la posicion
               Hunter.instance.AddForce(Hunter.instance.Arrive(Hunter.instance.patrolPoints[1].transform.position));
               Hunter.instance.Move();
               if (Vector3.Distance(Hunter.instance.transform.position, Hunter.instance.patrolPoints[1].transform.position) < 0.15f)
               {
                   wayPointObjective++;
                   distProximity = Hunter.instance.transform.position - Hunter.instance.patrolPoints[wayPointObjective].transform.position;
                   //
                   Hunter.instance.AddForce(Hunter.instance.Seek(Hunter.instance.patrolPoints[wayPointObjective].transform.position));//se mueve hacia la posicion
                   Hunter.instance.AddForce(Hunter.instance.Arrive(Hunter.instance.patrolPoints[wayPointObjective].transform.position));
                   Hunter.instance.Move();
               }

            }
            
        }
        

        //comentado a modo prueba
        Vector2 distWaypoint = Hunter.instance.transform.position - Hunter.instance.prey.transform.position;

        
        //no hace falta tocar
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
