using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] private GunType gunType;
    [SerializeField] private bool isGameManager = false;

    private enum GunType
    {
        Assault_Rifle,
        Shotgun,
        Pistol,
        Sniper
    }

    private GunInfo gunInfo;

    [SerializeField] private Sprite Assault_Rifle, Shotgun, Pistol, Sniper;

    // Start is called before the first frame update
    void Start()
    {

        if(!isGameManager)
        {
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
    }

    void Update()
    {
        gunInfo = FindObjectOfType<GunInfo>();
        Cheats();
    }

    public void GunTypeInfo()
    {
        switch(gunType)
        {
            case GunType.Assault_Rifle:
                gunInfo.GunTypeReceiver(Assault_Rifle, 4f);
                break;
            case GunType.Pistol:
                gunInfo.GunTypeReceiver(Pistol, 2f);
                break;
            case GunType.Shotgun:
                gunInfo.GunTypeReceiver(Shotgun, 1f);
                break;
            case GunType.Sniper:
                gunInfo.GunTypeReceiver(Sniper, 0.3f);
                break;
        }
    }

    private void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunType = GunType.Assault_Rifle;
            GunTypeInfo();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunType = GunType.Pistol;
            GunTypeInfo();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunType = GunType.Shotgun;
            GunTypeInfo();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gunType = GunType.Sniper;
            GunTypeInfo();
        }
    }
}
