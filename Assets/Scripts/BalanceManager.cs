using UnityEngine;
using System.Collections.Generic;

public class BalanceManager : MonoBehaviour {
    [SerializeField] private int _totalBalance;

    public int WinChance {get; set;}
    public int WinValue {get; set;}

    private int _roundBalance;
    private int _ticketCost = 1;

    private List<int> _diamondsValueList = new();

    public void CalculateTotalPrize(CardType cardType, int winAmount) {
        if (cardType == CardType.Jocker) _roundBalance += winAmount;
        else _diamondsValueList.Add(winAmount);
    }

    public void CalculateDiamondsPrize(int diamondsAmount) {
        int totalPrize = 1;

        for (int i = 0; i < diamondsAmount; ++i) {
            totalPrize *= _diamondsValueList[i];
        }

        _totalBalance += totalPrize;
    }

    public void SetRoundBalance(int cardsAmount) {
        _totalBalance += _roundBalance - cardsAmount * _ticketCost * WinValue;
        _roundBalance = 0;

        if (_totalBalance <= 0) _totalBalance = 1000;
    }
}