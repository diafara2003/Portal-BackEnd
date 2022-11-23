import{H as ye}from"./HeaderComponent.0bc9406d.js";import{k as ve,l as Te,s as U,m as f,r as d,u as be,aS as we,aP as Se,_ as $e,ai as ze,J as ke,j as e,n as Re,o as De,aR as Z,c as A,b as ee,a as M,a0 as re,d as h,Y as P,B as m,T as I,U as Ie,aT as Le,an as te,aU as L,G as w,f as Ae,am as He,a1 as ie,a2 as j}from"./index.ad9b7e92.js";import{b as Fe,L as Ne}from"./NavigationComponent.ea8de845.js";import{C as Be}from"./Checkbox.e07d5e11.js";import{A as se}from"./Add.73b42c3e.js";import{S as Oe}from"./SkeletonDynamic.d264f9d2.js";import{T as Me,a as Pe,b as We,c as oe,d as C,e as _e}from"./TableRow.15b8d78b.js";import{D as Ge}from"./DeleteOutline.b357c04c.js";import{d as ne}from"./Search.67c90013.js";import{d as je}from"./History.e8cf50f5.js";import{I as ae}from"./InputAdornment.67d4ad97.js";import"./SwitchBase.70273b65.js";function Ue(i){return ve("MuiCollapse",i)}Te("MuiCollapse",["root","horizontal","vertical","entered","hidden","wrapper","wrapperInner"]);const qe=["addEndListener","children","className","collapsedSize","component","easing","in","onEnter","onEntered","onEntering","onExit","onExited","onExiting","orientation","style","timeout","TransitionComponent"],Ke=i=>{const{orientation:t,classes:o}=i,n={root:["root",`${t}`],entered:["entered"],hidden:["hidden"],wrapper:["wrapper",`${t}`],wrapperInner:["wrapperInner",`${t}`]};return De(n,Ue,o)},Je=U("div",{name:"MuiCollapse",slot:"Root",overridesResolver:(i,t)=>{const{ownerState:o}=i;return[t.root,t[o.orientation],o.state==="entered"&&t.entered,o.state==="exited"&&!o.in&&o.collapsedSize==="0px"&&t.hidden]}})(({theme:i,ownerState:t})=>f({height:0,overflow:"hidden",transition:i.transitions.create("height")},t.orientation==="horizontal"&&{height:"auto",width:0,transition:i.transitions.create("width")},t.state==="entered"&&f({height:"auto",overflow:"visible"},t.orientation==="horizontal"&&{width:"auto"}),t.state==="exited"&&!t.in&&t.collapsedSize==="0px"&&{visibility:"hidden"})),Ye=U("div",{name:"MuiCollapse",slot:"Wrapper",overridesResolver:(i,t)=>t.wrapper})(({ownerState:i})=>f({display:"flex",width:"100%"},i.orientation==="horizontal"&&{width:"auto",height:"100%"})),Qe=U("div",{name:"MuiCollapse",slot:"WrapperInner",overridesResolver:(i,t)=>t.wrapperInner})(({ownerState:i})=>f({width:"100%"},i.orientation==="horizontal"&&{width:"auto",height:"100%"})),le=d.exports.forwardRef(function(t,o){const n=be({props:t,name:"MuiCollapse"}),{addEndListener:a,children:r,className:c,collapsedSize:p="0px",component:s,easing:u,in:y,onEnter:g,onEntered:v,onEntering:$,onExit:K,onExited:ce,onExiting:J,orientation:Y="vertical",style:W,timeout:T=we.standard,TransitionComponent:pe=Se}=n,ue=$e(n,qe),F=f({},n,{orientation:Y,collapsedSize:p}),z=Ke(F),Q=ze(),V=d.exports.useRef(),b=d.exports.useRef(null),_=d.exports.useRef(),N=typeof p=="number"?`${p}px`:p,k=Y==="horizontal",R=k?"width":"height";d.exports.useEffect(()=>()=>{clearTimeout(V.current)},[]);const B=d.exports.useRef(null),he=ke(o,B),S=l=>x=>{if(l){const E=B.current;x===void 0?l(E):l(E,x)}},G=()=>b.current?b.current[k?"clientWidth":"clientHeight"]:0,xe=S((l,x)=>{b.current&&k&&(b.current.style.position="absolute"),l.style[R]=N,g&&g(l,x)}),fe=S((l,x)=>{const E=G();b.current&&k&&(b.current.style.position="");const{duration:D,easing:O}=Z({style:W,timeout:T,easing:u},{mode:"enter"});if(T==="auto"){const X=Q.transitions.getAutoHeightDuration(E);l.style.transitionDuration=`${X}ms`,_.current=X}else l.style.transitionDuration=typeof D=="string"?D:`${D}ms`;l.style[R]=`${E}px`,l.style.transitionTimingFunction=O,$&&$(l,x)}),ge=S((l,x)=>{l.style[R]="auto",v&&v(l,x)}),me=S(l=>{l.style[R]=`${G()}px`,K&&K(l)}),Ee=S(ce),Ce=S(l=>{const x=G(),{duration:E,easing:D}=Z({style:W,timeout:T,easing:u},{mode:"exit"});if(T==="auto"){const O=Q.transitions.getAutoHeightDuration(x);l.style.transitionDuration=`${O}ms`,_.current=O}else l.style.transitionDuration=typeof E=="string"?E:`${E}ms`;l.style[R]=N,l.style.transitionTimingFunction=D,J&&J(l)});return e(pe,f({in:y,onEnter:xe,onEntered:ge,onEntering:fe,onExit:me,onExited:Ee,onExiting:Ce,addEndListener:l=>{T==="auto"&&(V.current=setTimeout(l,_.current||0)),a&&a(B.current,l)},nodeRef:B,timeout:T==="auto"?null:T},ue,{children:(l,x)=>e(Je,f({as:s,className:Re(z.root,c,{entered:z.entered,exited:!y&&N==="0px"&&z.hidden}[l]),style:f({[k?"minWidth":"minHeight"]:N},W),ownerState:f({},F,{state:l}),ref:he},x,{children:e(Ye,{ownerState:f({},F,{state:l}),className:z.wrapper,ref:b,children:e(Qe,{ownerState:f({},F,{state:l}),className:z.wrapperInner,children:r})})}))}))});le.muiSupportAuto=!0;const q=le,Ve=A(e("path",{d:"M10 6 8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6-6-6z"}),"ChevronRightOutlined"),Xe=A(e("path",{d:"M16.59 8.59 12 13.17 7.41 8.59 6 10l6 6 6-6-1.41-1.41z"}),"ExpandMoreOutlined"),Ze=A(e("path",{d:"M21 11H6.83l3.58-3.59L9 6l-6 6 6 6 1.41-1.41L6.83 13H21v-2z"}),"KeyboardBackspaceOutlined"),et=A(e("path",{d:"M7 11v2h10v-2H7zm5-9C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z"}),"RemoveCircleOutlineOutlined"),tt=A(e("path",{d:"M19 13H5v-2h14v2z"}),"RemoveOutlined"),it={especialidades:[]},ot=(i,t)=>{switch(t.type){case"add all":return{especialidades:[...t.payload]};case"add":return{especialidades:[...i.especialidades,t.payload]};case"remove":return{especialidades:[...i.especialidades.filter(o=>o.id!==t.payload)]}}},H=d.exports.createContext({state:[],dispatch:()=>{},addEspecialidad:i=>{},deleteEspecialidad(i){}}),nt=({children:i})=>{const[t,o]=d.exports.useReducer(ot,it),n=r=>{ee({metodo:"Especialidades/tercero/guardar",type:M.POST,data:{id:r.id}}).then(c=>{o({type:"add",payload:r})})},a=r=>{ee({metodo:`Especialidades/tercero/${r}`,type:M.DELETE}).then(c=>{o({type:"remove",payload:r})})};return e(H.Provider,{value:{state:t.especialidades,dispatch:o,addEspecialidad:n,deleteEspecialidad:a},children:i})},at=i=>{const{data:t,isLoading:o,doFetch:n}=re();return d.exports.useEffect(()=>{n({metodo:"Especialidad/todas",type:M.GET,AllowAnonymous:!1})},[]),{especialidades:t!=null?t:[],isLoading:o}},rt=()=>{const{state:i,deleteEspecialidad:t}=d.exports.useContext(H);return{state:i,deleteEspecialidad:t}},st=()=>{const{deleteEspecialidad:i,state:t}=rt();return h(P,{children:[e(m,{children:e(I,{variant:"h6",children:"Especialidades agregadas"})}),e(m,{justifyContent:"start",display:"flex",children:e(Ie,{sx:{width:"100%",height:"calc(100vh - 331px)",overflow:"auto"},children:e(Le,{children:t.map(o=>{const n=`${o.grupoTexto} / ${o.categoriaTexto} / ${o.nombre}`;return h(q,{children:[e(Fe,{sx:{p:0},children:h(Ne,{sx:{p:.5},children:[h(m,{display:"flex",justifyContent:"space-between",width:"100%",children:[e("div",{children:e(I,{sx:{color:"rgba(8, 21, 36, 0.87);",fontSize:12},variant:"body2",children:n})}),e("div",{children:e(et,{sx:{pl:.5},color:"primary",onClick:()=>i(o.id)})})]}),e(te,{})]})}),e(te,{})]},`collapse-${o.id}`)})})})})]})},lt=({especialidad:i,categoriaTexto:t,grupoTexto:o})=>{const[n,a]=d.exports.useState(!1),{addEspecialidad:r,deleteEspecialidad:c,state:p}=d.exports.useContext(H),[s,u]=d.exports.useState({categoria:0,especialidad:0,grupo:0,texto:""}),y=g=>{n?c(g):r({categoriaTexto:t,grupoTexto:o,nombre:s.texto,id:s.especialidad}),a(()=>!n)};return d.exports.useEffect(()=>{const g=p.find(v=>v.id==s.especialidad);g!=null&&g!=null?a(!0):a(!1)},[s,p]),d.exports.useEffect(()=>{u(i)},[i]),{checked:n,handleCLick:y,info:s}},de=({data:i,handleClick:t,id:o,item:n})=>{const[a,r]=d.exports.useState(!1),[c,p]=d.exports.useState({categoria:0,especialidad:0,grupo:0,texto:""}),[s,u]=d.exports.useState([]),y=()=>{r(()=>!a),a||u(()=>t(s,o))};return d.exports.useEffect(()=>{i.length>0&&u(()=>t(i,o)),p(n)},[i]),{show:a,info:s,item:c,Handleclick:y}},dt=({categoria:i,especialidad:t,grupo:o})=>{const{checked:n,handleCLick:a,info:r}=lt({especialidad:t,categoriaTexto:i.texto,grupoTexto:o.texto});return h(L,{onClick:()=>a(r.especialidad),display:"flex",alignItems:"center",pl:5.5,sx:{"&:hover":{backgroundColor:"#D4E4F1",cursor:"pointer"}},children:[e(Be,{checked:n,sx:{p:.4},color:"primary"}),e(I,{variant:"body2",sx:{color:"rgba(8, 21, 36, 0.87);"},children:r.texto})]},`cont-cat-${r.categoria}`)},ct=({categoria:i,especialidades:t})=>{const o=(p,s)=>p.filter(u=>u.categoria==s&&u.especialidad>0),{Handleclick:n,show:a,info:r,item:c}=de({data:t,handleClick:o,id:i.categoria,item:i});return h(L,{children:[h(L,{onClick:n,display:"flex",pl:2.5,pt:.5,sx:{"&:hover":{backgroundColor:"#D4E4F1",cursor:"pointer"}},children:[a?e(Xe,{color:"primary"}):e(Ve,{color:"primary"}),e(I,{variant:"subtitle2",sx:{color:"#1E62A1",fontWeight:500},children:c.texto})]}),e(q,{in:a,children:a&&r.length>0?r.map((p,s)=>e(dt,{categoria:{id:p.categoria,texto:p.texto},grupo:{id:c.grupo,texto:c.texto},especialidad:p},`espe-espe-${p.especialidad}-index-${s}`)):null})]})},pt=({grupo:i,categorias:t})=>{const o=(p,s)=>p.filter(u=>u.grupo==s&&u.especialidad==0),{Handleclick:n,show:a,info:r,item:c}=de({data:t,handleClick:o,id:i.grupo,item:i});return h(L,{children:[h(L,{display:"flex",p:.5,onClick:n,sx:{"&:hover":{backgroundColor:"#D4E4F1",cursor:"pointer"}},children:[a?e(tt,{color:"primary"}):e(se,{color:"primary"}),e(I,{variant:"subtitle2",sx:{color:"#1B344C",fontWeight:500,paddingBottom:"0"},children:c.texto})]},`flex-grupo-${c.grupo}`),e(q,{in:a,children:a&&r.length>0?r.map((p,s)=>e(ct,{categoria:p,grupo:c,especialidades:t.filter(u=>u.grupo==c.grupo&&u.categoria==p.categoria&&u.especialidad>0)},`espe-cat-${p.categoria}-index-${s}`)):null})]},`con-grupo-${c.grupo}`)},ut=({data:i,filter:t})=>{let o=[...i];const n=t.trimStart().trimEnd().toLowerCase();if(n!=""){const a=i.filter(s=>s.especialidad>0&&s.texto.toLowerCase().includes(n));let r=[];a.forEach(s=>{r.push({grupo:s.grupo,categoria:s.categoria})});const c=[...new Set(r.map(s=>s.grupo))],p=[...new Set(r.map(s=>s.categoria))];c.forEach(s=>a.push(i.find(u=>u.grupo==s&&u.categoria==0))),p.forEach(s=>a.push(i.find(u=>u.categoria==s&&u.especialidad==0))),o=a}return e(P,{children:o.filter(a=>a.categoria==0).map(a=>{const r=o.filter(c=>c.grupo==a.grupo&&c.categoria>0);return e(pt,{grupo:a,categorias:r},`grupo-${a.grupo}`)})})},ht=({filter:i})=>{d.exports.useRef(null);const{isLoading:t,especialidades:o}=at();return e(P,{children:t?e(Oe,{NoColumnas:1,NoFilas:5,Tipo:"table"},"SkeletonNuevaEspecialidad"):e(m,{children:h(w,{container:!0,spacing:2,mt:1,children:[e(w,{item:!0,xs:6,sx:{overflow:"auto",height:"calc(100vh - 269px)"},children:o.length==0?null:e(ut,{data:o,filter:i})}),e(w,{item:!0,xs:6,sx:{overflow:"auto",maxheight:"calc(100vh - 320px)"},children:e(st,{})})]})})})},xt=i=>{const{state:t,deleteEspecialidad:o}=d.exports.useContext(H),[n,a]=d.exports.useState(t);return d.exports.useEffect(()=>{a(()=>t)},[t]),d.exports.useEffect(()=>{const r=i.toLowerCase();r==""?a(t):a(t.filter(c=>c.categoriaTexto.toLowerCase().includes(r)||c.grupoTexto.toLowerCase().includes(r)||c.nombre.toLowerCase().includes(r)))},[i]),{data:n,deleteEspecialidad:o}},ft=({filter:i})=>{const{data:t,deleteEspecialidad:o}=xt(i);return e(Me,{sx:{maxHeight:440},children:h(Pe,{stickyHeader:!0,"aria-label":"sticky table",size:"small",children:[e(We,{children:h(oe,{children:[h(C,{align:"left",style:{fontWeight:"bold"},children:["Grupo"," "]},"thGrupo"),e(C,{align:"left",style:{fontWeight:"bold"},children:"Categor\xEDa"},"thCategoria"),e(C,{align:"left",style:{fontWeight:"bold"},children:"Descripci\xF3n"},"thDescripci\xF3n"),e(C,{align:"right",style:{fontWeight:"bold"},children:"Acciones"},"thAcciones")]})}),e(_e,{children:t.map(n=>h(oe,{hover:!0,role:"checkbox",tabIndex:-1,children:[e(C,{children:n.grupoTexto},`tdGrupoTexto${n.id}`),e(C,{children:n.categoriaTexto},`tdCategoriaTexto${n.id}`),e(C,{children:n.nombre},`tdEspecilidadTexto${n.id}`),e(C,{align:"right",children:e(Ae,{size:"small",onClick:()=>o(n.id),children:e(Ge,{color:"primary"})})},`tdEliminar${n.id}`)]},`tr${n.id}`))})]})})},gt=i=>{const[t,o]=d.exports.useState(!1),[n,a]=d.exports.useState({filterTable:"",filterNew:""}),{data:r,doFetch:c}=re(),{dispatch:p}=d.exports.useContext(H),{storeUsuario:s}=d.exports.useContext(He);return d.exports.useEffect(()=>{r!=null&&p({type:"add all",payload:r})},[r]),d.exports.useEffect(()=>{c({metodo:`Especialidades/tercero?id=${s.user.idEmpresa}`,type:M.GET,AllowAnonymous:!1})},[]),{handleDialog:()=>{o(()=>!t)},openNew:t,handleChangeTyping:g=>{var $;const v=($=i.current)==null?void 0:$.value;a({filterNew:g=="new"?v:n.filterNew,filterTable:g=="table"?v:n.filterTable})},inputfilter:n}},mt=()=>{const i=d.exports.useRef(null),{handleDialog:t,openNew:o,handleChangeTyping:n,inputfilter:a}=gt(i);return e(m,{sx:{background:"white"},children:h(m,{sx:{height:"calc(100vh - 190px)"},children:[e(m,{display:"flex",justifyContent:"end",children:o?h(w,{container:!0,justifyContent:"flex-start",children:[e(w,{item:!0,xs:10.5,children:e(ie,{id:"txtbuscarEspecialidad",size:"small",inputRef:i,onInput:()=>n("new"),placeholder:"Buscar especialidad...",sx:{width:"100%"},InputProps:{endAdornment:e(ae,{position:"end",children:e(ne,{})})},label:"Buscar especialidad",variant:"outlined"})}),e(w,{item:!0,xs:1.5,children:h(j,{onClick:t,sx:{ml:"20px"},variant:"text",children:[" ",e(Ze,{sx:{mr:"8px"}}),"Regresar"]})})]}):h(w,{children:[e(ie,{id:"outlined-basic",size:"small",inputRef:i,onInput:()=>n("table"),placeholder:"Buscar...",sx:{width:"400px"},InputProps:{endAdornment:e(ae,{position:"end",children:e(ne,{})})},label:"Buscar",variant:"outlined"}),h(j,{onClick:t,sx:{ml:"20px"},variant:"text",children:[" ",e(se,{sx:{mr:"8px"}}),"Agregar especialidad"]}),h(j,{variant:"text",children:[" ",e(je,{sx:{mr:"8px"}}),"Historial"]})]})}),o?e(m,{mt:3,children:e(ht,{filter:a.filterNew})}):e(m,{mt:3,children:e(ft,{filter:a.filterTable})})]})})},It=()=>h(P,{children:[e(ye,{title:"Especialidades"}),e(m,{p:2,children:e(nt,{children:e(mt,{})})})]});export{It as EspecialidadesPage,It as default};
