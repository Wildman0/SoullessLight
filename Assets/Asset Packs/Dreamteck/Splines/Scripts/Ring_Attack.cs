using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring_Attack : MonoBehaviour {

   Vector3 minScale;
    public Vector3 maxScale;
    public bool repeatable;
    public float speed = 2f;
    public float duration = 5f;

    // Use this for initialization
    IEnumerator Start()
    {
        minScale = transform.localScale;


        yield return RepeatLerp(minScale, maxScale, duration);

        while (repeatable)
        {
            yield return RepeatLerp(maxScale, minScale, duration);

            Destroy(this.gameObject, duration * 2);

        }

    
            if (transform.localScale == maxScale)

            {
                Destroy(this.gameObject);
            }

                    
        }
	 public IEnumerator RepeatLerp (Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
        
    }
	
}
