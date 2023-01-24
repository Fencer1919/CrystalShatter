using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    private bool isMoving;

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
                transform.position = destination;
                SetCoordinate((int)destination.x, (int)destination.y);
                break;

            }

            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp(elapsedTime / movementTime, 0f, 1f);

            transform.position = Vector3.Lerp(startPosition, destination, t);



            // Wait until next frame
            yield return null;
        }

        isMoving = false;

    }

}
