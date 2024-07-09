using System;   
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    [SerializeField] private Slider _cardsAmountSlider;
    [SerializeField] private Slider _ticketCostSlider;

    [SerializeField] private BalanceManager _balanceManager;

    private int _cardsAmount = 12;
    private int _minimumWinChance = 13;

    private void Update() {
        _cardsAmount = (int) _cardsAmountSlider.value;
        _balanceManager.WinChance = _minimumWinChance - _cardsAmount;

        _balanceManager.TicketCost = (int) _ticketCostSlider.value;
        _balanceManager.WinValue = _balanceManager.TicketCost / 2;
    }
}