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

    public float radiusForCircleAsteroid; // used for the circling asteroids that rotate around the center spawning
    public GameObject asteroidPrefab; // used as a prefab for the asteroids

    public float maxSpeed;// the max speed that the asteroid can travel at
    public float timeToReachMaxAcc;// time to reach the max acceleration
    public float acceleration; // used to determine the acceleration
    public Vector3 velocity; // used for the asteroids velocity
    public bool isCenterAsteroid; // used to detect which is a center asteroid


    // Start is called before the first frame update
    void Start()
    {
        
        acceleration = maxSpeed / timeToReachMaxAcc; // used to reach max speed
        randomPoint = new Vector3(Random.Range(-maxFloatDistance, maxFloatDistance), Random.Range(-maxFloatDistance, maxFloatDistance), 0); // chooses a random point somewhere on the map;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCenterAsteroid)
        {
            RotatingAsteroidSpawner(radiusForCircleAsteroid);
            isCenterAsteroid = false;
        }
        //AsteroidMovement(); used during a previous journal

        AsteroidMovementRotationPattern();
    }
    public void RotatingAsteroidSpawner(float radius)
    {
        float angleBetweenAsteroids = (360f / 2) * Mathf.Deg2Rad; // gets the angle between each asteroid to space equally
        float currentAngle = 0; // current angle to spawn a asteroid
        // adds the asteroids
        for (int i = 0; i < 2; i++) 
        {
            Vector2 newpoint = new Vector2(transform.position.x + Mathf.Cos(currentAngle) * radius, transform.position.y + Mathf.Sin(currentAngle) * radius);// creates a point along the circle
            GameObject asteroid = Instantiate(asteroidPrefab, newpoint, Quaternion.identity); // spawns the circling asteroids
            asteroid.GetComponent<Asteroid>().isCenterAsteroid = false;
            currentAngle += angleBetweenAsteroids;// incriemnts value of the angle between asteroids
        }
    }
    // used for the asteroid movement on the asteroid pattern
    public void AsteroidMovementRotationPattern()
    {
        velocity += Vector3.down * acceleration * Time.deltaTime; // uses the acceleration to set the velocity
        transform.position += velocity * Time.deltaTime; // moves the asteroid by the velocity
        // cap on speed
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;// velocity is set to the max speed that the player can travel
        }
    }
    public void AsteroidMovement() 
    {
        Vector3 asteroidDirection = (randomPoint - transform.position).normalized; // the asteroid relative to the random point and it is normalized to get the direction
        transform.position += asteroidDirection * moveSpeed * Time.deltaTime; // moves the asteroid towards the random point using the calculated direction
        // checks if the asteroid is in range of the arrival distance
        Vector3 distanceBeteenPlayerAndRandomPoint = randomPoint - transform.position;
        if (distanceBeteenPlayerAndRandomPoint.magnitude < arrivalDistance)
        {

            randomPoint = new Vector3(Random.Range(-maxFloatDistance, maxFloatDistance), Random.Range(-maxFloatDistance, maxFloatDistance), 0); // chooses a random point somewhere on the map;
        }
    }
}
