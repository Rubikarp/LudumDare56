using System;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private PlayerNet netPrefab;

    [SerializeField] private float netLaunchDuration = 1f;
    [SerializeField] private float netRadius = 1f;
    [SerializeField] private float netDistance = 5f;
    [SerializeField] private float netCoolDown = 5f;

    public Vector3 AimDirection { get; private set; }
    public Vector3 AimWorldPos { get; private set; }
    public Vector3 MouseWorldPos { get; private set; }

    public UnityEvent onLeftClick;
    public UnityEvent onRightClick;

    private Coroutine lauchNetCoroutine;
    private Vector3 launchNetPos;
    private Vector3 aimNetPos;


    private void Awake()
    {
        onLeftClick.AddListener(LauchNet);
    }

    void Update()
    {
        //Check if mouse in view frustrum
        if (!Camera.main.pixelRect.Contains(Input.mousePosition))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.back, Vector3.zero);

        if (plane.Raycast(ray, out float distance))
        {
            MouseWorldPos = ray.GetPoint(distance);

            AimDirection = MouseWorldPos - aimTransform.position;
            AimWorldPos = aimTransform.position + AimDirection.normalized;

            float angle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;
            aimTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Input.GetMouseButtonDown(0))
        {
            onLeftClick.Invoke();
        }
        if(Input.GetMouseButtonDown(1))
        {
            onRightClick.Invoke();
        }
    }

    private void LauchNet()
    {
        if(lauchNetCoroutine != null)
        {
            Debug.Log("Net is on cooldown");
            return;
        }
        lauchNetCoroutine = StartCoroutine(LauchNetCoroutine());
    }

    private IEnumerator LauchNetCoroutine()
    {
        launchNetPos = aimTransform.position;
        aimNetPos = launchNetPos + AimDirection.normalized * netDistance;

        var net = Instantiate(netPrefab, launchNetPos, Quaternion.identity);

        net.transform.position = launchNetPos;
        net.transform.localScale = Vector3.zero;

        var netLaunchSequence = DOTween.Sequence();

        netLaunchSequence.Append(net.transform.DOMove(aimNetPos, netLaunchDuration).SetEase(Ease.OutCirc));
        netLaunchSequence.Join(net.transform.DOScale(Vector3.one, netLaunchDuration).SetEase(Ease.InSine));

        netLaunchSequence.Play();
        yield return netLaunchSequence.WaitForCompletion();

        var netRetractSequence = DOTween.Sequence();

        netRetractSequence.Append(net.transform.DOMove(aimTransform.position, .3f).SetEase(Ease.OutCirc));
        netRetractSequence.Join(net.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InExpo));

        netRetractSequence.Play();
        yield return netRetractSequence.WaitForCompletion();


        yield return new WaitForSeconds(netCoolDown);
        lauchNetCoroutine = null;
    }
}

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerNet : MonoBehaviour
{
    public static UnityAction<FishItem> onFishCaught;

    private CircleCollider2D netCollider;
    private Transform netSkin;

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
            var fishItem = fish.CatchFish();
        }
    }

}
