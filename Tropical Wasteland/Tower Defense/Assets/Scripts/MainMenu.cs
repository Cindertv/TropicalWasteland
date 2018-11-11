using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string levelToLoad = "MainLevel";
    public string options = "Options";
    public string back = "MainMenu";

	public SceneFader sceneFader;

	public void Play ()
	{
		sceneFader.FadeTo(levelToLoad);
	}

	public void Quit ()
	{
		Debug.Log("Exciting...");
		Application.Quit();
	}

    public void Options ()
    {
        sceneFader.FadeTo(options);
    }

    public void Back ()
    {

        sceneFader.FadeTo(back);
    }

}
