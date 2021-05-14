using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPate : MonoBehaviour
{
    private GameObject m_go;
    private GameObject player;

    public Text m_name;
    public Slider m_hp;
    public Slider m_mp;
    public GameObject m_gather;
    public List<Image> m_gathers;
    public RoleType role;

    int timerid = -1;

    public void InitPate(int num)
    {
        m_go = GameObject.Instantiate(Resources.Load<GameObject>("pate"));
        m_go.transform.SetParent(UIMgr.Instance.m_hudroot.transform);
        m_go.transform.localPosition = Vector3.zero;
        m_go.transform.localScale = Vector3.one;

        m_name = m_go.transform.Find("name").GetComponent<Text>();
        m_hp = m_go.transform.Find("hp").GetComponent<Slider>();
        m_mp = m_go.transform.Find("mp").GetComponent<Slider>();
        m_gather = m_go.transform.Find("gather").gameObject;
        m_gathers = new List<Image>();
        for (int i = 0; i < m_gather.transform.childCount; i++)
        {
            m_gathers.Add(m_gather.transform.GetChild(i).GetComponent<Image>());
        }
    }

    public void Show(bool active)
    {
        if (m_go)
        {
            m_go.SetActive(active);
        }
    }

    public void SetData(string name,float hp,float mp, RoleType type)
    {
        role = type;
        m_name.text = name;
        m_hp.value = hp;
        m_mp.value = mp;
    }

    public void SetData(int gather)
    {

    }
    Vector3 camerapos = Vector3.zero;
    Vector3 cameraposs = Vector3.zero;
    private void Update()
    {
        if(this.gameObject.name == "player")
        {
            camerapos.Set(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 5, this.gameObject.transform.position.z);
        }
        else
        {
            camerapos.Set(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
        }
        m_go.transform.position = World.Instance.camera.WorldToScreenPoint(camerapos);
    }
    /// <summary>
    /// 希构造函数
    /// </summary>
    ~UIPate()
    {
        m_name = null;
        m_hp = null;
        m_mp = null;
        m_gather = null;
        if (m_gathers!=null)
        {
            m_gathers.Clear();
        }
        m_gathers = null;
    }
}
