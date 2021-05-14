using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    private static LineRenderer GetLineRenderer(Transform t)
    {
        LineRenderer lr = t.GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = t.gameObject.AddComponent<LineRenderer>();
        }
        lr.SetWidth(0.1f, 0.1f);
        return lr;
    }
    
    
    public Transform Target;
    public float SkillDistance = 5;//扇形距离
    public float SkillJiaodu = 180;//扇形的角度

    void LateUpdate()
    {
        Vector3 norVector = transform.rotation * Vector3.forward ; //得到一个自己向前方的向量
        Vector3 temVector = Target.position - transform.position; //
        /// <summary>
        ///  1.norVec.normalized 和 temVec.normalized 表示的是两个向量的单位向量, 因为在公式里，有向量和模的除法，得出来的结果就是单位向量，所以我们这里和后面都直接用单位向量来计算，省去不少麻烦。 
        ///  2.Mathf.Rad2Deg表示的是 单位弧度的度数。
        ///  3.Acos代表余弦值为指定数字的角度。
        /// </summary>
        float jiajiao = Mathf.Acos(Vector3.Dot(norVector.normalized, temVector.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
        float distance = Vector3.Distance(transform.position, Target.position);//求距离
        if (distance < SkillDistance)
        {
            if (jiajiao <= SkillJiaodu * 0.5f)
            {
                Debug.Log("在扇形范围内");
            }
        }
        DrawSector(transform, transform.position, SkillJiaodu, SkillDistance);
    }

    /// <summary>
    /// 绘制扇形
    /// </summary>
    /// <param name="t">自己的位置</param>
    /// <param name="center">中心</param>
    /// <param name="angle">角度</param>
    /// <param name="radius">半径</param>
    public static void DrawSector(Transform t, Vector3 center, float angle, float radius)
    {
        LineRenderer lr = GetLineRenderer(t);
        int pointAmount = 50;//点的数目，值越大曲线越平滑  
        float eachAngle = angle / pointAmount;
        Vector3 forward = t.forward;
        lr.SetVertexCount(pointAmount);
        lr.SetPosition(0, center);
        for (int i = 1; i < pointAmount - 1; i++)
        {
            //Quaternion.Euler(x,y,z) 返回一个绕x轴旋转x度再绕y轴旋转y度再绕z轴旋转z度的Quaternion
            //Quaternion.Euler(0, 90, 0)返回一个绕y轴旋转90度的旋转操作。
            Vector3 pos = Quaternion.Euler(0f, -angle / 2 + eachAngle * (i - 1), 0f) * forward * radius + center;
            lr.SetPosition(i, pos);
        }
    }
}
