using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fryingPanSnap : MonoBehaviour {

    Collider2D food;
    int timer = 25;

    public GameObject boilAnimation;
    GameObject boilAni;
    SpriteRenderer rend;

    public GameObject flameLowAnimation;
    public GameObject flameMedAnimation;
    public GameObject flameHighAnimation;
    public GameObject flameInfernoAnimation;
    GameObject flameAni;
    bool flaming = false;
    int nextHeat = 1;

    boilController boilControl;

    private void OnTriggerEnter2D(Collider2D foodObj)
    {
        WorldManager.isPattyOnPan = true;
        food = foodObj;
        WorldManager.isInside = true;
        if (WorldManager.heat > 0 && !WorldManager.isBoiling)
        {
            WorldManager.isBoiling = true;
            boilAni = Instantiate(boilAnimation);
        }
        
    }

    private void OnTriggerExit2D(Collider2D foodObj)
    {
        WorldManager.isPattyOnPan = false;
        timer = 25;
        WorldManager.isInside = false;
        if (WorldManager.isBoiling)
        {
            boilControl.fadeDestroy();
            if (boilControl != null) StartCoroutine(destroyAfterTime());
        }
        WorldManager.isBoiling = false;
    }

    IEnumerator destroyAfterTime()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(boilAni);
    }

    // Use this for initialization
    void Start () {
        timer = 25;
        WorldManager.isBoiling = false;
        WorldManager.isInside = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (WorldManager.heat > 0 && !WorldManager.isBoiling && WorldManager.isInside)
        {
            boilAni = Instantiate(boilAnimation);
            WorldManager.isBoiling = true;
        }

        if (WorldManager.heat == 0 && WorldManager.isBoiling)
        {
            boilControl.fadeDestroy();
            if (boilControl != null) StartCoroutine(destroyAfterTime());
            WorldManager.isBoiling = false;
        }

        if (WorldManager.isBoiling && boilAni != null)
        {
            boilControl = boilAni.GetComponent<boilController>();
        }

        
        switch (WorldManager.heat)
        {
            case 0:
                if (flaming)
                {
                Destroy(flameAni);
                    flaming = false;
                    nextHeat = 1;
                }
                break;
            case 1:
                if (!flaming && nextHeat == 1)
                {
                    flameAni = Instantiate(flameLowAnimation);
                    flaming = true;
                    nextHeat = WorldManager.heat + 1;
                } 
                break;
            case 2:
                if (flaming && nextHeat == 2)
                {
                    Destroy(flameAni);
                    flameAni = Instantiate(flameMedAnimation);
                    nextHeat = WorldManager.heat + 1;
                }
                break;
            case 3:
                if (flaming && nextHeat == 3)
                {
                    Destroy(flameAni);
                    flameAni = Instantiate(flameHighAnimation);
                    nextHeat = WorldManager.heat + 1;
                }
                break;
            case 4:
                if (flaming && nextHeat == 4)
                {
                    Destroy(flameAni);
                    flameAni = Instantiate(flameInfernoAnimation);
                    nextHeat = WorldManager.heat + 1;
                }
                break;
        }
            
        

        if (WorldManager.isPattyOnPan) //snaps to pan and releases after time
        {
            if (timer > 0)
            food.transform.position = new Vector2(-1.75f, 1.3f);
            timer--;
            if (timer == 0) {
                WorldManager.isPattyOnPan = false;
            }
            
        }
	}
}
