using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] private GunType gunType;
    
    private enum GunType
    {
        Assault_Rifle,
        Shotgun,
        Pistol,
        Sniper
    }

    private Player_Movement playerMovementScript;

    [SerializeField] private Sprite Assault_Rifle, Shotgun, Pistol, Sniper;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = FindObjectOfType<Player_Movement>();

        switch (gunType)
        {
            case GunType.Assault_Rifle:
                GetComponent<SpriteRenderer>().sprite = Assault_Rifle;
                break;
            case GunType.Pistol:
                GetComponent<SpriteRenderer>().sprite = Pistol;
                break;
            case GunType.Shotgun:
                GetComponent<SpriteRenderer>().sprite = Shotgun;
                break;
            case GunType.Sniper:
                GetComponent<SpriteRenderer>().sprite = Sniper;
                break;
        }
    }

    public void GunTypeInfo()
    {
        switch(gunType)
        {
            case GunType.Assault_Rifle:
                playerMovementScript.GunTypeReciever(Assault_Rifle, 4f);
                break;
            case GunType.Pistol:
                playerMovementScript.GunTypeReciever(Pistol, 2f);
                break;
            case GunType.Shotgun:
                playerMovementScript.GunTypeReciever(Shotgun, 1f);
                break;
            case GunType.Sniper:
                playerMovementScript.GunTypeReciever(Sniper, 0.3f);
                break;
        }
    }
}
