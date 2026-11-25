using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class NVUI : MonoBehaviour
{
    public Image battery;
    public Image icons;
    public Light flashlight;
    bool icontriggered=false;
    bool flashlighttriggered=false;
    void Update()
    {
        if(Keyboard.current.fKey.wasPressedThisFrame)
        {
			if (icontriggered)
			{
                icontriggered = false;
			}
            else if(!icontriggered)
            {
            icontriggered = true;
            }
			icons.gameObject.SetActive(icontriggered);

			if (flashlighttriggered)
			{
				flashlighttriggered = false;
			}
			else if (!flashlighttriggered)
			{
				flashlighttriggered = true;
			}
			flashlight.gameObject.SetActive(flashlighttriggered);

		}

        if (Keyboard.current.eKey.isPressed)
        {
        battery.gameObject.SetActive(false);
        }
    }
}
