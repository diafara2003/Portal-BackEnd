import{aA as b,aB as P,aC as B,m as h,a8 as k,a9 as p,aa as T,ab as D,ac as R,r as d,a7 as N,_,j as A,n as F,o as M,k as O,ad as U}from"./index.ad9b7e92.js";const $=["component","direction","spacing","divider","children","className"],E=b(),L=P("div",{name:"MuiStack",slot:"Root",overridesResolver:(e,s)=>s.root});function G(e){return B({props:e,name:"MuiStack",defaultTheme:E})}function I(e,s){const n=d.exports.Children.toArray(e).filter(Boolean);return n.reduce((a,c,o)=>(a.push(c),o<n.length-1&&a.push(d.exports.cloneElement(s,{key:`separator-${o}`})),a),[])}const W=e=>({row:"Left","row-reverse":"Right",column:"Top","column-reverse":"Bottom"})[e],q=({ownerState:e,theme:s})=>{let n=h({display:"flex",flexDirection:"column"},k({theme:s},p({values:e.direction,breakpoints:s.breakpoints.values}),a=>({flexDirection:a})));if(e.spacing){const a=T(s),c=Object.keys(s.breakpoints.values).reduce((t,r)=>((typeof e.spacing=="object"&&e.spacing[r]!=null||typeof e.direction=="object"&&e.direction[r]!=null)&&(t[r]=!0),t),{}),o=p({values:e.direction,base:c}),m=p({values:e.spacing,base:c});typeof o=="object"&&Object.keys(o).forEach((t,r,i)=>{if(!o[t]){const u=r>0?o[i[r-1]]:"column";o[t]=u}}),n=D(n,k({theme:s},m,(t,r)=>({"& > :not(style) + :not(style)":{margin:0,[`margin${W(r?o[r]:e.direction)}`]:U(a,t)}})))}return n=R(s.breakpoints,n),n};function z(e={}){const{createStyledComponent:s=L,useThemeProps:n=G,componentName:a="MuiStack"}=e,c=()=>M({root:["root"]},t=>O(a,t),{}),o=s(q);return d.exports.forwardRef(function(t,r){const i=n(t),l=N(i),{component:u="div",direction:v="column",spacing:x=0,divider:y,children:g,className:S}=l,j=_(l,$),C={direction:v,spacing:x},V=c();return A(o,h({as:u,ownerState:C,ref:r,className:F(V.root,S)},j,{children:y?I(g,y):g}))})}const H=z(),K=H;export{K as S};