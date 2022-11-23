import{r as m,ai as ie,_ as oe,J as j,ay as H,j as e,m as w,aP as se,ax as le,aQ as ce,aR as q,g as de,i as ue,h as fe,a as z,b as $,B as y,Y as D,d as f,a1 as me,an as V,U as pe,X as he,T as C,G as S,aH as xe,a2 as O}from"./index.ad9b7e92.js";import{H as ge}from"./HeaderComponent.0bc9406d.js";import{S as ve}from"./SinInformacion.9e7bb934.js";import{S as Ce}from"./SkeletonDynamic.d264f9d2.js";import{I as be}from"./InputAdornment.67d4ad97.js";import{L as ye,a as Ee}from"./NavigationComponent.ea8de845.js";import{B as Se}from"./GestionProveedoresPage.49451b31.js";import{C as Te,a as ke,b as we}from"./CardHeader.67279b64.js";import{T as De}from"./Tooltip.76d5c635.js";import{C as _}from"./Checkbox.e07d5e11.js";import{S as X}from"./Stack.9bed6c39.js";import{C as Ie}from"./CardActions.8f5f35f6.js";import{T as Ne,a as M}from"./Tabs.f8373235.js";import{D as Le,a as Re,b as Fe,c as ze}from"./DialogTitle.c4648013.js";import{D as $e}from"./DialogContentText.514f3ba6.js";import"./Popper.d79c6681.js";import"./SwitchBase.70273b65.js";const Ae=["addEndListener","appear","children","container","direction","easing","in","onEnter","onEntered","onEntering","onExit","onExited","onExiting","style","timeout","TransitionComponent"];function Be(t,r,s){const n=r.getBoundingClientRect(),a=s&&s.getBoundingClientRect(),c=H(r);let l;if(r.fakeTransform)l=r.fakeTransform;else{const i=c.getComputedStyle(r);l=i.getPropertyValue("-webkit-transform")||i.getPropertyValue("transform")}let d=0,u=0;if(l&&l!=="none"&&typeof l=="string"){const i=l.split("(")[1].split(")")[0].split(",");d=parseInt(i[4],10),u=parseInt(i[5],10)}return t==="left"?a?`translateX(${a.right+d-n.left}px)`:`translateX(${c.innerWidth+d-n.left}px)`:t==="right"?a?`translateX(-${n.right-a.left-d}px)`:`translateX(-${n.left+n.width-d}px)`:t==="up"?a?`translateY(${a.bottom+u-n.top}px)`:`translateY(${c.innerHeight+u-n.top}px)`:a?`translateY(-${n.top-a.top+n.height-u}px)`:`translateY(-${n.top+n.height-u}px)`}function Pe(t){return typeof t=="function"?t():t}function I(t,r,s){const n=Pe(s),a=Be(t,r,n);a&&(r.style.webkitTransform=a,r.style.transform=a)}const We=m.exports.forwardRef(function(r,s){const n=ie(),a={enter:n.transitions.easing.easeOut,exit:n.transitions.easing.sharp},c={enter:n.transitions.duration.enteringScreen,exit:n.transitions.duration.leavingScreen},{addEndListener:l,appear:d=!0,children:u,container:i,direction:p="down",easing:E=a,in:g,onEnter:N,onEntered:B,onEntering:v,onExit:h,onExited:T,onExiting:L,style:R,timeout:F=c,TransitionComponent:G=se}=r,J=oe(r,Ae),b=m.exports.useRef(null),Q=j(u.ref,b),U=j(Q,s),k=o=>x=>{o&&(x===void 0?o(b.current):o(b.current,x))},K=k((o,x)=>{I(p,o,i),ce(o),N&&N(o,x)}),Z=k((o,x)=>{const W=q({timeout:F,style:R,easing:E},{mode:"enter"});o.style.webkitTransition=n.transitions.create("-webkit-transform",w({},W)),o.style.transition=n.transitions.create("transform",w({},W)),o.style.webkitTransform="none",o.style.transform="none",v&&v(o,x)}),ee=k(B),te=k(L),ne=k(o=>{const x=q({timeout:F,style:R,easing:E},{mode:"exit"});o.style.webkitTransition=n.transitions.create("-webkit-transform",x),o.style.transition=n.transitions.create("transform",x),I(p,o,i),h&&h(o)}),re=k(o=>{o.style.webkitTransition="",o.style.transition="",T&&T(o)}),ae=o=>{l&&l(b.current,o)},P=m.exports.useCallback(()=>{b.current&&I(p,b.current,i)},[p,i]);return m.exports.useEffect(()=>{if(g||p==="down"||p==="right")return;const o=le(()=>{b.current&&I(p,b.current,i)}),x=H(b.current);return x.addEventListener("resize",o),()=>{o.clear(),x.removeEventListener("resize",o)}},[p,g,i]),m.exports.useEffect(()=>{g||P()},[g,P]),e(G,w({nodeRef:b,onEnter:K,onEntered:ee,onEntering:Z,onExit:ne,onExited:re,onExiting:te,addEndListener:ae,appear:d,in:g,timeout:F},J,{children:(o,x)=>m.exports.cloneElement(u,w({ref:U,style:w({visibility:o==="exited"&&!g?"hidden":void 0},R,u.props.style)},x))}))}),je=We;var A={},qe=ue.exports;Object.defineProperty(A,"__esModule",{value:!0});var Y=A.default=void 0,Oe=qe(de()),_e=fe,Me=(0,Oe.default)((0,_e.jsx)("path",{d:"M15.5 14h-.79l-.28-.27C15.41 12.59 16 11.11 16 9.5 16 5.91 13.09 3 9.5 3S3 5.91 3 9.5 5.91 16 9.5 16c1.61 0 3.09-.59 4.23-1.57l.27.28v.79l5 4.99L20.49 19l-4.99-5zm-6 0C7.01 14 5 11.99 5 9.5S7.01 5 9.5 5 14 7.01 14 9.5 11.99 14 9.5 14z"}),"SearchOutlined");Y=A.default=Me;const He=()=>{const[t,r]=m.exports.useState(),[s,n]=m.exports.useState(!0),a=async()=>{const c={AllowAnonymous:!1,metodo:"Novedad/ConstructoraNovedad",type:z.GET},l=await $(c);r(l),n(!1)};return m.exports.useEffect(()=>{a()},[]),{dataConst:t!=null?t:[],isLoading:s}},Ve=({onClick:t})=>{const[r,s]=m.exports.useState(0),{dataConst:n,isLoading:a}=He(),c=(l,d)=>{s(d),t(n[d])};return e(y,{ml:1,children:a?e(D,{children:e(Ce,{NoColumnas:1,NoFilas:4,Tipo:"formulario"})}):f(D,{children:[e(me,{sx:{marginBottom:"1rem"},id:"filled-basic",label:"Buscar",variant:"outlined",size:"small",fullWidth:!0,InputProps:{endAdornment:e(be,{position:"end",children:e(Y,{})})}}),e(V,{}),e(pe,{component:"nav","aria-label":"main mailbox folders",children:n.length!=0?n.map(({contNotificaciones:l,nombreConst:d,constructoraId:u},i)=>f(ye,{selected:r===i,onClick:p=>c(p,i),children:[e(he,{sx:{ml:2},children:e(Se,{badgeContent:Number(l),color:"info"})},`Const_${u}`),e(Ee,{primary:d})]},`constructiraid${u}`)):null})]})})},Xe=({detalle:t})=>{const r=new Intl.ListFormat("es");let s=[];return(()=>{t==null||t.map(({tipo:a,nombre:c})=>{a=="documento"&&s.push(c.split("(")[0])})})(),f(y,{justifyContent:"flex-start",display:"flex",children:[e(C,{mb:.5,variant:"body1",color:"primary",fontSize:"14px",fontWeight:500,children:"Documentos faltantes: "}),e(C,{variant:"subtitle2",children:r.format(s)})]})},Ye=({detalle:t})=>{const r=new Intl.ListFormat("es");let s=[];return(()=>{t==null||t.map(({tipo:a,nombre:c})=>{a=="formulario"&&s.push(c.split("(")[0])})})(),f(y,{justifyContent:"flex-start",display:"flex",children:[e(C,{mb:.5,variant:"body1",color:"primary",fontSize:"14px",fontWeight:500,children:"Formularios faltantes: "}),e(C,{variant:"subtitle2",children:r.format(s)})]})},Ge=({novedad:t,numNovedad:r,clickFinaliza:s})=>{var d,u;const n=(d=t.detalle)==null?void 0:d.find(i=>i.tipo=="documento"),a=(u=t.detalle)==null?void 0:u.find(i=>i.tipo=="formulario"),c=t.isActiva?"rgba(30, 98, 161, 0.04);":"#e8e8e8",l=i=>{s(i)};return t.ischecked==null&&(t.ischecked=!1),f(Te,{variant:"outlined",sx:{backgroundColor:c},children:[e(ke,{title:f(C,{fontWeight:600,color:"primary",children:["Novedad N\xB0 ",r]}),action:f(C,{variant:"subtitle2",color:"primary",mt:1,mr:1,children:[new Date(t.fecha).toLocaleDateString()," "]}),sx:{margin:"2px 6px !important",padding:"2px 6px !important"}}),e(V,{orientation:"horizontal",flexItem:!0}),e(we,{children:f(S,{container:!0,children:[e(S,{display:"flex",justifyContent:"center",alignItems:"center",item:!0,xs:1,children:e(y,{display:"flex",justifyContent:"center",alignItems:"center",children:e(De,{children:t.isActiva?e(_,{checked:t.ischecked,color:"success",onClick:()=>l(t.numero)}):e(_,{color:"success",disabled:!0,defaultChecked:!0}),title:"Finalizar novedad"})})}),f(S,{item:!0,xs:11,children:[f(y,{children:[e(C,{mb:.5,variant:"subtitle2",fontSize:"14px",fontWeight:600,children:"Observaciones"}),e(C,{variant:"body1",fontWeight:400,children:t.comentario})]}),f(X,{mt:2,children:[e(C,{mb:.5,variant:"subtitle2",fontSize:"14px",fontWeight:600,children:"Motivos del rechazo"}),f(S,{container:!0,mt:1,children:[e(y,{width:"100%",children:a!=null?e(D,{children:e(Ye,{detalle:t.detalle})}):null}),e(y,{width:"100%",children:n!=null?e(D,{children:e(Xe,{detalle:t.detalle})}):null})]})]})]})]})}),e(Ie,{})]})},Je=()=>{const[t,r]=m.exports.useState([]),[s,n]=m.exports.useState(!1),[a,c]=m.exports.useState(0),[l,d]=m.exports.useState(0),[u,i]=m.exports.useState([]),p=(v,h)=>{c(h)},E=async v=>{const h={metodo:`Novedad/constructora?constructora=${v.constructoraId}`,type:z.GET},T=await $(h);T!=null&&(i(T),r(T.filter(L=>L.isActiva==Boolean(a-1))))},g=async()=>{const v={metodo:"Novedad/cambiarestado",type:z.POST,data:{codigo:l}};await $(v),r(()=>t.map(h=>(h.numero==l&&(h.isActiva=!1),h)))};return m.exports.useEffect(()=>{r(u.filter(v=>v.isActiva==Boolean(a-1)))},[a]),{consultarNovedades:E,dataNovedades:t,handleClose:v=>{n(!1),v?g():r(()=>t.map(h=>(h.numero==l&&(h.ischecked=!1),h)))},openDialog:s,openModal:()=>{n(!0)},setIdOpen:d,setDataNovedades:r,handleChange:p,valueTab:a}},Qe=xe.forwardRef(function(r,s){return e(je,{direction:"up",ref:s,...r})}),pt=()=>{const{dataNovedades:t,consultarNovedades:r,handleClose:s,openDialog:n,openModal:a,setIdOpen:c,setDataNovedades:l,handleChange:d,valueTab:u}=Je();return f(D,{children:[e(ge,{title:"Novedades"}),f(S,{container:!0,spacing:4,p:2,children:[e(S,{mt:2,item:!0,xs:2,md:3,children:e(Ve,{onClick:i=>{r(i)}})}),f(S,{item:!0,xs:8,md:9,children:[f(Ne,{value:u,onChange:d,"aria-label":"basic tabs example",sx:{borderBottom:1,borderColor:"divider"},children:[e(M,{label:"Pendientes por resolver"},"tabPendiente"),e(M,{label:"Resueltas"},"tabResueltas")]}),t.length!=0?e(y,{mt:2,style:{overflow:"auto",maxHeight:"calc(100vh - 16rem)"},children:e(X,{m:2,spacing:2,children:t.map((i,p)=>(i.ischecked==null&&(i.ischecked=!1),e(Ge,{novedad:i,numNovedad:p+1,clickFinaliza:E=>{l(t.map(g=>(g.numero==E&&(g.ischecked=!0),g))),c(E),a()}},`cardNovidad${i.numero}`)))})}):e(y,{mt:2,justifyContent:"center",display:"flex",children:e(ve,{})})]})]}),f(Le,{open:n,TransitionComponent:Qe,keepMounted:!0,onClose:s,"aria-describedby":"alert-dialog-slide-description",children:[e(Re,{fontWeight:600,color:"inherit",children:"Notificar a la constructora"}),e(Fe,{children:f($e,{id:"alert-dialog-slide-description",children:[e(C,{fontWeight:600,children:"\xBFEsta seguro que desa cerrar la novedad?"}),e(C,{mt:1,children:"Al cerrarla se le notificar\xE1 a la constructora para continuar con la validaci\xF3n"})]})}),f(ze,{children:[e(O,{variant:"text",color:"error",onClick:()=>{s(!1)},children:"Cancelar"}),e(O,{variant:"outlined",color:"primary",onClick:()=>s(!0),children:"Aceptar"})]})]})]})};export{pt as NovedadesPage,pt as default};