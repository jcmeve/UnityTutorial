using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour { 
    [Header("현재 장착된 총")]
    [SerializeField] Gun currentGun;
    float currentFireRate=0;
    //
    Queue<GameObject> bullets = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //
            CreateBullets();
        //
    }
    #region PoolingScripts
    void CreateBullets() {
        for (int i = 0; i < 10; i++) {
            GameObject temp = Instantiate(currentGun.go_Bullet_Prefab);
            
            temp.SetActive(false);
            bullets.Enqueue(temp);
        }
    }
    public void PushToBullets(GameObject temp) {
        
        temp.SetActive(false);
        bullets.Enqueue(temp);
    }
    GameObject PopFromBulltes() {
        if (bullets.Count == 0)
            CreateBullets();
        return bullets.Dequeue();
    }
    #endregion PoolingScripts
    // Update is called once per frame
    void Update()
    {
        FireRateCalc();
        TryFire();
        LockOnMouse();
    }
    void FireRateCalc() {
        if (currentFireRate > 0) {
            currentFireRate -= Time.deltaTime;
        }
    }
    void TryFire() {
        if (Input.GetButton("Fire1")) {
            if (currentFireRate <= 0) {
                Fire();
                currentFireRate = currentGun.fireRate;
            }
        }
    }
    void Fire() {
        SoundManager.instance.PlaySE(currentGun.soundFire);
        // currentGun.animator.SetTrigger("GunFire");
        currentGun.ps_MuzzleFlash.Play();
        var clone = PopFromBulltes();
        clone.transform.position = currentGun.ps_MuzzleFlash.transform.position;
        clone.transform.rotation = transform.rotation;
        clone.SetActive(true);
        
        clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * currentGun.speed);
    }

    void LockOnMouse() {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraPos.x));
        Vector3 target = new Vector3(0f, mousePos.y, mousePos.z);
        transform.LookAt(target);
    }
}
