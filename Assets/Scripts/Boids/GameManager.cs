using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //para que sea unico
    [SerializeField] float _boundWidth;
    [SerializeField] float _boundHeight;
    // public bool IsPaused; test

    public List<SteeringAgent> Allignments = new List<SteeringAgent>();
    public List<SteeringAgent> Separation = new List<SteeringAgent>();

    private void Awake()
    {
        if (instance == null) instance = this; //siempre en el awake
        else Destroy(gameObject); // para que destruya el que sobra
    }
    private void Start()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(_boundWidth, _boundHeight)); //para que se vean las lineas del wire
    }
    public Vector3 AdjustPositionToBounds(Vector3 pos)
    {
        //pasar los limites y dividiendolos
        float height = _boundHeight / 2;
        float Width = _boundWidth / 2;

        if (pos.y > height) pos.y = -height; //si supera la parte mayor, lo pasamos a negativo para que 
        //respawnee en la parte inferior
        if (pos.y < -height) pos.y = -height;

        if (pos.x > Width) pos.x = -Width;

        if (pos.x < -Width) pos.x = Width;
        return pos;
    }
}
