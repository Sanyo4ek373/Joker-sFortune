using System;
using UnityEngine;
using System.Collections.Generic;

public class BalanceManager : MonoBehaviour {
    public Action<int, int> OnBalanceUpdate;
 
    [SerializeField] private int _baseBalance;

    private int _totalBalance = 500;
    private int _roundBalance;
    private int _totalCost = 2;

    private List<int> _diamondsValueList = new();

    public int TotalBalance => _totalBalance;
    public int TotalCost => _totalCost;

    public int WinChance {get; set;}
    public int WinValue {get; set;}
    public int TicketCost {get; set;}

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

    public void SetTotalPrice(int cardsAmount) {
        _totalCost = cardsAmount * TicketCost;
    }

    public void SetRoundBalance(int cardsAmount) {
        _totalBalance += _roundBalance - _totalCost;
        if (_totalBalance <= 0) _totalBalance = _baseBalance;
 
        OnBalanceUpdate?.Invoke(_totalBalance, _roundBalance);

        _roundBalance = 0;
        _diamondsValueList = new();
    }

    private void Awake() {
        OnBalanceUpdate?.Invoke(_totalBalance, _roundBalance);
    }
}