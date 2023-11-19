using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMEWON : MonoBehaviour
{
    [SerializeField] private AudioSource _opmeSound = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GAMEWONSCRIPT());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GAMEWONSCRIPT()
    {
        var canvasPanelHolder = GameObject.FindGameObjectWithTag("canvas").transform.GetChild(0);
        var panel1 = canvasPanelHolder.transform.GetChild(3).gameObject;
        var panel2 = canvasPanelHolder.transform.GetChild(2).gameObject;
        var panel3 = canvasPanelHolder.transform.GetChild(1).gameObject;
        var panel4 = canvasPanelHolder.transform.GetChild(0).gameObject;
        yield return new WaitForSeconds(0.8f);
        panel1.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        panel2.SetActive(false);
        _opmeSound.Play();
        yield return new WaitForSeconds(1.5f);
        panel3.SetActive(false);
    }
}
