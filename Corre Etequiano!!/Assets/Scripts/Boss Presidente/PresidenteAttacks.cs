using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresidenteAttacks : MonoBehaviour
{
    [Header("Stage 1")]
    [SerializeField]
    private Transform hand;
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float minFireTime;
    [SerializeField]
    private float maxFireTime;

    [SerializeField]
    private List<FireType> fireTypes;

    [System.Serializable]
    public class FireType
    {
        public string name;
        public float y;
    }

    private float firePositionY;
    private bool chegouPerto;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
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

    private IEnumerator Fire()
    {
        firePositionY = fireTypes[0].y;
        while (true)
        {
            float RamdomFireTime = Random.Range(minFireTime, maxFireTime);
            yield return new WaitForSeconds(RamdomFireTime);

            GameObject Ins = Instantiate(bullet, hand.position, transform.rotation); //Criar obstacle
            Ins.transform.parent = this.transform.parent;

            RamdomizeFireType();
        }
    }
}
