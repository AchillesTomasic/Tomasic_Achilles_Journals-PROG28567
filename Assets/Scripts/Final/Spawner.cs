using JetBrains.Annotations;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    private int asteroidsPool = 100; // sets the number of asteroids to spawn in the scene
    private asteroidData[] asteroidsInScene; // list to hold the different asteroids
    public GameObject asteroidPrefabOriginal; // prefab for all of the asteroids
    // used as the data for each of the asteroids
    public struct asteroidData
    {
        public float maxAcceleration; // max acceeration for the asteroids
        public float timeToReachMaxAccel; // used to determine the time it takes to reach the max acceleation
        public bool stateOfActivity;// checks if the asteroid is on or off
        public GameObject asteroidPrefab; // prefab for the asteroid
        public bool hasTrail;// checks if the asteroid has a trail of smaller asteroids
        public bool trailDistance; //  checks the distance between asteroids in the trail
        public Transform asteroidTransform; // used to move the asteroid across the screen
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        asteroidsInScene = new asteroidData[asteroidsPool]; // sets the number of asteroids on an array
        AsteroidSpawner(asteroidsPool); // spawns all of the asteroids
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // spawns asteroids into the scene at the start of the game
    public void AsteroidSpawner(int numberOfAsteroidsToPool)
    {
        for (int i = 0; i < numberOfAsteroidsToPool; i++)
        {
            GameObject CurrentAsteroid = Instantiate(asteroidPrefabOriginal, transform.position, Quaternion.identity); // spawns an asteroid gameobject in the scene
            asteroidData uniqueAsteroidData = new asteroidData(); // creates a unique set of data for the asteroid
            uniqueAsteroidData.asteroidPrefab = CurrentAsteroid; // sets the created gameobject asteroid to the unique asteroids gameobject slot
            asteroidsInScene[i] = uniqueAsteroidData;// adds that unqiue asteroid to the list
        }
    }
}
