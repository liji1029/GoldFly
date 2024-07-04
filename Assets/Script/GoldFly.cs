using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GoldFly : MonoBehaviour {

    private bool m_bInited = false;
    private Text m_text = null;
    public Vector2 m_TargetPos;
    public delegate void OnFinish(int value);
    private OnFinish OnCall;
    public static GoldFly goldFly = null;
    private GameObject m_Target = null;
    private GameObject m_info = null;
    private void Init()
    {
        if (m_bInited)
            return;
        m_bInited = true;
        m_text = GetComponentInChildren<Text>();
        m_info = transform.Find("info").gameObject ;
        Vector2 _BirthPos;
        if (m_Target != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Camera.main.
                 WorldToScreenPoint(m_Target.transform.position), null, out _BirthPos);
            m_info.transform.localPosition = _BirthPos;
        }
        else {
            m_info.transform.localPosition = Vector3.zero;
        }
    }
    void Start () {
       // FlyTo();
    }

    public string text {
        set {
            Init();
            m_text.text = value;
        }
        get {
            return m_text.text;
        }
    }

 

    public void FlyTo()
    {
        RectTransform rt = m_info.transform as RectTransform;
        rt.localScale = Vector3.zero;
        CanvasGroup _canvasGroup = rt.GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
            _canvasGroup = rt.gameObject.AddComponent<CanvasGroup>();
        Sequence mySequence = DOTween.Sequence();
        Tweener scale = rt.DOScale(Vector3.one, 0.1f).SetEase(Ease.Linear);
        Tweener move = DOTween.To(() => { return rt.anchoredPosition; }, v => { rt.anchoredPosition = v; }, m_TargetPos, 1.5f);
        Tweener alpha = _canvasGroup.DOFade(0, 0.2f);
        mySequence.Append(scale);
       // mySequence.AppendInterval(0.5f);
        mySequence.Append(move);
        mySequence.AppendInterval(0.2f);
        mySequence.Join(alpha).OnStepComplete(OnFinished);
    }
    private void OnFinished()
    {
        if (OnCall != null)
            OnCall(int.Parse(text));
        Destroy(gameObject);
    }
    public void OnRegister(OnFinish _call) {
        OnCall -= _call;
        OnCall += _call;
    }

    public static GoldFly CreatGoldFly(GameObject _target, Vector2 _EndPos) {
        GameObject obj = Instantiate(Resources.Load("GoldFly") as GameObject);
        Transform parent = GameObject.Find("Canvas").transform;
        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero;
         goldFly = obj.GetComponent<GoldFly>();
        if (goldFly == null)
            goldFly = obj.AddComponent<GoldFly>();
        goldFly.m_Target = _target;
        goldFly.m_TargetPos = _EndPos;
        goldFly.Init();
        goldFly.FlyTo();
        return goldFly;
    }

}
