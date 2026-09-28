using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class RowGeneration : MonoBehaviour
{
    private Vector2[] SquarePointsRefrence = new Vector2[4];// refrence points for the first square
    private float squareSize = 1;// size of the square
    public TMP_InputField SquareInputFeild;// inout feild results
    public List<Vector2> SquarePointsSaveList;// list that holds currently saved squares
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // the four points on the first square drawn
        SquarePointsRefrence[0] = new Vector2(0,0);
        SquarePointsRefrence[1] = new Vector2(0, -squareSize);
        SquarePointsRefrence[2] = new Vector2(0 + squareSize, 0);
        SquarePointsRefrence[3] = new Vector2(0 + squareSize, -squareSize);
    }

    // Update is called once per frame
    void Update()
    {
        drawCurrentSquares(); // draws the current number of squares on the screen
    }
    //generates squares that equally follow one another
    public void GenerateSquares()
    {
        int num; // used to check if the text value is an integer
        //checks if text value is an integer
        if (int.TryParse(SquareInputFeild.text, out num)) { 
        int numberOfSquares = int.Parse(SquareInputFeild.text);
        //removes all previous squares from list before generating
        for (int i = SquarePointsSaveList.Count - 1; i >= 0; i--)
        {
            SquarePointsSaveList.Remove(SquarePointsSaveList[i]);
        }
        // adds all the new squares based on the typed number
        for (int i = 0; i < numberOfSquares; i++)
        {
            Vector2 newRefrencePoint = new Vector2(i, 0); // adds to the squares so that they move left to right
            //adds points on that square to list
            SquarePointsSaveList.Add(SquarePointsRefrence[0] + newRefrencePoint);
            SquarePointsSaveList.Add(SquarePointsRefrence[1] + newRefrencePoint);
            SquarePointsSaveList.Add(SquarePointsRefrence[2] + newRefrencePoint);
            SquarePointsSaveList.Add(SquarePointsRefrence[3] + newRefrencePoint);
        }
        }
        else
        {
            Debug.Log("this is not a valid number"); // throws debug if not an int
        }

    }
    //draws the current squares saved
    public void drawCurrentSquares()
    {
        //loops through each square that can be drawn
        for (int i = 0; i < SquarePointsSaveList.Count - 1; i = i + 4)
        {
            DrawSquare(SquarePointsSaveList[i],
                SquarePointsSaveList[i + 1],
                SquarePointsSaveList[i + 2],
                SquarePointsSaveList[i + 3]);// draws the square
        }
    }
    // used to draw the squares
    private void DrawSquare(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
    {
        Debug.DrawLine(topLeft, topRight, Color.white);// draws the top side of the square
        Debug.DrawLine(topRight, bottomRight, Color.white);// draws the top side of the square
        Debug.DrawLine(bottomRight, bottomLeft, Color.white);// draws the top side of the square
        Debug.DrawLine(bottomLeft, topLeft, Color.white);// draws the top side of the square
    }
    
}
