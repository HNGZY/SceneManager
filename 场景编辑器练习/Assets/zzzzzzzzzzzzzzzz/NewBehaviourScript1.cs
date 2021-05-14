using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{

    public class aaa
    {
        public override bool Equals(object obj)
        {
            Debug.Log(1111);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    void Start()
    {
        aaa a1 = new aaa();
        aaa a2 = new aaa();
        if (!a1.Equals(a2))
        {
            Debug.Log("sss");
        }

        HashSet<aaa> hs = new HashSet<aaa>();  //对比器，T必须实现 Equals 和 GetHashCode

        List<aaa> list = new List<aaa>();
    }
    
}
