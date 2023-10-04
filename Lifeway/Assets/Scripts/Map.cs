using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] GameObject train;
    [SerializeField] GameObject memoryPrefab;
    [SerializeField] bool rotate;
    [SerializeField] Transform pointer;
    [SerializeField] float minPointerRot, maxPointerRot;
    [SerializeField] float minSpeed, maxSpeed, speedReduce, speedDivisor, endReduce, endLerp, timeToStop;
    [SerializeField] float Speed, endSpeed, RotationSpeed, mapFlyToCameraTime;
    [SerializeField] Animation backAnimation, endAnimation, cameraAnim, fadeBlackAnimation;
    [SerializeField][TextArea] string finalRu, finalEng;
    [SerializeField] string finalTitleRu, finalTitleEng;
    [SerializeField] TextMeshProUGUI finalTitle, finalTextField;

    public Transform[] Points;
    public float rotationDistanceToPoint;

    private float timerStop;
    private Transform currentPoint;
    private int index;
    private Vector3 direction;
    private bool ending, death;

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
        {
            cameraAnim["CameraShake"].speed = 0;

            if(ending)
            {
                fadeBlackAnimation.gameObject.SetActive(true);
                if (GameStats.Instance.language == Lang.Eng)
                {
                    finalTextField.text = finalEng;
                    finalTitle.text = finalTitleEng;
                }
                else
                {
                    finalTextField.text = finalRu;
                    finalTitle.text = finalTitleRu;
                }

                transform.parent = fadeBlackAnimation.transform.parent;

                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.left * 1.5f, mapFlyToCameraTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(75,0,0), mapFlyToCameraTime);
                Debug.Log(transform.localRotation.x);
                if(transform.localRotation.x > 0.608f)
                {
                    finalTextField.color = Color.Lerp(finalTextField.color, Color.white, mapFlyToCameraTime);
                    finalTitle.color = Color.Lerp(finalTitle.color, Color.white, mapFlyToCameraTime);

                }
            }
        }
    }

    public void ChangeSpeed(float value)
    {
        Speed += value / speedDivisor;
        Mathf.Clamp(Speed, minSpeed, maxSpeed);
    }

    public void SpawnMemory(Sprite iconSprite)
    {
        Instantiate(memoryPrefab, train.transform.position, Quaternion.identity, gameObject.transform).GetComponent<SpriteRenderer>().sprite = iconSprite;
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
                if (Speed > 0)
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
