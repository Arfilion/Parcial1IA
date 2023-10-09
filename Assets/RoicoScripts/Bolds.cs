using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolds : SteeringAgent
{
    [SerializeField] Transform _target;
    [SerializeField] float _rangeToKill;

    [SerializeField, Range(0f, 5f)] float _aligmentWeight = 1;
    [SerializeField, Range(0f, 5f)] float _separationWeight = 1;
    [SerializeField, Range(0f, 5f)] float _cohesionWeight = 1;

    private void Start()
    {
        //  Debug.Log(GameManager.instance.IsPaused); prueba de static

        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        var dir = new Vector3(x, y); //si no pasas Z se queda en 
        _veclocity = dir.normalized * _maxspeed; //para que vayan a misma velociad y misma distancia

        GameManager.instance.Allignments.Add(this); //apenas arrranca la lista se actualiza y los agrega 


    }


    // Update is called once per frame
    void Update()
    {

        if (!HasToUseObstacleAvoidance())
        {
            AddForce(Arrive(_target.position));
        }
        

        Move();
        KillMyTarget();
        UpdateBoundPosition();
        Flocking();
    }

    private void Flocking()
    {
        var boids = GameManager.instance.Allignments; //aca podriamos poner las distintos tipos listas
        AddForce(Alignment(boids) * _aligmentWeight); //aca pasamos las listas que tenemos, como tenemos 1,
        //si multiplicamos por 0.5f se aplica a la mitad de la fuerza.

        AddForce(Separated(boids) * _separationWeight); //prueba del separa // se aplique un radio mas chico al actual aplicar la separacion

        AddForce(Cohesion(boids) * _cohesionWeight);
    }
    private void UpdateBoundPosition()
    {
        transform.position = GameManager.instance.AdjustPositionToBounds(transform.position);
    }
    private void KillMyTarget()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) <= _rangeToKill)
        {
            _target.position = new Vector3 (Random.Range(0,6f), Random.Range(0, 6f),0f);
            
        }
            
    }
    protected override void OnDrawGizmos()
    {
        //si esta vacio no tenemos ningun gizmo en pantalla

        // Gizmos.DrawWireSphere(transform.position, _viewRadius);

    }
}
