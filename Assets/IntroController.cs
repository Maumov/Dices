using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [SerializeField] GameObject intro;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject play;
    [SerializeField] MainMenuController mainMenuController;

    private void Start()
    {
        mainMenu.SetActive(false);
        play.SetActive(false);
        intro.SetActive(true);
    }

    public void GoToNextScreen()
    {
        intro.SetActive( false );
        mainMenuController.Show();
    }

}
