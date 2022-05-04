using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PresidenteAttacks : MonoBehaviour
{
    [SerializeField]
    private Transform hand;

    [SerializeField]
    private GameObject bulletStage1;
    public GameObject bulletStage2;
    public GameObject currentBullet;

    [SerializeField]
    private float minFireTime;
    [SerializeField]
    private float maxFireTime;
    [SerializeField]
    private float margemTime;
    [SerializeField]
    private float decreaseTime;

    [SerializeField]
    private List<FireType> fireTypes;

    private PresidenteController controller;

    [System.Serializable]
    public class FireType
    {
        public string name;
        public float y;
    }

    private float firePositionY;
    private bool chegouPerto;

    [SerializeField]
    private UnityEvent fireEvent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
        StartCoroutine(Every1Second());
        controller = GetComponent<PresidenteController>();
        currentBullet = bulletStage1;
    }

    private void RamdomizeFireType()
    {
        int RamdomNum = Random.Range(0, fireTypes.Count);
        firePositionY = fireTypes[RamdomNum].y;
        chegouPerto = false;

    }

    private void Update()
    {
        if (transform.position.y < firePositionY && !chegouPerto)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + GameController.instance.CurrentSpeed * Time.deltaTime);
            if (transform.position.y > firePositionY)
                chegouPerto = true;
        }

        else if (transform.position.y > firePositionY && !chegouPerto)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - GameController.instance.CurrentSpeed * Time.deltaTime);
            if (transform.position.y < firePositionY)
                chegouPerto = true;
        }
    }

    IEnumerator Every1Second() //A cada segundo
    {
        while (true) //Loop infinito
        {
            yield return new WaitForSeconds(1f); //Esperar um segundo

            if (GameController.instance.IsPaused == false)
            {
                if (maxFireTime > minFireTime + margemTime) //Se o tempo max for maior q o tempo min + margem de tempo
                {
                    maxFireTime -= decreaseTime / controller.life; //Diminuir tempo maximo
                }
            }
        }
    }

    private IEnumerator Fire()
    {
        firePositionY = fireTypes[0].y;
        while (true)
        {
            float RamdomFireTime = Random.Range(minFireTime, maxFireTime);
            yield return new WaitForSeconds(RamdomFireTime);

            fireEvent.Invoke();
            GameObject Ins = Instantiate(currentBullet, hand.position, transform.rotation); //Criar obstacle
            Ins.transform.parent = this.transform.parent;

            RamdomizeFireType();
        }
    }
}
