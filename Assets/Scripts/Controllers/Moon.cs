using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float radiusofOrbit; // radius for the moon from the planet
    public float speedOfPlanet; // speed the planet moves at
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(radiusofOrbit, speedOfPlanet, planetTransform); //orbits the move
    }
   public void OrbitalMotion(float radius, float speed, Transform target)
    {
        Vector2 distance = target.position - transform.position;// the distance between the moon and the planet
        float planetAngle = Mathf.Atan2(distance.y, distance.x); // obtains the angleffrom the moon to the current planet position
        planetAngle += speed * Time.deltaTime;// adds to the angle by a set speed
        Vector2 pointToMove = new Vector2(transform.position.x + Mathf.Cos(planetAngle) * radius, transform.position.y + Mathf.Sin(planetAngle) * radius);// uses the centerpoint to determine the next  posiiton point for the planet
        target.position = pointToMove; // moves the planet in orbit
    }
}
