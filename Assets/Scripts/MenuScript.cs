using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Canvas mainMenu;
    public Button menuStartButton;
    public Button menuCreditsButton;
    public Button menuControllsButton;
    public Button menuExitButton;

    public Canvas creditMenu;
    public Button creditMenuExitButton;

    public Canvas controllsMenu;
    public Button controllsMenuExitButton;

    public Canvas quitMenu;
    public Button quitMenuYesButton;
    public Button quitMenuNoButton;

    public Canvas startScreen;
    public Canvas startScreen2;

    private int screen1 = 0;
    private bool isScreen1 = false;
    private int screen2 = 0;
    private bool isScreen2 = false;

    // Use this for initialization
    void Start()
    {
        mainMenu = mainMenu.GetComponent<Canvas>();
        menuStartButton = menuStartButton.GetComponent<Button>();
        menuCreditsButton = menuCreditsButton.GetComponent<Button>();
        menuControllsButton = menuControllsButton.GetComponent<Button>();
        menuExitButton = menuExitButton.GetComponent<Button>();

        creditMenu = creditMenu.GetComponent<Canvas>();
        creditMenuExitButton = creditMenuExitButton.GetComponent<Button>();

        controllsMenu = controllsMenu.GetComponent<Canvas>();
        controllsMenuExitButton = controllsMenuExitButton.GetComponent<Button>();

        quitMenu = quitMenu.GetComponent<Canvas>();
        quitMenuYesButton = quitMenuYesButton.GetComponent<Button>();
        quitMenuNoButton = quitMenuNoButton.GetComponent<Button>();

        quitMenu.enabled = false;
        creditMenu.enabled = false;
        controllsMenu.enabled = false;
        startScreen.enabled = false;
        startScreen2.enabled = false;
    }

    public void FixedUpdate()
    {
        if (isScreen1)
        {
            screen1++;

            if (screen1 >= 60*1)
            {
                screen2 = 0;
                isScreen1 = false;
                isScreen2 = true;
                startScreen.enabled = false;
                startScreen2.enabled = true;
            }
        }

        else if (isScreen2)
        {
            screen2++;

            if (screen2 >= 60 * 1)
            {
                SceneManager.LoadScene("level1");
            }
        }
    }

    public void MenuStartButtonPress()
    {
        mainMenu.enabled = false;
        startScreen.enabled = true;

        isScreen1 = true;
        screen1 = 0;
    }

    public void MenuCreditsButtonPress()
    {
        mainMenu.enabled = false;
        creditMenu.enabled = true;
    }

    public void MenuControllsButtonPress()
    {
        mainMenu.enabled = false;
        controllsMenu.enabled = true;
    }

    public void MenuExitButtonPress()
    {
        mainMenu.enabled = false;
        quitMenu.enabled = true;
    }

    public void CreditMenuExitButton()
    {
        creditMenu.enabled = false;
        mainMenu.enabled = true;
    }

    public void ControllsMenuExitButton()
    {
        controllsMenu.enabled = false;
        mainMenu.enabled = true;
    }

    public void QuitMenuYesButtonPress()
    {
        Application.Quit();
    }

    public void QuitMenuNoButtonPress()
    {
        quitMenu.enabled = false;
        mainMenu.enabled = true;
    }
}
