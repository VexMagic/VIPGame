using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Vector2 mousePos;
    public GridObject.Direction direction;
    public GameObject directionArrow;

    private void Start()
    {
        SetRotation();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeDirection();
            SetRotation();
        }
    }

    private void ChangeDirection()
    {
        switch (direction)
        {
            case GridObject.Direction.Up:
                direction = GridObject.Direction.Right;
                break;
            case GridObject.Direction.Down:
                direction = GridObject.Direction.Left;
                break;
            case GridObject.Direction.Left:
                direction = GridObject.Direction.Up;
                break;
            case GridObject.Direction.Right:
                direction = GridObject.Direction.Down;
                break;
        }
    }

    private void SetRotation()
    {
        directionArrow.transform.eulerAngles = new Vector3(0, 0, DirectionToFloat(direction));
    }

    public float DirectionToFloat(GridObject.Direction direction)
    {
        float value = 0f;

        switch (direction)
        {
            case GridObject.Direction.Up:
                value = 180f;
                break;
            case GridObject.Direction.Down:
                value = 0f;
                break;
            case GridObject.Direction.Left:
                value = 270f;
                break;
            case GridObject.Direction.Right:
                value = 90f;
                break;
        }

        return value;
    }
}
