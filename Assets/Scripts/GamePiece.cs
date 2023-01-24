using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    private bool isMoving;

    // Mathematical, for more natural movement
    public InterpolationType interpolation;

    public enum InterpolationType
    {
        Linear,
        EaseOut,
        EaseIn,
        SmoothStep,
        SmootherStep

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCoordinate(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    // X and Y coordinates of the destination and the time of the movement action
    public void Move (int destX, int destY, float movementTime)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), movementTime));
        }
    }

    IEnumerator MoveRoutine(Vector3 destination, float movementTime)
    {
        Vector3 startPosition = transform.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;

        isMoving = true;

        while (!reachedDestination)
        {
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {

                reachedDestination = true;

                // Round our position to the final destination on integer values
                transform.position = destination;

                // Set the xIndex and yIndex of the game piece
                SetCoordinate((int)destination.x, (int)destination.y);
                break;

            }

            // Track the total running time
            elapsedTime += Time.deltaTime;

            // Calculate the lerp value
            float t = Mathf.Clamp(elapsedTime / movementTime, 0f, 1f);

            // Mathematical for more natural movement
            switch (interpolation)
            {
                case InterpolationType.Linear:
                    break;
                case InterpolationType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
                    break;
                case InterpolationType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
                    break;
                case InterpolationType.SmoothStep:
                    t = t * t * (3 - 2 * t);
                    break;
                case InterpolationType.SmootherStep:
                    t = t * t * t * (t * (t * 6 - 15) + 10);
                    break;
            }

            // Move the game piece
            transform.position = Vector3.Lerp(startPosition, destination, t);

            // Wait until next frame
            yield return null;
        }

        isMoving = false;

    }

}
