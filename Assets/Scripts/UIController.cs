using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private int _maxHealthPoint;
    [SerializeField] private int _healthPoint;

    [Space(10)]
    [SerializeField] private GameObject _endRoundPanel;

    [Header("Components")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _healthText; 
    [SerializeField] private TextMeshProUGUI _coinAmountText;

    [Header("Buttons")]
    [SerializeField] private GameObject _healthButton;
    [SerializeField] private GameObject _damageButton;
    [SerializeField] private GameObject _speedButton;
    [SerializeField] private GameObject _moneyButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _healthStatText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _moneyText;

    private static UIController _instance;

    public static UIController Instance { get {  return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _healthPoint = _maxHealthPoint;
        UpdateHPUI(_healthPoint);
        UpdateCoinText(PlayerController.Instance.Statistics.Money);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateHPUI(int healthAmount)
    {
        _healthSlider.maxValue = _maxHealthPoint;
        _healthSlider.value = healthAmount;

        _healthText.SetText($"HP: {healthAmount}");
    }

    public void UpdateCoinText(int coinAmount)
    {
        _coinAmountText.SetText($"Player's coins: {coinAmount}");
    }

    public void ChangeHealthStat(int level, int amount)
    {
        _maxHealthPoint = amount;
        UpdateHPUI(amount);
        _healthStatText.text = $"Health: {amount}";
        _healthButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }

    public void ChangeDamageStat(int level, int amount)
    {
        _damageText.text = $"PL Damage: {amount}";
        _damageButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }

    public void ChangeSpeedStat(int level, float amount)
    {
        _speedText.text = $"Speed: {amount}";
        _speedButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }

    public void ChangeMoneyStat(int level, float amount)
    {
        _moneyText.text = $"Money mul: {amount}";
        _moneyButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{10 + (5 * level)} coins";
    }

    public void ShowStatShop()
    {
        _endRoundPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StartNewRound()
    {
        _endRoundPanel.SetActive(false);
    }
}
