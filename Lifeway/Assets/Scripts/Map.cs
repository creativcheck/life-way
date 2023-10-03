using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] GameObject train;
    [SerializeField] bool rotate;
    [SerializeField] Transform pointer;
    [SerializeField] float minPointerRot, maxPointerRot;
    [SerializeField] float minSpeed, maxSpeed, speedReduce, speedDivisor, endReduce, endLerp, timeToStop;
    [SerializeField] float Speed, endSpeed, RotationSpeed;
    [SerializeField] Animation backAnimation, endAnimation, cameraAnim;
    
    public Transform[] Points;
    public float rotationDistanceToPoint;

    private float timerStop;
    private Transform currentPoint;
    private int index;
    private Vector3 direction;
    private bool ending;

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
        if(Speed > 0)
            UpdateSpeed();
        else
            cameraAnim["CameraShake"].speed = 0;
    }

    public void ChangeSpeed(float value)
    {
        Speed += value / speedDivisor;
        Mathf.Clamp(Speed, minSpeed, maxSpeed);
    }

    private void UpdateSpeed()
    {
        if(cameraAnim["CameraShake"].speed == 0)
            cameraAnim["CameraShake"].speed = 1;

        if(ending)
        {
            
            if(timerStop > 0)
            {
                timerStop -= Time.deltaTime;
                Speed = Mathf.Lerp(Speed, endSpeed, endLerp);
            }
            else
            {
                Speed -= endReduce;
            }
        }
        else
        {
            Speed -= speedReduce;
        }
        
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);

        backAnimation["Background"].speed = endAnimation["Background"].speed = Speed / 0.02f;
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
                //Destroy(gameObject); Конец игры
            }
            else
            {
                currentPoint = Points[index];

                if(index ==  Points.Length - 1)
                {
                    ending = true;
                    timerStop = timeToStop;
                    GameStats.Instance.ending = true;
                    endAnimation.gameObject.SetActive(true);
                }
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
