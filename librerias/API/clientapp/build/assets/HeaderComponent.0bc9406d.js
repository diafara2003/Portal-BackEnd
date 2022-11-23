import{k as w,l as R,a4 as v,s as _,m as r,D as S,a5 as p,r as $,u as B,_ as U,j as s,n as j,o as F,B as M,a6 as T,G as A,d as W,T as X}from"./index.ad9b7e92.js";function E(t){return String(t).match(/[\d.\-+]*\s*(.*)/)[1]||""}function N(t){return parseFloat(t)}function G(t){return w("MuiSkeleton",t)}R("MuiSkeleton",["root","text","rectangular","rounded","circular","pulse","wave","withChildren","fitContent","heightAuto"]);const H=["animation","className","component","height","style","variant","width"];let l=t=>t,g,m,f,b;const I=t=>{const{classes:e,variant:a,animation:n,hasChildren:o,width:d,height:i}=t;return F({root:["root",a,n,o&&"withChildren",o&&!d&&"fitContent",o&&!i&&"heightAuto"]},G,e)},K=v(g||(g=l`
  0% {
    opacity: 1;
  }

  50% {
    opacity: 0.4;
  }

  100% {
    opacity: 1;
  }
`)),L=v(m||(m=l`
  0% {
    transform: translateX(-100%);
  }

  50% {
    /* +0.5s of delay between each loop */
    transform: translateX(100%);
  }

  100% {
    transform: translateX(100%);
  }
`)),P=_("span",{name:"MuiSkeleton",slot:"Root",overridesResolver:(t,e)=>{const{ownerState:a}=t;return[e.root,e[a.variant],a.animation!==!1&&e[a.animation],a.hasChildren&&e.withChildren,a.hasChildren&&!a.width&&e.fitContent,a.hasChildren&&!a.height&&e.heightAuto]}})(({theme:t,ownerState:e})=>{const a=E(t.shape.borderRadius)||"px",n=N(t.shape.borderRadius);return r({display:"block",backgroundColor:t.vars?t.vars.palette.Skeleton.bg:S(t.palette.text.primary,t.palette.mode==="light"?.11:.13),height:"1.2em"},e.variant==="text"&&{marginTop:0,marginBottom:0,height:"auto",transformOrigin:"0 55%",transform:"scale(1, 0.60)",borderRadius:`${n}${a}/${Math.round(n/.6*10)/10}${a}`,"&:empty:before":{content:'"\\00a0"'}},e.variant==="circular"&&{borderRadius:"50%"},e.variant==="rounded"&&{borderRadius:(t.vars||t).shape.borderRadius},e.hasChildren&&{"& > *":{visibility:"hidden"}},e.hasChildren&&!e.width&&{maxWidth:"fit-content"},e.hasChildren&&!e.height&&{height:"auto"})},({ownerState:t})=>t.animation==="pulse"&&p(f||(f=l`
      animation: ${0} 1.5s ease-in-out 0.5s infinite;
    `),K),({ownerState:t,theme:e})=>t.animation==="wave"&&p(b||(b=l`
      position: relative;
      overflow: hidden;

      /* Fix bug in Safari https://bugs.webkit.org/show_bug.cgi?id=68196 */
      -webkit-mask-image: -webkit-radial-gradient(white, black);

      &::after {
        animation: ${0} 1.6s linear 0.5s infinite;
        background: linear-gradient(
          90deg,
          transparent,
          ${0},
          transparent
        );
        content: '';
        position: absolute;
        transform: translateX(-100%); /* Avoid flash during server-side hydration */
        bottom: 0;
        left: 0;
        right: 0;
        top: 0;
      }
    `),L,(e.vars||e).palette.action.hover)),z=$.exports.forwardRef(function(e,a){const n=B({props:e,name:"MuiSkeleton"}),{animation:o="pulse",className:d,component:i="span",height:h,style:C,variant:k="text",width:y}=n,c=U(n,H),u=r({},n,{animation:o,component:i,variant:k,hasChildren:Boolean(c.children)}),x=I(u);return s(P,r({as:i,ref:a,className:j(x.root,d),ownerState:u},c,{style:r({width:y,height:h},C)}))}),O=z,V=({title:t="",marginLeft:e=0})=>s(M,{position:"sticky",sx:{top:-4,zIndex:9,backgroundColor:"#FBFBFB",color:"#1E1E1E"},children:s(T,{children:s(A,{container:!0,direction:"row",justifyContent:"space-between",alignItems:"center",children:W(X,{sx:{color:"#283340",fontWeight:"600"},variant:"body1",noWrap:!0,component:"div",children:[" ",t," "]})})})});export{V as H,O as S};
