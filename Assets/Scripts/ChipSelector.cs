using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ChipSelector : MonoBehaviour
{
    [SerializeField] private KeyCode[] keybinds;

    [SerializeField] private ItemContainer chipsContainer;
    [SerializeField] private PlayerChips playerChips;
    [SerializeField] private int currentSelectedChip;


    // Update is called once per frame
    void Update()
    {
        CheckButtonBinding(); 
    }

    void SetSelectedChip( int selection)
    {
        GameObject go = chipsContainer.GetItem(selection);
        if ( go == null)
        {
            return;
        }
        ChipController chip = chipsContainer.GetItem( selection ).GetComponent<ChipController>();
        if ( chip == null)
        {
            return;
        }
        playerChips.SelectChip( chip );
    }

    void CheckButtonBinding()
    {
        int chipsCount = playerChips.ChipsCount();
        for(int i = 0 ; i < chipsCount ; i++)
        {
            if ( Input.GetKeyDown( keybinds[i] ) )
            {
                SetSelectedChip( i );
            }
        }
    }
    public void BetButtonClicked( BetButton button )
    {
        playerChips.AddChipToBet( button );
    }
}
