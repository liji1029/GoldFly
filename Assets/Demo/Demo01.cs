using UnityEngine;
using UnityEngine.UI;

public class Demo01 : MonoBehaviour
{
    public NumPlus _assemble;//滚动字幕
    int number;//计数
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        //_assemble.EnableDelayShow(true);
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj == null)
                    return;
                GoldFly goldFly = GoldFly.CreatGoldFly(obj, new Vector2(788, 500));
                ScoreInfo score = obj.GetComponent<ScoreInfo>();
                goldFly.text = score.Score.ToString();
                goldFly.OnRegister((int v) =>
                {
                    number += v;
                    _assemble.value = number;
                });

                Destroy(hit.collider.gameObject);
            }
        }

     
    }
}
