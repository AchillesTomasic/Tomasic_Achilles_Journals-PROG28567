using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;



public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    // variables for task 1 A //
    public Vector2 bombOffset;// vector for offset
    // Variables for task 1 B //
    public float bombTrailSpacing; // used to space the bombs along the trail
    public int numberOfTrailBombs; // used to determine the number of bombs in the trail
    // Variables used for task 2 //
    public float randomBombDistance; // used for the distance of the random bombs
    // Variavles used for Task 4
    public float MaxRange;// maximum range of the asteroid radar
    // used for the coroutine assignment provided in class //
    public float bombSpawnWaitTime = 3f; // time for the bomb to wait before spawning
    private IEnumerator bombWaitCoroutine; // coroutine for the bomb
    [Space(10)]
    ////////////////////
    // week 3 content //
    ////////////////////
    #region Week3Content

    private Vector3 velocity;
    private Vector3 AccelerationDirection; // direction for the acceleration
    public float maxSpeed; // max speed of the ship
    public float accelerationTime; // player will reach this speed after an inteval of time
    public float deAccelerationTime; // time taken to deaccelerate
    public float acceleration; // intreval that the velocity moves by over time
    public float deAcceleration; // deacceleration value
    #endregion
    /// <summary>
    /// week 4 content
    /// </summary>
    #region Week4Content
    public List<Vector2> hitboxPointPos;
    private Color hitboxColour = Color.green; // colour of the hitbox
    public int circlePointsInput = 5; //changable number of circle points the player can input
    public float radiusInput; // radius of the circle for the hitbox
    //powerups
    public float powerupRadius; // radius that the power ups can spawn from
    public int powerupCount;// number of powerups that can spawn
    public GameObject powerupPrefab; // prefab for powerups
    #endregion
    void Start()
    {
        acceleration = maxSpeed / accelerationTime; // sets a value for the acceleration to change by
        deAcceleration = maxSpeed / deAccelerationTime;// sets the deacceleration so that it uses the speed and the deacceleration time
    }
    // Update is called once per frame
    void Update()
    {
        
        // moving the player //
        PlayerMovement(); // used to move the player
        



        // checks if the b key is pressed
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            bombOffset = transform.position + transform.up; // offet of the bomb
            SpawnBombAtOffset(bombOffset); // spawns bomb
        }
        // checks if the t key is pressed
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs); // spawns a trail of bombs behind the player
        }
        // checks if o is pressed
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            SpawnBombOnRandomCorner(randomBombDistance); // spawns a bomb at a random corner of the screen
        }
        // checks if j is pressed
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            float ratio = Random.Range(0f, 1f); // sets random value between 0 and 1
            WarpPlayer(enemyTransform,ratio);// warps the player a set distance based on the ratio to the enemy
        }
        //checks if v is pressed
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            SpawnPowerups(powerupRadius, powerupCount); //spawns powerups around the player using rotation
        }
        DetectAsteroids(MaxRange, asteroidTransforms); // detects if asteroids are a certian distance from the player
        EnemyRadar(radiusInput,circlePointsInput);
    }
    // used to spawn power ups around the player using rotation from the radius
    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        float equalAngle = 360f / numberOfPowerups; // gets an equal distance between points
        float currentAngle = 0f;
        for (int i = 0; i < numberOfPowerups; i++)
        {
            float currentRadians = currentAngle * Mathf.Deg2Rad;// converst the angle from degrees to radians 
            Vector2 currentPoint = new Vector2(transform.position.x + Mathf.Cos(currentRadians) * radius, transform.position.y + Mathf.Sin(currentRadians) * radius); // sets the point of the powerup spawn relative to the player
            Instantiate(powerupPrefab, currentPoint, Quaternion.identity); // creates a prefab at the currentpoint location
            currentAngle += equalAngle; // adds to the angle
        }
    }
    //used to create an enemy radius that detects if the enemy is within a radius distance of the player
    public void EnemyRadar(float radius, int circlePoints)
    {
        //recycles the list so that it is empty upon use
        hitboxPointPos.Clear();
        float anlgeToRotate = 360f / circlePoints;// will get the angle that each circle point will spawn from to make a perfect circle
        float currentAngle = 0f; // current angle to spawn points
        
        for (int i = 0; i < circlePoints; i++)
        {
            float currentRadians = currentAngle * Mathf.Deg2Rad;// converts the degrees to radians
            Vector2 currentPoint = new Vector2(transform.position.x + Mathf.Cos(currentRadians) * radius, transform.position.y + Mathf.Sin(currentRadians) * radius); // sets the point relative to the player position
            hitboxPointPos.Add(currentPoint);// adds the point to the list
            currentAngle += anlgeToRotate;// adds to the angle
            if(i > 0)
            {
                Debug.DrawLine(hitboxPointPos[i], hitboxPointPos[i - 1],hitboxColour); // draws the line for each point
            }
            if(i == circlePoints - 1)
            {
                Debug.DrawLine(hitboxPointPos[0], hitboxPointPos[i],hitboxColour); // draws the line from the final to the start
            }
        }
        float enemyDistance = (enemyTransform.position - transform.position).magnitude;// distance between enemy transform and player
        // changes the colour of the hitbox depending on how far from the player the enemy transform is
        if (enemyDistance < radius)
        {
            hitboxColour = Color.red; /* changes hitbox to red */ 
        }
        else 
        { 
            hitboxColour = Color.green; /* changes hitbox to green */ 
        }  
    }
    // used to move the player using their velocity
    public void PlayerMovement()
    {
        // resets the direction vector each time you move
        AccelerationDirection = Vector3.zero;
        // handles input that changes the velocity

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            AccelerationDirection = Vector3.left; // changes velocity to be left
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            AccelerationDirection = Vector3.right; // change velcoity to the right
        }
        else if (Keyboard.current.upArrowKey.isPressed)
        {
            AccelerationDirection = Vector3.up; // changes velocity to up
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            AccelerationDirection = Vector3.down; // changes velocity to up
        }
        // checks if the velocity exceeds the max speed
        if(velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed; // sets the speed to exactly the max speed
        }
        // checks if there is no input
        if(AccelerationDirection == Vector3.zero)
        {
            velocity += -velocity.normalized * deAcceleration * Time.deltaTime; // deaccelerates the player over a set period of time when there is no input
        }
        // changes the velocity by taking its direction, muktiplying it by th acceleration, then setting it over gametime
        velocity += AccelerationDirection.normalized * acceleration * Time.deltaTime;
        // the displacement from whatever current position the ship is at equals the velocity * by the time
        transform.position += velocity * Time.deltaTime;
        

    }
    //detects if an asteroid is in range then draws a line to that asteroid
    public void DetectAsteroids(float inMaxRange,List<Transform> inAsteroids)
    {
        // loops over every asteroid in the scene
        foreach (Transform asteroid in inAsteroids)
        {
            float magnitudeFromPlayer = (asteroid.position - transform.position).magnitude; // checks the magnitude from the asteroid to the player
            //checks if the magnitude is less than the max range value
            if(magnitudeFromPlayer < inMaxRange)
            {
                Vector2 exactAsteroidLength = (Vector2)transform.position + NormalizeCustom(asteroid.position - transform.position) * 2.5f; //normalizes the asteroids position to simply give a directional value then is increased to exactly 2.5f
                Debug.DrawLine(transform.position, exactAsteroidLength);// draws from the player to the asteroid at exactly 2.5f
            }
        }
    }
    // moves the players to a target position by a set ratio amount
    public void WarpPlayer(Transform target, float ratio)
    {
        Vector2 distance = target.position - transform.position;// distance between the player position and the target position
        float mag = distance.magnitude; // gets the magnitude of this distance
        float pointBetweenMag = Mathf.Lerp(0,mag,ratio); // sets the magnitude equal 
        Vector2 NormalizedDistance =  NormalizeCustom(distance); // gets the distance normalized for the direction
        transform.position += (Vector3)NormalizedDistance * pointBetweenMag; //sets the player pos to the direction mutliplied by the smaller magnitude 
    }

    // spawns a bomb on a random corner of the screen
    public void SpawnBombOnRandomCorner(float inDistance)
    {
        // selects a random number between 0 and 1 then rounds to either 0 or 1 to give two options
        Vector2 randomBombPosition = new Vector2(Mathf.Round( Random.Range(0, 2)), Mathf.Round(Random.Range(0, 2)));
        randomBombPosition *= 2; // raises the value so that if it is a 1, then it becomes a 2
        // lowers the value so that it either becomes -1 or 1
        randomBombPosition.x -= 1; 
        randomBombPosition.y -= 1;

        Instantiate(bombPrefab, randomBombPosition + (Vector2)transform.position, Quaternion.identity);// spawns bomb
    }
    // used to spawn a trail of bombs behind the player
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        Vector3 bombOffsetAmount = transform.position; // used locally to keep track of the distance between bombs
        for(int i = 0; i < inNumberOfBombs; i++)
        {
            bombOffsetAmount.y -= inBombSpacing;
            Instantiate(bombPrefab,bombOffsetAmount , Quaternion.identity);// spawns bomb

        }
    }
    // function that will be used to spawn bomb at an offset
    public void SpawnBombAtOffset(Vector3 inOffset) {
            bombWaitCoroutine = waitToSpawnBomb(bombSpawnWaitTime, inOffset); // sets the bomb coroutine at the start of the game
            StartCoroutine(bombWaitCoroutine); // only starts this isntance of the coroutine                                                              
    }

    // classwork methods //
    //coroutine that is used before spawning bombs when b is pressed
    private IEnumerator waitToSpawnBomb(float waitTimer,Vector3 inOffset)
    {

        yield return new WaitForSeconds(waitTimer); // waits set time
        Instantiate(bombPrefab, inOffset, Quaternion.identity);// spawns bomb
        
    }
    // calculation used to normalize a vector ///classwork////
    public Vector2 NormalizeCustom(Vector2 inVector)
    {
        float mag = inVector.magnitude;
        Vector2 normalize = new Vector2(inVector.x / mag, inVector.y / mag);
        return normalize;
    }
    // calculation used to find the dot product of a vector set. used to find angle between two vectors //// claswork/////
    public float dotProduct(Vector2 inVector, Vector2 enemyVector)
    {
        float mag = inVector.magnitude;
     
        float enemyMag = enemyVector.magnitude;
        // dot product for finding the angle
        float dotprod = ((inVector.x * enemyVector.x) + (inVector.y * enemyVector.y)) / Mathf.Abs(mag) * Mathf.Abs(enemyMag);
        return Mathf.Cos(dotprod); 
    }
}
