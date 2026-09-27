using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    public float elapsedTime;
    public int currentStarItteration = 0;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        // checks if the current star is on the last avalible star
        if (currentStarItteration >= starTransforms.Count - 1)
        {
            currentStarItteration = 0;// resets the stars
        }
        startPosition = starTransforms[currentStarItteration].transform.position; // first star
        endPosition = starTransforms[currentStarItteration + 1].transform.position; // star after this star

        elapsedTime += Time.deltaTime;// incriments the time

        float currentTime = elapsedTime / drawingTime; // sets the elapsed time to only be the length of what is set
        currentPosition = Vector2.Lerp(startPosition, endPosition, currentTime); // sets the current position of the drawing line
        Debug.DrawLine(startPosition, currentPosition);// draws the current line
        // checks if the timer is greater then 1
        if(currentTime > 1)
        {
            currentStarItteration++;// moves onto the next itteration
            elapsedTime = 0; //resets the time
        }
               
    }
}
