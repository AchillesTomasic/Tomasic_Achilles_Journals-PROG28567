using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Pipeline : MonoBehaviour
{
    public float timer = 0;
    public float maxTimer = 0.1f;
    public List<Vector2> savedPoints; // saved points on the line 
    public Vector2 mousePos; // used as a variable for the position of the mouse
    public float totalSize = 0; // total size of the line
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clickAddToList();
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); // sets the mouse  to its worldspace point
        drawPipeline();
    }
    void clickAddToList()
    {
        // when the user clicks, add that point to the list and do this every 0.1 seconds
        if (Mouse.current.leftButton.isPressed)
        {
            timer -= Time.deltaTime; // counts down the timer
            // checks if the timer is completed
            if (timer <= 0)
            {
                savedPoints.Add(mousePos); // add saved point for the mouse to list
                timer = maxTimer; // resets the timer
            }
        }
        // removes the line if the user lifts their mouse
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            calculateLengthOfLine();
            for (int i = savedPoints.Count - 1; i >= 0; i--)
            {
                savedPoints.RemoveAt(i);   // removes the current saved point
            }
            totalSize = 0;// resets the size for the next line
        }
    }
    void drawPipeline()
    {
        //loop that will draw lines if two or more points exhist
        for (int i = 0; i + 1 < savedPoints.Count; i++)
        {
            Debug.DrawLine(savedPoints[i], savedPoints[i + 1]);// draws the line
        }

    }
    // calculate the length of the line using the magnitude between each point
    void calculateLengthOfLine()
    {
        for (int i = 0; i + 1 < savedPoints.Count; i++)
        {
            float xSize = ((savedPoints[i + 1].x - savedPoints[i].x) * (savedPoints[i + 1].x - savedPoints[i].x));
            float ySize = ((savedPoints[i + 1].y - savedPoints[i].y) * (savedPoints[i + 1].y - savedPoints[i].y));
            totalSize += Mathf.Abs(Mathf.Sqrt(xSize + ySize));// calulates the size of one line then adds it to the total. uses abs to ensure that the lines continually grows
        }
        Debug.Log(totalSize);
    }
}
