using UnityEngine;
using System.Collections.Generic;

public class BalanceManager : MonoBehaviour {
    [SerializeField] private int _balance;

    private int _jocker = 7;
    private int _diamond = 1;

    private List<int> _diamondsValueList = new();

    public void CalculateTotalPrize(int cardType, int winAmount) {
        if (cardType == _jocker) _balance += winAmount;
        else if (cardType == _diamond) _diamondsValueList.Add(winAmount);
    }

    public void CalculateDiamondsPrize(int diamondsAmount) {
        int totalPrize = 1;

        for (int i = 0; i < diamondsAmount; ++i) {
            totalPrize *= _diamondsValueList[i];
        }

        _balance += totalPrize;
    }
}