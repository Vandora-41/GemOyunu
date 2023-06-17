using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
public class Gem : MonoBehaviour
{
    public string gemName;
    public float salePrice;
    public Image gemIcon;
    public float spawnChance = 0;
    public GameObject gemModel;
    public int id;

    public float scaleTime = 5.0f; 
    public float currentScale = 0.25f; 
    public Vector3 initialScale = new Vector3(0.25f, 0.25f, 0.25f); 
    public Vector3 targetScale = new Vector3(1f, 1f, 1f);

    private void Start() {
    }

    public void ResetSize(){
        transform.localScale = initialScale;
        transform.localScale = initialScale;

        


    }

    public void StartGem(){
        StartCoroutine(Scale());
        transform.DOMoveY(transform.position.y + 0.5f, 3f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo); 

        transform.DORotate(new Vector3(0f, 360f, 0f), 5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear) 
            .SetLoops(-1, LoopType.Restart); 
    }

    private IEnumerator Scale()
    {
        float startTime = Time.time;

        while (Time.time < startTime + scaleTime)
        {
            currentScale = (Time.time - startTime) / scaleTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, currentScale);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
