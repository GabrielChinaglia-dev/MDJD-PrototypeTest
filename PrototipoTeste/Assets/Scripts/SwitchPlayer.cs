using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform character;
    public List<Transform> possibleCharacter;
    private int wichCharacter;

    public List<GameObject> possibleTerrains;
    public CinemachineFreeLook cam;

    // Start is called before the first frame update
    void Start()
    {
        if(character == null && possibleCharacter.Count >= 1)
        {
            character = possibleCharacter[0];
        }
        Swap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(wichCharacter == 0)
            {
                wichCharacter = possibleCharacter.Count - 1;
            }
            else
            {
                wichCharacter --;
            }
            print(wichCharacter);
            Swap();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(wichCharacter == possibleCharacter.Count - 1)
            {
                wichCharacter = 0;
            }
            else
            {
                wichCharacter ++;
            }
            print(wichCharacter);
            Swap();
        }
    }

    public void Swap()
    {
        character = possibleCharacter[wichCharacter];
        character.GetComponent<ThirdPersonMovement>().enabled = true;
        possibleTerrains[wichCharacter].SetActive(true);
        for (int i = 0; i < possibleCharacter.Count; i++)
        {
            if (possibleCharacter[i] != character)
            {
                possibleCharacter[i].GetComponent<ThirdPersonMovement>().enabled = false;
                possibleTerrains[i].SetActive(false);
            }
        }
        cam.LookAt = character;
        cam.Follow = character;
    }
}