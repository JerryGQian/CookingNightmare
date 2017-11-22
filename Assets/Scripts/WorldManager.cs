using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

    //#####[ Patty stove stuff ]#####
    //0=off 1=low 2=med 3=high 4=inferno
    public static int heat = 0;
    public static bool isPattyOnPan = false;
    public static bool isBoiling;
    public static bool isInside;

    public static int stage; //stage of level
    public static List<string> messages = new List<string>(10);
    public static bool isChaosMode = false;

    

    Vector2 panPosition = new Vector2(-1.79f, 1.4f); //for circle
    Vector2 cbPosition = new Vector2(5.51f, 1.52f);
    public GameObject circleIndicatorAnimation;
    GameObject circleIndicatorAni;

    public GameObject heatOverlay;
    Image overlay;
    float overlayOpacity;


    int money = 0;
    int currentMoneyValue;
    public Text moneyText;
    public Text dialogueText;
    public GameObject ttcButton;
    public Canvas canvas;
    Button ttcButtonInstance;
    public static bool typedYet = false;
    

    // Use this for initialization
    void Start () {
        stage = 0;
        overlay = heatOverlay.GetComponent<Image>();
        overlayOpacity = overlay.color.a;

        messages.Add("Hey Chef, welcome to your kitchen!!");    //0
        messages.Add("Lets begin by grilling a yummy patty!!"); //1
        messages.Add("Drag the patty onto the pan and turn on the stove!!"); //2
        messages.Add("Great! Now wait until the patty is a crispy brown.."); //3
        messages.Add("Nice! Now drag the patty onto the board, it's ready to serve!!"); //4 ##
        messages.Add("Bon Apetit! Well done, you're a super good Chef!!"); //5
        messages.Add("Awesome! Let's cook another one! You've got this!!"); //6 ##
        messages.Add("Great! Wait until it's nice and brown.!"); //7
        messages.Add("Ok, your patty is ready to be served! Drag it onto the board!!"); //8 ##
        messages.Add("You burnt it you little s**t! WTF are you doing Chef? Remove it...!"); //9
        messages.Add("Okay, don't listen, you little b***h, but at least turn the stove off...!"); //10
        messages.Add("Oh my gawd what f**k are you doing! Turn it off!!"); //11
        messages.Add("You better stop f**king around... You're going to kill us all!!"); //12
        messages.Add("AHHH! MY SKIN IS BOILING! IT HURTS!!"); //13
        messages.Add(""); //14
        messages.Add(""); //15
        messages.Add(""); //16
        messages.Add(""); //17
        messages.Add(""); //18
        messages.Add(""); //19
        messages.Add(""); //20
    }
	
	void Update () {
        //special cases to change stage
        if (stage <= 1 && isBoiling && heat > 0) stage = 2;
        if (stage == 2 && isBoiling && heat > 0) nextStage();
        if (stage >= 2 && stage <= 3 && isInside) Destroy(circleIndicatorAni); //on drag destroys circle PAN
        if (stage == 3 && pattyCook.cookedness <= .52f) nextStage();
        if (stage == 4 && CuttingBoardController.isSwapped) nextStage();
        if (stage == 5) Destroy(circleIndicatorAni); //on drag destroys circle CB
        if (stage == 6 && isBoiling && heat > 0) nextStage();
        if (stage >= 6 && stage <= 7 && isInside) Destroy(circleIndicatorAni); //on drag destroys circle PAN
        if (stage == 7 && pattyCook.cookedness <= .52f) nextStage();
        if (stage == 8 && pattyCook.cookedness <= .3f) nextStage();
        if (stage == 9) Destroy(circleIndicatorAni); //on drag destroys circle CB
        if (stage == 10 && heat == 4) nextStage();
        if (stage < 10 && heat == 4 && isChaosMode) {
            if (typedYet) typedYet = false;
            stage = 11;
        }

        switch (stage) {
            case 0:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    ttcButton.SetActive(true);
                }
                break;
            case 1:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    ttcButton.SetActive(true);
                }
                break;
            case 2:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    if (!isInside) circleIndicatorAni = Instantiate(circleIndicatorAnimation, panPosition, new Quaternion(0, 0, 0, 0));
                }
                break;
            case 3:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    if (pattyCook.cookedness <= .52f) nextStage();
                }
                break;
            case 4:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    circleIndicatorAni = Instantiate(circleIndicatorAnimation, cbPosition, new Quaternion(0, 0, 0, 0));
                }
                break;
            case 5:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    ttcButton.SetActive(true);
                    Invoke("nextStage", 3.5f);
                    addMoney(8);
                }
                break;
            case 6:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    circleIndicatorAni = Instantiate(circleIndicatorAnimation, panPosition, new Quaternion(0, 0, 0, 0));
                }
                break;
            case 7:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();

                }
                break;
            case 8:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    isChaosMode = true; //turns kitchen into a nightmare - Gordon. Im Nino!
                    circleIndicatorAni = Instantiate(circleIndicatorAnimation, cbPosition, new Quaternion(0, 0, 0, 0));
                }
                break;
            case 9:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    Invoke("nextStage", 6f);
                }
                break;
            case 10:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                }
                break;
            case 11:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    Invoke("nextStage", 6f);
                    StartCoroutine(fadeOpacity());
                }
                break;
            case 12:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                    Invoke("nextStage", 6f);
                }
                break;
            case 13:
                if (!typedYet)
                {
                    dialogueText.GetComponent<TypeWriterEffect>().typeText();
                }
                break;
        }
	}

    public void nextStage()
    {
        ttcButton.SetActive(false);
        stage++;
        if (typedYet) typedYet = false;
    }

    IEnumerator nextStageTimed(float delayTime)
    {
        ttcButton.SetActive(false);
        yield return new WaitForSeconds(delayTime);
        stage++;
        if (typedYet) typedYet = false;
    }

    void updateMoneyOLD(int increaseAmount)
    {
        money = money + increaseAmount;
        moneyText.text = "$" + money.ToString();
    }

    void addMoney(int increaseAmount)
    {
        currentMoneyValue = money;
        money = money + increaseAmount;
        StartCoroutine(updateMoney());
    }

    IEnumerator updateMoney()
    {
        moneyText.text = "$" + currentMoneyValue.ToString();
        while (currentMoneyValue < money)
        {
            currentMoneyValue++;
            moneyText.text = "$" + currentMoneyValue.ToString();
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator fadeOpacity()
    {
        while (overlayOpacity < .2f) 
        {
            overlayOpacity = overlayOpacity + .01f;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, overlayOpacity);
            yield return new WaitForSeconds(.1f);
        }
    }
}
