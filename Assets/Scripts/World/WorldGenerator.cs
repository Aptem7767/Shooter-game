using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int worldSize = 300;
    public int treeCount = 300;
    public int buildingCount = 30;
    public int enemyCount = 20;
    
    void Start()
    {
        GenerateWorld();
    }
    
    public void GenerateWorld()
    {
        CreateGround();
        CreateBounds();
        SpawnTrees();
        SpawnBuildings();
        SpawnEnemies();
    }
    
    void CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.position = new Vector3(worldSize/2f, 0, worldSize/2f);
        ground.transform.localScale = new Vector3(worldSize/10f, 1, worldSize/10f);
        ground.layer = LayerMask.NameToLayer("Ground");
        
        var renderer = ground.GetComponent<Renderer>();
        renderer.material.color = new Color(0.3f, 0.5f, 0.2f);
    }
    
    void CreateBounds()
    {
        float h = 20f;
        CreateWall(new Vector3(worldSize/2f, h/2f, 0), new Vector3(worldSize, h, 2));
        CreateWall(new Vector3(worldSize/2f, h/2f, worldSize), new Vector3(worldSize, h, 2));
        CreateWall(new Vector3(0, h/2f, worldSize/2f), new Vector3(2, h, worldSize));
        CreateWall(new Vector3(worldSize, h/2f, worldSize/2f), new Vector3(2, h, worldSize));
    }
    
    void CreateWall(Vector3 pos, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = pos;
        wall.transform.localScale = scale;
        wall.GetComponent<Renderer>().material.color = Color.gray;
    }
    
    void SpawnTrees()
    {
        for (int i = 0; i < treeCount; i++)
        {
            Vector3 pos = RandomPosition();
            CreateTree(pos);
        }
    }
    
    void CreateTree(Vector3 pos)
    {
        GameObject tree = new GameObject("Tree");
        tree.transform.position = pos;
        
        // Ствол
        GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        trunk.transform.SetParent(tree.transform);
        trunk.transform.localPosition = new Vector3(0, 2, 0);
        trunk.transform.localScale = new Vector3(0.5f, 2f, 0.5f);
        trunk.GetComponent<Renderer>().material.color = new Color(0.4f, 0.26f, 0.13f);
        
        // Листва
        GameObject leaves = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        leaves.transform.SetParent(tree.transform);
        leaves.transform.localPosition = new Vector3(0, 4.5f, 0);
        leaves.transform.localScale = new Vector3(3f, 3f, 3f);
        leaves.GetComponent<Renderer>().material.color = new Color(0.13f, 0.55f, 0.13f);
    }
    
    void SpawnBuildings()
    {
        for (int i = 0; i < buildingCount; i++)
        {
            Vector3 pos = RandomPosition();
            CreateBuilding(pos);
        }
    }
    
    void CreateBuilding(Vector3 pos)
    {
        float w = Random.Range(5f, 15f);
        float h = Random.Range(5f, 20f);
        float d = Random.Range(5f, 15f);
        
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.name = "Building";
        building.transform.position = pos + Vector3.up * h/2f;
        building.transform.localScale = new Vector3(w, h, d);
        building.GetComponent<Renderer>().material.color = new Color(
            Random.Range(0.5f, 0.9f),
            Random.Range(0.5f, 0.9f),
            Random.Range(0.5f, 0.9f)
        );
    }
    
    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 pos = RandomPosition();
            pos.y = 1f;
            CreateEnemy(pos);
        }
    }
    
    void CreateEnemy(Vector3 pos)
    {
        GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        enemy.name = "Enemy";
        enemy.transform.position = pos;
        enemy.GetComponent<Renderer>().material.color = Color.red;
        enemy.AddComponent<Health>();
        enemy.AddComponent<EnemyAI>();
    }
    
    Vector3 RandomPosition()
    {
        return new Vector3(
            Random.Range(20f, worldSize - 20f),
            0,
            Random.Range(20f, worldSize - 20f)
        );
    }
}
