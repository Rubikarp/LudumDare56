using System;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using NaughtyAttributes;


public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private PlayerNet net;

    [SerializeField] private float netLaunchDuration = 1f;
    [SerializeField] private float netRadius = 1f;
    [SerializeField] private float netDistance = 5f;
    [SerializeField] private float netCoolDown = 5f;
    [HorizontalLine]
    [SerializeField] private Ease launchScaleEsae  = Ease.InBack;
    [SerializeField] private Ease retrieveScaleEsae  = Ease.InBack;
    [Space]
    [SerializeField] private Ease launchMoveEsae = Ease.InBack;
    [SerializeField] private Ease retrieveMoveEsae = Ease.InBack;



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
        var coroutine = LauchNetCoroutine(netLaunchDuration, netDistance, netRadius);
        lauchNetCoroutine = StartCoroutine(coroutine);
    }

    private IEnumerator LauchNetCoroutine(float launchDur, float launchDist, float netScale)
    {
        launchNetPos = aimTransform.position;
        aimNetPos = launchNetPos + AimDirection.normalized * launchDist;

        net.gameObject.SetActive(true);
        net.transform.position = aimTransform.position;
        net.transform.localScale = Vector3.one * .25f;

        var netLaunchSequence = DOTween.Sequence();

        netLaunchSequence.Append(net.transform.DOMove(aimNetPos, launchDur).SetEase(launchMoveEsae));
        netLaunchSequence.Join(net.transform.DOScale(Vector3.one * netScale, launchDur).SetEase(launchScaleEsae));

        netLaunchSequence.Play();
        yield return netLaunchSequence.WaitForCompletion();

        var netRetractSequence = DOTween.Sequence();

        netRetractSequence.Append(net.transform.DOMove(aimTransform.position, .3f).SetEase(retrieveMoveEsae));
        netRetractSequence.Join(net.transform.DOScale(Vector3.zero, .3f).SetEase(retrieveScaleEsae));

        netRetractSequence.Play();
        yield return netRetractSequence.WaitForCompletion();


        yield return new WaitForSeconds(netCoolDown);

        net.gameObject.SetActive(false);
        lauchNetCoroutine = null;
    }
}
