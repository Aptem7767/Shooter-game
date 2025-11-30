using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Stats")]
    public string weaponName = "Pistol";
    public float damage = 25f;
    public float fireRate = 0.2f;
    public float range = 100f;
    public int magazineSize = 12;
    public int currentAmmo;
    public int reserveAmmo = 60;
    
    [Header("Effects")]
    public Transform muzzle;
    public ParticleSystem muzzleFlash;
    public AudioSource audioSource;
    public AudioClip shootSound;
    
    private float nextFireTime;
    private bool isReloading;
    private Camera mainCam;
    
    void Start()
    {
        mainCam = Camera.main;
        currentAmmo = magazineSize;
    }
    
    public void TryShoot()
    {
        if (isReloading || Time.time < nextFireTime || currentAmmo <= 0)
            return;
        
        Shoot();
    }
    
    void Shoot()
    {
        nextFireTime = Time.time + fireRate;
        currentAmmo--;
        
        // Эффекты
        if (muzzleFlash) muzzleFlash.Play();
        if (audioSource && shootSound) audioSource.PlayOneShot(shootSound);
        
        // Raycast
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log($"Hit: {hit.collider.name}");
            
            var health = hit.collider.GetComponent<Health>();
            if (health) health.TakeDamage(damage);
        }
        
        // Обновляем UI
        UIManager.Instance?.UpdateAmmo(currentAmmo, reserveAmmo);
    }
    
    public void Reload()
    {
        if (isReloading || currentAmmo == magazineSize || reserveAmmo <= 0)
            return;
        
        isReloading = true;
        Invoke(nameof(FinishReload), 2f);
    }
    
    void FinishReload()
    {
        int needed = magazineSize - currentAmmo;
        int toReload = Mathf.Min(needed, reserveAmmo);
        
        currentAmmo += toReload;
        reserveAmmo -= toReload;
        isReloading = false;
        
        UIManager.Instance?.UpdateAmmo(currentAmmo, reserveAmmo);
    }
}
