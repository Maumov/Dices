using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCinematic : MonoBehaviour
{
    [SerializeField] IntroController controller;

    [SerializeField] Button skipButton;
    [SerializeField] IntroOutro introOutro;
    [SerializeField] float duration;
    [SerializeField] GameFlowInteractions interactions;

    

    private void Start()
    {
        introOutro.gameObject.SetActive(true);
        skipButton.interactable = true;
        StartCoroutine( runningIntro() );
    }

    void Nothing() 
    { 
    }

    IEnumerator runningIntro()
    {
        introOutro.Intro( Nothing );
        float currentTime = 0f;
        while ( currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
        exitIntroCinematic();
    }


    public void exitIntroCinematic()
    {
        introOutro.Outro( GoToNextScreen );
    }

    void GoToNextScreen()
    {
        StopAllCoroutines();
        controller.GoToNextScreen();
    }

}
