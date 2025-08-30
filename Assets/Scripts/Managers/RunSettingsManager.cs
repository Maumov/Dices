using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSettingsManager : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    [SerializeField] GameManager gameManager;

    public delegate void RunSettingsChangeDelegate();
    public event RunSettingsChangeDelegate OnRunSettingsChange;

    public PlayerStart GetPlayerStart()
    {
        return playerState.GetPlayerStart();
    }

    public void Init()
    {
        playerState.InitPlayerStart();
        OnRunSettingsChange?.Invoke();
    }

    public void Next()
    {   
        playerState.SelectedStartSetup++;
        OnRunSettingsChange?.Invoke();
    }

    public void Previous()
    {
        playerState.SelectedStartSetup--;
        OnRunSettingsChange?.Invoke();
    }
}
