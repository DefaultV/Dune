// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "X Ray" {
   Properties {
   		_DiffuseColor ("Diffuse Color", Color) = (1,0,0,1)
   		_SpecularColor ("Specular Color", Color) = (1,1,1,1)
   		_Shininess ("Shininess", range(1,20)) = 1
   		_CutHeight ("Cut Height", range(-1,1)) = 1
   }
   SubShader {
      Tags {"Queue" = "Transparent"}   
   
      Pass {	
      	 Tags { "LightMode" = "ForwardBase" }
      	
      	 blend SrcAlpha OneMinusSrcAlpha   
      	 Cull front
      	    
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
 		 #include "UnityCG.cginc"
 
 		uniform float4 _DiffuseColor;
 		uniform float4 _SpecularColor;
 		uniform float _Shininess;
 		uniform float _CutHeight;
 			
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };
         struct vertexOutput {
			float4 pos : SV_POSITION;
			float4 normal : TEXCOORD0;
			float4 objPos: TEXCOORD1;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            
            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 viewMatrix = UNITY_MATRIX_V; 
            float4x4 projectionMatrix = UNITY_MATRIX_P;
            
            //Find the world position
            output.objPos = input.vertex;
            
            //Find the normals in world coordinates
            float4x4 modelMatrixInverse = unity_WorldToObject;
            output.normal = float4(normalize(mul(float4(input.normal,0),modelMatrixInverse).xyz),0);
            
            output.pos = mul(projectionMatrix,mul(viewMatrix,mul(modelMatrix,input.vertex)));
            
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
         	float4 lightPosition = float4(unity_4LightPosX0[0], 
                  unity_4LightPosY0[0], 
                  unity_4LightPosZ0[0], 1.0);
                  
                  
            float4 worldpos = mul(unity_ObjectToWorld,input.objPos);
                  
            float3 viewPosition = float4(_WorldSpaceCameraPos,1);
         
             //Calculate the light direction
            float3 lightDirection = normalize((lightPosition - worldpos).xyz);
            
            //Calculate the diffuse reflection intensity
            float diffuseReflectionIntensity = max(0.0, dot(-input.normal,lightDirection)) * 1/pow(0.5f*distance(worldpos,lightPosition),2);
            
            float3 reflectionVector = reflect(-lightDirection,-input.normal);
            
            float3 viewDirection = normalize((viewPosition - worldpos).xyz);
            
            //Calculate the specular reflection intensity
            float specularReflectionIntensity =  pow(max(0.0, dot(reflectionVector,viewDirection)),_Shininess);
            

            float4 output = diffuseReflectionIntensity * _DiffuseColor + specularReflectionIntensity * _SpecularColor;
            
            output.w = _DiffuseColor.w;
            if (input.objPos.y > _CutHeight)
            	output.w = 0.1;
            
            return output;
         }
         ENDCG
      }
      
      Pass {	
      	Tags { "LightMode" = "ForwardBase" }
      	 

      	 blend SrcAlpha OneMinusSrcAlpha   
      	 Cull back
      	    
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
 		 #include "UnityCG.cginc"
 
 		uniform float4 _DiffuseColor;
 		uniform float4 _SpecularColor;
 		uniform float _Shininess;
 		uniform float _CutHeight;
 			
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };
         struct vertexOutput {
			float4 pos : SV_POSITION;
			float4 normal : TEXCOORD0;
			float4 objPos: TEXCOORD1;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            
            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 viewMatrix = UNITY_MATRIX_V; 
            float4x4 projectionMatrix = UNITY_MATRIX_P;
            
            //Find the world position
            output.objPos = input.vertex;
            
            //Find the normals in world coordinates
            float4x4 modelMatrixInverse = unity_WorldToObject;
            output.normal = float4(normalize(mul(float4(input.normal,0),modelMatrixInverse).xyz),0);
            
            output.pos = mul(projectionMatrix,mul(viewMatrix,mul(modelMatrix,input.vertex)));
            
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
         	float4 lightPosition = float4(unity_4LightPosX0[0], 
                  unity_4LightPosY0[0], 
                  unity_4LightPosZ0[0], 1.0);
                  
                  
            float4 worldpos = mul(unity_ObjectToWorld,input.objPos);
                  
            float3 viewPosition = float4(_WorldSpaceCameraPos,1);
         
             //Calculate the light direction
            float3 lightDirection = normalize((lightPosition - worldpos).xyz);
            
            //Calculate the diffuse reflection intensity
            float diffuseReflectionIntensity = max(0.0, dot(input.normal,lightDirection)) * 1/pow(0.5f*distance(worldpos,lightPosition),2);
            
            float3 reflectionVector = reflect(-lightDirection,input.normal);
            
            float3 viewDirection = normalize((viewPosition - worldpos).xyz);
            
            //Calculate the specular reflection intensity
            float specularReflectionIntensity =  pow(max(0.0, dot(reflectionVector,viewDirection)),_Shininess);
            

            float4 output = diffuseReflectionIntensity * _DiffuseColor + specularReflectionIntensity * _SpecularColor;
            
            output.w = _DiffuseColor.w;
            if (input.objPos.y > _CutHeight)
            	output.w = 0.1;
            
            return output;
         }
         ENDCG
      }
   }
}
