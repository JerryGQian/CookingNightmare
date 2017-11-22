using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pattyCook : MonoBehaviour {

    SpriteRenderer rend;
    public static float cookedness;
    public float cookingSpeed = .3f; //.3 for normal speed (higher the slower)
    bool isCooking = false;
    static bool doneCookingOnce = false;

	// Use this for initialization
	void Start () {
        cookedness = 1f;
        rend = GetComponent<SpriteRenderer>();
        rend.color = new Color(1f, 1f, 1f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (WorldManager.heat > 0 && WorldManager.isInside && WorldManager.stage < 7) //cooks patty
        {
            if (!isCooking && !doneCookingOnce)
                StartCoroutine(cook(cookingSpeed));
        }
        if (WorldManager.heat > 0 && WorldManager.isInside && WorldManager.stage >= 7) //burns patty
        {
            if (!isCooking && doneCookingOnce)
                StartCoroutine(burn(cookingSpeed));
        }

        if (cookedness < .52f && !doneCookingOnce && !WorldManager.isInside)
        {
            doneCookingOnce = true;
        }
    }

    IEnumerator cook(float delayTime)
    {
        while (cookedness >= .5f && WorldManager.isInside && WorldManager.heat > 0)
        {
            isCooking = true;
            cookedness = cookedness-.01f;
            rend.color = new Color(cookedness, cookedness, cookedness, 1f);
            yield return new WaitForSeconds(delayTime);

        }
        isCooking = false;
    }

    IEnumerator burn(float delayTime)
    {
        while (cookedness >= .1f && WorldManager.isInside && WorldManager.heat > 0)
        {
            isCooking = true;
            cookedness = cookedness - .01f;
            rend.color = new Color(cookedness, cookedness, cookedness, 1f);
            yield return new WaitForSeconds(delayTime);

        }
        isCooking = false;
    }
}
