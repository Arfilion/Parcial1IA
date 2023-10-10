using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    public float cooldown;
    Hunter _hunter;
    Renderer _rend;
    public Transform _target;

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
