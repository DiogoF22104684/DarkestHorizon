using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] private GunType gunType;
    [SerializeField] public bool isGameManager = false;

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
        Cheats();
    }

    public void GunTypeInfo()
    {
        gunInfo = FindObjectOfType<GunInfo>().GetComponent<GunInfo>();
        switch(gunType)
        {
            case GunType.Assault_Rifle:
                gunInfo.GunTypeReceiver(Assault_Rifle, 4f, 10f, 1, 31, false);
                break;
            case GunType.Pistol:
                gunInfo.GunTypeReceiver(Pistol, 2f, 10f, 1, 10, true);
                break;
            case GunType.Shotgun:
                gunInfo.GunTypeReceiver(Shotgun, 2f, 15f, 2,11 , false);
                break;
            case GunType.Sniper:
                gunInfo.GunTypeReceiver(Sniper, 1f, 20f, 4,5 , false);
                break;
        }
    }

    private void Cheats()
    {
        gunInfo = FindObjectOfType<GunInfo>();
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
