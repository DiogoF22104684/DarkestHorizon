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
        Bazooka
    }

    private Player_Movement playerMovementScript;

    [SerializeField] private Sprite Assault_Rifle, Shotgun, Pistol, Bazooka;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = FindObjectOfType<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GunTypeInfo()
    {
        switch(gunType)
        {
            case GunType.Assault_Rifle:
                playerMovementScript.GunTypeReciever(Assault_Rifle);

                break;
            case GunType.Pistol:
                playerMovementScript.GunTypeReciever(Pistol);

                break;
            case GunType.Shotgun:
                playerMovementScript.GunTypeReciever(Shotgun);

                break;
            case GunType.Bazooka:
                playerMovementScript.GunTypeReciever(Bazooka);

                break;
        }
    }
}
