using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector3 randomPoint;
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    // Start is called before the first frame update
    void Start()
    {
        randomPoint = new Vector3(Random.Range(-maxFloatDistance, maxFloatDistance), Random.Range(-maxFloatDistance, maxFloatDistance), 0); // chooses a random point somewhere on the map;
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }
    public void AsteroidMovement() 
    {
        Vector3 asteroidDirection = (randomPoint - transform.position).normalized; // the asteroid relative to the random point and it is normalized to get the direction
        transform.position += asteroidDirection * moveSpeed; // moves the asteroid towards the random point using the calculated direction
        // checks if the asteroid is in range of the arrival distance
        if (Vector3.Distance(transform.position, randomPoint) < arrivalDistance)
        {
            randomPoint = new Vector3(Random.Range(-maxFloatDistance, maxFloatDistance), Random.Range(-maxFloatDistance, maxFloatDistance), 0); // chooses a random point somewhere on the map;
        }
    }
}
