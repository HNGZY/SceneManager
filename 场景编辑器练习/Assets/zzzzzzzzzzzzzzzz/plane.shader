Shader "Test/plane"
{
    Properties
    {
        [Normal]_MainTex("我是2D纹理", 2D) = "gray" {}

        [NoScaleOffset]_MainTex3d("我是3D纹理", 3d) = "" {}

        _color ("Base(RGB)",Color) = (1,1,1,1)


        _IntTest("int(test)",Int) = 1
        _Float("我是Float", Float) = 0.5
        _Float1("我是Float1", Range( 0 , 10)) = 0.5
        [Toggle]_Float2("我是Float2", Range( 0 , 1)) = 1
        [Enum(UnityEngine.Rendering.CullMode)]_Float3("我是Float3", Float) = 1
        _Vector("我是Vector", Vector) = (0,0,0,0)


        
    }
    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
			#pragma fragment frag
        
            float4 vert(float4 vertex : POSITION):SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }

            fixed4 _color;
            float4 frag():SV_TARGET
            {
                return _color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
    CustomEditor "EditorName"
}
