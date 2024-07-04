using UnityEngine;

public class Demo02 : MonoBehaviour
{
    NumPlus numPlus;
    int num = 0;
    void Start()
    {
        numPlus = FindObjectOfType<NumPlus>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            num += 300;
            numPlus.value = num;
        }
    }
}
