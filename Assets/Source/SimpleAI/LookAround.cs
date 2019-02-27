using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    PathManager pathManager;
    NavMeshHandler navHandler;

    public int lookChance;
    public float timeToLook = 40f;
    

    public bool canLook = true;
    public float turnSpeed = 70;
    public float timeToWonder = 5f;

	void Start ()
    {
        //Get Components from AI
        navHandler = GetComponent<NavMeshHandler>();
        pathManager = GetComponent<PathManager>();
        lookChance = 10;
	}
	
	void Update ()
    {
        LookChance();

        //If float = 0, AI goes back to wondering
        if (timeToWonder < 0)
        {
            lookChance = 10;
            timeToWonder = 5f;
            navHandler.agent.speed = 3.5f;
        }
	}

    void LookChance()
    {
        //Checks to see if isChasing is true, if so nothing happens, else the look timer will check to see if the AI can look
        if (pathManager.isChasing == true)
        {
           
        }
        else
        {
            timeToLook -= 1f * Time.deltaTime;
            if (timeToLook < 0)
            {
                lookChance = Random.Range(0, 10);
                timeToLook = 40f;
            }
            //If lookChancce is below 3 then the AI can have a look around for the player
            if (lookChance < 3 && canLook == true)
            {
                navHandler.agent.speed = 0;
                navHandler.agent.destination = transform.position;
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

                timeToWonder -= 1f * Time.deltaTime;
            }
        }

    }
}
