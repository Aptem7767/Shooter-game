using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameHUDPanel;
    public GameObject pausePanel;
    
    [Header("HUD")]
    public Slider healthBar;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI coinsText;
    
    [Header("Joysticks")]
    public Joystick moveJoystick;
    public Joystick lookJoystick;
    
    [Header("References")]
    public PlayerController player;
    public CameraController cameraController;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        // Передаём ввод с джойстиков
        if (player && moveJoystick)
            player.SetMoveInput(new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical));
        
        if (cameraController && lookJoystick)
            cameraController.SetLookInput(new Vector2(lookJoystick.Horizontal, lookJoystick.Vertical));
    }
    
    public void UpdateHealth(float percent)
    {
        if (healthBar) healthBar.value = percent;
    }
    
    public void UpdateAmmo(int current, int max)
    {
        if (ammoText) ammoText.text = $"{current}/{max}";
    }
    
    public void ShowMainMenu()
    {
        mainMenuPanel?.SetActive(true);
        gameHUDPanel?.SetActive(false);
        Time.timeScale = 0f;
    }
    
    public void StartGame()
    {
        mainMenuPanel?.SetActive(false);
        gameHUDPanel?.SetActive(true);
        Time.timeScale = 1f;
    }
    
    public void TogglePause()
    {
        bool isPaused = !pausePanel.activeSelf;
        pausePanel?.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
    
    // Кнопки действий
    public void OnJumpPressed()
    {
        player?.Jump();
    }
    
    public void OnSprintPressed(bool pressed)
    {
        player?.SetSprint(pressed);
    }
}
