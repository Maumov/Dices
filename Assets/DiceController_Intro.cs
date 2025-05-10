using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController_Intro : MonoBehaviour
{
    [SerializeField] Vector3 moveDirection = new Vector3( 0f,0f,-1f);
    [SerializeField] float speed = 1f;
    [SerializeField] float speedMin = 1f, speedMax = 5f;
    [SerializeField] bool randomRotationAngle = false;
    [SerializeField] Vector3 rotationAngles = new Vector3( 0f, 0f, 1f);
    [SerializeField] float rotationSpeed = 45f;

    [SerializeField] float highLimitZ = 7f;
    [SerializeField] float lowLimitZ = -7f;

    [SerializeField] bool relocate = true;
    // Start is called before the first frame update
    void Start()
    {
        if ( randomRotationAngle )
        {
            RandomizeRotationAngle();
        }
        if ( speed < 0f)
        {
            speed = Random.RandomRange( speedMin, speedMax );
        }
        //highLimitZ = transform.position.z;
    }

    void RandomizeRotationAngle()
    {
        rotationAngles = new Vector3( Random.Range( 0, 1f ), Random.Range( 0, 1f ), Random.Range( 0, 1f ) );
        rotationAngles.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        Relocate();
        Rotate();
        Move();
    }

    void Relocate()
    {
        if (!relocate)
        {
            return;
        }
        
        if ( transform.position.z < lowLimitZ )
        {
            transform.position = new Vector3( transform.position.x, transform.position.y, highLimitZ );
            if ( randomRotationAngle )
            {
                RandomizeRotationAngle();
            }
        }
    }

    void Move()
    {
        transform.Translate( speed * Time.deltaTime * moveDirection, Space.World );
    }

    void Rotate()
    {
        transform.Rotate( rotationAngles * rotationSpeed * Time.deltaTime, Space.World );
    }


}
