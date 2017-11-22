using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour {

	public float delay = 0.005f;
	public string fullText;
	private string currentText = "";

    // Use this for initialization
    void Start () {
		//StartCoroutine(ShowText());
	}
	
    

    public void typeText()
    {
        fullText = WorldManager.messages[WorldManager.stage];
        StartCoroutine(ShowText());
        WorldManager.typedYet = true;
    }



    IEnumerator ShowText(){
		for(int i = 0; i < fullText.Length; i++){
			currentText = fullText.Substring(0,i);
			this.GetComponent<Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
	}
}
