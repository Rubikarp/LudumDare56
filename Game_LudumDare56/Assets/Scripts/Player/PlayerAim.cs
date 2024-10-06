using UnityEngine;
using UnityEngine.Events;
public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;

    public Vector3 AimDirection { get; private set; }
    public Vector3 AimWorldPos { get; private set; }
    public Vector3 MouseWorldPos { get; private set; }


    public UnityEvent onLeftClick;
    public UnityEvent onRightClick;

    void Update()
    {
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
}
