using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarActivation : MonoBehaviour
{
    [SerializeField] public PlayerLevelStatus.StarColor colorGroup;
    [SerializeField] private Collider[] starColliders;
    [SerializeField] private MeshRenderer starMesh; 

    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material activeMaterial;

    private int activationCount;

    private void Start() 
    {
        ActivateStar(false);
    }

    private void Update() 
    {
        if(ColorActivated() && activationCount == 0)
        {
            activationCount += 1;
            ActivateStar(true);
        }    
    }

    bool ColorActivated()
    {
        if(colorGroup == PlayerLevelStatus.StarColor.Blue && PlayerLevelStatus.blueActive)
        {
            return true;
        }
        else if(colorGroup == PlayerLevelStatus.StarColor.Red && PlayerLevelStatus.redActive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ActivateStar(bool choice)
    {       
        for (int i = 0; i < starColliders.Length; i++)
        {
            starColliders[i].enabled = choice;    
        }
        
        if(choice)
        {
            starMesh.material = activeMaterial;
        }
        else
        {
            starMesh.material = inactiveMaterial;
        }
    }



}
