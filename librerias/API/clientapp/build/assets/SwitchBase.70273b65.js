import{k as $,l as W,s as x,z as A,m as d,r as D,_ as G,al as H,p as J,d as K,j as M,n as Q,t as T,o as V}from"./index.ad9b7e92.js";function X(e){return $("PrivateSwitchBase",e)}W("PrivateSwitchBase",["root","checked","disabled","input","edgeStart","edgeEnd"]);const Y=["autoFocus","checked","checkedIcon","className","defaultChecked","disabled","disableFocusRipple","edge","icon","id","inputProps","inputRef","name","onBlur","onChange","onFocus","readOnly","required","tabIndex","type","value"],Z=e=>{const{classes:a,checked:i,disabled:l,edge:o}=e,r={root:["root",i&&"checked",l&&"disabled",o&&`edge${T(o)}`],input:["input"]};return V(r,X,a)},ee=x(A)(({ownerState:e})=>d({padding:9,borderRadius:"50%"},e.edge==="start"&&{marginLeft:e.size==="small"?-3:-12},e.edge==="end"&&{marginRight:e.size==="small"?-3:-12})),se=x("input")({cursor:"inherit",position:"absolute",opacity:0,width:"100%",height:"100%",top:0,left:0,margin:0,padding:0,zIndex:1}),te=D.exports.forwardRef(function(a,i){const{autoFocus:l,checked:o,checkedIcon:r,className:F,defaultChecked:h,disabled:y,disableFocusRipple:p=!1,edge:w=!1,icon:S,id:R,inputProps:I,inputRef:P,name:z,onBlur:f,onChange:g,onFocus:b,readOnly:j,required:N,tabIndex:U,type:c,value:m}=a,_=G(a,Y),[k,q]=H({controlled:o,default:Boolean(h),name:"SwitchBase",state:"checked"}),t=J(),v=s=>{b&&b(s),t&&t.onFocus&&t.onFocus(s)},L=s=>{f&&f(s),t&&t.onBlur&&t.onBlur(s)},O=s=>{if(s.nativeEvent.defaultPrevented)return;const C=s.target.checked;q(C),g&&g(s,C)};let n=y;t&&typeof n>"u"&&(n=t.disabled);const E=c==="checkbox"||c==="radio",u=d({},a,{checked:k,disabled:n,disableFocusRipple:p,edge:w}),B=Z(u);return K(ee,d({component:"span",className:Q(B.root,F),centerRipple:!0,focusRipple:!p,disabled:n,tabIndex:null,role:void 0,onFocus:v,onBlur:L,ownerState:u,ref:i},_,{children:[M(se,d({autoFocus:l,checked:o,defaultChecked:h,className:B.input,disabled:n,id:E&&R,name:z,onChange:O,readOnly:j,ref:P,required:N,ownerState:u,tabIndex:U,type:c},c==="checkbox"&&m===void 0?{}:{value:m},I)),k?r:S]}))}),oe=te;export{oe as S};