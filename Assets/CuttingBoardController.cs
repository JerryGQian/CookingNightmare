using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoardController : MonoBehaviour {

    Animator anim;
    public GameObject rawPatty;
    GameObject patty;
    GameObject oldPatty;
    public static bool isSwapped = false;
    public static bool isOnCB = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D foodObj)
    {
        
        if (WorldManager.stage == 4)
        {
            isOnCB = true;
            oldPatty = foodObj.gameObject;
            oldPatty.transform.SetParent(this.transform);
            if (!isSwapped)
                StartCoroutine(cbCycle(oldPatty));

            if (WorldManager.stage < 7) pattyCook.cookedness = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D foodObj)
    {
        foodObj.transform.SetParent(null);
        isOnCB = false;
    }

    

    void cbExit()
    {
        Debug.Log("exit");
        anim.SetBool("removeCB", true);
    }

    IEnumerator destroyPattyDelay(GameObject oldFood)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(oldFood);

    }

    void cbEntry()
    {
        Debug.Log("entry");
        anim.SetBool("removeCB", false);
    }

    IEnumerator cbCycle(GameObject oldFood)
    {
        isSwapped = true;
        yield return new WaitForSeconds(1f);
        anim.SetBool("removeCB", true);
        yield return new WaitForSeconds(2f);
        Destroy(oldFood);
        patty = Instantiate(rawPatty);
        patty.transform.SetParent(this.transform);
        anim.SetBool("removeCB", false);

    }

    IEnumerator cbEntryDelay()
    {
        yield return new WaitForSeconds(1.5f);
        //patty = Instantiate(rawPatty);
        //patty.transform.SetParent(this.transform);
        anim.SetBool("removeCB", false);

    }
}


