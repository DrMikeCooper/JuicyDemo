using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JuicyButton : MonoBehaviour {

    // Use this for initialization
    void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => ScaleWobble());
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void MoveWobble()
    {
        // method 1 - chaining
        //iTween.MoveTo(gameObject, iTween.Hash("y", 1, "time", 0.5, "easeType", "easeInOutExpo", "oncomplete", "MoveWobble2", "oncompletetarget", gameObject));

        // method 2 - delay
        iTween.MoveTo(gameObject, iTween.Hash("y", 1, "time", 0.5, "easeType", "easeInOutExpo"));
        iTween.MoveTo(gameObject, iTween.Hash("y", 0, "time", 0.5, "easeType", "easeInOutExpo", "delay", 0.5));

    }

    public void MoveWobble2()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 0, "time", 0.5, "easeType", "easeInOutExpo"));
    }

    public void ScaleWobble()
    {
        //iTween.ScaleTo(gameObject, iTween.Hash("time", 0.25, "y", 2, "easeType", "easeInOutExpo", "loopType", "none"));
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(2, 2, 2), "time", 0.25, "easeType", "easeInBack", "oncomplete", "ScaleWobble2", "oncompletetarget", gameObject));
    }

    public void ScaleWobble2()
    {
        //iTween.ScaleTo(gameObject, iTween.Hash("time", 0.25, "y", 2, "easeType", "easeInOutExpo", "loopType", "none"));
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(1,1,1), "time", 0.25, "easeType", "easeOutBack"));
    }
}
