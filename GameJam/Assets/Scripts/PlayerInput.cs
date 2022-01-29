using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event System.Action Fire;
    public enum controlProfile { KeyBoard, Controller };
    public List<string> buttonNames = new List<string>();
    public string[] joysticks;

    public bool _isControllerConnected = false;
    public string menuName;


    public string Controller = "";

    public string UpKeyCode_string = "W", DownKeyCode_string = "S", LeftKeyCode_string = "A", RightKeyCode_string = "D", FireKeyCode_string = "F";
    public string UpKeyCode_string1 = "W", DownKeyCode_string1 = "S", LeftKeyCode_string1 = "A", RightKeyCode_string1 = "D", FireKeyCode_string1= "F";
    public string UpKeyCode_string2 = "T", DownKeyCode_string2 = "G", LeftKeyCode_string2 = "F", RightKeyCode_string2 = "L ", RightRotKeyCode_string = "right", LeftRotKeyCode_string = "left", FireKeyCode_string2 = "C";
    string VerticalMovement_string = "Xbox One Axis Y", HorizontalMovement_string = "Xbox One Axis X", VerticalRotation_string = "Xbox One Axis 4", HorizontalRotation_string = "Xbox One Axis 5", FireKeyCode_string3 = "Joystick1Button1";
    public bool SwapControllerJoySticksEnabled = false;
    KeyCode UpKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "W");
    KeyCode DownKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "S");
    KeyCode LeftKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "A");
    KeyCode RightKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "D");

    string keyToChange = "null";
    bool keyChanging = false;
    [SerializeField]bool isPaused = false;
    bool getInputKey = false;
    string inputKeyCode_string = " ";
    public string currentProfile = "Profile 1";

    string KeyDisplay;


    [SerializeField]
    float horizontal;
    [SerializeField]
    float vertical;
    [SerializeField]
    Vector3 rotation;
    public controlProfile currentControl = controlProfile.KeyBoard;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            togglePause();
        if (Input.GetKeyDown(KeyCode.Space))
            SwapControllerJoySticksEnabled = !SwapControllerJoySticksEnabled;
        GetKeys();
        checkInput();
        DetectController();
    }
    void DetectController()
    {
        try
        {
            joysticks = Input.GetJoystickNames();
            int joyStickIndex = -1;
            for(int i = 0; i < joysticks.Length; i++)
            {
                if(!joysticks[i].Equals(""))
                {
                    joyStickIndex = i;
                    break;
                }    
            }
            if(joyStickIndex != -1)
            {
                _isControllerConnected = true;
                IdentifyController(joyStickIndex);
            }
            else
            {
                Controller = "Not Connected";
                currentControl = controlProfile.KeyBoard;
                _isControllerConnected = false;
            }
        }
        catch
        {
            Controller = "Not Connected";
            currentControl = controlProfile.KeyBoard;
            _isControllerConnected = false;
        }
    }
    void IdentifyController(int joystickIndex = 0)
    {
        Controller = Input.GetJoystickNames()[joystickIndex];
            
    }
    void GetKeys()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(vKey))
            {
                KeyDisplay = vKey.ToString();
                if (!buttonNames.Contains(KeyDisplay))
                    buttonNames.Add(KeyDisplay);
                //Debug.Log(KeyDisplay);
            }
            else if(Input.GetKeyUp(vKey))
            {
                KeyDisplay = "";
            }
        }
        ChangeKeyCode();

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha1))
        {
            SwitchControlProfile("Profile 1");
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha2))
        {
            SwitchControlProfile("Profile 2");
        }
        else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha3))
        {
            SwitchControlProfile("Profile 3");
        }
    }
    void SwitchControlProfile(string Scheme)
    {
        switch(Scheme)
        {
            case "Profile 1":
                currentProfile = "Profile 1";
                UpKeyCode_string = UpKeyCode_string1;
                DownKeyCode_string = DownKeyCode_string1;
                LeftKeyCode_string = LeftKeyCode_string1;
                RightKeyCode_string = RightKeyCode_string1;
                break;
            case "Profile 2":
                currentProfile = "Profile 2";

                UpKeyCode_string = UpKeyCode_string2;
                DownKeyCode_string = DownKeyCode_string2;
                LeftKeyCode_string = LeftKeyCode_string2;
                RightKeyCode_string = RightKeyCode_string2;
                break;
            case "Profile 3":
                currentProfile = "Profile 3";
                break;
        }
        print("Current profile: " + Scheme);
        KeyCodeUpdate();
    }
    void KeyCodeUpdate()
    {
        UpKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), UpKeyCode_string);
        DownKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), DownKeyCode_string);
        LeftKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), LeftKeyCode_string);
        RightKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), RightKeyCode_string);
    }
    void ChangeKeyCode()
    {
        foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(vKey) && getInputKey)
            {
                inputKeyCode_string = vKey.ToString();
                print(inputKeyCode_string);
                getInputKey = false;
            }
        }
        if (inputKeyCode_string == "" || inputKeyCode_string == " ")
        {
            return;
        }
        if(inputKeyCode_string.Contains("Joystick"))
        {

        }
        else
        {
            if (keyToChange.Equals("up1"))
            {
                UpKeyCode_string1 = inputKeyCode_string;
            }
            else if (keyToChange.Equals("up2"))
            {
                UpKeyCode_string2 = inputKeyCode_string;
            }
            else if (keyToChange.Equals("down1"))
            {
                DownKeyCode_string1 = inputKeyCode_string;
            }
            else if (keyToChange.Equals("down2"))
            {
                DownKeyCode_string2 = inputKeyCode_string;
            }
            else if (keyToChange.Equals("right1"))
            {
                RightKeyCode_string1 = inputKeyCode_string;
            }
            else if (keyToChange.Equals("right2"))
            {
                RightKeyCode_string2 = inputKeyCode_string;
            }
            else if (keyToChange.Equals("left1"))
            {
                LeftKeyCode_string1 = inputKeyCode_string;
            }
            else if (keyToChange.Equals("left2"))
            {
                LeftKeyCode_string2 = inputKeyCode_string;
            }
            else
            {
                SaveButtons();
            }
        }
    }

    void checkInput()
    {
        if(!keyChanging && !isPaused)
        {
            switch (currentControl)
            {
                case controlProfile.KeyBoard:
                    KEYBOARD();
                    break;
                case controlProfile.Controller:
                    CONTROLLER();
                    break;
            }
        }
        DetectController();
    }
    void KEYBOARD()
    {
        float tempH = 0;
        float tempV = 0;
        if (Input.GetKey(LeftKeyCode))
            tempH += -1;
        if (Input.GetKey(RightKeyCode))
            tempH += 1;
        if (Input.GetKey(UpKeyCode))
            tempV += 1;
        if (Input.GetKey(DownKeyCode))
            tempV += -1;
        horizontal = tempH;
        vertical = tempV;
        if(currentProfile.Equals("Profile 1"))
            rotation = new Vector3(x: 0, Input.GetAxis("Mouse X"), 0);
        else if(currentProfile.Equals("Profile 2"))
        {
            float tempRot = 0;
            if(Input.GetKey(RightRotKeyCode_string))
            {
                tempRot += 1;
            }
            if(Input.GetKey(LeftRotKeyCode_string))
            {
                tempRot -= 1;
            }
            rotation = new Vector3(0, tempRot, 0);
        }
        //if (Input.GetKeyDown(KeyCode.F))
            //Fire();
    }
    void CONTROLLER()
    {
        float tempH = 0;
        float tempV = 0;
        if (Controller.Equals("Controller (Xbox One For Windows)"))
        {
            tempH = !SwapControllerJoySticksEnabled ?
                Input.GetAxisRaw("Xbox One Axis X") :
                Input.GetAxisRaw("Xbox One Axis 4");
            tempV = !SwapControllerJoySticksEnabled ?
                Input.GetAxisRaw("Xbox One Axis Y") :
                Input.GetAxisRaw("Xbox One Axis 5");

            float rotY = !SwapControllerJoySticksEnabled ?
                Input.GetAxisRaw("Xbox One Axis 4") :
                Input.GetAxisRaw("Xbox One Axis X");
            float rotX = !SwapControllerJoySticksEnabled ?
                Input.GetAxisRaw("Xbox One Axis 5") :
                Input.GetAxisRaw("Xbox One Axis Y");
            rotation = new Vector3( 0, rotY, 0);
        }
        horizontal = tempH;
        vertical = tempV;
        //if (Input.GetKeyDown(FireKeyCode_string3))
            //Fire();
    }
    public void OnGUI()
    {
        if(isPaused)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 300, 600, 800));
            GUI.Box(new Rect(0, 100, 600, 400), menuName + " (" + currentProfile + ")");

            if (GUI.Button(new Rect(150, 140, 135, 20), "Keyboard 1"))
            {
                SwitchControlProfile("Profile 1");
                currentProfile = "Profile 1";
                currentControl = controlProfile.KeyBoard;
            }
            if (GUI.Button(new Rect(325, 140, 135, 20), "Keyboard 2"))
            {
                SwitchControlProfile("Profile 2");
                currentProfile = "Profile 2";
                currentControl = controlProfile.KeyBoard;
            }
            if (GUI.Button(new Rect(500, 140, 135, 20), Controller))
            {
                SwitchControlProfile("Profile 3");
                currentProfile = "Profile 3";
                currentControl = controlProfile.Controller;
            }


            GUI.Label(new Rect(25, 175, 125, 20), "Keyboard Up ");
            if (GUI.Button(new Rect(150, 175, 135, 20), UpKeyCode_string1))
            {
                keyChanging = true;
                getInputKey = true;
                keyToChange = "up1";
                inputKeyCode_string = " ";
            }
            if (GUI.Button(new Rect(325, 175, 135, 20), UpKeyCode_string2))
            {
                keyChanging = true;
                getInputKey = true;
                keyToChange = "up2";
                inputKeyCode_string = " ";
            }

            GUI.Label(new Rect(25, 200, 125, 20), "Keyboard Down: ");
            if (GUI.Button(new Rect(150, 200, 135, 20), DownKeyCode_string1))
            {
                keyChanging = true;
                getInputKey = true;
                keyToChange = "down1";
                inputKeyCode_string = " ";
            }
            if (GUI.Button(new Rect(325, 200, 135, 20), DownKeyCode_string2))
            {
                keyChanging = true;
                getInputKey = true;
                keyToChange = "down2";
                inputKeyCode_string = " ";
            }

            GUI.Label(new Rect(25, 225, 125, 20), "Keyboard Right: ");
            if (GUI.Button(new Rect(150, 225, 135, 20), RightKeyCode_string1))
            {
                keyChanging = true;
                getInputKey = true;
                keyToChange = "right1";
                inputKeyCode_string = " ";
            }
            if (GUI.Button(new Rect(325, 225, 135, 20), RightKeyCode_string2))
            {
                keyChanging = true;
                getInputKey = true;
                keyToChange = "right2";
                inputKeyCode_string = " ";
            }

            GUI.Label(new Rect(25, 250, 125, 20), "Keyboard Left: ");
            if (GUI.Button(new Rect(150, 250, 135, 20), LeftKeyCode_string1))
            {
                keyChanging = true;
                getInputKey = true;
                keyToChange = "left1";
                inputKeyCode_string = " ";
            }
            if (GUI.Button(new Rect(325, 250, 135, 20), LeftKeyCode_string2))
            {
                keyChanging = true;
                getInputKey = true;
                keyToChange = "left2";
                inputKeyCode_string = " ";
            }

            GUI.Label(new Rect(500, 160, 125, 20), "Toggle Swap");
            if(GUI.Toggle(new Rect(575, 160, 90, 90), SwapControllerJoySticksEnabled, ""))
            {
                SwapControllerJoySticksEnabled = true;
            }
            else
            {
                SwapControllerJoySticksEnabled = false;
            }

            if (GUI.Button(new Rect(220, 300, 135, 20), "Save"))
            {
                SaveButtons();
            }
            GUI.EndGroup();

            GUI.Button(new Rect(25, 25, 125, 20), KeyDisplay);
        }
    }
    void togglePause()
    {
        isPaused = !isPaused;
        if (!isPaused)
            SaveButtons();
        else
        {
            horizontal = 0;
            vertical = 0;
            rotation = new Vector3();
        }
    }
    void SaveButtons()
    {
        SwitchControlProfile(currentProfile);
        KeyCodeUpdate();
        keyChanging = false;
    }

    public float Horizontal { get { return horizontal; } }
    public float Vertical { get { return vertical; } }
    public Vector3 Rotation { get { return rotation; } }
}
