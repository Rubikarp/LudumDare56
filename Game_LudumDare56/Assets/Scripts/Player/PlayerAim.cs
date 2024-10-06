using System;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using NaughtyAttributes;
using UnityEngine.UI;


public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private PlayerData playerdata;
    
    [SerializeField] private PlayerNet net;
    [SerializeField] private Image circularCooldown;

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
        playerdata = PlayerData.Instance;
        onLeftClick.AddListener(LauchNet);
        circularCooldown.gameObject.SetActive(false);
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
        var coroutine = LauchNetCoroutine(playerdata.CurrentNet);
        lauchNetCoroutine = StartCoroutine(coroutine);
    }

    private IEnumerator LauchNetCoroutine(Netdata netdata)
    {
        launchNetPos = aimTransform.position;
        aimNetPos = launchNetPos + AimDirection.normalized * netdata.launchDistance;

        net.gameObject.SetActive(true);
        net.transform.position = aimTransform.position;
        net.transform.localScale = Vector3.one * .25f;

        var netLaunchSequence = DOTween.Sequence();

        netLaunchSequence.Append(net.transform.DOMove(aimNetPos, netdata.launchDuration).SetEase(netdata.launchMoveEsae));
        netLaunchSequence.Join(net.transform.DOScale(Vector3.one * netdata.captureRadius, netdata.launchDuration).SetEase(netdata.launchScaleEsae));

        netLaunchSequence.Play();
        yield return netLaunchSequence.WaitForCompletion();

        var netRetractSequence = DOTween.Sequence();

        netRetractSequence.Append(net.transform.DOMove(aimTransform.position, .3f).SetEase(netdata.retrieveMoveEsae));
        netRetractSequence.Join(net.transform.DOScale(Vector3.zero, .3f).SetEase(netdata.retrieveScaleEsae));

        netRetractSequence.Play();
        yield return netRetractSequence.WaitForCompletion();

        circularCooldown.gameObject.SetActive(true);
        circularCooldown.DOFillAmount(1, netdata.useCoolDown).From(0);
        yield return new WaitForSeconds(netdata.useCoolDown);
        circularCooldown.gameObject.SetActive(false);

        net.gameObject.SetActive(false);
        lauchNetCoroutine = null;
    }
}

[Serializable]
public class Netdata
{
    public float launchDuration;
    public float launchDistance;
    public float captureRadius;
    public float useCoolDown;
    [HorizontalLine]
    public Ease launchScaleEsae = Ease.InOutSine;
    public Ease retrieveScaleEsae = Ease.InOutSine;
    [Space]
    public Ease launchMoveEsae = Ease.InOutSine;
    public Ease retrieveMoveEsae = Ease.InOutSine;

}
