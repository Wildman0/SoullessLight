using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public static bool rightRotation;
    public static bool leftRotation;

    private float smoothRotation = 0.2f;

    private Quaternion bossRotation;

    private Phase phase;

    private void Start()
    {
        phase = GetComponentInChildren<Phase>();

        bossRotation = transform.rotation;
    }

    private void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        if (Phase.trigger == true)
        {
            if (rightRotation == true)
            {
                phase.anim.SetBool("RotateRight", true);
                bossRotation *= Quaternion.AngleAxis(10, Vector3.up);

                rightRotation = false;
                StartCoroutine(NoRotation());
            }
            else if (leftRotation == true)
            {
                phase.anim.SetBool("RotateLeft", true);
                bossRotation *= Quaternion.AngleAxis(-10, Vector3.up);

                leftRotation = false;
                StartCoroutine(NoRotation());
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, bossRotation, 10 * smoothRotation * Time.deltaTime);
        }
    }

    private IEnumerator NoRotation()
    {
        yield return new WaitForSeconds(1f);
        phase.anim.SetBool("RotateRight", false);
        phase.anim.SetBool("RotateLeft", false);
    }
}
