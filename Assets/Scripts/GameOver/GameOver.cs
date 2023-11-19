using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource _knifePhys = null;
    [SerializeField] private AudioSource _bıçakSaplama = null;
    [SerializeField] private AudioSource _kızBagırma = null;

    void Start()
    {
        StartCoroutine(GameOverFX());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator GameOverFX()
    {
        var canvasPanelHolder = GameObject.FindGameObjectWithTag("canvas").transform.GetChild(0).gameObject;
        var panel1 = canvasPanelHolder.transform.GetChild(3).gameObject;
        var panel2 = canvasPanelHolder.transform.GetChild(2).gameObject;
        var panel3 = canvasPanelHolder.transform.GetChild(1).gameObject;
        var panel4 = canvasPanelHolder.transform.GetChild(0).gameObject;
        _knifePhys.Play();
        yield return new WaitForSeconds(1);
        panel1.SetActive(false);
        _kızBagırma.Play();
        yield return new WaitForSeconds(1);
        panel2.SetActive(false);
        _bıçakSaplama.Play();
        yield return new WaitForSeconds(1);
        panel3.SetActive(false);
    }
}
