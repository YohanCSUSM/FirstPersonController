using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public Text winText;
	public GameObject quitButton;

	void Start()
	{
		if (winText != null)
			winText.text = "";
	}

	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void End(bool win)
	{
		if (win)
			winText.text = "You win !";
		else
			winText.text = "You lose !";
		
		quitButton.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
	}
}
