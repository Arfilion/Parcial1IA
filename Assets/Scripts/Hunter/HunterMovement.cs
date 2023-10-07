using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMovement : MonoBehaviour
{

    [SerializeField] protected float _maxspeed, _maxforce;
    [SerializeField] float _viewRadius;
    public Vector3 _veclocity;

    [SerializeField] LayerMask _obstacles;

    void Start()
    {

    }
    protected Vector3 Seek(Vector3 targetPos)
    {
        return Seek(targetPos, _maxspeed);
    }

    public void Move()
    {
        transform.position += _veclocity * Time.deltaTime;
        if (_veclocity != Vector3.zero) transform.right = _veclocity;
    }
    protected bool HasToUseObstacleAvoidance()
    {
        Vector3 avoidanceObs = obstacleAvoidance();  

        if (avoidanceObs != Vector3.zero)
        {
            AddForce(avoidanceObs);
        }
        return avoidanceObs != Vector3.zero;
    }

    public Vector3 Seek(Vector3 targetPos, float speed)
    {
        Vector3 desired = (targetPos - transform.position).normalized * speed;
        Vector3 steering = desired - _veclocity;
        steering = Vector3.ClampMagnitude(steering, _maxforce * Time.deltaTime); 
        return steering;
    }

    protected Vector3 Arrive(Vector3 targetPos)
    {
        float Dist = Vector3.Distance(transform.position, targetPos);
        if (Dist > _viewRadius)
        {
            return Seek(targetPos);
        }        
        return Seek(targetPos, _maxspeed * (Dist / _viewRadius)); 
    }

    protected Vector3 obstacleAvoidance()
    {
    
        if (Physics.Raycast(transform.position, transform.right, _viewRadius, _obstacles)) 
            {
                Vector3 desired = transform.position - transform.up; 
                return Seek(desired);
            }
        else if (Physics.Raycast(transform.position + transform.up * 0.5f, transform.right, _viewRadius, _obstacles)) 
            {
                return Seek(transform.position - transform.up);
            }
        else if (Physics.Raycast(transform.position - transform.up * 0.5f, transform.right, _viewRadius, _obstacles))
            {
                return Seek(transform.position - transform.up);
            }
        return Vector3.zero;
    }

    /*public Vector3 Persuit(SteeringAgent targetAgent)
    {
        Vector3 futurePos = targetAgent.transform.position + targetAgent._veclocity;
        Debug.DrawLine(transform.position, futurePos, Color.cyan);
        return Seek(futurePos);
    }*/
    public void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    public Vector3 CalculateSteering(Vector3 desired)
    {
        return Vector3.ClampMagnitude(desired - _veclocity, _maxforce * Time.deltaTime);
    }

    public void AddForce(Vector3 force)
    {
        _veclocity = Vector3.ClampMagnitude(_veclocity + force, _maxspeed);
    }

    protected virtual void OnDrawGizmos() 
    {

        Gizmos.color = Color.green; 

        Gizmos.DrawLine(transform.position, transform.position + transform.right * _viewRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Vector3 leftRayPos = transform.position + transform.up * 0.5f;
        Vector3 rightRayPos = transform.position - transform.up * 0.5f;

        Gizmos.DrawLine(leftRayPos, leftRayPos + transform.right * _viewRadius);
        Gizmos.DrawLine(rightRayPos, rightRayPos + transform.right * _viewRadius);
    }
}
