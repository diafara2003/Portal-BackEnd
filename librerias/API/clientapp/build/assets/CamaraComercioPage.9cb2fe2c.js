import{H as j,S as b}from"./HeaderComponent.0bc9406d.js";import{d as q}from"./History.e8cf50f5.js";import{E as L}from"./Eliminar.18aca338.js";import{r as l,am as R,a as O,b as w,j as e,Y as T,d as o,f as B,G as v,F as z,I as P,aF as U,$ as D,a1 as S,a2 as y,B as A,a3 as $,T as N}from"./index.ad9b7e92.js";import{d as V}from"./DeleteOutlineOutlined.6e9278a5.js";import{T as Y,a as J,b as K,c as F,d as m,e as Q}from"./TableRow.15b8d78b.js";import{V as X}from"./ValidationForms.a47614c1.js";import{D as H,a as _,b as k,c as W}from"./DialogTitle.c4648013.js";import{H as Z,T as ee}from"./Historial-Model.35b6e462.js";import{A as ae}from"./Add.73b42c3e.js";import"./CardHeader.67279b64.js";import"./SinInformacion.9e7bb934.js";import"./Tabs.f8373235.js";import"./Stack.9bed6c39.js";const te=()=>{const{storeUsuario:d}=l.exports.useContext(R),[n,i]=l.exports.useState([]),[t,s]=l.exports.useState(!0),[u,c]=l.exports.useState(!1),[h,p]=l.exports.useState(0),[I,C]=l.exports.useState(!1),f=()=>{c(!1)},E=async()=>{const a={metodo:`TercerosGI/CamaraComercio?id=${d.user.idEmpresa}`,AllowAnonymous:!1,type:O.GET},x=await w(a);i(x),s(!1)},r=async()=>{s(!0),c(!1);const a={AllowAnonymous:!1,metodo:`TercerosGI/CamaraComercio/${h}`,type:O.DELETE},x=await w(a);console.log(x),x!=null&&x.success?(i(G=>[...G].filter(M=>M.id!=h)),p(-1)):console.log("no se pudo eliminar"),s(!1)};return l.exports.useEffect(()=>{E()},[]),{dataCamara:n,isLoading:t,openDelete:u,handleCloseDelete:f,handleDeleteCamara:r,setDataIdDelete:p,setOpenDelete:c,openNew:I,setOpenNew:C,newUser:a=>{i([...n,a])}}},re=({datatable:d,onDelete:n})=>{const i=t=>{n(t.id)};return e(T,{children:e(Y,{sx:{maxHeight:440},children:o(J,{stickyHeader:!0,"aria-label":"sticky table",size:"small",children:[e(K,{children:o(F,{children:[o(m,{align:"left",style:{fontWeight:"bold"},children:["Tipo documento"," "]},"thTipoDoc"),e(m,{align:"left",style:{fontWeight:"bold"},children:"Numero documento"},"thNumDoc"),e(m,{align:"left",style:{fontWeight:"bold"},children:"Nombre"},"thNombre"),e(m,{align:"left",style:{fontWeight:"bold"},children:"Cargo"},"thCargo"),e(m,{align:"center",style:{fontWeight:"bold"},children:"Acciones"},"thAcciones")]})}),e(Q,{children:d.map(t=>o(F,{hover:!0,role:"checkbox",tabIndex:-1,children:[e(m,{children:t.tipoDocumento}),e(m,{children:t.documento}),e(m,{children:t.nombre}),e(m,{children:t.cargo}),e(m,{align:"center",children:e(B,{edge:"end",onClick:()=>i(t),"aria-label":"delete",children:e(V,{color:"primary"})})})]},t.id))})]})})})},oe={documento:{hasError:!1,msn:""},nombre:{hasError:!1,msn:""},cargo:{hasError:!1,msn:""}},ne={cargo:"",documento:"",id:0,nombre:"",tipoDocumento:"cc"},ie=({handleSave:d})=>{const[n,i]=l.exports.useState(oe),[t,s]=l.exports.useState(ne),[u,c]=l.exports.useState(!1),[h,p]=l.exports.useState(!0),I=r=>{s({...t,tipoDocumento:r.target.value})},C=r=>{const{name:g,value:a}=r.target;s({...t,[g]:a}),i({...n,[g]:{hasError:!1,msn:""}})},f=async()=>{const r=E();if(r.isvalid){c(!0);const g={metodo:"TercerosGI/CamaraComercio",type:O.POST,data:t,AllowAnonymous:!1},a=await w(g);a!=null&&(p(!1),s(a),d(a)),c(!1)}else i({...n,[r.name]:r.property})},E=()=>{const r=t,g=new X;let a={isvalid:!0,property:{hasError:!1,msn:""},name:""};return r.nombre==null||r.nombre==""?(a.isvalid=!1,a.property={hasError:!0,msn:"El nombre es obligatorio"},a.name="nombre",a):r.cargo==null||r.cargo==""?(a.isvalid=!1,a.property={hasError:!0,msn:"El cargo es obligatorio"},a.name="cargo",a):r.documento==null||r.documento==""?(a.isvalid=!1,a.property={hasError:!0,msn:"El documento es obligatorio"},a.name="documento",a):(g.OnlyInteger(r.documento)||(a.isvalid=!1,a.property={hasError:!0,msn:"El documento ingresado no es valido"},a.name="documento"),a)};return{validation:n,onInputChange:C,dataInitialState:t,handleChange:I,isSaving:u,isOpen:h,setIsOpen:p,handleGuardar:f}},le=d=>{const{validation:n,dataInitialState:i,onInputChange:t,handleChange:s,isOpen:u,setIsOpen:c,handleGuardar:h}=ie(d);return e(T,{children:o(H,{open:u,onClose:()=>c(!u),"aria-labelledby":"alert-dialog-title","aria-describedby":"alert-dialog-description",maxWidth:"md",children:[e(_,{id:"alert-dialog-title",children:"Agregar usuario"}),e(k,{children:o(v,{container:!0,spacing:2,width:"100%",display:"flex",alignItems:"center",justifyContent:"center",p:1,children:[e(v,{item:!0,xs:6,children:o(z,{fullWidth:!0,children:[e(P,{id:"demo-simple-select-label",children:"Tipo documento"}),o(U,{size:"small",labelId:"demo-simple-select-label",id:"demo-simple-select",value:i.tipoDocumento,label:"Age",onChange:s,children:[e(D,{value:"cc",children:"C\xE9dula de ciudadan\xEDa"}),e(D,{value:"NIT",children:"NIT"}),e(D,{value:"ce",children:"C\xE9dula de extranjeria"})]})]})}),e(v,{item:!0,xs:6,children:e(S,{required:!0,type:"number",name:"documento",onChange:t,error:n.documento.hasError,helperText:n.documento.msn,value:i.documento,label:"Numero de documento",fullWidth:!0,size:"small"})}),e(v,{item:!0,xs:6,children:e(S,{required:!0,name:"cargo",onChange:t,error:n.cargo.hasError,helperText:n.cargo.msn,value:i.cargo,label:"Cargo",fullWidth:!0,size:"small"})}),e(v,{item:!0,xs:6,children:e(S,{required:!0,name:"nombre",onChange:t,error:n.nombre.hasError,helperText:n.nombre.msn,value:i.nombre,label:"Nombre",fullWidth:!0,size:"small"})})]})}),o(W,{children:[e(y,{variant:"outlined",onClick:()=>c(!1),children:"Cancelar"}),e(y,{variant:"contained",color:"primary",onClick:h,autoFocus:!0,children:"Guardar"})]})]})})},ve=()=>{const{dataCamara:d,isLoading:n,openDelete:i,handleCloseDelete:t,handleDeleteCamara:s,setDataIdDelete:u,setOpenDelete:c,openNew:h,setOpenNew:p,newUser:I}=te(),[C,f]=l.exports.useState(!1);return o(T,{children:[e(j,{title:`${C?"Historial ":""} Camara y comercio`}),C?e(Z,{_tipoAuditoria:ee.CamaraComercio,onClose:r=>{f(r)}}):o(A,{sx:{width:"100%"},children:[o(A,{display:"flex",justifyContent:"end",pt:"10px",children:[o(y,{onClick:()=>p(()=>!0),sx:{ml:"20px"},variant:"text",children:[" ",e(ae,{sx:{mr:"8px"}}),"Agregar usuario"]}),o(y,{variant:"text",onClick:()=>{f(!0)},children:[" ",e(q,{sx:{mr:"8px"}}),"Historial"]})]}),e(A,{m:"30px",mt:"25px",children:n?o(T,{children:[e(b,{animation:"wave",height:"40px"}),e(b,{animation:"wave",height:"40px"})," ",e(b,{animation:"wave",height:"40px"}),e(b,{animation:"wave",height:"40px"})," ",e(b,{animation:"wave",height:"40px"}),e(b,{animation:"wave",height:"40px"})]}):d==null?e($,{color:"inherit"}):e(re,{datatable:d,onDelete:r=>{u(r),c(!0)}})})]}),h?e(le,{handleSave:I}):null,o(H,{open:i,onClose:t,"aria-labelledby":"alert-dialog-title","aria-describedby":"alert-dialog-description",maxWidth:"md",children:[e(_,{id:"alert-dialog-title",justifyContent:"center",display:"flex",children:e(N,{children:"  Eliminar camara y comercio "})}),o(k,{children:[e(A,{justifyContent:"center",display:"flex",children:e(L,{})}),e(N,{children:"\xBFEsta seguro que desea eliminar este contacto?"})]}),o(W,{children:[e(y,{variant:"text",onClick:t,children:"Cancelar"}),e(y,{variant:"outlined",color:"error",onClick:s,autoFocus:!0,children:"Eliminar"})]})]})]})};export{ve as CamaraComercioPage,ve as default};
