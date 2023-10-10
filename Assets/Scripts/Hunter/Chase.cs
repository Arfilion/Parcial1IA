using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    Renderer _rend;
    Hunter _hunter;
    HunterSteering hunterSteering;


    
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
        Hunter.instance.AddForce(Hunter.instance.Persuit(Hunter.instance.prey));
        Hunter.instance.Move();
        Hunter.instance.Arrive(Hunter.instance._target.transform.position);

        Hunter.instance.energy -= Time.deltaTime * 2;
        Vector2 dist = Hunter.instance.transform.position - Hunter.instance._target.transform.position;
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
    }
     /*protected Vector3 Pursuit(SteeringAgent targetAgent) //Nosotros queremos ir hacia donde va nuestro objetivo, no hacia donde esta
     {
        Vector3 futurePos = targetAgent.transform.position + targetAgent._velocity;
        return Seek(futurePos);
     }
    protected Vector3 Seek(Vector3 targetPos)
    {
        return Seek(targetPos, Hunter.instance._maxSpeed);
    }

    protected Vector3 Seek(Vector3 targetPos, float speed)
    {
        Vector3 desired = (targetPos - Hunter.instance.transform.position).normalized * speed;

        Vector3 steering = desired - Hunter.instance._velocity;

        steering = Vector3.ClampMagnitude(steering, Hunter.instance._maxForce * Time.deltaTime);

        return steering;
    }
    protected void AddForce(Vector3 force)
    {
        Hunter.instance._velocity = Vector3.ClampMagnitude(Hunter.instance._velocity + force, Hunter.instance._maxSpeed);
    }

    protected void Move()
    {
        Hunter.instance.transform.position += Hunter.instance._velocity * Time.deltaTime;
        if (Hunter.instance._velocity != Vector3.zero) Hunter.instance.transform.right = Hunter.instance._velocity;
    }
    protected Vector3 Arrive(Vector3 targetPos)
    {
        float dist = Vector3.Distance(Hunter.instance.transform.position, targetPos);
        Debug.Log(dist);
        if (dist > Hunter.instance._viewRadius) return Seek(targetPos);

        return Seek(targetPos, Hunter.instance._maxSpeed * (dist / Hunter.instance._viewRadius));
    }*/
}
