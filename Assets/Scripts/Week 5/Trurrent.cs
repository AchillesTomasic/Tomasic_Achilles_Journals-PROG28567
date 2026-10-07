using System;
using UnityEngine;

public class Trurrent : MonoBehaviour
{
    public float angularSpeed;
    [Tooltip("measured In Degrees Per Second")]
    public Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        float deltaAngle = computeShortestAngle(transform.up, (target.position - transform.position).normalized);
        float rotateDir = Mathf.Sign(deltaAngle);
        float angleStep = angularSpeed * Time.deltaTime;
        if (angleStep < Mathf.Abs(angleStep))
        {
            transform.Rotate(0, 0, rotateDir * angularSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, deltaAngle);
        }
        detectDirFromUp(); // detects direction
        Debug.DrawLine(Vector2.zero, transform.position + transform.up, Color.green);
        Debug.DrawLine(Vector2.zero, target.position, Color.purple);
    }

    private float dotcalc(Vector2 upDir,Vector2 target)
    {
        float dotprod = upDir.x * target.x + upDir.y * target.y;
        return dotprod;
    }
    private void detectDirFromUp()
    {
        float dotresult = dotcalc(transform.position + transform.up, (target.position - transform.position).normalized);
        if (dotresult > 0) {
            Debug.Log("Infront");
        }
        else 
        {
            Debug.Log("Behind");
        }
    }
    private float computeShortestAngle(Vector2 a, Vector2 b)
    {
        float angleA = (Mathf.Atan2(a.y, a.x)) * Mathf.Deg2Rad;
        float angleB = (Mathf.Atan2(b.y, b.x)) * Mathf.Deg2Rad;

        return Mathf.DeltaAngle(angleA, angleB);
    }
}
