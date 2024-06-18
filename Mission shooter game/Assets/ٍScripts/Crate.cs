using UnityEngine;

public class Crate : MonoBehaviour
{
    private Animator animator;
    public Transform player;  // Oyuncunun transformunu burada atayın
    public float activationRadius = 1f;  // Sandığın açılabileceği mesafe
    public GameObject item;  // Sandığın içindeki nesne

    private bool isItemTaken = false; 
    
    [Header("Sounds")]
    public AudioClip audioClip; // Nesnenin alınıp alınmadığını kontrol etmek için
    public AudioSource audioSource; // Nesnenin alınıp alınmadığını kontrol etmek için

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator != null && player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= activationRadius)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    animator.SetTrigger("Open");
                    audioSource.PlayOneShot(audioClip);
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    animator.SetTrigger("Close");
                    audioSource.PlayOneShot(audioClip);
                }
                if (Input.GetKeyDown(KeyCode.F) && !isItemTaken)
                {
                    TakeItem();
                }
            }
        }
    }

    void TakeItem()
    {
        if (item != null)
        {
            // Nesneyi alınca yapılacak işlemler
            item.SetActive(false);  // Nesneyi devre dışı bırak
            isItemTaken = true;  // Nesnenin alındığını işaretle
            Debug.Log("Item has been taken!");
        }
    }
}
