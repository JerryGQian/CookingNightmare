using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveButtonController : MonoBehaviour {

    public Sprite offButton;
    public Sprite lowButton;
    public Sprite medButton;
    public Sprite highButton;
    public Sprite infernoButton;

    Button button;

    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextButton ()
    {
        if (WorldManager.heat < 4)
            WorldManager.heat++;
        if (WorldManager.heat == 4 && !WorldManager.isChaosMode && WorldManager.stage < 10)
            WorldManager.heat = 0;
        switch (WorldManager.heat)
        {
            case 0: button.image.overrideSprite = offButton; break;
            case 1: button.image.overrideSprite = lowButton; break;
            case 2: button.image.overrideSprite = medButton; break;
            case 3: button.image.overrideSprite = highButton; break;
            case 4: button.image.overrideSprite = infernoButton; break;
        }
        
    }
}
