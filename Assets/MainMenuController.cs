using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] RunSettingsController runSettingsController;

    [SerializeField] IntroOutro introOutro;

    void Nothing()
    {

    }

    public void Show()
    {
        mainMenu.gameObject.SetActive(true);
        introOutro.Intro( Nothing );
    }

    public void Play()
    {
        runSettingsController.Show();
    }

    public void Options()
    {
        

    }

    public void Exit()
    {

    }
}
