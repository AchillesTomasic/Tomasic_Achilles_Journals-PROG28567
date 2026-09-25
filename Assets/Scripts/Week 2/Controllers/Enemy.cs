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


    private void Start()
    {
        randomMovePoint = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0); // chooses a random point somewhere on the map
        acceleration = maxSpeed / accelerationTime; // acceleration for the player constant value
    }
    private void Update()
    {
        EnemyMovement();
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
