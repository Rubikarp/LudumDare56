using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Cette classe représente un lance pierre
public class InterfaceExemple : MonoBehaviour
{
    public IProjectile currentEquipedProjectil;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch(currentEquipedProjectil.Type)
            {
                case ProjectileType.Contondant:
                    currentEquipedProjectil.Launch(transform.position, transform.forward);
                    currentEquipedProjectil.Launch(transform.position, transform.forward);
                    currentEquipedProjectil.Launch(transform.position, transform.forward);
                    break;
                case ProjectileType.Perce:
                    Debug.Log("Lance une flèche");
                    break;
                case ProjectileType.Explosif:
                    Debug.Log("Lance une grenade");
                    break;
                case ProjectileType.Incendiaire:
                    Debug.Log("Lance un cocktail molotov");
                    break;
            }
            currentEquipedProjectil.Launch(transform.position, transform.forward);
        }

    }
}

public enum ProjectileType
{
    Contondant,
    Perce,
    Explosif,
    Incendiaire
}
public interface IProjectile
{
    ProjectileType Type { get; }

    public void Launch(Vector3 origin, Vector3 direction);
}

public class Cailloux : MonoBehaviour, IProjectile
{
    public ProjectileType Type => ProjectileType.Contondant;

    public void Launch(Vector3 origin, Vector3 direction)
    {
        Instantiate(gameObject, origin, Quaternion.LookRotation(direction));
    }
}
public class Fleche : MonoBehaviour, IProjectile
{
    public ProjectileType Type => ProjectileType.Perce;

    public void Launch(Vector3 origin, Vector3 direction)
    {
        Debug.Log("Lance une flèche");
    }
}

