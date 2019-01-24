using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public static bool rightRotation;
    public static bool leftRotation;

    public int index;
    public int lastIndex;

    private float smoothRotation = 0.2f;

    private Quaternion bossRotation;

    private void Start()
    {
        bossRotation = transform.rotation;
    }

    private void Update ()
    {
        Rotate();
    }

    public void Rotate()
    {
        if(rightRotation == true)
        {
            //BossAnim.Anim.SetBool("RightRotation",true) , this is the old code just for reference
            RandomRightRotation();
            bossRotation *= Quaternion.AngleAxis(20, Vector3.up);

            rightRotation = false;
            StartCoroutine(NoRotation());
        }
        else if(leftRotation == true)
        {
            //BossAnim.Anim.SetBool("LeftRotation",true) , this is the old code just for reference
            RandomLeftRotation();
            bossRotation *= Quaternion.AngleAxis(-20, Vector3.up);

            leftRotation = false;
            StartCoroutine(NoRotation());
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, bossRotation, 10 * smoothRotation * Time.deltaTime);
    }

    private void RandomLeftRotation()
    {
        if (index == lastIndex)
        {
            index = Random.Range(1, 2);
        }
        else
        {
            //BossAnim.anim.SetInteger("LeftRotation_Index", index);

            BossCollider.isRotating = true;
            Conditions.isRotating = true; 
        }
    }

    private void RandomRightRotation()
    {
        if (index == lastIndex)
        {
            index = Random.Range(1, 3);
        }
        else
        {
            //BossAnim.anim.SetInteger("RightRotation_Index", index);

            BossCollider.isRotating = true;
            Conditions.isRotating = true;
        }
    }

    private IEnumerator NoRotation()
    {
        yield return new WaitForSeconds(1f);
        Conditions.isRotating = false;

        //BossAnim.anim.SetInteger("LeftRotation_Index", 0);
        //BossAnim.anim.SetInteger("RightRotation_Index", 0);
        lastIndex = index;

        BossCollider.isRotating = false;
    }
}
