import{l as w,k as y,s as L,t as R,m as l,r as g,u as v,_ as $,p as G,aw as M,T as h,j as N,d as _,n as T,o as S}from"./index.ad9b7e92.js";function j(e){return y("MuiFormControlLabel",e)}const W=w("MuiFormControlLabel",["root","labelPlacementStart","labelPlacementTop","labelPlacementBottom","disabled","label","error"]),d=W,E=["checked","className","componentsProps","control","disabled","disableTypography","inputRef","label","labelPlacement","name","onChange","value"],k=e=>{const{classes:o,disabled:t,labelPlacement:r,error:s}=e,c={root:["root",t&&"disabled",`labelPlacement${R(r)}`,s&&"error"],label:["label",t&&"disabled"]};return S(c,j,o)},z=L("label",{name:"MuiFormControlLabel",slot:"Root",overridesResolver:(e,o)=>{const{ownerState:t}=e;return[{[`& .${d.label}`]:o.label},o.root,o[`labelPlacement${R(t.labelPlacement)}`]]}})(({theme:e,ownerState:o})=>l({display:"inline-flex",alignItems:"center",cursor:"pointer",verticalAlign:"middle",WebkitTapHighlightColor:"transparent",marginLeft:-11,marginRight:16,[`&.${d.disabled}`]:{cursor:"default"}},o.labelPlacement==="start"&&{flexDirection:"row-reverse",marginLeft:16,marginRight:-11},o.labelPlacement==="top"&&{flexDirection:"column-reverse",marginLeft:16},o.labelPlacement==="bottom"&&{flexDirection:"column",marginLeft:16},{[`& .${d.label}`]:{[`&.${d.disabled}`]:{color:(e.vars||e).palette.text.disabled}}})),A=g.exports.forwardRef(function(o,t){const r=v({props:o,name:"MuiFormControlLabel"}),{className:s,componentsProps:c={},control:a,disabled:u,disableTypography:b,label:m,labelPlacement:f="end"}=r,U=$(r,E),C=G();let n=u;typeof n>"u"&&typeof a.props.disabled<"u"&&(n=a.props.disabled),typeof n>"u"&&C&&(n=C.disabled);const F={disabled:n};["checked","name","onChange","value","inputRef"].forEach(p=>{typeof a.props[p]>"u"&&typeof r[p]<"u"&&(F[p]=r[p])});const D=M({props:r,muiFormControl:C,states:["error"]}),x=l({},r,{disabled:n,labelPlacement:f,error:D.error}),P=k(x);let i=m;return i!=null&&i.type!==h&&!b&&(i=N(h,l({component:"span",className:P.label},c.typography,{children:i}))),_(z,l({className:T(P.root,s),ownerState:x,ref:t},U,{children:[g.exports.cloneElement(a,F),i]}))}),O=A;function B(e){return y("MuiFormGroup",e)}w("MuiFormGroup",["root","row","error"]);const H=["className","row"],I=e=>{const{classes:o,row:t,error:r}=e;return S({root:["root",t&&"row",r&&"error"]},B,o)},q=L("div",{name:"MuiFormGroup",slot:"Root",overridesResolver:(e,o)=>{const{ownerState:t}=e;return[o.root,t.row&&o.row]}})(({ownerState:e})=>l({display:"flex",flexDirection:"column",flexWrap:"wrap"},e.row&&{flexDirection:"row"})),J=g.exports.forwardRef(function(o,t){const r=v({props:o,name:"MuiFormGroup"}),{className:s,row:c=!1}=r,a=$(r,H),u=G(),b=M({props:r,muiFormControl:u,states:["error"]}),m=l({},r,{row:c,error:b.error}),f=I(m);return N(q,l({className:T(f.root,s),ownerState:m,ref:t},a))}),Q=J;export{Q as F,O as a};