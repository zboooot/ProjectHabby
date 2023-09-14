using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE"};
    public static readonly string[] runDirections = { "Move N", "Move NW", "Move W", "Move SW", "Move S", "Move SE", "Move E", "Move NE" };
    public static readonly string[] attackDirections = { "Attack N", "Attack NW", "Attack W", "Attack SW", "Attack S", "Attack SE", "Attack E", "Attack NE" };

    Animator animator;
    int lastDirection;
    public Vector2 faceDirection;
    public Vector2 lastFaceDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    public void SetDirection(Vector2 direction)
    {
        //Use the move states by default
        string[] directionArray = null; //not sure if its a int or string, might have to change above

           
        //measure magnitude of input
        if (direction.magnitude < .01f)
        {
            //when standing still, we will use static states
            directionArray = staticDirections;
        }

        else
        {
            // calculate which direction we are going in
            // use directiontoindex to get the index of the slice from the direction vector
            // save the answer to last direction
            directionArray = runDirections;
            lastDirection = DirectionToIndex(direction, 8);

        }
        
        readFaceDir(direction);
        animator.Play(directionArray[lastDirection]);

    }

    public static int DirectionToIndex (Vector2 dir, int sliceCount)
    {
        //get normalized direction
        Vector2 normDir = dir.normalized;
        // calculate how many degrees one slice is
        float step = 360f / sliceCount;
        //calculate how many degrees half a slice is
        //need this to offset the pie so that north(up) is in the center
        float halfstep = step / 2;
        // get angle from -180 to 180 of the direction vector relative tot he Up vector
        // this will return the angle between dir and north
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        // add halfslice offset
        angle += halfstep;
        // if angle is negative, make it positive by adding 360 to wrap around
        if (angle<0)
        {
            angle += 360;
        }

        //calculate amount of steps required to reach this angle
        float stepCount = angle / step;
        //round it and get the answer
        return Mathf.FloorToInt(stepCount);
    }

    public void readFaceDir(Vector2 facePos)
    {
        if(facePos.x != 0 && facePos.y != 0)
        {
            faceDirection = facePos;
            lastFaceDir = facePos;
        }

        else if(facePos.x != 0 || facePos.y != 0)
        {
            faceDirection = facePos;
        }

        else
        {
            faceDirection = lastFaceDir;
        }
    }
}
