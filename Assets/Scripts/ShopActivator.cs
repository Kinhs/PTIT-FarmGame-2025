using UnityEngine;
using UnityEngine.InputSystem;

public class ShopActivator : MonoBehaviour
{
    public enum ShopType
    {
        plant,
        fish
    }

    public ShopType type;

    private bool canOpen;

    private void Update()
    {
        if (canOpen == true)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame)
            {
                switch (type)
                {
                    case ShopType.plant:
                        if (UIController.instance.theShop.gameObject.activeSelf == false)
                        {
                            UIController.instance.theShop.OpenClose();
                        }
                        break;

                    case ShopType.fish:
                        if (UIController.instance.fishShop.gameObject.activeSelf == false)
                        {
                            UIController.instance.fishShop.OpenClose();
                        }
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOpen = false;
        }
    }
}
