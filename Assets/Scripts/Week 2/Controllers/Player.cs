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
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
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
        DetectAsteroids(MaxRange, asteroidTransforms); // detects if asteroids are a certian distance from the player
    }
    public void PlayerMovement()
    {
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
        }
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
