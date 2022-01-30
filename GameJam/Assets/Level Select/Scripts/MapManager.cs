using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin StartPin;
	public Text SelectedLevelText;
	public InputManager inputManager;
    private void Awake()
    {
		inputManager = GetComponent<InputManager>();
		inputManager.Button1PressedEvent += EnterLevel;
    }
    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start ()
	{
		// Pass a ref and default the player Starting Pin
		Character.Initialise(this, StartPin);
	}


	/// <summary>
	/// This runs once a frame
	/// </summary>
	private void Update()
	{
		// Only check input when character is stopped
		if (Character.IsMoving) return;
		
		// First thing to do is try get the player input
		CheckForInput();
	}

	
	/// <summary>
	/// Check if the player has pressed a button
	/// </summary>
	private void CheckForInput()
	{
		if (inputManager.Vertical > 0)
		{
			Character.TrySetDirection(Direction.Up);
		}
		else if (inputManager.Horizontal < 0)
		{
			Character.TrySetDirection(Direction.Down);
		}
		else if (inputManager.Horizontal < 0)
		{
			Character.TrySetDirection(Direction.Left);
		}
		else if (inputManager.Horizontal > 0)
		{
			Character.TrySetDirection(Direction.Right);
		}
	}

	void EnterLevel()
    {
		try
        {
			if (Character.IsCurrentPinLevel())
				SceneManager.LoadScene(Character.CurrentPin.SceneToLoad);
		}
		catch
        {
			print("Couldnt load scene");
        }
    }

	
	/// <summary>
	/// Update the GUI text
	/// </summary>
	public void UpdateGui()
	{
		SelectedLevelText.text = string.Format("Current Level: {0}", Character.CurrentPin.SceneToLoad);
	}
}
