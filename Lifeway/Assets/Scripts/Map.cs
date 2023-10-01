using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] GameObject train;
    [SerializeField] bool rotate;

    public float Speed, RotationSpeed;
    public Transform[] Points;
    public float rotationDistanceToPoint;

    private Transform currentPoint;
    private int index;
    private Vector3 direction;

    void Start()
    {
        index = 0;
        currentPoint = Points[index];
        direction = currentPoint.position - train.transform.position;
        
        if(rotate)
            train.transform.rotation = Quaternion.LookRotation(direction);
    }

    

    public int GetPoint()
    {
        return index;
    }


    void Update()
    {
        if (Vector3.Distance(train.transform.position, currentPoint.position) < rotationDistanceToPoint && rotate)
        {
            direction = Points[index + 1].position - train.transform.position;

            Vector3 newDirection = Vector3.RotateTowards(train.transform.forward, direction, Time.deltaTime * RotationSpeed, 0);
            train.transform.rotation = Quaternion.LookRotation(newDirection);
        }


        train.transform.position = Vector3.MoveTowards(train.transform.position, currentPoint.position, Time.deltaTime * Speed);

        if (train.transform.position == currentPoint.position)
        {
            index++;

            if (index >= Points.Length)
            {
                //Destroy(gameObject);
            }
            else
            {
                currentPoint = Points[index];
            }

        }
    }
}
