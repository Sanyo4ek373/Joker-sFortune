using System;
using UnityEngine;
using System.Collections.Generic;

public class BalanceManager : MonoBehaviour {
    public Action<int, int> OnBalanceUpdate;
 
    [SerializeField] private int _baseBalance;

    public int TotalBalance => _totalBalance;

    public int WinChance {get; set;}
    public int WinValue {get; set;}
    public int TicketCost {get; set;}

    private int _totalBalance;
    private int _roundBalance;

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

        _roundBalance += totalPrize;
    }

    public void SetRoundBalance(int cardsAmount) {
        _totalBalance += _roundBalance - cardsAmount * TicketCost * WinValue;
        _roundBalance = 0;
        _diamondsValueList = new();

        if (_totalBalance <= 0) _totalBalance = _baseBalance;
 
        OnBalanceUpdate(_totalBalance, _roundBalance);
    }
}