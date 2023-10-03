using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    // [SerializeField] Transform _seekTarget, _fleeTarget;
    [SerializeField] protected float _maxspeed, _maxforce;
    [SerializeField] float _viewRadius;
    protected Vector3 _veclocity;

    [SerializeField] LayerMask _obstacles;

    // Start is called before the first frame update
    void Start()
    {

    }

    protected Vector3 Seek(Vector3 targetPos) //vectr 3 targetpos//llamarlo en update
    {
        return Seek(targetPos, _maxspeed);
    }

    protected void Move()
    {
        transform.position += _veclocity * Time.deltaTime;
        if (_veclocity != Vector3.zero) transform.right = _veclocity; //cuando no sea zero cumple con esto
    }
    protected bool HasToUseObstacleAvoidance()
    {
        Vector3 avoidanceObs = obstacleAvoidance(); //queremos un vector 3 que guarde el valor del raycast
        if (avoidanceObs != Vector3.zero) // si es diferente a vector3. zero
        {
            AddForce(avoidanceObs);
        }
        return avoidanceObs != Vector3.zero; // solo si es diferente de cero
    }

    protected Vector3 Seek(Vector3 targetPos, float speed) //vectr 3 targetpos//llamarlo en update
    {
        Vector3 desired = (targetPos - transform.position).normalized * speed;

        Vector3 steering = desired - _veclocity;
        steering = Vector3.ClampMagnitude(steering, _maxforce * Time.deltaTime);  //de a póquito gira hasta llegar alli
        return steering;
    }
    Vector3 Flee(Vector3 targetPos) => -Seek(targetPos);   //le decimos que VAYA a / GOES TO / tiene return
    //vectr 3 targetpos//llamarlo en update

    protected Vector3 Arrive(Vector3 targetPos) //para que vaya frenando de a poco
    {
        // return default; // el valor defaul de vectror 3 es ,0,0.0
        float Dist = Vector3.Distance(transform.position, targetPos);
        if (Dist > _viewRadius) //si la distancia es mayor, que le haga el seek
        {
            return Seek(targetPos);//que se muegva / retort
        }
        //   Vector3 desired = targetPos - transform.position;
        //   desired.Normalize();
        //   desired *= (_maxspeed *(Dist /_viewRadius)); //maxspeed * dist/radio para que vaya frenando
        //
        //   Vector3 steering = desired - _veclocity;
        //   steering = Vector3.ClampMagnitude(steering, _maxforce * Time.deltaTime);  //de a póquito gira hasta llegar alli

        return Seek(targetPos, _maxspeed * (Dist / _viewRadius)); //si es menor al radio que devuelva este cambio de spped
        //por la velocidad variable
    }

    protected Vector3 obstacleAvoidance() //para que vaya frenando de a poco
    {
        //(Physics.Raycast(transform.position, transform.right, _viewRadius,  1<<6)) esto para decirle que vea la layer 6
        //(Physics.Raycast(transform.position, transform.right, _viewRadius, 6  // toma todas las layers hasta la 6


        if (Physics.Raycast(transform.position, transform.right, _viewRadius, _obstacles)) //inicio,direccion, max distance de largo y layermask
        { //le agregamos el 0.5 de tama;'o del 
            Vector3 desired = transform.position - transform.up;  //si queremos ir a la derecha debemos restarlo / nose si esta en 2d

            return Seek(desired);
        }
        else if (Physics.Raycast(transform.position + transform.up * 0.5f, transform.right, _viewRadius, _obstacles)) //inicio,direccion, max distance de largo y layermask
        { //le agregamos el 0.5 de tama;'o del 
          // Vector3 desired = transform.position - transform.up;  //si queremos ir a la derecha debemos restarlo / nose si esta en 2d

            return Seek(transform.position - transform.up);
        }
        else if (Physics.Raycast(transform.position - transform.up * 0.5f, transform.right, _viewRadius, _obstacles))
        {
            //Vector3 desired = transform.position - transform.up;  //si queremos ir a la derecha debemos restarlo / nose si esta en 2d

            return Seek(transform.position - transform.up);
        }
        return Vector3.zero;//para que cuando no toque ningun obstaculo sea cero 
    }

    protected Vector3 Persuit(SteeringAgent targetAgent) //para que persiga al agente, queremos interceptar al obj
    {// como ambos objetos heredan de steering agent, podemos acceder

        Vector3 futurePos = targetAgent.transform.position + targetAgent._veclocity;
        Debug.DrawLine(transform.position, futurePos, Color.cyan);
        return Seek(futurePos);
        //en evade es lo contrario
    }

    protected Vector3 Evade(SteeringAgent targetAgent) //para que persiga al agente, queremos interceptar al obj
    {// como ambos objetos heredan de steering agent, podemos acceder


        return -Persuit(targetAgent); //la direccion contraria a donde va nuestro objetivo
        //en evade es lo contrario
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    protected Vector3 Separated(List<SteeringAgent> agents)
    {
        Vector3 desired = Vector3.zero;



        foreach (var item in agents) //collection reemplazamos por parametro / todos los items
        {
            if (item == this) continue; //ignoro mi propio calculo y fcontinuoa con el siguiente
            Vector3 dist = item.transform.position - transform.position; //distancia

            if (dist.sqrMagnitude > _viewRadius * _viewRadius) continue; //se multiplica para que no quede chico

            desired += dist;
        }
        if (desired == Vector3.zero) return Vector3.zero; //en vez de retornar el steering retorna zero para
        //que no se quede en pausa el movimiento

        desired *= -1;
        return CalculateSteering(desired.normalized * _maxspeed);
    }
    protected Vector3 Cohesion(List<SteeringAgent> agents)
    {
        Vector3 desired = Vector3.zero;

        int boidscount = 0;

        foreach (var item in agents) //collection reemplazamos por parametro / todos los items
        {
            if (item == this) continue; //ignoro mi propio calculo y fcontinuoa con el siguiente
            Vector3 dist = item.transform.position - transform.position; //distancia

            if (dist.sqrMagnitude > _viewRadius * _viewRadius) continue; //se multiplica para que no quede chico

            desired += item.transform.position;
            boidscount++;
        }
        if (boidscount == 0)
        {
            return Vector3.zero;
        }
        desired /= boidscount;
        return Seek(desired);
    }
    protected Vector3 Alignment(List<SteeringAgent> agents) //le pasamos la lista por parametro
    {

        Vector3 desired = Vector3.zero; //el promedio es 0 de base, puede darse el caso qu este separado
        // y como ese bold puede no encontrar a alguien en su rango, hasta quye entre a alguien va zero

        int boidsCount = 0; //contamos los bolds que tenemos dentro del rango

        foreach (var item in agents) //collection reemplazamos por parametro / todos los items
        {
            if (Vector3.Distance(item.transform.position, transform.position) > _viewRadius) continue;
            //calculamos la distancia de los items, prguntamos por los dew afuera
            //se hace una especie de break pero se usa continue, si el continue se ejecuta y lo de abajo\
            //nmo se ejecuta y pasa con el de abajo sin cortar el foreach

            //promedio = sumatodo / cantidad // 7,8,9 /// 24/3 // =8

            desired += item._veclocity;//le damos la direccion y lo sumamos al vector 3 deseado
            boidsCount++;
        }

        desired /= boidsCount;
        return CalculateSteering(desired.normalized * _maxspeed); //debemos retornar el steer
        //debemos normalizarlo para que vayan a la misma velocidad
    }

    protected Vector3 CalculateSteering(Vector3 desired)
    {
        //de a póquito gira hasta llegar alli
        return Vector3.ClampMagnitude(desired - _veclocity, _maxforce * Time.deltaTime);
    }

    protected void AddForce(Vector3 force) //nuestra locomocion
    {
        _veclocity = Vector3.ClampMagnitude(_veclocity + force, _maxspeed);
    }

    protected virtual void OnDrawGizmos() //nos sirve para ver la esfera de radio
    {

        Gizmos.color = Color.green; //

        Gizmos.DrawLine(transform.position, transform.position + transform.right * _viewRadius); //para el raycast

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);  //drawsphere no se usa porque solo queremos ver los bordes

        Vector3 leftRayPos = transform.position + transform.up * 0.5f;
        Vector3 rightRayPos = transform.position - transform.up * 0.5f;

        Gizmos.DrawLine(leftRayPos, leftRayPos + transform.right * _viewRadius);
        Gizmos.DrawLine(rightRayPos, rightRayPos + transform.right * _viewRadius);
    }
}
