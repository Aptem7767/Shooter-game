using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Player")]
    public GameObject playerPrefab;
    public Vector3 spawnPoint = new Vector3(150, 2, 150);
    
    [Header("Game State")]
    public int coins = 0;
    public int kills = 0;
    
    private GameObject currentPlayer;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        SpawnPlayer();
    }
    
    public void SpawnPlayer()
    {
        if (currentPlayer != null)
            Destroy(currentPlayer);
        
        if (playerPrefab != null)
        {
            currentPlayer = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        }
        else
        {
            // Создаём простого игрока
            currentPlayer = CreateSimplePlayer();
        }
        
        // Настраиваем камеру
        var cam = Camera.main.GetComponent<CameraController>();
        if (cam) cam.target = currentPlayer.transform;
        
        // Настраиваем UI
        var ui = FindObjectOfType<UIManager>();
        if (ui) ui.player = currentPlayer.GetComponent<PlayerController>();
    }
    
    GameObject CreateSimplePlayer()
    {
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = spawnPoint;
        
        // Визуал
        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        body.transform.SetParent(player.transform);
        body.transform.localPosition = Vector3.zero;
        body.GetComponent<Renderer>().material.color = Color.blue;
        Destroy(body.GetComponent<Collider>());
        
        // Компоненты
        player.AddComponent<CharacterController>();
        player.AddComponent<PlayerController>();
        player.AddComponent<PlayerHealth>();
        
        return player;
    }
    
    public void AddCoins(int amount)
    {
        coins += amount;
        UIManager.Instance?.coinsText?.SetText($"Coins: {coins}");
    }
    
    public void AddKill()
    {
        kills++;
    }
    
    public void Respawn()
    {
        SpawnPlayer();
    }
}
