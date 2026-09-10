using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquareSpawner : MonoBehaviour
{
    // points on the square
    private Vector2 topRightBoxPoint = new Vector2(1, 1); // top right point
    private Vector2 bottomRightBoxPoint = new Vector2(1, -1); // bottom right point
    private Vector2 bottomLeftBoxPoint = new Vector2(-1, -1); // bottom left point
    private Vector2 topLeftBoxPoint = new Vector2(-1, 1); // top left point

    private Vector2 topRightUpdated = new Vector2(1, 1); // top right point
    private Vector2 bottomRightUpdated = new Vector2(1, -1); // bottom right point
    private Vector2 bottomLeftUpdated = new Vector2(-1, -1); // bottom left point
    private Vector2 topLeftUpdated = new Vector2(-1, 1); // top left point

    private float squareScale = 1.0f; // scales the square
    public List<Vector2> SavedSquarePoints = new List<Vector2>();// saves the points on the square when clicked
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // calculations for the squares
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); // obtains the mouse pos and converts it to worldspace
        Vector2 scrollWheel = Mouse.current.scroll.ReadValue();// value for the scroll wheel
        squareScale += scrollWheel.y; // saves a value for the scroll wheel
        updateSquaresPoints(mousePos, squareScale);// updates the square points with the mouse and wheel
        saveSquareWhenClicked(); // when mouse is clicked it saves the four square points


        //drawing the squares
        DrawSquare(topLeftUpdated, topRightUpdated, bottomLeftUpdated, bottomRightUpdated, new Color(150, 150, 150, 0.5f)); // draws the faint white square using alpha
        drawSavedSquares();// draws all saved squares
    }
    //function that draws a white square at a given vector2
    void DrawSquare(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight, Color squareColour)
    {

        Debug.DrawLine(topLeft,topRight, squareColour);// draws the top side of the square
        Debug.DrawLine(topRight, bottomRight, squareColour);// draws the top side of the square
        Debug.DrawLine(bottomRight, bottomLeft, squareColour);// draws the top side of the square
        Debug.DrawLine(bottomLeft, topLeft, squareColour);// draws the top side of the square


    }
    //updates the squares points
    void updateSquaresPoints(Vector2 mousePosition, float mouseScale)
    {
        topLeftUpdated = mousePosition + topLeftBoxPoint * mouseScale; // topleft updated points
        topRightUpdated = mousePosition + topRightBoxPoint * mouseScale;// topright updated points
        bottomLeftUpdated = mousePosition + bottomLeftBoxPoint * mouseScale;// bottomleft updated points
        bottomRightUpdated = mousePosition + bottomRightBoxPoint * mouseScale;// bottomright updated points
    }
    // when clicked it saves the four square points
    void saveSquareWhenClicked()
    {
        //checks if the mouse is pressed at all
        if(Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame)
        {
            SavedSquarePoints.Add(topLeftUpdated); // saves topleft
            SavedSquarePoints.Add(topRightUpdated); // savestopright
            SavedSquarePoints.Add(bottomLeftUpdated);  // saves bottomleft
            SavedSquarePoints.Add(bottomRightUpdated); // saves bottomright
        }
    }
    void drawSavedSquares()
    {
        // loops over each square to be drawn, skips 4 instead of 1 to only draw that square
        for(int i = 0; i < SavedSquarePoints.Count; i += 4)
        {
            DrawSquare(SavedSquarePoints[i], SavedSquarePoints[i + 1], SavedSquarePoints[i + 2], SavedSquarePoints[i + 3], Color.white);
        }
    }


}