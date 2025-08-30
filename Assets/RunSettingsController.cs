using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSettingsController : MonoBehaviour
{
    [SerializeField] RunSettingsUI ui;
    [SerializeField] RunSettingsManager runSettingsManager;
    [SerializeField] PanelAnimator runSettingsAnimator;

    [SerializeField] IntroOutro introOutro;
    [SerializeField] GameObject mainMenu;
    [SerializeField] PlayController playController;

    public void Show()
    {
        runSettingsManager.Init();
        runSettingsAnimator.Show();
    }

    public void StartRun()
    {
        introOutro.Outro( Hide );
    }

    void Hide()
    {
        runSettingsAnimator.Hide();
        mainMenu.SetActive( false );
        playController.Show();
    }
}
