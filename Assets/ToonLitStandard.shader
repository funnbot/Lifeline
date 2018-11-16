// Standard Unity Toon Shader
// Modified by OhiraKyou to implement custom highlight/shadow colours
// Modified by Kurisu (aka Google & StackOverflow) to better suit my personal game making needs
// Fixed by MinionsArt because she's awesome and I appreciate her efforts

Shader "Toon/Lit Standard" {
    Properties {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
        _HighlightColor("Highlight Color", Color) = (0.65, 0.65, 0.65, 1.0)
        _ShadowColor("Shadow Color", Color) = (0.5, 0.5, 0.5, 1.0)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
        [Toggle(ALPHA)] _ALPHA("Alpha?", Float) = 0
    }

    SubShader {
        Tags {
            "Queue" = "AlphaTest"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        LOD 200
        CGPROGRAM
        // addshadow for transparency accurate shadows
        // alphatest for better alpha
        #pragma surface surf ToonRamp alphatest:_ALPHA addshadow

        sampler2D _Ramp;
        float4 _HighlightColor;
        float4 _ShadowColor;

        // custom lighting function that uses a texture ramp based
        // on angle between light direction and normal
        #pragma lighting ToonRamp exclude_path:prepass

        inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten) {
            #ifndef USING_DIRECTIONAL_LIGHT
                lightDir = normalize(lightDir);
            #endif

            // First, we need to determine how lit or shadowed a part of the object is.
            // We do this by checking how closely the surface normal matches the light direction

            // Dot product takes in two directions (vectors) and returns a value between -1 and 1.
            // 1 means two vectors are pointing in the same direction (-> ->)
            // -1 means the two vectors are facing in opposite direction (<- ->)
            // 0 means that the two vectors are perpendicular to each other (L-shaped).

            // By multiplying by 2 and adding 0.5, we get a value between 0 and 1 instead of -1 and 1
            // This value can be considered a percentage of how closely the surface normal matches the light direction
            half lightAlignmentPercent = dot (s.Normal, lightDir) * 0.5 + 0.5;

            // Now, intead of using this value raw, we can use it as a lookup in a texture (ramp)
            // This gets the ramp color associated with the light dot product (usually between black and white)
            float3 rampValue = tex2D(_Ramp, fixed2(lightAlignmentPercent, lightAlignmentPercent)).rgb;

            // Apply shadows (attenuation)
            rampValue *= atten;

            // This next part is optional. I like to be able to control how intense a shadow is by its alpha value
            // This applies the shadow's alpha value as intensity, showing more of the highlight color at lower values
            // lerp is short for linear interpolation, or simply going from one value to another based on a third value
            _ShadowColor = lerp(_HighlightColor, _ShadowColor, _ShadowColor.a);

            // Next, we interpolate to a color somewhere between the shadow color and the highlight color, according to the ramp's 0 to 1 value
            float3 finalLightColor = lerp(_ShadowColor.rgb, _HighlightColor.rgb, rampValue);

            // Finally, we apply the final light color to the surface albedo (tinted main texture)
            half4 finalColor;
            finalColor.rgb = s.Albedo * _LightColor0.rgb * finalLightColor;
            finalColor.a = s.Alpha;

            return finalColor;
        }

        sampler2D _MainTex;
        float4 _Color;

        struct Input {
            float2 uv_MainTex : TEXCOORD0;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            // This simply calculates the surface color as the main texture multiplied by a tint
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        ENDCG
    }

    Fallback "Diffuse"
}