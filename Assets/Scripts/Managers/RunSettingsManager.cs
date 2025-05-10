using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSettingsManager : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    [SerializeField] GameManager gameManager;

    public delegate void RunSettingsChangeDelegate();
    public event RunSettingsChangeDelegate OnRunSettingsChange;

    private void Start()
    {
        OnRunSettingsChange?.Invoke();
    }
    public PlayerStart GetPlayerStart()
    {
        return playerState.GetPlayerStart();
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
