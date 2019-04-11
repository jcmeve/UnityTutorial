using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JetEngineFuelManager : MonoBehaviour
{
    [SerializeField] float maxFuel;
    float currentFuel;

    [SerializeField] Slider slider_JetEngine;
    [SerializeField] Text txt_JetEngine;

    public bool isFuel { get; private set; }
    PlayerController thePlayer;

    [SerializeField] float waitRechargeFuel;
    float currentWaitRechargeFuel;
    [SerializeField] float rechargeSpeed;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        currentFuel = maxFuel;
        slider_JetEngine.maxValue = maxFuel;
        slider_JetEngine.value = currentFuel;
    }

    // Update is called once per frame
    void Update() {

        CheckFuel();
        UsedFuel();


        slider_JetEngine.value = currentFuel;
        txt_JetEngine.text = Mathf.Round(currentFuel / maxFuel * 100f).ToString() + " %";
    }
    void CheckFuel() {
        isFuel = currentFuel > 0 ? true : false;
    }

    void UsedFuel() {
        if (thePlayer.isJet) {
            slider_JetEngine.gameObject.SetActive(true);
            currentFuel -= Time.deltaTime;
            currentWaitRechargeFuel = 0f;
            if (currentFuel <= 0)
                currentFuel = 0f;

        }
        else {
            FuelRecharge();
        }
    }
    void FuelRecharge() {
        if (currentFuel < maxFuel) {

            currentWaitRechargeFuel += Time.deltaTime;
            if (currentWaitRechargeFuel >= waitRechargeFuel) {
                currentFuel += Time.deltaTime * rechargeSpeed;
            }
        }
        else
            slider_JetEngine.gameObject.SetActive(false);
    }
}
