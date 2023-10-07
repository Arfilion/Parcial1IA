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
       // _rend.material.color = Color.red;
    }

    public override void OnUpdate()
    {
        Vector2 dist = Hunter.instance.transform.position - Hunter.instance._target.position;
        if (Hunter.instance.energy <= 0)
        {
            fsm.ChangeState(EnemyActions.Rest);
        }
        else
        {
            if (dist.magnitude > Hunter.instance._radius)
            {
                fsm.ChangeState(EnemyActions.Patrol);
            }
        }
    }

    public override void OnExit()
    {
        //_rend.material.color = Color.white;

    }


}
