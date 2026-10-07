using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class DotProductTestClass : MonoBehaviour
{
    // angles
    public float redAngle;
    public float blueAngle;
    // vectors
    public Vector2 redVector;
    public Vector2 blueVector;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        redVector = calculateVectorFromAngle(redAngle, 1); // calculates red vector
        blueVector = calculateVectorFromAngle(blueAngle, 1); // calculates blue vector
        Debug.DrawLine(Vector2.zero, redVector, Color.red);
        Debug.DrawLine(Vector2.zero, blueVector, Color.blue);
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            float dotprod = DotProductWork(redVector, blueVector);
            if (dotprod <= float.Epsilon)
            {
                dotprod = 0;
            }
            Debug.Log(dotprod);
        }
    }
    private Vector2 calculateVectorFromAngle(float angle,float radius)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 point = new Vector2(transform.position.x + Mathf.Cos(rad) * radius, transform.position.y + Mathf.Sin(rad) * radius); // point calc from angle
        return point;
    }
    private float DotProductWork(Vector2 red, Vector2 blue)
    {
        float dot = red.x * blue.x + red.y * blue.y; // dot product calc
        return dot;
    }
}
