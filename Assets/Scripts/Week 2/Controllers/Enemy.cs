using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Vector3 velocity; // used for the enemies velocity
    public Vector3 randomMovePoint; // used to save the random move point
    public GameObject playerPosition; // used for the player position
    public float movePointDistance; //sets the distance from the player that the random point can spawn.
    public float resetPointDistance; // distance from point to reset
    public float maxSpeed;// used to set the max speed for the enemy
    public float accelerationTime; // used to detect time to accelerate
    public float acceleration;// used for the acceleration

    public float newPointThreshold; // distance between the enemy position and the random position before it changes


    private void Start()
    {
        randomMovePoint = new Vector3(0, Random.Range(0, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y), 0); // chooses a random point somewhere along the y axis
        acceleration = maxSpeed / accelerationTime; // acceleration for the player constant value
    }
    private void Update()
    {
        // EnemyMovement(); used for a previous enemy movement script
        enemyAsteroidMovement();// moves the enemyt
    }
    // moves the enemy from its current location to the new randomly selected point and spawns an asteroid
    public void enemyAsteroidMovement()
    {
        Vector3 enemyToPoint = (randomMovePoint - transform.position).normalized; // gets the direction that the enemy needs to move
        velocity += enemyToPoint * acceleration * Time.deltaTime; // sets the velocity eqaul to the direction * acceleration
        transform.position += velocity * Time.deltaTime; // moves the enemy using the velocity 
        // caps off the speed 
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;// velocity is set to the max speed that the player can travel
        }
        // checks the magnitude of distance between the current position and the random movepoint
        if ((randomMovePoint - transform.position).magnitude < newPointThreshold)
        {
            randomMovePoint = new Vector3(0, Random.Range(0, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y), 0); // chooses a random point somewhere along the y axis
        }
    }

    public void EnemyMovement()
    {
        Vector3 playerToRandomPoint = randomMovePoint - playerPosition.transform.position; // displacement between the random point and the player
        playerToRandomPoint = (playerToRandomPoint.normalized * movePointDistance) + playerPosition.transform.position; // gets the direction and places it on the offset of the player
        Debug.DrawLine(transform.position,playerToRandomPoint); // used for debugging to see if this works.

        Vector3 enemyToPoint = (playerToRandomPoint - transform.position).normalized; // gets the direction between the enemy and the point
        velocity += enemyToPoint * acceleration * Time.deltaTime; // change the direction of the player so that they move
        transform.position += velocity * Time.deltaTime; // changes the position by the velocity over time
        // caps off the speed 
        if(velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;// velocity is set to the max speed that the player can travel
        }
        
        if(Vector3.Distance(transform.position, playerToRandomPoint) < resetPointDistance)
        {
            randomMovePoint = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0); // chooses a random point somewhere on the map
        }
    }
}
