using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatsToBet : ChipEffect
{
    [SerializeField] Color textColor;
    [SerializeField] Color grayColor;
    [SerializeField] Color betColor;
    [SerializeField] Color multColor;
    [SerializeField] Color moneyColor;

    ChipController chipController;
    private void Start()
    {
        chipController = GetComponent<ChipController>();
    }

    public override string GetDescription()
    {
        string currently = string.Format( "<color={0}> (currently) </color>", IEffect.ToHex( grayColor ) );
        string bet = string.Format( "<color={0}> {1} </color>", IEffect.ToHex( betColor ), chipController.GetChipStats().chips );
        string mult = string.Format( "<color={0}> {1} </color>", IEffect.ToHex( multColor ), chipController.GetChipStats().multiplier );
        string money = string.Format( "<color={0}> {1} </color>", IEffect.ToHex( moneyColor ), chipController.GetChipStats().moneyReturn );
        string stats = bet +", " +mult+ ", " + money;
        string s = string.Format( "Adds the stats of the chip to the bet. \n {0} \n {1} ", currently, stats );
        return s;
    }
    public override IEnumerator ProcessEffect()
    {
        Debug.Log( "Process chip " );
        yield return null;
    }
}
