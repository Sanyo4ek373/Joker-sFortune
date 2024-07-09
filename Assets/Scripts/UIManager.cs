using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BalanceManager))]
public class UIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _totalBalanceLabel;
    [SerializeField] private TextMeshProUGUI _roundbalanceLabel;
    [SerializeField] private TextMeshProUGUI _roundPriceLabel;

    private BalanceManager _balanceManager;

    private void Awake() {
        _balanceManager = GetComponent<BalanceManager>();
        _balanceManager.OnBalanceUpdate += UIUpdate;
    }

    private void OnDestroy() {
        _balanceManager.OnBalanceUpdate -= UIUpdate;
    }

    private void UIUpdate(int totalBalance, int roundBalance) {
        _totalBalanceLabel.text = $"{totalBalance}";
        
        _roundbalanceLabel.text = roundBalance < 0 ? $"-{roundBalance}" : $"+{roundBalance}";
        _roundbalanceLabel.color = roundBalance < 0 ? Color.red : Color.green;
    }
}