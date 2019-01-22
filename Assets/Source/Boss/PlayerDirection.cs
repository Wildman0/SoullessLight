using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public Transform target;

    private float dirNum;

    public static string direction;

    private void Update()
    {
        FindDir();
    }
    public void FindDir()
    {
        Vector3 heading = target.position - transform.position;
        dirNum = AngleDir(transform.forward, heading, transform.up);

        direction = dirNum == 1f ? "Right" : dirNum == -1f ? "Left" : "CantFindDirection";
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
}
