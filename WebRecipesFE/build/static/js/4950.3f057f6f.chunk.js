"use strict";(self.webpackChunk_coreui_coreui_free_react_admin_template_cra=self.webpackChunk_coreui_coreui_free_react_admin_template_cra||[]).push([[4950],{84950:(e,t,n)=>{n.r(t),n.d(t,{default:()=>p});n(65043);var r=n(44101),o=n(44227),a=n(80861),c=n(25104),s=n(96105),l=n(52787),i=n(84709),u=n(47900),d=n(70579);const p=()=>(0,d.jsxs)(r.s,{children:[(0,d.jsx)(o.U,{xs:12,children:(0,d.jsxs)(a.E,{className:"mb-4",children:[(0,d.jsxs)(c.V,{children:[(0,d.jsx)("strong",{children:"React Popover"})," ",(0,d.jsx)("small",{children:"Basic example"})]}),(0,d.jsx)(s.W,{children:(0,d.jsx)(u.Eb,{href:"components/popover",children:(0,d.jsx)(l.p,{title:"Popover title",content:"And here\u2019s some amazing content. It\u2019s very engaging. Right?",placement:"right",children:(0,d.jsx)(i.Q,{color:"danger",size:"lg",children:"Click to toggle popover"})})})})]})}),(0,d.jsx)(o.U,{xs:12,children:(0,d.jsxs)(a.E,{className:"mb-4",children:[(0,d.jsxs)(c.V,{children:[(0,d.jsx)("strong",{children:"React Popover"})," ",(0,d.jsx)("small",{children:"Four directions"})]}),(0,d.jsxs)(s.W,{children:[(0,d.jsx)("p",{className:"text-body-secondary small",children:"Four options are available: top, right, bottom, and left aligned. Directions are mirrored when using CoreUI for React in RTL."}),(0,d.jsxs)(u.Eb,{href:"components/popover#four-directions",children:[(0,d.jsx)(l.p,{content:"Vivamus sagittis lacus vel augue laoreet rutrum faucibus.",placement:"top",children:(0,d.jsx)(i.Q,{color:"secondary",children:"Popover on top"})}),(0,d.jsx)(l.p,{content:"Vivamus sagittis lacus vel augue laoreet rutrum faucibus.",placement:"right",children:(0,d.jsx)(i.Q,{color:"secondary",children:"Popover on right"})}),(0,d.jsx)(l.p,{content:"Vivamus sagittis lacus vel augue laoreet rutrum faucibus.",placement:"bottom",children:(0,d.jsx)(i.Q,{color:"secondary",children:"Popover on bottom"})}),(0,d.jsx)(l.p,{content:"Vivamus sagittis lacus vel augue laoreet rutrum faucibus.",placement:"left",children:(0,d.jsx)(i.Q,{color:"secondary",children:"Popover on left"})})]})]})]})})]})},80861:(e,t,n)=>{n.d(t,{E:()=>i});var r=n(22378),o=n(65043),a=n(65173),c=n.n(a),s=n(25196),l=n(75232),i=(0,o.forwardRef)((function(e,t){var n,a=e.children,c=e.className,l=e.color,i=e.textBgColor,u=e.textColor,d=(0,r.Tt)(e,["children","className","color","textBgColor","textColor"]);return o.createElement("div",(0,r.Cl)({className:(0,s.A)("card",(n={},n["bg-".concat(l)]=l,n["text-".concat(u)]=u,n["text-bg-".concat(i)]=i,n),c)},d,{ref:t}),a)}));i.propTypes={children:c().node,className:c().string,color:l.TX,textBgColor:l.TX,textColor:c().string},i.displayName="CCard"},96105:(e,t,n)=>{n.d(t,{W:()=>l});var r=n(22378),o=n(65043),a=n(65173),c=n.n(a),s=n(25196),l=(0,o.forwardRef)((function(e,t){var n=e.children,a=e.className,c=(0,r.Tt)(e,["children","className"]);return o.createElement("div",(0,r.Cl)({className:(0,s.A)("card-body",a)},c,{ref:t}),n)}));l.propTypes={children:c().node,className:c().string},l.displayName="CCardBody"},25104:(e,t,n)=>{n.d(t,{V:()=>l});var r=n(22378),o=n(65043),a=n(65173),c=n.n(a),s=n(25196),l=(0,o.forwardRef)((function(e,t){var n=e.children,a=e.as,c=void 0===a?"div":a,l=e.className,i=(0,r.Tt)(e,["children","as","className"]);return o.createElement(c,(0,r.Cl)({className:(0,s.A)("card-header",l)},i,{ref:t}),n)}));l.propTypes={as:c().elementType,children:c().node,className:c().string},l.displayName="CCardHeader"},44227:(e,t,n)=>{n.d(t,{U:()=>i});var r=n(22378),o=n(65043),a=n(65173),c=n.n(a),s=n(25196),l=["xxl","xl","lg","md","sm","xs"],i=(0,o.forwardRef)((function(e,t){var n=e.children,a=e.className,c=(0,r.Tt)(e,["children","className"]),i=[];return l.forEach((function(e){var t=c[e];delete c[e];var n="xs"===e?"":"-".concat(e);"number"!==typeof t&&"string"!==typeof t||i.push("col".concat(n,"-").concat(t)),"boolean"===typeof t&&i.push("col".concat(n)),t&&"object"===typeof t&&("number"!==typeof t.span&&"string"!==typeof t.span||i.push("col".concat(n,"-").concat(t.span)),"boolean"===typeof t.span&&i.push("col".concat(n)),"number"!==typeof t.order&&"string"!==typeof t.order||i.push("order".concat(n,"-").concat(t.order)),"number"===typeof t.offset&&i.push("offset".concat(n,"-").concat(t.offset)))})),o.createElement("div",(0,r.Cl)({className:(0,s.A)(i.length>0?i:"col",a)},c,{ref:t}),n)})),u=c().oneOfType([c().bool,c().number,c().string,c().oneOf(["auto"])]),d=c().oneOfType([u,c().shape({span:u,offset:c().oneOfType([c().number,c().string]),order:c().oneOfType([c().oneOf(["first","last"]),c().number,c().string])})]);i.propTypes={children:c().node,className:c().string,xs:d,sm:d,md:d,lg:d,xl:d,xxl:d},i.displayName="CCol"},44101:(e,t,n)=>{n.d(t,{s:()=>i});var r=n(22378),o=n(65043),a=n(65173),c=n.n(a),s=n(25196),l=["xxl","xl","lg","md","sm","xs"],i=(0,o.forwardRef)((function(e,t){var n=e.children,a=e.className,c=(0,r.Tt)(e,["children","className"]),i=[];return l.forEach((function(e){var t=c[e];delete c[e];var n="xs"===e?"":"-".concat(e);"object"===typeof t&&(t.cols&&i.push("row-cols".concat(n,"-").concat(t.cols)),"number"===typeof t.gutter&&i.push("g".concat(n,"-").concat(t.gutter)),"number"===typeof t.gutterX&&i.push("gx".concat(n,"-").concat(t.gutterX)),"number"===typeof t.gutterY&&i.push("gy".concat(n,"-").concat(t.gutterY)))})),o.createElement("div",(0,r.Cl)({className:(0,s.A)("row",i,a)},c,{ref:t}),n)})),u=c().shape({cols:c().oneOfType([c().oneOf(["auto"]),c().number,c().string]),gutter:c().oneOfType([c().string,c().number]),gutterX:c().oneOfType([c().string,c().number]),gutterY:c().oneOfType([c().string,c().number])});i.propTypes={children:c().node,className:c().string,xs:u,sm:u,md:u,lg:u,xl:u,xxl:u},i.displayName="CRow"},52787:(e,t,n)=>{n.d(t,{p:()=>m});var r=n(22378),o=n(65043),a=n(25196),c=n(65173),s=n.n(c),l=n(372),i=n(94462),u=n(90063),d=n(75232),p=n(31889),f=n(54633),m=(0,o.forwardRef)((function(e,t){var n=e.children,c=e.animation,s=void 0===c||c,d=e.className,m=e.container,h=e.content,g=e.delay,v=void 0===g?0:g,b=e.fallbackPlacements,y=void 0===b?["top","right","bottom","left"]:b,x=e.offset,N=void 0===x?[0,8]:x,C=e.onHide,T=e.onShow,E=e.placement,j=void 0===E?"top":E,w=e.title,R=e.trigger,O=void 0===R?"click":R,P=e.visible,_=(0,r.Tt)(e,["children","animation","className","container","content","delay","fallbackPlacements","offset","onHide","onShow","placement","title","trigger","visible"]),k=(0,o.useRef)(null),A=(0,o.useRef)(null),F=(0,i.E2)(t,k),S="popover".concat((0,o.useId)()),V=(0,o.useState)(!1),B=V[0],Q=V[1],U=(0,o.useState)(P),X=U[0],H=U[1],L=(0,u.E)(),Y=L.initPopper,D=L.destroyPopper,I="number"===typeof v?{show:v,hide:v}:v,W={modifiers:[{name:"arrow",options:{element:".popover-arrow"}},{name:"flip",options:{fallbackPlacements:y}},{name:"offset",options:{offset:N}}],placement:(0,f.A)(j,A.current)};(0,o.useEffect)((function(){P?q():z()}),[P]),(0,o.useEffect)((function(){if(B&&A.current&&k.current)return Y(A.current,k.current,W),void setTimeout((function(){H(!0)}),I.show);!B&&A.current&&k.current&&D()}),[B]),(0,o.useEffect)((function(){!X&&A.current&&k.current&&(0,p.A)((function(){Q(!1)}),k.current)}),[X]);var q=function(){Q(!0),T&&T()},z=function(){setTimeout((function(){H(!1),C&&C()}),I.hide)};return o.createElement(o.Fragment,null,o.cloneElement(n,(0,r.Cl)((0,r.Cl)((0,r.Cl)((0,r.Cl)((0,r.Cl)({},X&&{"aria-describedby":S}),{ref:A}),("click"===O||O.includes("click"))&&{onClick:function(){return X?z():q()}}),("focus"===O||O.includes("focus"))&&{onFocus:function(){return q()},onBlur:function(){return z()}}),("hover"===O||O.includes("hover"))&&{onMouseEnter:function(){return q()},onMouseLeave:function(){return z()}})),o.createElement(l.Y,{container:m,portal:!0},B&&o.createElement("div",(0,r.Cl)({className:(0,a.A)("popover","bs-popover-auto",{fade:s,show:X},d),id:S,ref:F,role:"tooltip"},_),o.createElement("div",{className:"popover-arrow"}),o.createElement("div",{className:"popover-header"},w),o.createElement("div",{className:"popover-body"},h))))}));m.propTypes={animation:s().bool,children:s().node,className:s().string,container:s().any,content:s().oneOfType([s().string,s().node]),delay:s().oneOfType([s().number,s().shape({show:s().number.isRequired,hide:s().number.isRequired})]),fallbackPlacements:d.sS,offset:s().any,onHide:s().func,onShow:s().func,placement:s().oneOf(["auto","top","right","bottom","left"]),title:s().oneOfType([s().string,s().node]),trigger:d.Us,visible:s().bool},m.displayName="CPopover"},31889:(e,t,n)=>{n.d(t,{A:()=>o});var r=function(e){"function"===typeof e&&e()},o=function(e,t,n){if(void 0===n&&(n=!0),n){var o=function(e){if(!e)return 0;var t=window.getComputedStyle(e),n=t.transitionDuration,r=t.transitionDelay,o=Number.parseFloat(n),a=Number.parseFloat(r);return o||a?(n=n.split(",")[0],r=r.split(",")[0],1e3*(Number.parseFloat(n)+Number.parseFloat(r))):0}(t)+5,a=!1,c=function(n){n.target===t&&(a=!0,t.removeEventListener("transitionend",c),r(e))};t.addEventListener("transitionend",c),setTimeout((function(){a||t.dispatchEvent(new Event("transitionend"))}),o)}else r(e)}},54633:(e,t,n)=>{n.d(t,{A:()=>o});var r=n(79438),o=function(e,t){switch(e){case"right":return(0,r.A)(t)?"left":"right";case"left":return(0,r.A)(t)?"right":"left";default:return e}}}}]);
//# sourceMappingURL=4950.3f057f6f.chunk.js.map