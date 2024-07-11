using System;
using UnityEngine;
using System.Collections.Generic;

public class BalanceManager : MonoBehaviour {
    public Action<int, int> OnBalanceUpdate;

    public int TotalBalance => _totalBalance;
    public int TotalCost => _totalCost;

    public int WinChance {get; set;}
    public int WinValue {get; set;}
    public int TicketCost {get; set;}

    private int _totalBalance = 500;
    private int _baseBalance = 500;
    private int _roundBalance;
    private int _totalCost = 2;

    private List<int> _diamondsValueList = new();

    private const string TOTAL_BALANCE = "total balance";

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
        PlayerPrefs.SetInt(TOTAL_BALANCE, _totalBalance);
 
        OnBalanceUpdate?.Invoke(_totalBalance, _roundBalance);

        _roundBalance = 0;
        _diamondsValueList = new();
    }

    private void Awake() {
        int oldBalance = PlayerPrefs.GetInt(TOTAL_BALANCE);
        if (oldBalance != 0) _totalBalance = oldBalance;
    }

    private void Start() {
        OnBalanceUpdate?.Invoke(_totalBalance, _roundBalance);
    }
}