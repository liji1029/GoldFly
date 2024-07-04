using UnityEngine;
using UnityEngine.UI;

public class NumPlus : MonoBehaviour
{
    private Text m_label;
    private float m_num = 0;
    private int m_newNum = 0;
    private float m_delta = 0;
    private string m_strFormat = null;
    void Start()
    {
        m_label = GetComponent<Text>();
    }

    public int value
    {
        get
        {
            return m_newNum;
        }
        set
        {
            m_newNum = value;

            m_delta = 0.1f;// 数值跳动的大小  越小跳动的越慢

            ShowNum();
        }
    }

   

    public string format
    {
        get
        {
            return m_strFormat;
        }
        set
        {
            m_strFormat = value;
            ShowNum();
        }

    }

    void ShowNum()
    {
        if (m_label == null)
            m_label = GetComponentInChildren<Text>();
        if (m_strFormat == null)
            m_label.text = "" + (int)m_num;
        else
        {
            string str = string.Format(m_strFormat, (int)m_num);
            m_label.text = str;
        }
    }

    void Update()
    {
        if (m_delta != 0)
        {
            m_num += m_delta;

            if (m_delta > 0)
            {
                if (m_num > m_newNum)
                {
                    m_num = m_newNum;
                }
            }
            else
            {
                if (m_num <= m_newNum)
                    m_num = m_newNum;
            }
            if (m_num == m_newNum)
                m_delta = 0;
            ShowNum();
        }
    }
}
