using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerNet : MonoBehaviour
{
    public static UnityAction<FishItem> onFishCaught;

    [SerializeField]
    private Transform netSkin;
    private CircleCollider2D netCollider;

    private void Awake()
    {
        netCollider = GetComponent<CircleCollider2D>();
        netCollider.isTrigger = true;
    }

    public void Init(float radius)
    {
        netCollider.radius = 1f;
        netSkin.localScale = Vector3.one * radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Fish fish))
        {
            Debug.Log("Fish caught");
            var fishItem = fish.CatchFish();
            onFishCaught?.Invoke(fishItem);
        }
    }

}
