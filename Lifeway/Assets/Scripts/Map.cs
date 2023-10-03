using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] GameObject train;
    [SerializeField] bool rotate;
    [SerializeField] Transform pointer;
    [SerializeField] float minPointerRot, maxPointerRot;
    [SerializeField] float minSpeed, maxSpeed, speedReduce, speedDivisor;
    [SerializeField] float Speed, RotationSpeed;
    [SerializeField] Animation backAnimation;
    
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


    void Update()
    {
        if (rotate)
            RotateTrain();

        Move();
        UpdateSpeed();
    }

    public void ChangeSpeed(float value)
    {
        Speed += value / speedDivisor;
        Mathf.Clamp(Speed, minSpeed, maxSpeed);
    }

    private void UpdateSpeed()
    {
        Speed -= speedReduce;
        Mathf.Clamp(Speed, minSpeed, maxSpeed);

        //backAnimation.
        pointer.localRotation = Quaternion.AngleAxis(minPointerRot + ((maxPointerRot-minPointerRot) * (Speed / maxSpeed)), Vector3.forward);
    }

    private void Move()
    {
        train.transform.position = Vector3.MoveTowards(train.transform.position, currentPoint.position, Time.deltaTime * Speed);

        if (train.transform.position == currentPoint.position)
        {
            index++;

            if (index >= Points.Length)
            {
                //Destroy(gameObject); ����� ����
            }
            else
            {
                currentPoint = Points[index];
            }
        }
    }

    private void RotateTrain()
    {
        if (Vector3.Distance(train.transform.position, currentPoint.position) < rotationDistanceToPoint)
        {
            direction = Points[index + 1].position - train.transform.position;

            Vector3 newDirection = Vector3.RotateTowards(train.transform.forward, direction, Time.deltaTime * RotationSpeed, 0);
            train.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
