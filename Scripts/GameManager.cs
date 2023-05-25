using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    ProgressBar pbHealth, pbEnergy, pbFood;
    
    //Hunger
    [SerializeField] float decreaseFood=1f, decreaseRate=0.5f;

    //Energy
    [SerializeField] float walkEnergy = 1f, runEnergy = 3f;

    bool dead = false;

	void Start () {
        StartCoroutine(DecreaseHunger());
       
	}
	
	IEnumerator DecreaseHunger()
    {        
        while (pbFood.Val>0)
        {
            pbFood.Val -= decreaseFood;
            yield return new WaitForSeconds(decreaseRate);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().ClaireDead();

    }

    private void Update()
    {
        //Energy

        if(Input.GetAxis("Vertical")!=0 && !dead)
        {
            pbEnergy.Val -= walkEnergy;

            if (pbEnergy.Val == 0)
            {
                dead = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().ClaireDead();
            }


        }

        if (Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.LeftControl) && !dead)
        {
            pbEnergy.Val -= runEnergy;
            if (pbEnergy.Val == 0)
            {
                dead = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().ClaireDead();
            }
               
        }
    }
    
}
