// Start Bundle: _jquery
/*! jQuery v1.11.3 | (c) 2005, 2015 jQuery Foundation, Inc. | jquery.org/license */
!function (a, b) { "object" == typeof module && "object" == typeof module.exports ? module.exports = a.document ? b(a, !0) : function (a) { if (!a.document) throw new Error("jQuery requires a window with a document"); return b(a) } : b(a) }("undefined" != typeof window ? window : this, function (a, b) {
    var c = [], d = c.slice, e = c.concat, f = c.push, g = c.indexOf, h = {}, i = h.toString, j = h.hasOwnProperty, k = {}, l = "1.11.3", m = function (a, b) { return new m.fn.init(a, b) }, n = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g, o = /^-ms-/, p = /-([\da-z])/gi, q = function (a, b) { return b.toUpperCase() }; m.fn = m.prototype = { jquery: l, constructor: m, selector: "", length: 0, toArray: function () { return d.call(this) }, get: function (a) { return null != a ? 0 > a ? this[a + this.length] : this[a] : d.call(this) }, pushStack: function (a) { var b = m.merge(this.constructor(), a); return b.prevObject = this, b.context = this.context, b }, each: function (a, b) { return m.each(this, a, b) }, map: function (a) { return this.pushStack(m.map(this, function (b, c) { return a.call(b, c, b) })) }, slice: function () { return this.pushStack(d.apply(this, arguments)) }, first: function () { return this.eq(0) }, last: function () { return this.eq(-1) }, eq: function (a) { var b = this.length, c = +a + (0 > a ? b : 0); return this.pushStack(c >= 0 && b > c ? [this[c]] : []) }, end: function () { return this.prevObject || this.constructor(null) }, push: f, sort: c.sort, splice: c.splice }, m.extend = m.fn.extend = function () { var a, b, c, d, e, f, g = arguments[0] || {}, h = 1, i = arguments.length, j = !1; for ("boolean" == typeof g && (j = g, g = arguments[h] || {}, h++), "object" == typeof g || m.isFunction(g) || (g = {}), h === i && (g = this, h--); i > h; h++)if (null != (e = arguments[h])) for (d in e) a = g[d], c = e[d], g !== c && (j && c && (m.isPlainObject(c) || (b = m.isArray(c))) ? (b ? (b = !1, f = a && m.isArray(a) ? a : []) : f = a && m.isPlainObject(a) ? a : {}, g[d] = m.extend(j, f, c)) : void 0 !== c && (g[d] = c)); return g }, m.extend({ expando: "jQuery" + (l + Math.random()).replace(/\D/g, ""), isReady: !0, error: function (a) { throw new Error(a) }, noop: function () { }, isFunction: function (a) { return "function" === m.type(a) }, isArray: Array.isArray || function (a) { return "array" === m.type(a) }, isWindow: function (a) { return null != a && a == a.window }, isNumeric: function (a) { return !m.isArray(a) && a - parseFloat(a) + 1 >= 0 }, isEmptyObject: function (a) { var b; for (b in a) return !1; return !0 }, isPlainObject: function (a) { var b; if (!a || "object" !== m.type(a) || a.nodeType || m.isWindow(a)) return !1; try { if (a.constructor && !j.call(a, "constructor") && !j.call(a.constructor.prototype, "isPrototypeOf")) return !1 } catch (c) { return !1 } if (k.ownLast) for (b in a) return j.call(a, b); for (b in a); return void 0 === b || j.call(a, b) }, type: function (a) { return null == a ? a + "" : "object" == typeof a || "function" == typeof a ? h[i.call(a)] || "object" : typeof a }, globalEval: function (b) { b && m.trim(b) && (a.execScript || function (b) { a.eval.call(a, b) })(b) }, camelCase: function (a) { return a.replace(o, "ms-").replace(p, q) }, nodeName: function (a, b) { return a.nodeName && a.nodeName.toLowerCase() === b.toLowerCase() }, each: function (a, b, c) { var d, e = 0, f = a.length, g = r(a); if (c) { if (g) { for (; f > e; e++)if (d = b.apply(a[e], c), d === !1) break } else for (e in a) if (d = b.apply(a[e], c), d === !1) break } else if (g) { for (; f > e; e++)if (d = b.call(a[e], e, a[e]), d === !1) break } else for (e in a) if (d = b.call(a[e], e, a[e]), d === !1) break; return a }, trim: function (a) { return null == a ? "" : (a + "").replace(n, "") }, makeArray: function (a, b) { var c = b || []; return null != a && (r(Object(a)) ? m.merge(c, "string" == typeof a ? [a] : a) : f.call(c, a)), c }, inArray: function (a, b, c) { var d; if (b) { if (g) return g.call(b, a, c); for (d = b.length, c = c ? 0 > c ? Math.max(0, d + c) : c : 0; d > c; c++)if (c in b && b[c] === a) return c } return -1 }, merge: function (a, b) { var c = +b.length, d = 0, e = a.length; while (c > d) a[e++] = b[d++]; if (c !== c) while (void 0 !== b[d]) a[e++] = b[d++]; return a.length = e, a }, grep: function (a, b, c) { for (var d, e = [], f = 0, g = a.length, h = !c; g > f; f++)d = !b(a[f], f), d !== h && e.push(a[f]); return e }, map: function (a, b, c) { var d, f = 0, g = a.length, h = r(a), i = []; if (h) for (; g > f; f++)d = b(a[f], f, c), null != d && i.push(d); else for (f in a) d = b(a[f], f, c), null != d && i.push(d); return e.apply([], i) }, guid: 1, proxy: function (a, b) { var c, e, f; return "string" == typeof b && (f = a[b], b = a, a = f), m.isFunction(a) ? (c = d.call(arguments, 2), e = function () { return a.apply(b || this, c.concat(d.call(arguments))) }, e.guid = a.guid = a.guid || m.guid++, e) : void 0 }, now: function () { return +new Date }, support: k }), m.each("Boolean Number String Function Array Date RegExp Object Error".split(" "), function (a, b) { h["[object " + b + "]"] = b.toLowerCase() }); function r(a) { var b = "length" in a && a.length, c = m.type(a); return "function" === c || m.isWindow(a) ? !1 : 1 === a.nodeType && b ? !0 : "array" === c || 0 === b || "number" == typeof b && b > 0 && b - 1 in a } var s = function (a) { var b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u = "sizzle" + 1 * new Date, v = a.document, w = 0, x = 0, y = ha(), z = ha(), A = ha(), B = function (a, b) { return a === b && (l = !0), 0 }, C = 1 << 31, D = {}.hasOwnProperty, E = [], F = E.pop, G = E.push, H = E.push, I = E.slice, J = function (a, b) { for (var c = 0, d = a.length; d > c; c++)if (a[c] === b) return c; return -1 }, K = "checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped", L = "[\\x20\\t\\r\\n\\f]", M = "(?:\\\\.|[\\w-]|[^\\x00-\\xa0])+", N = M.replace("w", "w#"), O = "\\[" + L + "*(" + M + ")(?:" + L + "*([*^$|!~]?=)" + L + "*(?:'((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\"|(" + N + "))|)" + L + "*\\]", P = ":(" + M + ")(?:\\((('((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\")|((?:\\\\.|[^\\\\()[\\]]|" + O + ")*)|.*)\\)|)", Q = new RegExp(L + "+", "g"), R = new RegExp("^" + L + "+|((?:^|[^\\\\])(?:\\\\.)*)" + L + "+$", "g"), S = new RegExp("^" + L + "*," + L + "*"), T = new RegExp("^" + L + "*([>+~]|" + L + ")" + L + "*"), U = new RegExp("=" + L + "*([^\\]'\"]*?)" + L + "*\\]", "g"), V = new RegExp(P), W = new RegExp("^" + N + "$"), X = { ID: new RegExp("^#(" + M + ")"), CLASS: new RegExp("^\\.(" + M + ")"), TAG: new RegExp("^(" + M.replace("w", "w*") + ")"), ATTR: new RegExp("^" + O), PSEUDO: new RegExp("^" + P), CHILD: new RegExp("^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\(" + L + "*(even|odd|(([+-]|)(\\d*)n|)" + L + "*(?:([+-]|)" + L + "*(\\d+)|))" + L + "*\\)|)", "i"), bool: new RegExp("^(?:" + K + ")$", "i"), needsContext: new RegExp("^" + L + "*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\(" + L + "*((?:-\\d)?\\d*)" + L + "*\\)|)(?=[^-]|$)", "i") }, Y = /^(?:input|select|textarea|button)$/i, Z = /^h\d$/i, $ = /^[^{]+\{\s*\[native \w/, _ = /^(?:#([\w-]+)|(\w+)|\.([\w-]+))$/, aa = /[+~]/, ba = /'|\\/g, ca = new RegExp("\\\\([\\da-f]{1,6}" + L + "?|(" + L + ")|.)", "ig"), da = function (a, b, c) { var d = "0x" + b - 65536; return d !== d || c ? b : 0 > d ? String.fromCharCode(d + 65536) : String.fromCharCode(d >> 10 | 55296, 1023 & d | 56320) }, ea = function () { m() }; try { H.apply(E = I.call(v.childNodes), v.childNodes), E[v.childNodes.length].nodeType } catch (fa) { H = { apply: E.length ? function (a, b) { G.apply(a, I.call(b)) } : function (a, b) { var c = a.length, d = 0; while (a[c++] = b[d++]); a.length = c - 1 } } } function ga(a, b, d, e) { var f, h, j, k, l, o, r, s, w, x; if ((b ? b.ownerDocument || b : v) !== n && m(b), b = b || n, d = d || [], k = b.nodeType, "string" != typeof a || !a || 1 !== k && 9 !== k && 11 !== k) return d; if (!e && p) { if (11 !== k && (f = _.exec(a))) if (j = f[1]) { if (9 === k) { if (h = b.getElementById(j), !h || !h.parentNode) return d; if (h.id === j) return d.push(h), d } else if (b.ownerDocument && (h = b.ownerDocument.getElementById(j)) && t(b, h) && h.id === j) return d.push(h), d } else { if (f[2]) return H.apply(d, b.getElementsByTagName(a)), d; if ((j = f[3]) && c.getElementsByClassName) return H.apply(d, b.getElementsByClassName(j)), d } if (c.qsa && (!q || !q.test(a))) { if (s = r = u, w = b, x = 1 !== k && a, 1 === k && "object" !== b.nodeName.toLowerCase()) { o = g(a), (r = b.getAttribute("id")) ? s = r.replace(ba, "\\$&") : b.setAttribute("id", s), s = "[id='" + s + "'] ", l = o.length; while (l--) o[l] = s + ra(o[l]); w = aa.test(a) && pa(b.parentNode) || b, x = o.join(",") } if (x) try { return H.apply(d, w.querySelectorAll(x)), d } catch (y) { } finally { r || b.removeAttribute("id") } } } return i(a.replace(R, "$1"), b, d, e) } function ha() { var a = []; function b(c, e) { return a.push(c + " ") > d.cacheLength && delete b[a.shift()], b[c + " "] = e } return b } function ia(a) { return a[u] = !0, a } function ja(a) { var b = n.createElement("div"); try { return !!a(b) } catch (c) { return !1 } finally { b.parentNode && b.parentNode.removeChild(b), b = null } } function ka(a, b) { var c = a.split("|"), e = a.length; while (e--) d.attrHandle[c[e]] = b } function la(a, b) { var c = b && a, d = c && 1 === a.nodeType && 1 === b.nodeType && (~b.sourceIndex || C) - (~a.sourceIndex || C); if (d) return d; if (c) while (c = c.nextSibling) if (c === b) return -1; return a ? 1 : -1 } function ma(a) { return function (b) { var c = b.nodeName.toLowerCase(); return "input" === c && b.type === a } } function na(a) { return function (b) { var c = b.nodeName.toLowerCase(); return ("input" === c || "button" === c) && b.type === a } } function oa(a) { return ia(function (b) { return b = +b, ia(function (c, d) { var e, f = a([], c.length, b), g = f.length; while (g--) c[e = f[g]] && (c[e] = !(d[e] = c[e])) }) }) } function pa(a) { return a && "undefined" != typeof a.getElementsByTagName && a } c = ga.support = {}, f = ga.isXML = function (a) { var b = a && (a.ownerDocument || a).documentElement; return b ? "HTML" !== b.nodeName : !1 }, m = ga.setDocument = function (a) { var b, e, g = a ? a.ownerDocument || a : v; return g !== n && 9 === g.nodeType && g.documentElement ? (n = g, o = g.documentElement, e = g.defaultView, e && e !== e.top && (e.addEventListener ? e.addEventListener("unload", ea, !1) : e.attachEvent && e.attachEvent("onunload", ea)), p = !f(g), c.attributes = ja(function (a) { return a.className = "i", !a.getAttribute("className") }), c.getElementsByTagName = ja(function (a) { return a.appendChild(g.createComment("")), !a.getElementsByTagName("*").length }), c.getElementsByClassName = $.test(g.getElementsByClassName), c.getById = ja(function (a) { return o.appendChild(a).id = u, !g.getElementsByName || !g.getElementsByName(u).length }), c.getById ? (d.find.ID = function (a, b) { if ("undefined" != typeof b.getElementById && p) { var c = b.getElementById(a); return c && c.parentNode ? [c] : [] } }, d.filter.ID = function (a) { var b = a.replace(ca, da); return function (a) { return a.getAttribute("id") === b } }) : (delete d.find.ID, d.filter.ID = function (a) { var b = a.replace(ca, da); return function (a) { var c = "undefined" != typeof a.getAttributeNode && a.getAttributeNode("id"); return c && c.value === b } }), d.find.TAG = c.getElementsByTagName ? function (a, b) { return "undefined" != typeof b.getElementsByTagName ? b.getElementsByTagName(a) : c.qsa ? b.querySelectorAll(a) : void 0 } : function (a, b) { var c, d = [], e = 0, f = b.getElementsByTagName(a); if ("*" === a) { while (c = f[e++]) 1 === c.nodeType && d.push(c); return d } return f }, d.find.CLASS = c.getElementsByClassName && function (a, b) { return p ? b.getElementsByClassName(a) : void 0 }, r = [], q = [], (c.qsa = $.test(g.querySelectorAll)) && (ja(function (a) { o.appendChild(a).innerHTML = "<a id='" + u + "'></a><select id='" + u + "-\f]' msallowcapture=''><option selected=''></option></select>", a.querySelectorAll("[msallowcapture^='']").length && q.push("[*^$]=" + L + "*(?:''|\"\")"), a.querySelectorAll("[selected]").length || q.push("\\[" + L + "*(?:value|" + K + ")"), a.querySelectorAll("[id~=" + u + "-]").length || q.push("~="), a.querySelectorAll(":checked").length || q.push(":checked"), a.querySelectorAll("a#" + u + "+*").length || q.push(".#.+[+~]") }), ja(function (a) { var b = g.createElement("input"); b.setAttribute("type", "hidden"), a.appendChild(b).setAttribute("name", "D"), a.querySelectorAll("[name=d]").length && q.push("name" + L + "*[*^$|!~]?="), a.querySelectorAll(":enabled").length || q.push(":enabled", ":disabled"), a.querySelectorAll("*,:x"), q.push(",.*:") })), (c.matchesSelector = $.test(s = o.matches || o.webkitMatchesSelector || o.mozMatchesSelector || o.oMatchesSelector || o.msMatchesSelector)) && ja(function (a) { c.disconnectedMatch = s.call(a, "div"), s.call(a, "[s!='']:x"), r.push("!=", P) }), q = q.length && new RegExp(q.join("|")), r = r.length && new RegExp(r.join("|")), b = $.test(o.compareDocumentPosition), t = b || $.test(o.contains) ? function (a, b) { var c = 9 === a.nodeType ? a.documentElement : a, d = b && b.parentNode; return a === d || !(!d || 1 !== d.nodeType || !(c.contains ? c.contains(d) : a.compareDocumentPosition && 16 & a.compareDocumentPosition(d))) } : function (a, b) { if (b) while (b = b.parentNode) if (b === a) return !0; return !1 }, B = b ? function (a, b) { if (a === b) return l = !0, 0; var d = !a.compareDocumentPosition - !b.compareDocumentPosition; return d ? d : (d = (a.ownerDocument || a) === (b.ownerDocument || b) ? a.compareDocumentPosition(b) : 1, 1 & d || !c.sortDetached && b.compareDocumentPosition(a) === d ? a === g || a.ownerDocument === v && t(v, a) ? -1 : b === g || b.ownerDocument === v && t(v, b) ? 1 : k ? J(k, a) - J(k, b) : 0 : 4 & d ? -1 : 1) } : function (a, b) { if (a === b) return l = !0, 0; var c, d = 0, e = a.parentNode, f = b.parentNode, h = [a], i = [b]; if (!e || !f) return a === g ? -1 : b === g ? 1 : e ? -1 : f ? 1 : k ? J(k, a) - J(k, b) : 0; if (e === f) return la(a, b); c = a; while (c = c.parentNode) h.unshift(c); c = b; while (c = c.parentNode) i.unshift(c); while (h[d] === i[d]) d++; return d ? la(h[d], i[d]) : h[d] === v ? -1 : i[d] === v ? 1 : 0 }, g) : n }, ga.matches = function (a, b) { return ga(a, null, null, b) }, ga.matchesSelector = function (a, b) { if ((a.ownerDocument || a) !== n && m(a), b = b.replace(U, "='$1']"), !(!c.matchesSelector || !p || r && r.test(b) || q && q.test(b))) try { var d = s.call(a, b); if (d || c.disconnectedMatch || a.document && 11 !== a.document.nodeType) return d } catch (e) { } return ga(b, n, null, [a]).length > 0 }, ga.contains = function (a, b) { return (a.ownerDocument || a) !== n && m(a), t(a, b) }, ga.attr = function (a, b) { (a.ownerDocument || a) !== n && m(a); var e = d.attrHandle[b.toLowerCase()], f = e && D.call(d.attrHandle, b.toLowerCase()) ? e(a, b, !p) : void 0; return void 0 !== f ? f : c.attributes || !p ? a.getAttribute(b) : (f = a.getAttributeNode(b)) && f.specified ? f.value : null }, ga.error = function (a) { throw new Error("Syntax error, unrecognized expression: " + a) }, ga.uniqueSort = function (a) { var b, d = [], e = 0, f = 0; if (l = !c.detectDuplicates, k = !c.sortStable && a.slice(0), a.sort(B), l) { while (b = a[f++]) b === a[f] && (e = d.push(f)); while (e--) a.splice(d[e], 1) } return k = null, a }, e = ga.getText = function (a) { var b, c = "", d = 0, f = a.nodeType; if (f) { if (1 === f || 9 === f || 11 === f) { if ("string" == typeof a.textContent) return a.textContent; for (a = a.firstChild; a; a = a.nextSibling)c += e(a) } else if (3 === f || 4 === f) return a.nodeValue } else while (b = a[d++]) c += e(b); return c }, d = ga.selectors = { cacheLength: 50, createPseudo: ia, match: X, attrHandle: {}, find: {}, relative: { ">": { dir: "parentNode", first: !0 }, " ": { dir: "parentNode" }, "+": { dir: "previousSibling", first: !0 }, "~": { dir: "previousSibling" } }, preFilter: { ATTR: function (a) { return a[1] = a[1].replace(ca, da), a[3] = (a[3] || a[4] || a[5] || "").replace(ca, da), "~=" === a[2] && (a[3] = " " + a[3] + " "), a.slice(0, 4) }, CHILD: function (a) { return a[1] = a[1].toLowerCase(), "nth" === a[1].slice(0, 3) ? (a[3] || ga.error(a[0]), a[4] = +(a[4] ? a[5] + (a[6] || 1) : 2 * ("even" === a[3] || "odd" === a[3])), a[5] = +(a[7] + a[8] || "odd" === a[3])) : a[3] && ga.error(a[0]), a }, PSEUDO: function (a) { var b, c = !a[6] && a[2]; return X.CHILD.test(a[0]) ? null : (a[3] ? a[2] = a[4] || a[5] || "" : c && V.test(c) && (b = g(c, !0)) && (b = c.indexOf(")", c.length - b) - c.length) && (a[0] = a[0].slice(0, b), a[2] = c.slice(0, b)), a.slice(0, 3)) } }, filter: { TAG: function (a) { var b = a.replace(ca, da).toLowerCase(); return "*" === a ? function () { return !0 } : function (a) { return a.nodeName && a.nodeName.toLowerCase() === b } }, CLASS: function (a) { var b = y[a + " "]; return b || (b = new RegExp("(^|" + L + ")" + a + "(" + L + "|$)")) && y(a, function (a) { return b.test("string" == typeof a.className && a.className || "undefined" != typeof a.getAttribute && a.getAttribute("class") || "") }) }, ATTR: function (a, b, c) { return function (d) { var e = ga.attr(d, a); return null == e ? "!=" === b : b ? (e += "", "=" === b ? e === c : "!=" === b ? e !== c : "^=" === b ? c && 0 === e.indexOf(c) : "*=" === b ? c && e.indexOf(c) > -1 : "$=" === b ? c && e.slice(-c.length) === c : "~=" === b ? (" " + e.replace(Q, " ") + " ").indexOf(c) > -1 : "|=" === b ? e === c || e.slice(0, c.length + 1) === c + "-" : !1) : !0 } }, CHILD: function (a, b, c, d, e) { var f = "nth" !== a.slice(0, 3), g = "last" !== a.slice(-4), h = "of-type" === b; return 1 === d && 0 === e ? function (a) { return !!a.parentNode } : function (b, c, i) { var j, k, l, m, n, o, p = f !== g ? "nextSibling" : "previousSibling", q = b.parentNode, r = h && b.nodeName.toLowerCase(), s = !i && !h; if (q) { if (f) { while (p) { l = b; while (l = l[p]) if (h ? l.nodeName.toLowerCase() === r : 1 === l.nodeType) return !1; o = p = "only" === a && !o && "nextSibling" } return !0 } if (o = [g ? q.firstChild : q.lastChild], g && s) { k = q[u] || (q[u] = {}), j = k[a] || [], n = j[0] === w && j[1], m = j[0] === w && j[2], l = n && q.childNodes[n]; while (l = ++n && l && l[p] || (m = n = 0) || o.pop()) if (1 === l.nodeType && ++m && l === b) { k[a] = [w, n, m]; break } } else if (s && (j = (b[u] || (b[u] = {}))[a]) && j[0] === w) m = j[1]; else while (l = ++n && l && l[p] || (m = n = 0) || o.pop()) if ((h ? l.nodeName.toLowerCase() === r : 1 === l.nodeType) && ++m && (s && ((l[u] || (l[u] = {}))[a] = [w, m]), l === b)) break; return m -= e, m === d || m % d === 0 && m / d >= 0 } } }, PSEUDO: function (a, b) { var c, e = d.pseudos[a] || d.setFilters[a.toLowerCase()] || ga.error("unsupported pseudo: " + a); return e[u] ? e(b) : e.length > 1 ? (c = [a, a, "", b], d.setFilters.hasOwnProperty(a.toLowerCase()) ? ia(function (a, c) { var d, f = e(a, b), g = f.length; while (g--) d = J(a, f[g]), a[d] = !(c[d] = f[g]) }) : function (a) { return e(a, 0, c) }) : e } }, pseudos: { not: ia(function (a) { var b = [], c = [], d = h(a.replace(R, "$1")); return d[u] ? ia(function (a, b, c, e) { var f, g = d(a, null, e, []), h = a.length; while (h--) (f = g[h]) && (a[h] = !(b[h] = f)) }) : function (a, e, f) { return b[0] = a, d(b, null, f, c), b[0] = null, !c.pop() } }), has: ia(function (a) { return function (b) { return ga(a, b).length > 0 } }), contains: ia(function (a) { return a = a.replace(ca, da), function (b) { return (b.textContent || b.innerText || e(b)).indexOf(a) > -1 } }), lang: ia(function (a) { return W.test(a || "") || ga.error("unsupported lang: " + a), a = a.replace(ca, da).toLowerCase(), function (b) { var c; do if (c = p ? b.lang : b.getAttribute("xml:lang") || b.getAttribute("lang")) return c = c.toLowerCase(), c === a || 0 === c.indexOf(a + "-"); while ((b = b.parentNode) && 1 === b.nodeType); return !1 } }), target: function (b) { var c = a.location && a.location.hash; return c && c.slice(1) === b.id }, root: function (a) { return a === o }, focus: function (a) { return a === n.activeElement && (!n.hasFocus || n.hasFocus()) && !!(a.type || a.href || ~a.tabIndex) }, enabled: function (a) { return a.disabled === !1 }, disabled: function (a) { return a.disabled === !0 }, checked: function (a) { var b = a.nodeName.toLowerCase(); return "input" === b && !!a.checked || "option" === b && !!a.selected }, selected: function (a) { return a.parentNode && a.parentNode.selectedIndex, a.selected === !0 }, empty: function (a) { for (a = a.firstChild; a; a = a.nextSibling)if (a.nodeType < 6) return !1; return !0 }, parent: function (a) { return !d.pseudos.empty(a) }, header: function (a) { return Z.test(a.nodeName) }, input: function (a) { return Y.test(a.nodeName) }, button: function (a) { var b = a.nodeName.toLowerCase(); return "input" === b && "button" === a.type || "button" === b }, text: function (a) { var b; return "input" === a.nodeName.toLowerCase() && "text" === a.type && (null == (b = a.getAttribute("type")) || "text" === b.toLowerCase()) }, first: oa(function () { return [0] }), last: oa(function (a, b) { return [b - 1] }), eq: oa(function (a, b, c) { return [0 > c ? c + b : c] }), even: oa(function (a, b) { for (var c = 0; b > c; c += 2)a.push(c); return a }), odd: oa(function (a, b) { for (var c = 1; b > c; c += 2)a.push(c); return a }), lt: oa(function (a, b, c) { for (var d = 0 > c ? c + b : c; --d >= 0;)a.push(d); return a }), gt: oa(function (a, b, c) { for (var d = 0 > c ? c + b : c; ++d < b;)a.push(d); return a }) } }, d.pseudos.nth = d.pseudos.eq; for (b in { radio: !0, checkbox: !0, file: !0, password: !0, image: !0 }) d.pseudos[b] = ma(b); for (b in { submit: !0, reset: !0 }) d.pseudos[b] = na(b); function qa() { } qa.prototype = d.filters = d.pseudos, d.setFilters = new qa, g = ga.tokenize = function (a, b) { var c, e, f, g, h, i, j, k = z[a + " "]; if (k) return b ? 0 : k.slice(0); h = a, i = [], j = d.preFilter; while (h) { (!c || (e = S.exec(h))) && (e && (h = h.slice(e[0].length) || h), i.push(f = [])), c = !1, (e = T.exec(h)) && (c = e.shift(), f.push({ value: c, type: e[0].replace(R, " ") }), h = h.slice(c.length)); for (g in d.filter) !(e = X[g].exec(h)) || j[g] && !(e = j[g](e)) || (c = e.shift(), f.push({ value: c, type: g, matches: e }), h = h.slice(c.length)); if (!c) break } return b ? h.length : h ? ga.error(a) : z(a, i).slice(0) }; function ra(a) { for (var b = 0, c = a.length, d = ""; c > b; b++)d += a[b].value; return d } function sa(a, b, c) { var d = b.dir, e = c && "parentNode" === d, f = x++; return b.first ? function (b, c, f) { while (b = b[d]) if (1 === b.nodeType || e) return a(b, c, f) } : function (b, c, g) { var h, i, j = [w, f]; if (g) { while (b = b[d]) if ((1 === b.nodeType || e) && a(b, c, g)) return !0 } else while (b = b[d]) if (1 === b.nodeType || e) { if (i = b[u] || (b[u] = {}), (h = i[d]) && h[0] === w && h[1] === f) return j[2] = h[2]; if (i[d] = j, j[2] = a(b, c, g)) return !0 } } } function ta(a) { return a.length > 1 ? function (b, c, d) { var e = a.length; while (e--) if (!a[e](b, c, d)) return !1; return !0 } : a[0] } function ua(a, b, c) { for (var d = 0, e = b.length; e > d; d++)ga(a, b[d], c); return c } function va(a, b, c, d, e) { for (var f, g = [], h = 0, i = a.length, j = null != b; i > h; h++)(f = a[h]) && (!c || c(f, d, e)) && (g.push(f), j && b.push(h)); return g } function wa(a, b, c, d, e, f) { return d && !d[u] && (d = wa(d)), e && !e[u] && (e = wa(e, f)), ia(function (f, g, h, i) { var j, k, l, m = [], n = [], o = g.length, p = f || ua(b || "*", h.nodeType ? [h] : h, []), q = !a || !f && b ? p : va(p, m, a, h, i), r = c ? e || (f ? a : o || d) ? [] : g : q; if (c && c(q, r, h, i), d) { j = va(r, n), d(j, [], h, i), k = j.length; while (k--) (l = j[k]) && (r[n[k]] = !(q[n[k]] = l)) } if (f) { if (e || a) { if (e) { j = [], k = r.length; while (k--) (l = r[k]) && j.push(q[k] = l); e(null, r = [], j, i) } k = r.length; while (k--) (l = r[k]) && (j = e ? J(f, l) : m[k]) > -1 && (f[j] = !(g[j] = l)) } } else r = va(r === g ? r.splice(o, r.length) : r), e ? e(null, g, r, i) : H.apply(g, r) }) } function xa(a) { for (var b, c, e, f = a.length, g = d.relative[a[0].type], h = g || d.relative[" "], i = g ? 1 : 0, k = sa(function (a) { return a === b }, h, !0), l = sa(function (a) { return J(b, a) > -1 }, h, !0), m = [function (a, c, d) { var e = !g && (d || c !== j) || ((b = c).nodeType ? k(a, c, d) : l(a, c, d)); return b = null, e }]; f > i; i++)if (c = d.relative[a[i].type]) m = [sa(ta(m), c)]; else { if (c = d.filter[a[i].type].apply(null, a[i].matches), c[u]) { for (e = ++i; f > e; e++)if (d.relative[a[e].type]) break; return wa(i > 1 && ta(m), i > 1 && ra(a.slice(0, i - 1).concat({ value: " " === a[i - 2].type ? "*" : "" })).replace(R, "$1"), c, e > i && xa(a.slice(i, e)), f > e && xa(a = a.slice(e)), f > e && ra(a)) } m.push(c) } return ta(m) } function ya(a, b) { var c = b.length > 0, e = a.length > 0, f = function (f, g, h, i, k) { var l, m, o, p = 0, q = "0", r = f && [], s = [], t = j, u = f || e && d.find.TAG("*", k), v = w += null == t ? 1 : Math.random() || .1, x = u.length; for (k && (j = g !== n && g); q !== x && null != (l = u[q]); q++) { if (e && l) { m = 0; while (o = a[m++]) if (o(l, g, h)) { i.push(l); break } k && (w = v) } c && ((l = !o && l) && p--, f && r.push(l)) } if (p += q, c && q !== p) { m = 0; while (o = b[m++]) o(r, s, g, h); if (f) { if (p > 0) while (q--) r[q] || s[q] || (s[q] = F.call(i)); s = va(s) } H.apply(i, s), k && !f && s.length > 0 && p + b.length > 1 && ga.uniqueSort(i) } return k && (w = v, j = t), r }; return c ? ia(f) : f } return h = ga.compile = function (a, b) { var c, d = [], e = [], f = A[a + " "]; if (!f) { b || (b = g(a)), c = b.length; while (c--) f = xa(b[c]), f[u] ? d.push(f) : e.push(f); f = A(a, ya(e, d)), f.selector = a } return f }, i = ga.select = function (a, b, e, f) { var i, j, k, l, m, n = "function" == typeof a && a, o = !f && g(a = n.selector || a); if (e = e || [], 1 === o.length) { if (j = o[0] = o[0].slice(0), j.length > 2 && "ID" === (k = j[0]).type && c.getById && 9 === b.nodeType && p && d.relative[j[1].type]) { if (b = (d.find.ID(k.matches[0].replace(ca, da), b) || [])[0], !b) return e; n && (b = b.parentNode), a = a.slice(j.shift().value.length) } i = X.needsContext.test(a) ? 0 : j.length; while (i--) { if (k = j[i], d.relative[l = k.type]) break; if ((m = d.find[l]) && (f = m(k.matches[0].replace(ca, da), aa.test(j[0].type) && pa(b.parentNode) || b))) { if (j.splice(i, 1), a = f.length && ra(j), !a) return H.apply(e, f), e; break } } } return (n || h(a, o))(f, b, !p, e, aa.test(a) && pa(b.parentNode) || b), e }, c.sortStable = u.split("").sort(B).join("") === u, c.detectDuplicates = !!l, m(), c.sortDetached = ja(function (a) { return 1 & a.compareDocumentPosition(n.createElement("div")) }), ja(function (a) { return a.innerHTML = "<a href='#'></a>", "#" === a.firstChild.getAttribute("href") }) || ka("type|href|height|width", function (a, b, c) { return c ? void 0 : a.getAttribute(b, "type" === b.toLowerCase() ? 1 : 2) }), c.attributes && ja(function (a) { return a.innerHTML = "<input/>", a.firstChild.setAttribute("value", ""), "" === a.firstChild.getAttribute("value") }) || ka("value", function (a, b, c) { return c || "input" !== a.nodeName.toLowerCase() ? void 0 : a.defaultValue }), ja(function (a) { return null == a.getAttribute("disabled") }) || ka(K, function (a, b, c) { var d; return c ? void 0 : a[b] === !0 ? b.toLowerCase() : (d = a.getAttributeNode(b)) && d.specified ? d.value : null }), ga }(a); m.find = s, m.expr = s.selectors, m.expr[":"] = m.expr.pseudos, m.unique = s.uniqueSort, m.text = s.getText, m.isXMLDoc = s.isXML, m.contains = s.contains; var t = m.expr.match.needsContext, u = /^<(\w+)\s*\/?>(?:<\/\1>|)$/, v = /^.[^:#\[\.,]*$/; function w(a, b, c) { if (m.isFunction(b)) return m.grep(a, function (a, d) { return !!b.call(a, d, a) !== c }); if (b.nodeType) return m.grep(a, function (a) { return a === b !== c }); if ("string" == typeof b) { if (v.test(b)) return m.filter(b, a, c); b = m.filter(b, a) } return m.grep(a, function (a) { return m.inArray(a, b) >= 0 !== c }) } m.filter = function (a, b, c) { var d = b[0]; return c && (a = ":not(" + a + ")"), 1 === b.length && 1 === d.nodeType ? m.find.matchesSelector(d, a) ? [d] : [] : m.find.matches(a, m.grep(b, function (a) { return 1 === a.nodeType })) }, m.fn.extend({ find: function (a) { var b, c = [], d = this, e = d.length; if ("string" != typeof a) return this.pushStack(m(a).filter(function () { for (b = 0; e > b; b++)if (m.contains(d[b], this)) return !0 })); for (b = 0; e > b; b++)m.find(a, d[b], c); return c = this.pushStack(e > 1 ? m.unique(c) : c), c.selector = this.selector ? this.selector + " " + a : a, c }, filter: function (a) { return this.pushStack(w(this, a || [], !1)) }, not: function (a) { return this.pushStack(w(this, a || [], !0)) }, is: function (a) { return !!w(this, "string" == typeof a && t.test(a) ? m(a) : a || [], !1).length } }); var x, y = a.document, z = /^(?:\s*(<[\w\W]+>)[^>]*|#([\w-]*))$/, A = m.fn.init = function (a, b) { var c, d; if (!a) return this; if ("string" == typeof a) { if (c = "<" === a.charAt(0) && ">" === a.charAt(a.length - 1) && a.length >= 3 ? [null, a, null] : z.exec(a), !c || !c[1] && b) return !b || b.jquery ? (b || x).find(a) : this.constructor(b).find(a); if (c[1]) { if (b = b instanceof m ? b[0] : b, m.merge(this, m.parseHTML(c[1], b && b.nodeType ? b.ownerDocument || b : y, !0)), u.test(c[1]) && m.isPlainObject(b)) for (c in b) m.isFunction(this[c]) ? this[c](b[c]) : this.attr(c, b[c]); return this } if (d = y.getElementById(c[2]), d && d.parentNode) { if (d.id !== c[2]) return x.find(a); this.length = 1, this[0] = d } return this.context = y, this.selector = a, this } return a.nodeType ? (this.context = this[0] = a, this.length = 1, this) : m.isFunction(a) ? "undefined" != typeof x.ready ? x.ready(a) : a(m) : (void 0 !== a.selector && (this.selector = a.selector, this.context = a.context), m.makeArray(a, this)) }; A.prototype = m.fn, x = m(y); var B = /^(?:parents|prev(?:Until|All))/, C = { children: !0, contents: !0, next: !0, prev: !0 }; m.extend({ dir: function (a, b, c) { var d = [], e = a[b]; while (e && 9 !== e.nodeType && (void 0 === c || 1 !== e.nodeType || !m(e).is(c))) 1 === e.nodeType && d.push(e), e = e[b]; return d }, sibling: function (a, b) { for (var c = []; a; a = a.nextSibling)1 === a.nodeType && a !== b && c.push(a); return c } }), m.fn.extend({ has: function (a) { var b, c = m(a, this), d = c.length; return this.filter(function () { for (b = 0; d > b; b++)if (m.contains(this, c[b])) return !0 }) }, closest: function (a, b) { for (var c, d = 0, e = this.length, f = [], g = t.test(a) || "string" != typeof a ? m(a, b || this.context) : 0; e > d; d++)for (c = this[d]; c && c !== b; c = c.parentNode)if (c.nodeType < 11 && (g ? g.index(c) > -1 : 1 === c.nodeType && m.find.matchesSelector(c, a))) { f.push(c); break } return this.pushStack(f.length > 1 ? m.unique(f) : f) }, index: function (a) { return a ? "string" == typeof a ? m.inArray(this[0], m(a)) : m.inArray(a.jquery ? a[0] : a, this) : this[0] && this[0].parentNode ? this.first().prevAll().length : -1 }, add: function (a, b) { return this.pushStack(m.unique(m.merge(this.get(), m(a, b)))) }, addBack: function (a) { return this.add(null == a ? this.prevObject : this.prevObject.filter(a)) } }); function D(a, b) { do a = a[b]; while (a && 1 !== a.nodeType); return a } m.each({ parent: function (a) { var b = a.parentNode; return b && 11 !== b.nodeType ? b : null }, parents: function (a) { return m.dir(a, "parentNode") }, parentsUntil: function (a, b, c) { return m.dir(a, "parentNode", c) }, next: function (a) { return D(a, "nextSibling") }, prev: function (a) { return D(a, "previousSibling") }, nextAll: function (a) { return m.dir(a, "nextSibling") }, prevAll: function (a) { return m.dir(a, "previousSibling") }, nextUntil: function (a, b, c) { return m.dir(a, "nextSibling", c) }, prevUntil: function (a, b, c) { return m.dir(a, "previousSibling", c) }, siblings: function (a) { return m.sibling((a.parentNode || {}).firstChild, a) }, children: function (a) { return m.sibling(a.firstChild) }, contents: function (a) { return m.nodeName(a, "iframe") ? a.contentDocument || a.contentWindow.document : m.merge([], a.childNodes) } }, function (a, b) { m.fn[a] = function (c, d) { var e = m.map(this, b, c); return "Until" !== a.slice(-5) && (d = c), d && "string" == typeof d && (e = m.filter(d, e)), this.length > 1 && (C[a] || (e = m.unique(e)), B.test(a) && (e = e.reverse())), this.pushStack(e) } }); var E = /\S+/g, F = {}; function G(a) { var b = F[a] = {}; return m.each(a.match(E) || [], function (a, c) { b[c] = !0 }), b } m.Callbacks = function (a) { a = "string" == typeof a ? F[a] || G(a) : m.extend({}, a); var b, c, d, e, f, g, h = [], i = !a.once && [], j = function (l) { for (c = a.memory && l, d = !0, f = g || 0, g = 0, e = h.length, b = !0; h && e > f; f++)if (h[f].apply(l[0], l[1]) === !1 && a.stopOnFalse) { c = !1; break } b = !1, h && (i ? i.length && j(i.shift()) : c ? h = [] : k.disable()) }, k = { add: function () { if (h) { var d = h.length; !function f(b) { m.each(b, function (b, c) { var d = m.type(c); "function" === d ? a.unique && k.has(c) || h.push(c) : c && c.length && "string" !== d && f(c) }) }(arguments), b ? e = h.length : c && (g = d, j(c)) } return this }, remove: function () { return h && m.each(arguments, function (a, c) { var d; while ((d = m.inArray(c, h, d)) > -1) h.splice(d, 1), b && (e >= d && e--, f >= d && f--) }), this }, has: function (a) { return a ? m.inArray(a, h) > -1 : !(!h || !h.length) }, empty: function () { return h = [], e = 0, this }, disable: function () { return h = i = c = void 0, this }, disabled: function () { return !h }, lock: function () { return i = void 0, c || k.disable(), this }, locked: function () { return !i }, fireWith: function (a, c) { return !h || d && !i || (c = c || [], c = [a, c.slice ? c.slice() : c], b ? i.push(c) : j(c)), this }, fire: function () { return k.fireWith(this, arguments), this }, fired: function () { return !!d } }; return k }, m.extend({ Deferred: function (a) { var b = [["resolve", "done", m.Callbacks("once memory"), "resolved"], ["reject", "fail", m.Callbacks("once memory"), "rejected"], ["notify", "progress", m.Callbacks("memory")]], c = "pending", d = { state: function () { return c }, always: function () { return e.done(arguments).fail(arguments), this }, then: function () { var a = arguments; return m.Deferred(function (c) { m.each(b, function (b, f) { var g = m.isFunction(a[b]) && a[b]; e[f[1]](function () { var a = g && g.apply(this, arguments); a && m.isFunction(a.promise) ? a.promise().done(c.resolve).fail(c.reject).progress(c.notify) : c[f[0] + "With"](this === d ? c.promise() : this, g ? [a] : arguments) }) }), a = null }).promise() }, promise: function (a) { return null != a ? m.extend(a, d) : d } }, e = {}; return d.pipe = d.then, m.each(b, function (a, f) { var g = f[2], h = f[3]; d[f[1]] = g.add, h && g.add(function () { c = h }, b[1 ^ a][2].disable, b[2][2].lock), e[f[0]] = function () { return e[f[0] + "With"](this === e ? d : this, arguments), this }, e[f[0] + "With"] = g.fireWith }), d.promise(e), a && a.call(e, e), e }, when: function (a) { var b = 0, c = d.call(arguments), e = c.length, f = 1 !== e || a && m.isFunction(a.promise) ? e : 0, g = 1 === f ? a : m.Deferred(), h = function (a, b, c) { return function (e) { b[a] = this, c[a] = arguments.length > 1 ? d.call(arguments) : e, c === i ? g.notifyWith(b, c) : --f || g.resolveWith(b, c) } }, i, j, k; if (e > 1) for (i = new Array(e), j = new Array(e), k = new Array(e); e > b; b++)c[b] && m.isFunction(c[b].promise) ? c[b].promise().done(h(b, k, c)).fail(g.reject).progress(h(b, j, i)) : --f; return f || g.resolveWith(k, c), g.promise() } }); var H; m.fn.ready = function (a) { return m.ready.promise().done(a), this }, m.extend({ isReady: !1, readyWait: 1, holdReady: function (a) { a ? m.readyWait++ : m.ready(!0) }, ready: function (a) { if (a === !0 ? !--m.readyWait : !m.isReady) { if (!y.body) return setTimeout(m.ready); m.isReady = !0, a !== !0 && --m.readyWait > 0 || (H.resolveWith(y, [m]), m.fn.triggerHandler && (m(y).triggerHandler("ready"), m(y).off("ready"))) } } }); function I() { y.addEventListener ? (y.removeEventListener("DOMContentLoaded", J, !1), a.removeEventListener("load", J, !1)) : (y.detachEvent("onreadystatechange", J), a.detachEvent("onload", J)) } function J() { (y.addEventListener || "load" === event.type || "complete" === y.readyState) && (I(), m.ready()) } m.ready.promise = function (b) { if (!H) if (H = m.Deferred(), "complete" === y.readyState) setTimeout(m.ready); else if (y.addEventListener) y.addEventListener("DOMContentLoaded", J, !1), a.addEventListener("load", J, !1); else { y.attachEvent("onreadystatechange", J), a.attachEvent("onload", J); var c = !1; try { c = null == a.frameElement && y.documentElement } catch (d) { } c && c.doScroll && !function e() { if (!m.isReady) { try { c.doScroll("left") } catch (a) { return setTimeout(e, 50) } I(), m.ready() } }() } return H.promise(b) }; var K = "undefined", L; for (L in m(k)) break; k.ownLast = "0" !== L, k.inlineBlockNeedsLayout = !1, m(function () { var a, b, c, d; c = y.getElementsByTagName("body")[0], c && c.style && (b = y.createElement("div"), d = y.createElement("div"), d.style.cssText = "position:absolute;border:0;width:0;height:0;top:0;left:-9999px", c.appendChild(d).appendChild(b), typeof b.style.zoom !== K && (b.style.cssText = "display:inline;margin:0;border:0;padding:1px;width:1px;zoom:1", k.inlineBlockNeedsLayout = a = 3 === b.offsetWidth, a && (c.style.zoom = 1)), c.removeChild(d)) }), function () { var a = y.createElement("div"); if (null == k.deleteExpando) { k.deleteExpando = !0; try { delete a.test } catch (b) { k.deleteExpando = !1 } } a = null }(), m.acceptData = function (a) { var b = m.noData[(a.nodeName + " ").toLowerCase()], c = +a.nodeType || 1; return 1 !== c && 9 !== c ? !1 : !b || b !== !0 && a.getAttribute("classid") === b }; var M = /^(?:\{[\w\W]*\}|\[[\w\W]*\])$/, N = /([A-Z])/g; function O(a, b, c) { if (void 0 === c && 1 === a.nodeType) { var d = "data-" + b.replace(N, "-$1").toLowerCase(); if (c = a.getAttribute(d), "string" == typeof c) { try { c = "true" === c ? !0 : "false" === c ? !1 : "null" === c ? null : +c + "" === c ? +c : M.test(c) ? m.parseJSON(c) : c } catch (e) { } m.data(a, b, c) } else c = void 0 } return c } function P(a) {
        var b; for (b in a) if (("data" !== b || !m.isEmptyObject(a[b])) && "toJSON" !== b) return !1;

        return !0
    } function Q(a, b, d, e) { if (m.acceptData(a)) { var f, g, h = m.expando, i = a.nodeType, j = i ? m.cache : a, k = i ? a[h] : a[h] && h; if (k && j[k] && (e || j[k].data) || void 0 !== d || "string" != typeof b) return k || (k = i ? a[h] = c.pop() || m.guid++ : h), j[k] || (j[k] = i ? {} : { toJSON: m.noop }), ("object" == typeof b || "function" == typeof b) && (e ? j[k] = m.extend(j[k], b) : j[k].data = m.extend(j[k].data, b)), g = j[k], e || (g.data || (g.data = {}), g = g.data), void 0 !== d && (g[m.camelCase(b)] = d), "string" == typeof b ? (f = g[b], null == f && (f = g[m.camelCase(b)])) : f = g, f } } function R(a, b, c) { if (m.acceptData(a)) { var d, e, f = a.nodeType, g = f ? m.cache : a, h = f ? a[m.expando] : m.expando; if (g[h]) { if (b && (d = c ? g[h] : g[h].data)) { m.isArray(b) ? b = b.concat(m.map(b, m.camelCase)) : b in d ? b = [b] : (b = m.camelCase(b), b = b in d ? [b] : b.split(" ")), e = b.length; while (e--) delete d[b[e]]; if (c ? !P(d) : !m.isEmptyObject(d)) return } (c || (delete g[h].data, P(g[h]))) && (f ? m.cleanData([a], !0) : k.deleteExpando || g != g.window ? delete g[h] : g[h] = null) } } } m.extend({ cache: {}, noData: { "applet ": !0, "embed ": !0, "object ": "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" }, hasData: function (a) { return a = a.nodeType ? m.cache[a[m.expando]] : a[m.expando], !!a && !P(a) }, data: function (a, b, c) { return Q(a, b, c) }, removeData: function (a, b) { return R(a, b) }, _data: function (a, b, c) { return Q(a, b, c, !0) }, _removeData: function (a, b) { return R(a, b, !0) } }), m.fn.extend({ data: function (a, b) { var c, d, e, f = this[0], g = f && f.attributes; if (void 0 === a) { if (this.length && (e = m.data(f), 1 === f.nodeType && !m._data(f, "parsedAttrs"))) { c = g.length; while (c--) g[c] && (d = g[c].name, 0 === d.indexOf("data-") && (d = m.camelCase(d.slice(5)), O(f, d, e[d]))); m._data(f, "parsedAttrs", !0) } return e } return "object" == typeof a ? this.each(function () { m.data(this, a) }) : arguments.length > 1 ? this.each(function () { m.data(this, a, b) }) : f ? O(f, a, m.data(f, a)) : void 0 }, removeData: function (a) { return this.each(function () { m.removeData(this, a) }) } }), m.extend({ queue: function (a, b, c) { var d; return a ? (b = (b || "fx") + "queue", d = m._data(a, b), c && (!d || m.isArray(c) ? d = m._data(a, b, m.makeArray(c)) : d.push(c)), d || []) : void 0 }, dequeue: function (a, b) { b = b || "fx"; var c = m.queue(a, b), d = c.length, e = c.shift(), f = m._queueHooks(a, b), g = function () { m.dequeue(a, b) }; "inprogress" === e && (e = c.shift(), d--), e && ("fx" === b && c.unshift("inprogress"), delete f.stop, e.call(a, g, f)), !d && f && f.empty.fire() }, _queueHooks: function (a, b) { var c = b + "queueHooks"; return m._data(a, c) || m._data(a, c, { empty: m.Callbacks("once memory").add(function () { m._removeData(a, b + "queue"), m._removeData(a, c) }) }) } }), m.fn.extend({ queue: function (a, b) { var c = 2; return "string" != typeof a && (b = a, a = "fx", c--), arguments.length < c ? m.queue(this[0], a) : void 0 === b ? this : this.each(function () { var c = m.queue(this, a, b); m._queueHooks(this, a), "fx" === a && "inprogress" !== c[0] && m.dequeue(this, a) }) }, dequeue: function (a) { return this.each(function () { m.dequeue(this, a) }) }, clearQueue: function (a) { return this.queue(a || "fx", []) }, promise: function (a, b) { var c, d = 1, e = m.Deferred(), f = this, g = this.length, h = function () { --d || e.resolveWith(f, [f]) }; "string" != typeof a && (b = a, a = void 0), a = a || "fx"; while (g--) c = m._data(f[g], a + "queueHooks"), c && c.empty && (d++, c.empty.add(h)); return h(), e.promise(b) } }); var S = /[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source, T = ["Top", "Right", "Bottom", "Left"], U = function (a, b) { return a = b || a, "none" === m.css(a, "display") || !m.contains(a.ownerDocument, a) }, V = m.access = function (a, b, c, d, e, f, g) { var h = 0, i = a.length, j = null == c; if ("object" === m.type(c)) { e = !0; for (h in c) m.access(a, b, h, c[h], !0, f, g) } else if (void 0 !== d && (e = !0, m.isFunction(d) || (g = !0), j && (g ? (b.call(a, d), b = null) : (j = b, b = function (a, b, c) { return j.call(m(a), c) })), b)) for (; i > h; h++)b(a[h], c, g ? d : d.call(a[h], h, b(a[h], c))); return e ? a : j ? b.call(a) : i ? b(a[0], c) : f }, W = /^(?:checkbox|radio)$/i; !function () { var a = y.createElement("input"), b = y.createElement("div"), c = y.createDocumentFragment(); if (b.innerHTML = "  <link/><table></table><a href='/a'>a</a><input type='checkbox'/>", k.leadingWhitespace = 3 === b.firstChild.nodeType, k.tbody = !b.getElementsByTagName("tbody").length, k.htmlSerialize = !!b.getElementsByTagName("link").length, k.html5Clone = "<:nav></:nav>" !== y.createElement("nav").cloneNode(!0).outerHTML, a.type = "checkbox", a.checked = !0, c.appendChild(a), k.appendChecked = a.checked, b.innerHTML = "<textarea>x</textarea>", k.noCloneChecked = !!b.cloneNode(!0).lastChild.defaultValue, c.appendChild(b), b.innerHTML = "<input type='radio' checked='checked' name='t'/>", k.checkClone = b.cloneNode(!0).cloneNode(!0).lastChild.checked, k.noCloneEvent = !0, b.attachEvent && (b.attachEvent("onclick", function () { k.noCloneEvent = !1 }), b.cloneNode(!0).click()), null == k.deleteExpando) { k.deleteExpando = !0; try { delete b.test } catch (d) { k.deleteExpando = !1 } } }(), function () { var b, c, d = y.createElement("div"); for (b in { submit: !0, change: !0, focusin: !0 }) c = "on" + b, (k[b + "Bubbles"] = c in a) || (d.setAttribute(c, "t"), k[b + "Bubbles"] = d.attributes[c].expando === !1); d = null }(); var X = /^(?:input|select|textarea)$/i, Y = /^key/, Z = /^(?:mouse|pointer|contextmenu)|click/, $ = /^(?:focusinfocus|focusoutblur)$/, _ = /^([^.]*)(?:\.(.+)|)$/; function aa() { return !0 } function ba() { return !1 } function ca() { try { return y.activeElement } catch (a) { } } m.event = { global: {}, add: function (a, b, c, d, e) { var f, g, h, i, j, k, l, n, o, p, q, r = m._data(a); if (r) { c.handler && (i = c, c = i.handler, e = i.selector), c.guid || (c.guid = m.guid++), (g = r.events) || (g = r.events = {}), (k = r.handle) || (k = r.handle = function (a) { return typeof m === K || a && m.event.triggered === a.type ? void 0 : m.event.dispatch.apply(k.elem, arguments) }, k.elem = a), b = (b || "").match(E) || [""], h = b.length; while (h--) f = _.exec(b[h]) || [], o = q = f[1], p = (f[2] || "").split(".").sort(), o && (j = m.event.special[o] || {}, o = (e ? j.delegateType : j.bindType) || o, j = m.event.special[o] || {}, l = m.extend({ type: o, origType: q, data: d, handler: c, guid: c.guid, selector: e, needsContext: e && m.expr.match.needsContext.test(e), namespace: p.join(".") }, i), (n = g[o]) || (n = g[o] = [], n.delegateCount = 0, j.setup && j.setup.call(a, d, p, k) !== !1 || (a.addEventListener ? a.addEventListener(o, k, !1) : a.attachEvent && a.attachEvent("on" + o, k))), j.add && (j.add.call(a, l), l.handler.guid || (l.handler.guid = c.guid)), e ? n.splice(n.delegateCount++, 0, l) : n.push(l), m.event.global[o] = !0); a = null } }, remove: function (a, b, c, d, e) { var f, g, h, i, j, k, l, n, o, p, q, r = m.hasData(a) && m._data(a); if (r && (k = r.events)) { b = (b || "").match(E) || [""], j = b.length; while (j--) if (h = _.exec(b[j]) || [], o = q = h[1], p = (h[2] || "").split(".").sort(), o) { l = m.event.special[o] || {}, o = (d ? l.delegateType : l.bindType) || o, n = k[o] || [], h = h[2] && new RegExp("(^|\\.)" + p.join("\\.(?:.*\\.|)") + "(\\.|$)"), i = f = n.length; while (f--) g = n[f], !e && q !== g.origType || c && c.guid !== g.guid || h && !h.test(g.namespace) || d && d !== g.selector && ("**" !== d || !g.selector) || (n.splice(f, 1), g.selector && n.delegateCount--, l.remove && l.remove.call(a, g)); i && !n.length && (l.teardown && l.teardown.call(a, p, r.handle) !== !1 || m.removeEvent(a, o, r.handle), delete k[o]) } else for (o in k) m.event.remove(a, o + b[j], c, d, !0); m.isEmptyObject(k) && (delete r.handle, m._removeData(a, "events")) } }, trigger: function (b, c, d, e) { var f, g, h, i, k, l, n, o = [d || y], p = j.call(b, "type") ? b.type : b, q = j.call(b, "namespace") ? b.namespace.split(".") : []; if (h = l = d = d || y, 3 !== d.nodeType && 8 !== d.nodeType && !$.test(p + m.event.triggered) && (p.indexOf(".") >= 0 && (q = p.split("."), p = q.shift(), q.sort()), g = p.indexOf(":") < 0 && "on" + p, b = b[m.expando] ? b : new m.Event(p, "object" == typeof b && b), b.isTrigger = e ? 2 : 3, b.namespace = q.join("."), b.namespace_re = b.namespace ? new RegExp("(^|\\.)" + q.join("\\.(?:.*\\.|)") + "(\\.|$)") : null, b.result = void 0, b.target || (b.target = d), c = null == c ? [b] : m.makeArray(c, [b]), k = m.event.special[p] || {}, e || !k.trigger || k.trigger.apply(d, c) !== !1)) { if (!e && !k.noBubble && !m.isWindow(d)) { for (i = k.delegateType || p, $.test(i + p) || (h = h.parentNode); h; h = h.parentNode)o.push(h), l = h; l === (d.ownerDocument || y) && o.push(l.defaultView || l.parentWindow || a) } n = 0; while ((h = o[n++]) && !b.isPropagationStopped()) b.type = n > 1 ? i : k.bindType || p, f = (m._data(h, "events") || {})[b.type] && m._data(h, "handle"), f && f.apply(h, c), f = g && h[g], f && f.apply && m.acceptData(h) && (b.result = f.apply(h, c), b.result === !1 && b.preventDefault()); if (b.type = p, !e && !b.isDefaultPrevented() && (!k._default || k._default.apply(o.pop(), c) === !1) && m.acceptData(d) && g && d[p] && !m.isWindow(d)) { l = d[g], l && (d[g] = null), m.event.triggered = p; try { d[p]() } catch (r) { } m.event.triggered = void 0, l && (d[g] = l) } return b.result } }, dispatch: function (a) { a = m.event.fix(a); var b, c, e, f, g, h = [], i = d.call(arguments), j = (m._data(this, "events") || {})[a.type] || [], k = m.event.special[a.type] || {}; if (i[0] = a, a.delegateTarget = this, !k.preDispatch || k.preDispatch.call(this, a) !== !1) { h = m.event.handlers.call(this, a, j), b = 0; while ((f = h[b++]) && !a.isPropagationStopped()) { a.currentTarget = f.elem, g = 0; while ((e = f.handlers[g++]) && !a.isImmediatePropagationStopped()) (!a.namespace_re || a.namespace_re.test(e.namespace)) && (a.handleObj = e, a.data = e.data, c = ((m.event.special[e.origType] || {}).handle || e.handler).apply(f.elem, i), void 0 !== c && (a.result = c) === !1 && (a.preventDefault(), a.stopPropagation())) } return k.postDispatch && k.postDispatch.call(this, a), a.result } }, handlers: function (a, b) { var c, d, e, f, g = [], h = b.delegateCount, i = a.target; if (h && i.nodeType && (!a.button || "click" !== a.type)) for (; i != this; i = i.parentNode || this)if (1 === i.nodeType && (i.disabled !== !0 || "click" !== a.type)) { for (e = [], f = 0; h > f; f++)d = b[f], c = d.selector + " ", void 0 === e[c] && (e[c] = d.needsContext ? m(c, this).index(i) >= 0 : m.find(c, this, null, [i]).length), e[c] && e.push(d); e.length && g.push({ elem: i, handlers: e }) } return h < b.length && g.push({ elem: this, handlers: b.slice(h) }), g }, fix: function (a) { if (a[m.expando]) return a; var b, c, d, e = a.type, f = a, g = this.fixHooks[e]; g || (this.fixHooks[e] = g = Z.test(e) ? this.mouseHooks : Y.test(e) ? this.keyHooks : {}), d = g.props ? this.props.concat(g.props) : this.props, a = new m.Event(f), b = d.length; while (b--) c = d[b], a[c] = f[c]; return a.target || (a.target = f.srcElement || y), 3 === a.target.nodeType && (a.target = a.target.parentNode), a.metaKey = !!a.metaKey, g.filter ? g.filter(a, f) : a }, props: "altKey bubbles cancelable ctrlKey currentTarget eventPhase metaKey relatedTarget shiftKey target timeStamp view which".split(" "), fixHooks: {}, keyHooks: { props: "char charCode key keyCode".split(" "), filter: function (a, b) { return null == a.which && (a.which = null != b.charCode ? b.charCode : b.keyCode), a } }, mouseHooks: { props: "button buttons clientX clientY fromElement offsetX offsetY pageX pageY screenX screenY toElement".split(" "), filter: function (a, b) { var c, d, e, f = b.button, g = b.fromElement; return null == a.pageX && null != b.clientX && (d = a.target.ownerDocument || y, e = d.documentElement, c = d.body, a.pageX = b.clientX + (e && e.scrollLeft || c && c.scrollLeft || 0) - (e && e.clientLeft || c && c.clientLeft || 0), a.pageY = b.clientY + (e && e.scrollTop || c && c.scrollTop || 0) - (e && e.clientTop || c && c.clientTop || 0)), !a.relatedTarget && g && (a.relatedTarget = g === a.target ? b.toElement : g), a.which || void 0 === f || (a.which = 1 & f ? 1 : 2 & f ? 3 : 4 & f ? 2 : 0), a } }, special: { load: { noBubble: !0 }, focus: { trigger: function () { if (this !== ca() && this.focus) try { return this.focus(), !1 } catch (a) { } }, delegateType: "focusin" }, blur: { trigger: function () { return this === ca() && this.blur ? (this.blur(), !1) : void 0 }, delegateType: "focusout" }, click: { trigger: function () { return m.nodeName(this, "input") && "checkbox" === this.type && this.click ? (this.click(), !1) : void 0 }, _default: function (a) { return m.nodeName(a.target, "a") } }, beforeunload: { postDispatch: function (a) { void 0 !== a.result && a.originalEvent && (a.originalEvent.returnValue = a.result) } } }, simulate: function (a, b, c, d) { var e = m.extend(new m.Event, c, { type: a, isSimulated: !0, originalEvent: {} }); d ? m.event.trigger(e, null, b) : m.event.dispatch.call(b, e), e.isDefaultPrevented() && c.preventDefault() } }, m.removeEvent = y.removeEventListener ? function (a, b, c) { a.removeEventListener && a.removeEventListener(b, c, !1) } : function (a, b, c) { var d = "on" + b; a.detachEvent && (typeof a[d] === K && (a[d] = null), a.detachEvent(d, c)) }, m.Event = function (a, b) { return this instanceof m.Event ? (a && a.type ? (this.originalEvent = a, this.type = a.type, this.isDefaultPrevented = a.defaultPrevented || void 0 === a.defaultPrevented && a.returnValue === !1 ? aa : ba) : this.type = a, b && m.extend(this, b), this.timeStamp = a && a.timeStamp || m.now(), void (this[m.expando] = !0)) : new m.Event(a, b) }, m.Event.prototype = { isDefaultPrevented: ba, isPropagationStopped: ba, isImmediatePropagationStopped: ba, preventDefault: function () { var a = this.originalEvent; this.isDefaultPrevented = aa, a && (a.preventDefault ? a.preventDefault() : a.returnValue = !1) }, stopPropagation: function () { var a = this.originalEvent; this.isPropagationStopped = aa, a && (a.stopPropagation && a.stopPropagation(), a.cancelBubble = !0) }, stopImmediatePropagation: function () { var a = this.originalEvent; this.isImmediatePropagationStopped = aa, a && a.stopImmediatePropagation && a.stopImmediatePropagation(), this.stopPropagation() } }, m.each({ mouseenter: "mouseover", mouseleave: "mouseout", pointerenter: "pointerover", pointerleave: "pointerout" }, function (a, b) { m.event.special[a] = { delegateType: b, bindType: b, handle: function (a) { var c, d = this, e = a.relatedTarget, f = a.handleObj; return (!e || e !== d && !m.contains(d, e)) && (a.type = f.origType, c = f.handler.apply(this, arguments), a.type = b), c } } }), k.submitBubbles || (m.event.special.submit = { setup: function () { return m.nodeName(this, "form") ? !1 : void m.event.add(this, "click._submit keypress._submit", function (a) { var b = a.target, c = m.nodeName(b, "input") || m.nodeName(b, "button") ? b.form : void 0; c && !m._data(c, "submitBubbles") && (m.event.add(c, "submit._submit", function (a) { a._submit_bubble = !0 }), m._data(c, "submitBubbles", !0)) }) }, postDispatch: function (a) { a._submit_bubble && (delete a._submit_bubble, this.parentNode && !a.isTrigger && m.event.simulate("submit", this.parentNode, a, !0)) }, teardown: function () { return m.nodeName(this, "form") ? !1 : void m.event.remove(this, "._submit") } }), k.changeBubbles || (m.event.special.change = { setup: function () { return X.test(this.nodeName) ? (("checkbox" === this.type || "radio" === this.type) && (m.event.add(this, "propertychange._change", function (a) { "checked" === a.originalEvent.propertyName && (this._just_changed = !0) }), m.event.add(this, "click._change", function (a) { this._just_changed && !a.isTrigger && (this._just_changed = !1), m.event.simulate("change", this, a, !0) })), !1) : void m.event.add(this, "beforeactivate._change", function (a) { var b = a.target; X.test(b.nodeName) && !m._data(b, "changeBubbles") && (m.event.add(b, "change._change", function (a) { !this.parentNode || a.isSimulated || a.isTrigger || m.event.simulate("change", this.parentNode, a, !0) }), m._data(b, "changeBubbles", !0)) }) }, handle: function (a) { var b = a.target; return this !== b || a.isSimulated || a.isTrigger || "radio" !== b.type && "checkbox" !== b.type ? a.handleObj.handler.apply(this, arguments) : void 0 }, teardown: function () { return m.event.remove(this, "._change"), !X.test(this.nodeName) } }), k.focusinBubbles || m.each({ focus: "focusin", blur: "focusout" }, function (a, b) { var c = function (a) { m.event.simulate(b, a.target, m.event.fix(a), !0) }; m.event.special[b] = { setup: function () { var d = this.ownerDocument || this, e = m._data(d, b); e || d.addEventListener(a, c, !0), m._data(d, b, (e || 0) + 1) }, teardown: function () { var d = this.ownerDocument || this, e = m._data(d, b) - 1; e ? m._data(d, b, e) : (d.removeEventListener(a, c, !0), m._removeData(d, b)) } } }), m.fn.extend({ on: function (a, b, c, d, e) { var f, g; if ("object" == typeof a) { "string" != typeof b && (c = c || b, b = void 0); for (f in a) this.on(f, b, c, a[f], e); return this } if (null == c && null == d ? (d = b, c = b = void 0) : null == d && ("string" == typeof b ? (d = c, c = void 0) : (d = c, c = b, b = void 0)), d === !1) d = ba; else if (!d) return this; return 1 === e && (g = d, d = function (a) { return m().off(a), g.apply(this, arguments) }, d.guid = g.guid || (g.guid = m.guid++)), this.each(function () { m.event.add(this, a, d, c, b) }) }, one: function (a, b, c, d) { return this.on(a, b, c, d, 1) }, off: function (a, b, c) { var d, e; if (a && a.preventDefault && a.handleObj) return d = a.handleObj, m(a.delegateTarget).off(d.namespace ? d.origType + "." + d.namespace : d.origType, d.selector, d.handler), this; if ("object" == typeof a) { for (e in a) this.off(e, b, a[e]); return this } return (b === !1 || "function" == typeof b) && (c = b, b = void 0), c === !1 && (c = ba), this.each(function () { m.event.remove(this, a, c, b) }) }, trigger: function (a, b) { return this.each(function () { m.event.trigger(a, b, this) }) }, triggerHandler: function (a, b) { var c = this[0]; return c ? m.event.trigger(a, b, c, !0) : void 0 } }); function da(a) { var b = ea.split("|"), c = a.createDocumentFragment(); if (c.createElement) while (b.length) c.createElement(b.pop()); return c } var ea = "abbr|article|aside|audio|bdi|canvas|data|datalist|details|figcaption|figure|footer|header|hgroup|mark|meter|nav|output|progress|section|summary|time|video", fa = / jQuery\d+="(?:null|\d+)"/g, ga = new RegExp("<(?:" + ea + ")[\\s/>]", "i"), ha = /^\s+/, ia = /<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:]+)[^>]*)\/>/gi, ja = /<([\w:]+)/, ka = /<tbody/i, la = /<|&#?\w+;/, ma = /<(?:script|style|link)/i, na = /checked\s*(?:[^=]|=\s*.checked.)/i, oa = /^$|\/(?:java|ecma)script/i, pa = /^true\/(.*)/, qa = /^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g, ra = { option: [1, "<select multiple='multiple'>", "</select>"], legend: [1, "<fieldset>", "</fieldset>"], area: [1, "<map>", "</map>"], param: [1, "<object>", "</object>"], thead: [1, "<table>", "</table>"], tr: [2, "<table><tbody>", "</tbody></table>"], col: [2, "<table><tbody></tbody><colgroup>", "</colgroup></table>"], td: [3, "<table><tbody><tr>", "</tr></tbody></table>"], _default: k.htmlSerialize ? [0, "", ""] : [1, "X<div>", "</div>"] }, sa = da(y), ta = sa.appendChild(y.createElement("div")); ra.optgroup = ra.option, ra.tbody = ra.tfoot = ra.colgroup = ra.caption = ra.thead, ra.th = ra.td; function ua(a, b) { var c, d, e = 0, f = typeof a.getElementsByTagName !== K ? a.getElementsByTagName(b || "*") : typeof a.querySelectorAll !== K ? a.querySelectorAll(b || "*") : void 0; if (!f) for (f = [], c = a.childNodes || a; null != (d = c[e]); e++)!b || m.nodeName(d, b) ? f.push(d) : m.merge(f, ua(d, b)); return void 0 === b || b && m.nodeName(a, b) ? m.merge([a], f) : f } function va(a) { W.test(a.type) && (a.defaultChecked = a.checked) } function wa(a, b) { return m.nodeName(a, "table") && m.nodeName(11 !== b.nodeType ? b : b.firstChild, "tr") ? a.getElementsByTagName("tbody")[0] || a.appendChild(a.ownerDocument.createElement("tbody")) : a } function xa(a) { return a.type = (null !== m.find.attr(a, "type")) + "/" + a.type, a } function ya(a) { var b = pa.exec(a.type); return b ? a.type = b[1] : a.removeAttribute("type"), a } function za(a, b) { for (var c, d = 0; null != (c = a[d]); d++)m._data(c, "globalEval", !b || m._data(b[d], "globalEval")) } function Aa(a, b) { if (1 === b.nodeType && m.hasData(a)) { var c, d, e, f = m._data(a), g = m._data(b, f), h = f.events; if (h) { delete g.handle, g.events = {}; for (c in h) for (d = 0, e = h[c].length; e > d; d++)m.event.add(b, c, h[c][d]) } g.data && (g.data = m.extend({}, g.data)) } } function Ba(a, b) { var c, d, e; if (1 === b.nodeType) { if (c = b.nodeName.toLowerCase(), !k.noCloneEvent && b[m.expando]) { e = m._data(b); for (d in e.events) m.removeEvent(b, d, e.handle); b.removeAttribute(m.expando) } "script" === c && b.text !== a.text ? (xa(b).text = a.text, ya(b)) : "object" === c ? (b.parentNode && (b.outerHTML = a.outerHTML), k.html5Clone && a.innerHTML && !m.trim(b.innerHTML) && (b.innerHTML = a.innerHTML)) : "input" === c && W.test(a.type) ? (b.defaultChecked = b.checked = a.checked, b.value !== a.value && (b.value = a.value)) : "option" === c ? b.defaultSelected = b.selected = a.defaultSelected : ("input" === c || "textarea" === c) && (b.defaultValue = a.defaultValue) } } m.extend({ clone: function (a, b, c) { var d, e, f, g, h, i = m.contains(a.ownerDocument, a); if (k.html5Clone || m.isXMLDoc(a) || !ga.test("<" + a.nodeName + ">") ? f = a.cloneNode(!0) : (ta.innerHTML = a.outerHTML, ta.removeChild(f = ta.firstChild)), !(k.noCloneEvent && k.noCloneChecked || 1 !== a.nodeType && 11 !== a.nodeType || m.isXMLDoc(a))) for (d = ua(f), h = ua(a), g = 0; null != (e = h[g]); ++g)d[g] && Ba(e, d[g]); if (b) if (c) for (h = h || ua(a), d = d || ua(f), g = 0; null != (e = h[g]); g++)Aa(e, d[g]); else Aa(a, f); return d = ua(f, "script"), d.length > 0 && za(d, !i && ua(a, "script")), d = h = e = null, f }, buildFragment: function (a, b, c, d) { for (var e, f, g, h, i, j, l, n = a.length, o = da(b), p = [], q = 0; n > q; q++)if (f = a[q], f || 0 === f) if ("object" === m.type(f)) m.merge(p, f.nodeType ? [f] : f); else if (la.test(f)) { h = h || o.appendChild(b.createElement("div")), i = (ja.exec(f) || ["", ""])[1].toLowerCase(), l = ra[i] || ra._default, h.innerHTML = l[1] + f.replace(ia, "<$1></$2>") + l[2], e = l[0]; while (e--) h = h.lastChild; if (!k.leadingWhitespace && ha.test(f) && p.push(b.createTextNode(ha.exec(f)[0])), !k.tbody) { f = "table" !== i || ka.test(f) ? "<table>" !== l[1] || ka.test(f) ? 0 : h : h.firstChild, e = f && f.childNodes.length; while (e--) m.nodeName(j = f.childNodes[e], "tbody") && !j.childNodes.length && f.removeChild(j) } m.merge(p, h.childNodes), h.textContent = ""; while (h.firstChild) h.removeChild(h.firstChild); h = o.lastChild } else p.push(b.createTextNode(f)); h && o.removeChild(h), k.appendChecked || m.grep(ua(p, "input"), va), q = 0; while (f = p[q++]) if ((!d || -1 === m.inArray(f, d)) && (g = m.contains(f.ownerDocument, f), h = ua(o.appendChild(f), "script"), g && za(h), c)) { e = 0; while (f = h[e++]) oa.test(f.type || "") && c.push(f) } return h = null, o }, cleanData: function (a, b) { for (var d, e, f, g, h = 0, i = m.expando, j = m.cache, l = k.deleteExpando, n = m.event.special; null != (d = a[h]); h++)if ((b || m.acceptData(d)) && (f = d[i], g = f && j[f])) { if (g.events) for (e in g.events) n[e] ? m.event.remove(d, e) : m.removeEvent(d, e, g.handle); j[f] && (delete j[f], l ? delete d[i] : typeof d.removeAttribute !== K ? d.removeAttribute(i) : d[i] = null, c.push(f)) } } }), m.fn.extend({ text: function (a) { return V(this, function (a) { return void 0 === a ? m.text(this) : this.empty().append((this[0] && this[0].ownerDocument || y).createTextNode(a)) }, null, a, arguments.length) }, append: function () { return this.domManip(arguments, function (a) { if (1 === this.nodeType || 11 === this.nodeType || 9 === this.nodeType) { var b = wa(this, a); b.appendChild(a) } }) }, prepend: function () { return this.domManip(arguments, function (a) { if (1 === this.nodeType || 11 === this.nodeType || 9 === this.nodeType) { var b = wa(this, a); b.insertBefore(a, b.firstChild) } }) }, before: function () { return this.domManip(arguments, function (a) { this.parentNode && this.parentNode.insertBefore(a, this) }) }, after: function () { return this.domManip(arguments, function (a) { this.parentNode && this.parentNode.insertBefore(a, this.nextSibling) }) }, remove: function (a, b) { for (var c, d = a ? m.filter(a, this) : this, e = 0; null != (c = d[e]); e++)b || 1 !== c.nodeType || m.cleanData(ua(c)), c.parentNode && (b && m.contains(c.ownerDocument, c) && za(ua(c, "script")), c.parentNode.removeChild(c)); return this }, empty: function () { for (var a, b = 0; null != (a = this[b]); b++) { 1 === a.nodeType && m.cleanData(ua(a, !1)); while (a.firstChild) a.removeChild(a.firstChild); a.options && m.nodeName(a, "select") && (a.options.length = 0) } return this }, clone: function (a, b) { return a = null == a ? !1 : a, b = null == b ? a : b, this.map(function () { return m.clone(this, a, b) }) }, html: function (a) { return V(this, function (a) { var b = this[0] || {}, c = 0, d = this.length; if (void 0 === a) return 1 === b.nodeType ? b.innerHTML.replace(fa, "") : void 0; if (!("string" != typeof a || ma.test(a) || !k.htmlSerialize && ga.test(a) || !k.leadingWhitespace && ha.test(a) || ra[(ja.exec(a) || ["", ""])[1].toLowerCase()])) { a = a.replace(ia, "<$1></$2>"); try { for (; d > c; c++)b = this[c] || {}, 1 === b.nodeType && (m.cleanData(ua(b, !1)), b.innerHTML = a); b = 0 } catch (e) { } } b && this.empty().append(a) }, null, a, arguments.length) }, replaceWith: function () { var a = arguments[0]; return this.domManip(arguments, function (b) { a = this.parentNode, m.cleanData(ua(this)), a && a.replaceChild(b, this) }), a && (a.length || a.nodeType) ? this : this.remove() }, detach: function (a) { return this.remove(a, !0) }, domManip: function (a, b) { a = e.apply([], a); var c, d, f, g, h, i, j = 0, l = this.length, n = this, o = l - 1, p = a[0], q = m.isFunction(p); if (q || l > 1 && "string" == typeof p && !k.checkClone && na.test(p)) return this.each(function (c) { var d = n.eq(c); q && (a[0] = p.call(this, c, d.html())), d.domManip(a, b) }); if (l && (i = m.buildFragment(a, this[0].ownerDocument, !1, this), c = i.firstChild, 1 === i.childNodes.length && (i = c), c)) { for (g = m.map(ua(i, "script"), xa), f = g.length; l > j; j++)d = i, j !== o && (d = m.clone(d, !0, !0), f && m.merge(g, ua(d, "script"))), b.call(this[j], d, j); if (f) for (h = g[g.length - 1].ownerDocument, m.map(g, ya), j = 0; f > j; j++)d = g[j], oa.test(d.type || "") && !m._data(d, "globalEval") && m.contains(h, d) && (d.src ? m._evalUrl && m._evalUrl(d.src) : m.globalEval((d.text || d.textContent || d.innerHTML || "").replace(qa, ""))); i = c = null } return this } }), m.each({ appendTo: "append", prependTo: "prepend", insertBefore: "before", insertAfter: "after", replaceAll: "replaceWith" }, function (a, b) { m.fn[a] = function (a) { for (var c, d = 0, e = [], g = m(a), h = g.length - 1; h >= d; d++)c = d === h ? this : this.clone(!0), m(g[d])[b](c), f.apply(e, c.get()); return this.pushStack(e) } }); var Ca, Da = {}; function Ea(b, c) { var d, e = m(c.createElement(b)).appendTo(c.body), f = a.getDefaultComputedStyle && (d = a.getDefaultComputedStyle(e[0])) ? d.display : m.css(e[0], "display"); return e.detach(), f } function Fa(a) { var b = y, c = Da[a]; return c || (c = Ea(a, b), "none" !== c && c || (Ca = (Ca || m("<iframe frameborder='0' width='0' height='0'/>")).appendTo(b.documentElement), b = (Ca[0].contentWindow || Ca[0].contentDocument).document, b.write(), b.close(), c = Ea(a, b), Ca.detach()), Da[a] = c), c } !function () { var a; k.shrinkWrapBlocks = function () { if (null != a) return a; a = !1; var b, c, d; return c = y.getElementsByTagName("body")[0], c && c.style ? (b = y.createElement("div"), d = y.createElement("div"), d.style.cssText = "position:absolute;border:0;width:0;height:0;top:0;left:-9999px", c.appendChild(d).appendChild(b), typeof b.style.zoom !== K && (b.style.cssText = "-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box;display:block;margin:0;border:0;padding:1px;width:1px;zoom:1", b.appendChild(y.createElement("div")).style.width = "5px", a = 3 !== b.offsetWidth), c.removeChild(d), a) : void 0 } }(); var Ga = /^margin/, Ha = new RegExp("^(" + S + ")(?!px)[a-z%]+$", "i"), Ia, Ja, Ka = /^(top|right|bottom|left)$/; a.getComputedStyle ? (Ia = function (b) { return b.ownerDocument.defaultView.opener ? b.ownerDocument.defaultView.getComputedStyle(b, null) : a.getComputedStyle(b, null) }, Ja = function (a, b, c) { var d, e, f, g, h = a.style; return c = c || Ia(a), g = c ? c.getPropertyValue(b) || c[b] : void 0, c && ("" !== g || m.contains(a.ownerDocument, a) || (g = m.style(a, b)), Ha.test(g) && Ga.test(b) && (d = h.width, e = h.minWidth, f = h.maxWidth, h.minWidth = h.maxWidth = h.width = g, g = c.width, h.width = d, h.minWidth = e, h.maxWidth = f)), void 0 === g ? g : g + "" }) : y.documentElement.currentStyle && (Ia = function (a) { return a.currentStyle }, Ja = function (a, b, c) { var d, e, f, g, h = a.style; return c = c || Ia(a), g = c ? c[b] : void 0, null == g && h && h[b] && (g = h[b]), Ha.test(g) && !Ka.test(b) && (d = h.left, e = a.runtimeStyle, f = e && e.left, f && (e.left = a.currentStyle.left), h.left = "fontSize" === b ? "1em" : g, g = h.pixelLeft + "px", h.left = d, f && (e.left = f)), void 0 === g ? g : g + "" || "auto" }); function La(a, b) { return { get: function () { var c = a(); if (null != c) return c ? void delete this.get : (this.get = b).apply(this, arguments) } } } !function () { var b, c, d, e, f, g, h; if (b = y.createElement("div"), b.innerHTML = "  <link/><table></table><a href='/a'>a</a><input type='checkbox'/>", d = b.getElementsByTagName("a")[0], c = d && d.style) { c.cssText = "float:left;opacity:.5", k.opacity = "0.5" === c.opacity, k.cssFloat = !!c.cssFloat, b.style.backgroundClip = "content-box", b.cloneNode(!0).style.backgroundClip = "", k.clearCloneStyle = "content-box" === b.style.backgroundClip, k.boxSizing = "" === c.boxSizing || "" === c.MozBoxSizing || "" === c.WebkitBoxSizing, m.extend(k, { reliableHiddenOffsets: function () { return null == g && i(), g }, boxSizingReliable: function () { return null == f && i(), f }, pixelPosition: function () { return null == e && i(), e }, reliableMarginRight: function () { return null == h && i(), h } }); function i() { var b, c, d, i; c = y.getElementsByTagName("body")[0], c && c.style && (b = y.createElement("div"), d = y.createElement("div"), d.style.cssText = "position:absolute;border:0;width:0;height:0;top:0;left:-9999px", c.appendChild(d).appendChild(b), b.style.cssText = "-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;display:block;margin-top:1%;top:1%;border:1px;padding:1px;width:4px;position:absolute", e = f = !1, h = !0, a.getComputedStyle && (e = "1%" !== (a.getComputedStyle(b, null) || {}).top, f = "4px" === (a.getComputedStyle(b, null) || { width: "4px" }).width, i = b.appendChild(y.createElement("div")), i.style.cssText = b.style.cssText = "-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box;display:block;margin:0;border:0;padding:0", i.style.marginRight = i.style.width = "0", b.style.width = "1px", h = !parseFloat((a.getComputedStyle(i, null) || {}).marginRight), b.removeChild(i)), b.innerHTML = "<table><tr><td></td><td>t</td></tr></table>", i = b.getElementsByTagName("td"), i[0].style.cssText = "margin:0;border:0;padding:0;display:none", g = 0 === i[0].offsetHeight, g && (i[0].style.display = "", i[1].style.display = "none", g = 0 === i[0].offsetHeight), c.removeChild(d)) } } }(), m.swap = function (a, b, c, d) { var e, f, g = {}; for (f in b) g[f] = a.style[f], a.style[f] = b[f]; e = c.apply(a, d || []); for (f in b) a.style[f] = g[f]; return e }; var Ma = /alpha\([^)]*\)/i, Na = /opacity\s*=\s*([^)]*)/, Oa = /^(none|table(?!-c[ea]).+)/, Pa = new RegExp("^(" + S + ")(.*)$", "i"), Qa = new RegExp("^([+-])=(" + S + ")", "i"), Ra = { position: "absolute", visibility: "hidden", display: "block" }, Sa = { letterSpacing: "0", fontWeight: "400" }, Ta = ["Webkit", "O", "Moz", "ms"]; function Ua(a, b) { if (b in a) return b; var c = b.charAt(0).toUpperCase() + b.slice(1), d = b, e = Ta.length; while (e--) if (b = Ta[e] + c, b in a) return b; return d } function Va(a, b) { for (var c, d, e, f = [], g = 0, h = a.length; h > g; g++)d = a[g], d.style && (f[g] = m._data(d, "olddisplay"), c = d.style.display, b ? (f[g] || "none" !== c || (d.style.display = ""), "" === d.style.display && U(d) && (f[g] = m._data(d, "olddisplay", Fa(d.nodeName)))) : (e = U(d), (c && "none" !== c || !e) && m._data(d, "olddisplay", e ? c : m.css(d, "display")))); for (g = 0; h > g; g++)d = a[g], d.style && (b && "none" !== d.style.display && "" !== d.style.display || (d.style.display = b ? f[g] || "" : "none")); return a } function Wa(a, b, c) { var d = Pa.exec(b); return d ? Math.max(0, d[1] - (c || 0)) + (d[2] || "px") : b } function Xa(a, b, c, d, e) { for (var f = c === (d ? "border" : "content") ? 4 : "width" === b ? 1 : 0, g = 0; 4 > f; f += 2)"margin" === c && (g += m.css(a, c + T[f], !0, e)), d ? ("content" === c && (g -= m.css(a, "padding" + T[f], !0, e)), "margin" !== c && (g -= m.css(a, "border" + T[f] + "Width", !0, e))) : (g += m.css(a, "padding" + T[f], !0, e), "padding" !== c && (g += m.css(a, "border" + T[f] + "Width", !0, e))); return g } function Ya(a, b, c) { var d = !0, e = "width" === b ? a.offsetWidth : a.offsetHeight, f = Ia(a), g = k.boxSizing && "border-box" === m.css(a, "boxSizing", !1, f); if (0 >= e || null == e) { if (e = Ja(a, b, f), (0 > e || null == e) && (e = a.style[b]), Ha.test(e)) return e; d = g && (k.boxSizingReliable() || e === a.style[b]), e = parseFloat(e) || 0 } return e + Xa(a, b, c || (g ? "border" : "content"), d, f) + "px" } m.extend({ cssHooks: { opacity: { get: function (a, b) { if (b) { var c = Ja(a, "opacity"); return "" === c ? "1" : c } } } }, cssNumber: { columnCount: !0, fillOpacity: !0, flexGrow: !0, flexShrink: !0, fontWeight: !0, lineHeight: !0, opacity: !0, order: !0, orphans: !0, widows: !0, zIndex: !0, zoom: !0 }, cssProps: { "float": k.cssFloat ? "cssFloat" : "styleFloat" }, style: function (a, b, c, d) { if (a && 3 !== a.nodeType && 8 !== a.nodeType && a.style) { var e, f, g, h = m.camelCase(b), i = a.style; if (b = m.cssProps[h] || (m.cssProps[h] = Ua(i, h)), g = m.cssHooks[b] || m.cssHooks[h], void 0 === c) return g && "get" in g && void 0 !== (e = g.get(a, !1, d)) ? e : i[b]; if (f = typeof c, "string" === f && (e = Qa.exec(c)) && (c = (e[1] + 1) * e[2] + parseFloat(m.css(a, b)), f = "number"), null != c && c === c && ("number" !== f || m.cssNumber[h] || (c += "px"), k.clearCloneStyle || "" !== c || 0 !== b.indexOf("background") || (i[b] = "inherit"), !(g && "set" in g && void 0 === (c = g.set(a, c, d))))) try { i[b] = c } catch (j) { } } }, css: function (a, b, c, d) { var e, f, g, h = m.camelCase(b); return b = m.cssProps[h] || (m.cssProps[h] = Ua(a.style, h)), g = m.cssHooks[b] || m.cssHooks[h], g && "get" in g && (f = g.get(a, !0, c)), void 0 === f && (f = Ja(a, b, d)), "normal" === f && b in Sa && (f = Sa[b]), "" === c || c ? (e = parseFloat(f), c === !0 || m.isNumeric(e) ? e || 0 : f) : f } }), m.each(["height", "width"], function (a, b) { m.cssHooks[b] = { get: function (a, c, d) { return c ? Oa.test(m.css(a, "display")) && 0 === a.offsetWidth ? m.swap(a, Ra, function () { return Ya(a, b, d) }) : Ya(a, b, d) : void 0 }, set: function (a, c, d) { var e = d && Ia(a); return Wa(a, c, d ? Xa(a, b, d, k.boxSizing && "border-box" === m.css(a, "boxSizing", !1, e), e) : 0) } } }), k.opacity || (m.cssHooks.opacity = { get: function (a, b) { return Na.test((b && a.currentStyle ? a.currentStyle.filter : a.style.filter) || "") ? .01 * parseFloat(RegExp.$1) + "" : b ? "1" : "" }, set: function (a, b) { var c = a.style, d = a.currentStyle, e = m.isNumeric(b) ? "alpha(opacity=" + 100 * b + ")" : "", f = d && d.filter || c.filter || ""; c.zoom = 1, (b >= 1 || "" === b) && "" === m.trim(f.replace(Ma, "")) && c.removeAttribute && (c.removeAttribute("filter"), "" === b || d && !d.filter) || (c.filter = Ma.test(f) ? f.replace(Ma, e) : f + " " + e) } }), m.cssHooks.marginRight = La(k.reliableMarginRight, function (a, b) { return b ? m.swap(a, { display: "inline-block" }, Ja, [a, "marginRight"]) : void 0 }), m.each({ margin: "", padding: "", border: "Width" }, function (a, b) { m.cssHooks[a + b] = { expand: function (c) { for (var d = 0, e = {}, f = "string" == typeof c ? c.split(" ") : [c]; 4 > d; d++)e[a + T[d] + b] = f[d] || f[d - 2] || f[0]; return e } }, Ga.test(a) || (m.cssHooks[a + b].set = Wa) }), m.fn.extend({ css: function (a, b) { return V(this, function (a, b, c) { var d, e, f = {}, g = 0; if (m.isArray(b)) { for (d = Ia(a), e = b.length; e > g; g++)f[b[g]] = m.css(a, b[g], !1, d); return f } return void 0 !== c ? m.style(a, b, c) : m.css(a, b) }, a, b, arguments.length > 1) }, show: function () { return Va(this, !0) }, hide: function () { return Va(this) }, toggle: function (a) { return "boolean" == typeof a ? a ? this.show() : this.hide() : this.each(function () { U(this) ? m(this).show() : m(this).hide() }) } }); function Za(a, b, c, d, e) {
        return new Za.prototype.init(a, b, c, d, e)
    } m.Tween = Za, Za.prototype = { constructor: Za, init: function (a, b, c, d, e, f) { this.elem = a, this.prop = c, this.easing = e || "swing", this.options = b, this.start = this.now = this.cur(), this.end = d, this.unit = f || (m.cssNumber[c] ? "" : "px") }, cur: function () { var a = Za.propHooks[this.prop]; return a && a.get ? a.get(this) : Za.propHooks._default.get(this) }, run: function (a) { var b, c = Za.propHooks[this.prop]; return this.options.duration ? this.pos = b = m.easing[this.easing](a, this.options.duration * a, 0, 1, this.options.duration) : this.pos = b = a, this.now = (this.end - this.start) * b + this.start, this.options.step && this.options.step.call(this.elem, this.now, this), c && c.set ? c.set(this) : Za.propHooks._default.set(this), this } }, Za.prototype.init.prototype = Za.prototype, Za.propHooks = { _default: { get: function (a) { var b; return null == a.elem[a.prop] || a.elem.style && null != a.elem.style[a.prop] ? (b = m.css(a.elem, a.prop, ""), b && "auto" !== b ? b : 0) : a.elem[a.prop] }, set: function (a) { m.fx.step[a.prop] ? m.fx.step[a.prop](a) : a.elem.style && (null != a.elem.style[m.cssProps[a.prop]] || m.cssHooks[a.prop]) ? m.style(a.elem, a.prop, a.now + a.unit) : a.elem[a.prop] = a.now } } }, Za.propHooks.scrollTop = Za.propHooks.scrollLeft = { set: function (a) { a.elem.nodeType && a.elem.parentNode && (a.elem[a.prop] = a.now) } }, m.easing = { linear: function (a) { return a }, swing: function (a) { return .5 - Math.cos(a * Math.PI) / 2 } }, m.fx = Za.prototype.init, m.fx.step = {}; var $a, _a, ab = /^(?:toggle|show|hide)$/, bb = new RegExp("^(?:([+-])=|)(" + S + ")([a-z%]*)$", "i"), cb = /queueHooks$/, db = [ib], eb = { "*": [function (a, b) { var c = this.createTween(a, b), d = c.cur(), e = bb.exec(b), f = e && e[3] || (m.cssNumber[a] ? "" : "px"), g = (m.cssNumber[a] || "px" !== f && +d) && bb.exec(m.css(c.elem, a)), h = 1, i = 20; if (g && g[3] !== f) { f = f || g[3], e = e || [], g = +d || 1; do h = h || ".5", g /= h, m.style(c.elem, a, g + f); while (h !== (h = c.cur() / d) && 1 !== h && --i) } return e && (g = c.start = +g || +d || 0, c.unit = f, c.end = e[1] ? g + (e[1] + 1) * e[2] : +e[2]), c }] }; function fb() { return setTimeout(function () { $a = void 0 }), $a = m.now() } function gb(a, b) { var c, d = { height: a }, e = 0; for (b = b ? 1 : 0; 4 > e; e += 2 - b)c = T[e], d["margin" + c] = d["padding" + c] = a; return b && (d.opacity = d.width = a), d } function hb(a, b, c) { for (var d, e = (eb[b] || []).concat(eb["*"]), f = 0, g = e.length; g > f; f++)if (d = e[f].call(c, b, a)) return d } function ib(a, b, c) { var d, e, f, g, h, i, j, l, n = this, o = {}, p = a.style, q = a.nodeType && U(a), r = m._data(a, "fxshow"); c.queue || (h = m._queueHooks(a, "fx"), null == h.unqueued && (h.unqueued = 0, i = h.empty.fire, h.empty.fire = function () { h.unqueued || i() }), h.unqueued++, n.always(function () { n.always(function () { h.unqueued--, m.queue(a, "fx").length || h.empty.fire() }) })), 1 === a.nodeType && ("height" in b || "width" in b) && (c.overflow = [p.overflow, p.overflowX, p.overflowY], j = m.css(a, "display"), l = "none" === j ? m._data(a, "olddisplay") || Fa(a.nodeName) : j, "inline" === l && "none" === m.css(a, "float") && (k.inlineBlockNeedsLayout && "inline" !== Fa(a.nodeName) ? p.zoom = 1 : p.display = "inline-block")), c.overflow && (p.overflow = "hidden", k.shrinkWrapBlocks() || n.always(function () { p.overflow = c.overflow[0], p.overflowX = c.overflow[1], p.overflowY = c.overflow[2] })); for (d in b) if (e = b[d], ab.exec(e)) { if (delete b[d], f = f || "toggle" === e, e === (q ? "hide" : "show")) { if ("show" !== e || !r || void 0 === r[d]) continue; q = !0 } o[d] = r && r[d] || m.style(a, d) } else j = void 0; if (m.isEmptyObject(o)) "inline" === ("none" === j ? Fa(a.nodeName) : j) && (p.display = j); else { r ? "hidden" in r && (q = r.hidden) : r = m._data(a, "fxshow", {}), f && (r.hidden = !q), q ? m(a).show() : n.done(function () { m(a).hide() }), n.done(function () { var b; m._removeData(a, "fxshow"); for (b in o) m.style(a, b, o[b]) }); for (d in o) g = hb(q ? r[d] : 0, d, n), d in r || (r[d] = g.start, q && (g.end = g.start, g.start = "width" === d || "height" === d ? 1 : 0)) } } function jb(a, b) { var c, d, e, f, g; for (c in a) if (d = m.camelCase(c), e = b[d], f = a[c], m.isArray(f) && (e = f[1], f = a[c] = f[0]), c !== d && (a[d] = f, delete a[c]), g = m.cssHooks[d], g && "expand" in g) { f = g.expand(f), delete a[d]; for (c in f) c in a || (a[c] = f[c], b[c] = e) } else b[d] = e } function kb(a, b, c) { var d, e, f = 0, g = db.length, h = m.Deferred().always(function () { delete i.elem }), i = function () { if (e) return !1; for (var b = $a || fb(), c = Math.max(0, j.startTime + j.duration - b), d = c / j.duration || 0, f = 1 - d, g = 0, i = j.tweens.length; i > g; g++)j.tweens[g].run(f); return h.notifyWith(a, [j, f, c]), 1 > f && i ? c : (h.resolveWith(a, [j]), !1) }, j = h.promise({ elem: a, props: m.extend({}, b), opts: m.extend(!0, { specialEasing: {} }, c), originalProperties: b, originalOptions: c, startTime: $a || fb(), duration: c.duration, tweens: [], createTween: function (b, c) { var d = m.Tween(a, j.opts, b, c, j.opts.specialEasing[b] || j.opts.easing); return j.tweens.push(d), d }, stop: function (b) { var c = 0, d = b ? j.tweens.length : 0; if (e) return this; for (e = !0; d > c; c++)j.tweens[c].run(1); return b ? h.resolveWith(a, [j, b]) : h.rejectWith(a, [j, b]), this } }), k = j.props; for (jb(k, j.opts.specialEasing); g > f; f++)if (d = db[f].call(j, a, k, j.opts)) return d; return m.map(k, hb, j), m.isFunction(j.opts.start) && j.opts.start.call(a, j), m.fx.timer(m.extend(i, { elem: a, anim: j, queue: j.opts.queue })), j.progress(j.opts.progress).done(j.opts.done, j.opts.complete).fail(j.opts.fail).always(j.opts.always) } m.Animation = m.extend(kb, { tweener: function (a, b) { m.isFunction(a) ? (b = a, a = ["*"]) : a = a.split(" "); for (var c, d = 0, e = a.length; e > d; d++)c = a[d], eb[c] = eb[c] || [], eb[c].unshift(b) }, prefilter: function (a, b) { b ? db.unshift(a) : db.push(a) } }), m.speed = function (a, b, c) { var d = a && "object" == typeof a ? m.extend({}, a) : { complete: c || !c && b || m.isFunction(a) && a, duration: a, easing: c && b || b && !m.isFunction(b) && b }; return d.duration = m.fx.off ? 0 : "number" == typeof d.duration ? d.duration : d.duration in m.fx.speeds ? m.fx.speeds[d.duration] : m.fx.speeds._default, (null == d.queue || d.queue === !0) && (d.queue = "fx"), d.old = d.complete, d.complete = function () { m.isFunction(d.old) && d.old.call(this), d.queue && m.dequeue(this, d.queue) }, d }, m.fn.extend({ fadeTo: function (a, b, c, d) { return this.filter(U).css("opacity", 0).show().end().animate({ opacity: b }, a, c, d) }, animate: function (a, b, c, d) { var e = m.isEmptyObject(a), f = m.speed(b, c, d), g = function () { var b = kb(this, m.extend({}, a), f); (e || m._data(this, "finish")) && b.stop(!0) }; return g.finish = g, e || f.queue === !1 ? this.each(g) : this.queue(f.queue, g) }, stop: function (a, b, c) { var d = function (a) { var b = a.stop; delete a.stop, b(c) }; return "string" != typeof a && (c = b, b = a, a = void 0), b && a !== !1 && this.queue(a || "fx", []), this.each(function () { var b = !0, e = null != a && a + "queueHooks", f = m.timers, g = m._data(this); if (e) g[e] && g[e].stop && d(g[e]); else for (e in g) g[e] && g[e].stop && cb.test(e) && d(g[e]); for (e = f.length; e--;)f[e].elem !== this || null != a && f[e].queue !== a || (f[e].anim.stop(c), b = !1, f.splice(e, 1)); (b || !c) && m.dequeue(this, a) }) }, finish: function (a) { return a !== !1 && (a = a || "fx"), this.each(function () { var b, c = m._data(this), d = c[a + "queue"], e = c[a + "queueHooks"], f = m.timers, g = d ? d.length : 0; for (c.finish = !0, m.queue(this, a, []), e && e.stop && e.stop.call(this, !0), b = f.length; b--;)f[b].elem === this && f[b].queue === a && (f[b].anim.stop(!0), f.splice(b, 1)); for (b = 0; g > b; b++)d[b] && d[b].finish && d[b].finish.call(this); delete c.finish }) } }), m.each(["toggle", "show", "hide"], function (a, b) { var c = m.fn[b]; m.fn[b] = function (a, d, e) { return null == a || "boolean" == typeof a ? c.apply(this, arguments) : this.animate(gb(b, !0), a, d, e) } }), m.each({ slideDown: gb("show"), slideUp: gb("hide"), slideToggle: gb("toggle"), fadeIn: { opacity: "show" }, fadeOut: { opacity: "hide" }, fadeToggle: { opacity: "toggle" } }, function (a, b) { m.fn[a] = function (a, c, d) { return this.animate(b, a, c, d) } }), m.timers = [], m.fx.tick = function () { var a, b = m.timers, c = 0; for ($a = m.now(); c < b.length; c++)a = b[c], a() || b[c] !== a || b.splice(c--, 1); b.length || m.fx.stop(), $a = void 0 }, m.fx.timer = function (a) { m.timers.push(a), a() ? m.fx.start() : m.timers.pop() }, m.fx.interval = 13, m.fx.start = function () { _a || (_a = setInterval(m.fx.tick, m.fx.interval)) }, m.fx.stop = function () { clearInterval(_a), _a = null }, m.fx.speeds = { slow: 600, fast: 200, _default: 400 }, m.fn.delay = function (a, b) { return a = m.fx ? m.fx.speeds[a] || a : a, b = b || "fx", this.queue(b, function (b, c) { var d = setTimeout(b, a); c.stop = function () { clearTimeout(d) } }) }, function () { var a, b, c, d, e; b = y.createElement("div"), b.setAttribute("className", "t"), b.innerHTML = "  <link/><table></table><a href='/a'>a</a><input type='checkbox'/>", d = b.getElementsByTagName("a")[0], c = y.createElement("select"), e = c.appendChild(y.createElement("option")), a = b.getElementsByTagName("input")[0], d.style.cssText = "top:1px", k.getSetAttribute = "t" !== b.className, k.style = /top/.test(d.getAttribute("style")), k.hrefNormalized = "/a" === d.getAttribute("href"), k.checkOn = !!a.value, k.optSelected = e.selected, k.enctype = !!y.createElement("form").enctype, c.disabled = !0, k.optDisabled = !e.disabled, a = y.createElement("input"), a.setAttribute("value", ""), k.input = "" === a.getAttribute("value"), a.value = "t", a.setAttribute("type", "radio"), k.radioValue = "t" === a.value }(); var lb = /\r/g; m.fn.extend({ val: function (a) { var b, c, d, e = this[0]; { if (arguments.length) return d = m.isFunction(a), this.each(function (c) { var e; 1 === this.nodeType && (e = d ? a.call(this, c, m(this).val()) : a, null == e ? e = "" : "number" == typeof e ? e += "" : m.isArray(e) && (e = m.map(e, function (a) { return null == a ? "" : a + "" })), b = m.valHooks[this.type] || m.valHooks[this.nodeName.toLowerCase()], b && "set" in b && void 0 !== b.set(this, e, "value") || (this.value = e)) }); if (e) return b = m.valHooks[e.type] || m.valHooks[e.nodeName.toLowerCase()], b && "get" in b && void 0 !== (c = b.get(e, "value")) ? c : (c = e.value, "string" == typeof c ? c.replace(lb, "") : null == c ? "" : c) } } }), m.extend({ valHooks: { option: { get: function (a) { var b = m.find.attr(a, "value"); return null != b ? b : m.trim(m.text(a)) } }, select: { get: function (a) { for (var b, c, d = a.options, e = a.selectedIndex, f = "select-one" === a.type || 0 > e, g = f ? null : [], h = f ? e + 1 : d.length, i = 0 > e ? h : f ? e : 0; h > i; i++)if (c = d[i], !(!c.selected && i !== e || (k.optDisabled ? c.disabled : null !== c.getAttribute("disabled")) || c.parentNode.disabled && m.nodeName(c.parentNode, "optgroup"))) { if (b = m(c).val(), f) return b; g.push(b) } return g }, set: function (a, b) { var c, d, e = a.options, f = m.makeArray(b), g = e.length; while (g--) if (d = e[g], m.inArray(m.valHooks.option.get(d), f) >= 0) try { d.selected = c = !0 } catch (h) { d.scrollHeight } else d.selected = !1; return c || (a.selectedIndex = -1), e } } } }), m.each(["radio", "checkbox"], function () { m.valHooks[this] = { set: function (a, b) { return m.isArray(b) ? a.checked = m.inArray(m(a).val(), b) >= 0 : void 0 } }, k.checkOn || (m.valHooks[this].get = function (a) { return null === a.getAttribute("value") ? "on" : a.value }) }); var mb, nb, ob = m.expr.attrHandle, pb = /^(?:checked|selected)$/i, qb = k.getSetAttribute, rb = k.input; m.fn.extend({ attr: function (a, b) { return V(this, m.attr, a, b, arguments.length > 1) }, removeAttr: function (a) { return this.each(function () { m.removeAttr(this, a) }) } }), m.extend({ attr: function (a, b, c) { var d, e, f = a.nodeType; if (a && 3 !== f && 8 !== f && 2 !== f) return typeof a.getAttribute === K ? m.prop(a, b, c) : (1 === f && m.isXMLDoc(a) || (b = b.toLowerCase(), d = m.attrHooks[b] || (m.expr.match.bool.test(b) ? nb : mb)), void 0 === c ? d && "get" in d && null !== (e = d.get(a, b)) ? e : (e = m.find.attr(a, b), null == e ? void 0 : e) : null !== c ? d && "set" in d && void 0 !== (e = d.set(a, c, b)) ? e : (a.setAttribute(b, c + ""), c) : void m.removeAttr(a, b)) }, removeAttr: function (a, b) { var c, d, e = 0, f = b && b.match(E); if (f && 1 === a.nodeType) while (c = f[e++]) d = m.propFix[c] || c, m.expr.match.bool.test(c) ? rb && qb || !pb.test(c) ? a[d] = !1 : a[m.camelCase("default-" + c)] = a[d] = !1 : m.attr(a, c, ""), a.removeAttribute(qb ? c : d) }, attrHooks: { type: { set: function (a, b) { if (!k.radioValue && "radio" === b && m.nodeName(a, "input")) { var c = a.value; return a.setAttribute("type", b), c && (a.value = c), b } } } } }), nb = { set: function (a, b, c) { return b === !1 ? m.removeAttr(a, c) : rb && qb || !pb.test(c) ? a.setAttribute(!qb && m.propFix[c] || c, c) : a[m.camelCase("default-" + c)] = a[c] = !0, c } }, m.each(m.expr.match.bool.source.match(/\w+/g), function (a, b) { var c = ob[b] || m.find.attr; ob[b] = rb && qb || !pb.test(b) ? function (a, b, d) { var e, f; return d || (f = ob[b], ob[b] = e, e = null != c(a, b, d) ? b.toLowerCase() : null, ob[b] = f), e } : function (a, b, c) { return c ? void 0 : a[m.camelCase("default-" + b)] ? b.toLowerCase() : null } }), rb && qb || (m.attrHooks.value = { set: function (a, b, c) { return m.nodeName(a, "input") ? void (a.defaultValue = b) : mb && mb.set(a, b, c) } }), qb || (mb = { set: function (a, b, c) { var d = a.getAttributeNode(c); return d || a.setAttributeNode(d = a.ownerDocument.createAttribute(c)), d.value = b += "", "value" === c || b === a.getAttribute(c) ? b : void 0 } }, ob.id = ob.name = ob.coords = function (a, b, c) { var d; return c ? void 0 : (d = a.getAttributeNode(b)) && "" !== d.value ? d.value : null }, m.valHooks.button = { get: function (a, b) { var c = a.getAttributeNode(b); return c && c.specified ? c.value : void 0 }, set: mb.set }, m.attrHooks.contenteditable = { set: function (a, b, c) { mb.set(a, "" === b ? !1 : b, c) } }, m.each(["width", "height"], function (a, b) { m.attrHooks[b] = { set: function (a, c) { return "" === c ? (a.setAttribute(b, "auto"), c) : void 0 } } })), k.style || (m.attrHooks.style = { get: function (a) { return a.style.cssText || void 0 }, set: function (a, b) { return a.style.cssText = b + "" } }); var sb = /^(?:input|select|textarea|button|object)$/i, tb = /^(?:a|area)$/i; m.fn.extend({ prop: function (a, b) { return V(this, m.prop, a, b, arguments.length > 1) }, removeProp: function (a) { return a = m.propFix[a] || a, this.each(function () { try { this[a] = void 0, delete this[a] } catch (b) { } }) } }), m.extend({ propFix: { "for": "htmlFor", "class": "className" }, prop: function (a, b, c) { var d, e, f, g = a.nodeType; if (a && 3 !== g && 8 !== g && 2 !== g) return f = 1 !== g || !m.isXMLDoc(a), f && (b = m.propFix[b] || b, e = m.propHooks[b]), void 0 !== c ? e && "set" in e && void 0 !== (d = e.set(a, c, b)) ? d : a[b] = c : e && "get" in e && null !== (d = e.get(a, b)) ? d : a[b] }, propHooks: { tabIndex: { get: function (a) { var b = m.find.attr(a, "tabindex"); return b ? parseInt(b, 10) : sb.test(a.nodeName) || tb.test(a.nodeName) && a.href ? 0 : -1 } } } }), k.hrefNormalized || m.each(["href", "src"], function (a, b) { m.propHooks[b] = { get: function (a) { return a.getAttribute(b, 4) } } }), k.optSelected || (m.propHooks.selected = { get: function (a) { var b = a.parentNode; return b && (b.selectedIndex, b.parentNode && b.parentNode.selectedIndex), null } }), m.each(["tabIndex", "readOnly", "maxLength", "cellSpacing", "cellPadding", "rowSpan", "colSpan", "useMap", "frameBorder", "contentEditable"], function () { m.propFix[this.toLowerCase()] = this }), k.enctype || (m.propFix.enctype = "encoding"); var ub = /[\t\r\n\f]/g; m.fn.extend({ addClass: function (a) { var b, c, d, e, f, g, h = 0, i = this.length, j = "string" == typeof a && a; if (m.isFunction(a)) return this.each(function (b) { m(this).addClass(a.call(this, b, this.className)) }); if (j) for (b = (a || "").match(E) || []; i > h; h++)if (c = this[h], d = 1 === c.nodeType && (c.className ? (" " + c.className + " ").replace(ub, " ") : " ")) { f = 0; while (e = b[f++]) d.indexOf(" " + e + " ") < 0 && (d += e + " "); g = m.trim(d), c.className !== g && (c.className = g) } return this }, removeClass: function (a) { var b, c, d, e, f, g, h = 0, i = this.length, j = 0 === arguments.length || "string" == typeof a && a; if (m.isFunction(a)) return this.each(function (b) { m(this).removeClass(a.call(this, b, this.className)) }); if (j) for (b = (a || "").match(E) || []; i > h; h++)if (c = this[h], d = 1 === c.nodeType && (c.className ? (" " + c.className + " ").replace(ub, " ") : "")) { f = 0; while (e = b[f++]) while (d.indexOf(" " + e + " ") >= 0) d = d.replace(" " + e + " ", " "); g = a ? m.trim(d) : "", c.className !== g && (c.className = g) } return this }, toggleClass: function (a, b) { var c = typeof a; return "boolean" == typeof b && "string" === c ? b ? this.addClass(a) : this.removeClass(a) : this.each(m.isFunction(a) ? function (c) { m(this).toggleClass(a.call(this, c, this.className, b), b) } : function () { if ("string" === c) { var b, d = 0, e = m(this), f = a.match(E) || []; while (b = f[d++]) e.hasClass(b) ? e.removeClass(b) : e.addClass(b) } else (c === K || "boolean" === c) && (this.className && m._data(this, "__className__", this.className), this.className = this.className || a === !1 ? "" : m._data(this, "__className__") || "") }) }, hasClass: function (a) { for (var b = " " + a + " ", c = 0, d = this.length; d > c; c++)if (1 === this[c].nodeType && (" " + this[c].className + " ").replace(ub, " ").indexOf(b) >= 0) return !0; return !1 } }), m.each("blur focus focusin focusout load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup error contextmenu".split(" "), function (a, b) { m.fn[b] = function (a, c) { return arguments.length > 0 ? this.on(b, null, a, c) : this.trigger(b) } }), m.fn.extend({ hover: function (a, b) { return this.mouseenter(a).mouseleave(b || a) }, bind: function (a, b, c) { return this.on(a, null, b, c) }, unbind: function (a, b) { return this.off(a, null, b) }, delegate: function (a, b, c, d) { return this.on(b, a, c, d) }, undelegate: function (a, b, c) { return 1 === arguments.length ? this.off(a, "**") : this.off(b, a || "**", c) } }); var vb = m.now(), wb = /\?/, xb = /(,)|(\[|{)|(}|])|"(?:[^"\\\r\n]|\\["\\\/bfnrt]|\\u[\da-fA-F]{4})*"\s*:?|true|false|null|-?(?!0\d)\d+(?:\.\d+|)(?:[eE][+-]?\d+|)/g; m.parseJSON = function (b) { if (a.JSON && a.JSON.parse) return a.JSON.parse(b + ""); var c, d = null, e = m.trim(b + ""); return e && !m.trim(e.replace(xb, function (a, b, e, f) { return c && b && (d = 0), 0 === d ? a : (c = e || b, d += !f - !e, "") })) ? Function("return " + e)() : m.error("Invalid JSON: " + b) }, m.parseXML = function (b) { var c, d; if (!b || "string" != typeof b) return null; try { a.DOMParser ? (d = new DOMParser, c = d.parseFromString(b, "text/xml")) : (c = new ActiveXObject("Microsoft.XMLDOM"), c.async = "false", c.loadXML(b)) } catch (e) { c = void 0 } return c && c.documentElement && !c.getElementsByTagName("parsererror").length || m.error("Invalid XML: " + b), c }; var yb, zb, Ab = /#.*$/, Bb = /([?&])_=[^&]*/, Cb = /^(.*?):[ \t]*([^\r\n]*)\r?$/gm, Db = /^(?:about|app|app-storage|.+-extension|file|res|widget):$/, Eb = /^(?:GET|HEAD)$/, Fb = /^\/\//, Gb = /^([\w.+-]+:)(?:\/\/(?:[^\/?#]*@|)([^\/?#:]*)(?::(\d+)|)|)/, Hb = {}, Ib = {}, Jb = "*/".concat("*"); try { zb = location.href } catch (Kb) { zb = y.createElement("a"), zb.href = "", zb = zb.href } yb = Gb.exec(zb.toLowerCase()) || []; function Lb(a) { return function (b, c) { "string" != typeof b && (c = b, b = "*"); var d, e = 0, f = b.toLowerCase().match(E) || []; if (m.isFunction(c)) while (d = f[e++]) "+" === d.charAt(0) ? (d = d.slice(1) || "*", (a[d] = a[d] || []).unshift(c)) : (a[d] = a[d] || []).push(c) } } function Mb(a, b, c, d) { var e = {}, f = a === Ib; function g(h) { var i; return e[h] = !0, m.each(a[h] || [], function (a, h) { var j = h(b, c, d); return "string" != typeof j || f || e[j] ? f ? !(i = j) : void 0 : (b.dataTypes.unshift(j), g(j), !1) }), i } return g(b.dataTypes[0]) || !e["*"] && g("*") } function Nb(a, b) { var c, d, e = m.ajaxSettings.flatOptions || {}; for (d in b) void 0 !== b[d] && ((e[d] ? a : c || (c = {}))[d] = b[d]); return c && m.extend(!0, a, c), a } function Ob(a, b, c) { var d, e, f, g, h = a.contents, i = a.dataTypes; while ("*" === i[0]) i.shift(), void 0 === e && (e = a.mimeType || b.getResponseHeader("Content-Type")); if (e) for (g in h) if (h[g] && h[g].test(e)) { i.unshift(g); break } if (i[0] in c) f = i[0]; else { for (g in c) { if (!i[0] || a.converters[g + " " + i[0]]) { f = g; break } d || (d = g) } f = f || d } return f ? (f !== i[0] && i.unshift(f), c[f]) : void 0 } function Pb(a, b, c, d) { var e, f, g, h, i, j = {}, k = a.dataTypes.slice(); if (k[1]) for (g in a.converters) j[g.toLowerCase()] = a.converters[g]; f = k.shift(); while (f) if (a.responseFields[f] && (c[a.responseFields[f]] = b), !i && d && a.dataFilter && (b = a.dataFilter(b, a.dataType)), i = f, f = k.shift()) if ("*" === f) f = i; else if ("*" !== i && i !== f) { if (g = j[i + " " + f] || j["* " + f], !g) for (e in j) if (h = e.split(" "), h[1] === f && (g = j[i + " " + h[0]] || j["* " + h[0]])) { g === !0 ? g = j[e] : j[e] !== !0 && (f = h[0], k.unshift(h[1])); break } if (g !== !0) if (g && a["throws"]) b = g(b); else try { b = g(b) } catch (l) { return { state: "parsererror", error: g ? l : "No conversion from " + i + " to " + f } } } return { state: "success", data: b } } m.extend({ active: 0, lastModified: {}, etag: {}, ajaxSettings: { url: zb, type: "GET", isLocal: Db.test(yb[1]), global: !0, processData: !0, async: !0, contentType: "application/x-www-form-urlencoded; charset=UTF-8", accepts: { "*": Jb, text: "text/plain", html: "text/html", xml: "application/xml, text/xml", json: "application/json, text/javascript" }, contents: { xml: /xml/, html: /html/, json: /json/ }, responseFields: { xml: "responseXML", text: "responseText", json: "responseJSON" }, converters: { "* text": String, "text html": !0, "text json": m.parseJSON, "text xml": m.parseXML }, flatOptions: { url: !0, context: !0 } }, ajaxSetup: function (a, b) { return b ? Nb(Nb(a, m.ajaxSettings), b) : Nb(m.ajaxSettings, a) }, ajaxPrefilter: Lb(Hb), ajaxTransport: Lb(Ib), ajax: function (a, b) { "object" == typeof a && (b = a, a = void 0), b = b || {}; var c, d, e, f, g, h, i, j, k = m.ajaxSetup({}, b), l = k.context || k, n = k.context && (l.nodeType || l.jquery) ? m(l) : m.event, o = m.Deferred(), p = m.Callbacks("once memory"), q = k.statusCode || {}, r = {}, s = {}, t = 0, u = "canceled", v = { readyState: 0, getResponseHeader: function (a) { var b; if (2 === t) { if (!j) { j = {}; while (b = Cb.exec(f)) j[b[1].toLowerCase()] = b[2] } b = j[a.toLowerCase()] } return null == b ? null : b }, getAllResponseHeaders: function () { return 2 === t ? f : null }, setRequestHeader: function (a, b) { var c = a.toLowerCase(); return t || (a = s[c] = s[c] || a, r[a] = b), this }, overrideMimeType: function (a) { return t || (k.mimeType = a), this }, statusCode: function (a) { var b; if (a) if (2 > t) for (b in a) q[b] = [q[b], a[b]]; else v.always(a[v.status]); return this }, abort: function (a) { var b = a || u; return i && i.abort(b), x(0, b), this } }; if (o.promise(v).complete = p.add, v.success = v.done, v.error = v.fail, k.url = ((a || k.url || zb) + "").replace(Ab, "").replace(Fb, yb[1] + "//"), k.type = b.method || b.type || k.method || k.type, k.dataTypes = m.trim(k.dataType || "*").toLowerCase().match(E) || [""], null == k.crossDomain && (c = Gb.exec(k.url.toLowerCase()), k.crossDomain = !(!c || c[1] === yb[1] && c[2] === yb[2] && (c[3] || ("http:" === c[1] ? "80" : "443")) === (yb[3] || ("http:" === yb[1] ? "80" : "443")))), k.data && k.processData && "string" != typeof k.data && (k.data = m.param(k.data, k.traditional)), Mb(Hb, k, b, v), 2 === t) return v; h = m.event && k.global, h && 0 === m.active++ && m.event.trigger("ajaxStart"), k.type = k.type.toUpperCase(), k.hasContent = !Eb.test(k.type), e = k.url, k.hasContent || (k.data && (e = k.url += (wb.test(e) ? "&" : "?") + k.data, delete k.data), k.cache === !1 && (k.url = Bb.test(e) ? e.replace(Bb, "$1_=" + vb++) : e + (wb.test(e) ? "&" : "?") + "_=" + vb++)), k.ifModified && (m.lastModified[e] && v.setRequestHeader("If-Modified-Since", m.lastModified[e]), m.etag[e] && v.setRequestHeader("If-None-Match", m.etag[e])), (k.data && k.hasContent && k.contentType !== !1 || b.contentType) && v.setRequestHeader("Content-Type", k.contentType), v.setRequestHeader("Accept", k.dataTypes[0] && k.accepts[k.dataTypes[0]] ? k.accepts[k.dataTypes[0]] + ("*" !== k.dataTypes[0] ? ", " + Jb + "; q=0.01" : "") : k.accepts["*"]); for (d in k.headers) v.setRequestHeader(d, k.headers[d]); if (k.beforeSend && (k.beforeSend.call(l, v, k) === !1 || 2 === t)) return v.abort(); u = "abort"; for (d in { success: 1, error: 1, complete: 1 }) v[d](k[d]); if (i = Mb(Ib, k, b, v)) { v.readyState = 1, h && n.trigger("ajaxSend", [v, k]), k.async && k.timeout > 0 && (g = setTimeout(function () { v.abort("timeout") }, k.timeout)); try { t = 1, i.send(r, x) } catch (w) { if (!(2 > t)) throw w; x(-1, w) } } else x(-1, "No Transport"); function x(a, b, c, d) { var j, r, s, u, w, x = b; 2 !== t && (t = 2, g && clearTimeout(g), i = void 0, f = d || "", v.readyState = a > 0 ? 4 : 0, j = a >= 200 && 300 > a || 304 === a, c && (u = Ob(k, v, c)), u = Pb(k, u, v, j), j ? (k.ifModified && (w = v.getResponseHeader("Last-Modified"), w && (m.lastModified[e] = w), w = v.getResponseHeader("etag"), w && (m.etag[e] = w)), 204 === a || "HEAD" === k.type ? x = "nocontent" : 304 === a ? x = "notmodified" : (x = u.state, r = u.data, s = u.error, j = !s)) : (s = x, (a || !x) && (x = "error", 0 > a && (a = 0))), v.status = a, v.statusText = (b || x) + "", j ? o.resolveWith(l, [r, x, v]) : o.rejectWith(l, [v, x, s]), v.statusCode(q), q = void 0, h && n.trigger(j ? "ajaxSuccess" : "ajaxError", [v, k, j ? r : s]), p.fireWith(l, [v, x]), h && (n.trigger("ajaxComplete", [v, k]), --m.active || m.event.trigger("ajaxStop"))) } return v }, getJSON: function (a, b, c) { return m.get(a, b, c, "json") }, getScript: function (a, b) { return m.get(a, void 0, b, "script") } }), m.each(["get", "post"], function (a, b) { m[b] = function (a, c, d, e) { return m.isFunction(c) && (e = e || d, d = c, c = void 0), m.ajax({ url: a, type: b, dataType: e, data: c, success: d }) } }), m._evalUrl = function (a) { return m.ajax({ url: a, type: "GET", dataType: "script", async: !1, global: !1, "throws": !0 }) }, m.fn.extend({ wrapAll: function (a) { if (m.isFunction(a)) return this.each(function (b) { m(this).wrapAll(a.call(this, b)) }); if (this[0]) { var b = m(a, this[0].ownerDocument).eq(0).clone(!0); this[0].parentNode && b.insertBefore(this[0]), b.map(function () { var a = this; while (a.firstChild && 1 === a.firstChild.nodeType) a = a.firstChild; return a }).append(this) } return this }, wrapInner: function (a) { return this.each(m.isFunction(a) ? function (b) { m(this).wrapInner(a.call(this, b)) } : function () { var b = m(this), c = b.contents(); c.length ? c.wrapAll(a) : b.append(a) }) }, wrap: function (a) { var b = m.isFunction(a); return this.each(function (c) { m(this).wrapAll(b ? a.call(this, c) : a) }) }, unwrap: function () { return this.parent().each(function () { m.nodeName(this, "body") || m(this).replaceWith(this.childNodes) }).end() } }), m.expr.filters.hidden = function (a) { return a.offsetWidth <= 0 && a.offsetHeight <= 0 || !k.reliableHiddenOffsets() && "none" === (a.style && a.style.display || m.css(a, "display")) }, m.expr.filters.visible = function (a) { return !m.expr.filters.hidden(a) }; var Qb = /%20/g, Rb = /\[\]$/, Sb = /\r?\n/g, Tb = /^(?:submit|button|image|reset|file)$/i, Ub = /^(?:input|select|textarea|keygen)/i; function Vb(a, b, c, d) { var e; if (m.isArray(b)) m.each(b, function (b, e) { c || Rb.test(a) ? d(a, e) : Vb(a + "[" + ("object" == typeof e ? b : "") + "]", e, c, d) }); else if (c || "object" !== m.type(b)) d(a, b); else for (e in b) Vb(a + "[" + e + "]", b[e], c, d) } m.param = function (a, b) { var c, d = [], e = function (a, b) { b = m.isFunction(b) ? b() : null == b ? "" : b, d[d.length] = encodeURIComponent(a) + "=" + encodeURIComponent(b) }; if (void 0 === b && (b = m.ajaxSettings && m.ajaxSettings.traditional), m.isArray(a) || a.jquery && !m.isPlainObject(a)) m.each(a, function () { e(this.name, this.value) }); else for (c in a) Vb(c, a[c], b, e); return d.join("&").replace(Qb, "+") }, m.fn.extend({ serialize: function () { return m.param(this.serializeArray()) }, serializeArray: function () { return this.map(function () { var a = m.prop(this, "elements"); return a ? m.makeArray(a) : this }).filter(function () { var a = this.type; return this.name && !m(this).is(":disabled") && Ub.test(this.nodeName) && !Tb.test(a) && (this.checked || !W.test(a)) }).map(function (a, b) { var c = m(this).val(); return null == c ? null : m.isArray(c) ? m.map(c, function (a) { return { name: b.name, value: a.replace(Sb, "\r\n") } }) : { name: b.name, value: c.replace(Sb, "\r\n") } }).get() } }), m.ajaxSettings.xhr = void 0 !== a.ActiveXObject ? function () { return !this.isLocal && /^(get|post|head|put|delete|options)$/i.test(this.type) && Zb() || $b() } : Zb; var Wb = 0, Xb = {}, Yb = m.ajaxSettings.xhr(); a.attachEvent && a.attachEvent("onunload", function () { for (var a in Xb) Xb[a](void 0, !0) }), k.cors = !!Yb && "withCredentials" in Yb, Yb = k.ajax = !!Yb, Yb && m.ajaxTransport(function (a) { if (!a.crossDomain || k.cors) { var b; return { send: function (c, d) { var e, f = a.xhr(), g = ++Wb; if (f.open(a.type, a.url, a.async, a.username, a.password), a.xhrFields) for (e in a.xhrFields) f[e] = a.xhrFields[e]; a.mimeType && f.overrideMimeType && f.overrideMimeType(a.mimeType), a.crossDomain || c["X-Requested-With"] || (c["X-Requested-With"] = "XMLHttpRequest"); for (e in c) void 0 !== c[e] && f.setRequestHeader(e, c[e] + ""); f.send(a.hasContent && a.data || null), b = function (c, e) { var h, i, j; if (b && (e || 4 === f.readyState)) if (delete Xb[g], b = void 0, f.onreadystatechange = m.noop, e) 4 !== f.readyState && f.abort(); else { j = {}, h = f.status, "string" == typeof f.responseText && (j.text = f.responseText); try { i = f.statusText } catch (k) { i = "" } h || !a.isLocal || a.crossDomain ? 1223 === h && (h = 204) : h = j.text ? 200 : 404 } j && d(h, i, j, f.getAllResponseHeaders()) }, a.async ? 4 === f.readyState ? setTimeout(b) : f.onreadystatechange = Xb[g] = b : b() }, abort: function () { b && b(void 0, !0) } } } }); function Zb() { try { return new a.XMLHttpRequest } catch (b) { } } function $b() { try { return new a.ActiveXObject("Microsoft.XMLHTTP") } catch (b) { } } m.ajaxSetup({ accepts: { script: "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript" }, contents: { script: /(?:java|ecma)script/ }, converters: { "text script": function (a) { return m.globalEval(a), a } } }), m.ajaxPrefilter("script", function (a) { void 0 === a.cache && (a.cache = !1), a.crossDomain && (a.type = "GET", a.global = !1) }), m.ajaxTransport("script", function (a) { if (a.crossDomain) { var b, c = y.head || m("head")[0] || y.documentElement; return { send: function (d, e) { b = y.createElement("script"), b.async = !0, a.scriptCharset && (b.charset = a.scriptCharset), b.src = a.url, b.onload = b.onreadystatechange = function (a, c) { (c || !b.readyState || /loaded|complete/.test(b.readyState)) && (b.onload = b.onreadystatechange = null, b.parentNode && b.parentNode.removeChild(b), b = null, c || e(200, "success")) }, c.insertBefore(b, c.firstChild) }, abort: function () { b && b.onload(void 0, !0) } } } }); var _b = [], ac = /(=)\?(?=&|$)|\?\?/; m.ajaxSetup({ jsonp: "callback", jsonpCallback: function () { var a = _b.pop() || m.expando + "_" + vb++; return this[a] = !0, a } }), m.ajaxPrefilter("json jsonp", function (b, c, d) { var e, f, g, h = b.jsonp !== !1 && (ac.test(b.url) ? "url" : "string" == typeof b.data && !(b.contentType || "").indexOf("application/x-www-form-urlencoded") && ac.test(b.data) && "data"); return h || "jsonp" === b.dataTypes[0] ? (e = b.jsonpCallback = m.isFunction(b.jsonpCallback) ? b.jsonpCallback() : b.jsonpCallback, h ? b[h] = b[h].replace(ac, "$1" + e) : b.jsonp !== !1 && (b.url += (wb.test(b.url) ? "&" : "?") + b.jsonp + "=" + e), b.converters["script json"] = function () { return g || m.error(e + " was not called"), g[0] }, b.dataTypes[0] = "json", f = a[e], a[e] = function () { g = arguments }, d.always(function () { a[e] = f, b[e] && (b.jsonpCallback = c.jsonpCallback, _b.push(e)), g && m.isFunction(f) && f(g[0]), g = f = void 0 }), "script") : void 0 }), m.parseHTML = function (a, b, c) { if (!a || "string" != typeof a) return null; "boolean" == typeof b && (c = b, b = !1), b = b || y; var d = u.exec(a), e = !c && []; return d ? [b.createElement(d[1])] : (d = m.buildFragment([a], b, e), e && e.length && m(e).remove(), m.merge([], d.childNodes)) }; var bc = m.fn.load; m.fn.load = function (a, b, c) { if ("string" != typeof a && bc) return bc.apply(this, arguments); var d, e, f, g = this, h = a.indexOf(" "); return h >= 0 && (d = m.trim(a.slice(h, a.length)), a = a.slice(0, h)), m.isFunction(b) ? (c = b, b = void 0) : b && "object" == typeof b && (f = "POST"), g.length > 0 && m.ajax({ url: a, type: f, dataType: "html", data: b }).done(function (a) { e = arguments, g.html(d ? m("<div>").append(m.parseHTML(a)).find(d) : a) }).complete(c && function (a, b) { g.each(c, e || [a.responseText, b, a]) }), this }, m.each(["ajaxStart", "ajaxStop", "ajaxComplete", "ajaxError", "ajaxSuccess", "ajaxSend"], function (a, b) { m.fn[b] = function (a) { return this.on(b, a) } }), m.expr.filters.animated = function (a) { return m.grep(m.timers, function (b) { return a === b.elem }).length }; var cc = a.document.documentElement; function dc(a) { return m.isWindow(a) ? a : 9 === a.nodeType ? a.defaultView || a.parentWindow : !1 } m.offset = { setOffset: function (a, b, c) { var d, e, f, g, h, i, j, k = m.css(a, "position"), l = m(a), n = {}; "static" === k && (a.style.position = "relative"), h = l.offset(), f = m.css(a, "top"), i = m.css(a, "left"), j = ("absolute" === k || "fixed" === k) && m.inArray("auto", [f, i]) > -1, j ? (d = l.position(), g = d.top, e = d.left) : (g = parseFloat(f) || 0, e = parseFloat(i) || 0), m.isFunction(b) && (b = b.call(a, c, h)), null != b.top && (n.top = b.top - h.top + g), null != b.left && (n.left = b.left - h.left + e), "using" in b ? b.using.call(a, n) : l.css(n) } }, m.fn.extend({ offset: function (a) { if (arguments.length) return void 0 === a ? this : this.each(function (b) { m.offset.setOffset(this, a, b) }); var b, c, d = { top: 0, left: 0 }, e = this[0], f = e && e.ownerDocument; if (f) return b = f.documentElement, m.contains(b, e) ? (typeof e.getBoundingClientRect !== K && (d = e.getBoundingClientRect()), c = dc(f), { top: d.top + (c.pageYOffset || b.scrollTop) - (b.clientTop || 0), left: d.left + (c.pageXOffset || b.scrollLeft) - (b.clientLeft || 0) }) : d }, position: function () { if (this[0]) { var a, b, c = { top: 0, left: 0 }, d = this[0]; return "fixed" === m.css(d, "position") ? b = d.getBoundingClientRect() : (a = this.offsetParent(), b = this.offset(), m.nodeName(a[0], "html") || (c = a.offset()), c.top += m.css(a[0], "borderTopWidth", !0), c.left += m.css(a[0], "borderLeftWidth", !0)), { top: b.top - c.top - m.css(d, "marginTop", !0), left: b.left - c.left - m.css(d, "marginLeft", !0) } } }, offsetParent: function () { return this.map(function () { var a = this.offsetParent || cc; while (a && !m.nodeName(a, "html") && "static" === m.css(a, "position")) a = a.offsetParent; return a || cc }) } }), m.each({ scrollLeft: "pageXOffset", scrollTop: "pageYOffset" }, function (a, b) { var c = /Y/.test(b); m.fn[a] = function (d) { return V(this, function (a, d, e) { var f = dc(a); return void 0 === e ? f ? b in f ? f[b] : f.document.documentElement[d] : a[d] : void (f ? f.scrollTo(c ? m(f).scrollLeft() : e, c ? e : m(f).scrollTop()) : a[d] = e) }, a, d, arguments.length, null) } }), m.each(["top", "left"], function (a, b) { m.cssHooks[b] = La(k.pixelPosition, function (a, c) { return c ? (c = Ja(a, b), Ha.test(c) ? m(a).position()[b] + "px" : c) : void 0 }) }), m.each({ Height: "height", Width: "width" }, function (a, b) { m.each({ padding: "inner" + a, content: b, "": "outer" + a }, function (c, d) { m.fn[d] = function (d, e) { var f = arguments.length && (c || "boolean" != typeof d), g = c || (d === !0 || e === !0 ? "margin" : "border"); return V(this, function (b, c, d) { var e; return m.isWindow(b) ? b.document.documentElement["client" + a] : 9 === b.nodeType ? (e = b.documentElement, Math.max(b.body["scroll" + a], e["scroll" + a], b.body["offset" + a], e["offset" + a], e["client" + a])) : void 0 === d ? m.css(b, c, g) : m.style(b, c, d, g) }, b, f ? d : void 0, f, null) } }) }), m.fn.size = function () { return this.length }, m.fn.andSelf = m.fn.addBack, "function" == typeof define && define.amd && define("jquery", [], function () { return m }); var ec = a.jQuery, fc = a.$; return m.noConflict = function (b) { return a.$ === m && (a.$ = fc), b && a.jQuery === m && (a.jQuery = ec), m }, typeof b === K && (a.jQuery = a.$ = m), m
});
//# sourceMappingURL=jquery.min.map
// End Bundle: _jquery

// Start Bundle: _requirejs
/** vim: et:ts=4:sw=4:sts=4
 * @license RequireJS 2.3.6 Copyright jQuery Foundation and other contributors.
 * Released under MIT license, https://github.com/requirejs/requirejs/blob/master/LICENSE
 */
//Not using strict: uneven strict support in browsers, #392, and causes
//problems with requirejs.exec()/transpiler plugins that may not be strict.
/*jslint regexp: true, nomen: true, sloppy: true */
/*global window, navigator, document, importScripts, setTimeout, opera */

var requirejs, require, define;
(function (global, setTimeout) {
    var req, s, head, baseElement, dataMain, src,
        interactiveScript, currentlyAddingScript, mainScript, subPath,
        version = '2.3.6',
        commentRegExp = /\/\*[\s\S]*?\*\/|([^:"'=]|^)\/\/.*$/mg,
        cjsRequireRegExp = /[^.]\s*require\s*\(\s*["']([^'"\s]+)["']\s*\)/g,
        jsSuffixRegExp = /\.js$/,
        currDirRegExp = /^\.\//,
        op = Object.prototype,
        ostring = op.toString,
        hasOwn = op.hasOwnProperty,
        isBrowser = !!(typeof window !== 'undefined' && typeof navigator !== 'undefined' && window.document),
        isWebWorker = !isBrowser && typeof importScripts !== 'undefined',
        //PS3 indicates loaded and complete, but need to wait for complete
        //specifically. Sequence is 'loading', 'loaded', execution,
        // then 'complete'. The UA check is unfortunate, but not sure how
        //to feature test w/o causing perf issues.
        readyRegExp = isBrowser && navigator.platform === 'PLAYSTATION 3' ?
            /^complete$/ : /^(complete|loaded)$/,
        defContextName = '_',
        //Oh the tragedy, detecting opera. See the usage of isOpera for reason.
        isOpera = typeof opera !== 'undefined' && opera.toString() === '[object Opera]',
        contexts = {},
        cfg = {},
        globalDefQueue = [],
        useInteractive = false;

    //Could match something like ')//comment', do not lose the prefix to comment.
    function commentReplace(match, singlePrefix) {
        return singlePrefix || '';
    }

    function isFunction(it) {
        return ostring.call(it) === '[object Function]';
    }

    function isArray(it) {
        return ostring.call(it) === '[object Array]';
    }

    /**
     * Helper function for iterating over an array. If the func returns
     * a true value, it will break out of the loop.
     */
    function each(ary, func) {
        if (ary) {
            var i;
            for (i = 0; i < ary.length; i += 1) {
                if (ary[i] && func(ary[i], i, ary)) {
                    break;
                }
            }
        }
    }

    /**
     * Helper function for iterating over an array backwards. If the func
     * returns a true value, it will break out of the loop.
     */
    function eachReverse(ary, func) {
        if (ary) {
            var i;
            for (i = ary.length - 1; i > -1; i -= 1) {
                if (ary[i] && func(ary[i], i, ary)) {
                    break;
                }
            }
        }
    }

    function hasProp(obj, prop) {
        return hasOwn.call(obj, prop);
    }

    function getOwn(obj, prop) {
        return hasProp(obj, prop) && obj[prop];
    }

    /**
     * Cycles over properties in an object and calls a function for each
     * property value. If the function returns a truthy value, then the
     * iteration is stopped.
     */
    function eachProp(obj, func) {
        var prop;
        for (prop in obj) {
            if (hasProp(obj, prop)) {
                if (func(obj[prop], prop)) {
                    break;
                }
            }
        }
    }

    /**
     * Simple function to mix in properties from source into target,
     * but only if target does not already have a property of the same name.
     */
    function mixin(target, source, force, deepStringMixin) {
        if (source) {
            eachProp(source, function (value, prop) {
                if (force || !hasProp(target, prop)) {
                    if (deepStringMixin && typeof value === 'object' && value &&
                        !isArray(value) && !isFunction(value) &&
                        !(value instanceof RegExp)) {

                        if (!target[prop]) {
                            target[prop] = {};
                        }
                        mixin(target[prop], value, force, deepStringMixin);
                    } else {
                        target[prop] = value;
                    }
                }
            });
        }
        return target;
    }

    //Similar to Function.prototype.bind, but the 'this' object is specified
    //first, since it is easier to read/figure out what 'this' will be.
    function bind(obj, fn) {
        return function () {
            return fn.apply(obj, arguments);
        };
    }

    function scripts() {
        return document.getElementsByTagName('script');
    }

    function defaultOnError(err) {
        throw err;
    }

    //Allow getting a global that is expressed in
    //dot notation, like 'a.b.c'.
    function getGlobal(value) {
        if (!value) {
            return value;
        }
        var g = global;
        each(value.split('.'), function (part) {
            g = g[part];
        });
        return g;
    }

    /**
     * Constructs an error with a pointer to an URL with more information.
     * @param {String} id the error ID that maps to an ID on a web page.
     * @param {String} message human readable error.
     * @param {Error} [err] the original error, if there is one.
     *
     * @returns {Error}
     */
    function makeError(id, msg, err, requireModules) {
        var e = new Error(msg + '\nhttps://requirejs.org/docs/errors.html#' + id);
        e.requireType = id;
        e.requireModules = requireModules;
        if (err) {
            e.originalError = err;
        }
        return e;
    }

    if (typeof define !== 'undefined') {
        //If a define is already in play via another AMD loader,
        //do not overwrite.
        return;
    }

    if (typeof requirejs !== 'undefined') {
        if (isFunction(requirejs)) {
            //Do not overwrite an existing requirejs instance.
            return;
        }
        cfg = requirejs;
        requirejs = undefined;
    }

    //Allow for a require config object
    if (typeof require !== 'undefined' && !isFunction(require)) {
        //assume it is a config object.
        cfg = require;
        require = undefined;
    }

    function newContext(contextName) {
        var inCheckLoaded, Module, context, handlers,
            checkLoadedTimeoutId,
            config = {
                //Defaults. Do not set a default for map
                //config to speed up normalize(), which
                //will run faster if there is no default.
                waitSeconds: 7,
                baseUrl: './',
                paths: {},
                bundles: {},
                pkgs: {},
                shim: {},
                config: {}
            },
            registry = {},
            //registry of just enabled modules, to speed
            //cycle breaking code when lots of modules
            //are registered, but not activated.
            enabledRegistry = {},
            undefEvents = {},
            defQueue = [],
            defined = {},
            urlFetched = {},
            bundlesMap = {},
            requireCounter = 1,
            unnormalizedCounter = 1;

        /**
         * Trims the . and .. from an array of path segments.
         * It will keep a leading path segment if a .. will become
         * the first path segment, to help with module name lookups,
         * which act like paths, but can be remapped. But the end result,
         * all paths that use this function should look normalized.
         * NOTE: this method MODIFIES the input array.
         * @param {Array} ary the array of path segments.
         */
        function trimDots(ary) {
            var i, part;
            for (i = 0; i < ary.length; i++) {
                part = ary[i];
                if (part === '.') {
                    ary.splice(i, 1);
                    i -= 1;
                } else if (part === '..') {
                    // If at the start, or previous value is still ..,
                    // keep them so that when converted to a path it may
                    // still work when converted to a path, even though
                    // as an ID it is less than ideal. In larger point
                    // releases, may be better to just kick out an error.
                    if (i === 0 || (i === 1 && ary[2] === '..') || ary[i - 1] === '..') {
                        continue;
                    } else if (i > 0) {
                        ary.splice(i - 1, 2);
                        i -= 2;
                    }
                }
            }
        }

        /**
         * Given a relative module name, like ./something, normalize it to
         * a real name that can be mapped to a path.
         * @param {String} name the relative name
         * @param {String} baseName a real name that the name arg is relative
         * to.
         * @param {Boolean} applyMap apply the map config to the value. Should
         * only be done if this normalization is for a dependency ID.
         * @returns {String} normalized name
         */
        function normalize(name, baseName, applyMap) {
            var pkgMain, mapValue, nameParts, i, j, nameSegment, lastIndex,
                foundMap, foundI, foundStarMap, starI, normalizedBaseParts,
                baseParts = (baseName && baseName.split('/')),
                map = config.map,
                starMap = map && map['*'];

            //Adjust any relative paths.
            if (name) {
                name = name.split('/');
                lastIndex = name.length - 1;

                // If wanting node ID compatibility, strip .js from end
                // of IDs. Have to do this here, and not in nameToUrl
                // because node allows either .js or non .js to map
                // to same file.
                if (config.nodeIdCompat && jsSuffixRegExp.test(name[lastIndex])) {
                    name[lastIndex] = name[lastIndex].replace(jsSuffixRegExp, '');
                }

                // Starts with a '.' so need the baseName
                if (name[0].charAt(0) === '.' && baseParts) {
                    //Convert baseName to array, and lop off the last part,
                    //so that . matches that 'directory' and not name of the baseName's
                    //module. For instance, baseName of 'one/two/three', maps to
                    //'one/two/three.js', but we want the directory, 'one/two' for
                    //this normalization.
                    normalizedBaseParts = baseParts.slice(0, baseParts.length - 1);
                    name = normalizedBaseParts.concat(name);
                }

                trimDots(name);
                name = name.join('/');
            }

            //Apply map config if available.
            if (applyMap && map && (baseParts || starMap)) {
                nameParts = name.split('/');

                outerLoop: for (i = nameParts.length; i > 0; i -= 1) {
                    nameSegment = nameParts.slice(0, i).join('/');

                    if (baseParts) {
                        //Find the longest baseName segment match in the config.
                        //So, do joins on the biggest to smallest lengths of baseParts.
                        for (j = baseParts.length; j > 0; j -= 1) {
                            mapValue = getOwn(map, baseParts.slice(0, j).join('/'));

                            //baseName segment has config, find if it has one for
                            //this name.
                            if (mapValue) {
                                mapValue = getOwn(mapValue, nameSegment);
                                if (mapValue) {
                                    //Match, update name to the new value.
                                    foundMap = mapValue;
                                    foundI = i;
                                    break outerLoop;
                                }
                            }
                        }
                    }

                    //Check for a star map match, but just hold on to it,
                    //if there is a shorter segment match later in a matching
                    //config, then favor over this star map.
                    if (!foundStarMap && starMap && getOwn(starMap, nameSegment)) {
                        foundStarMap = getOwn(starMap, nameSegment);
                        starI = i;
                    }
                }

                if (!foundMap && foundStarMap) {
                    foundMap = foundStarMap;
                    foundI = starI;
                }

                if (foundMap) {
                    nameParts.splice(0, foundI, foundMap);
                    name = nameParts.join('/');
                }
            }

            // If the name points to a package's name, use
            // the package main instead.
            pkgMain = getOwn(config.pkgs, name);

            return pkgMain ? pkgMain : name;
        }

        function removeScript(name) {
            if (isBrowser) {
                each(scripts(), function (scriptNode) {
                    if (scriptNode.getAttribute('data-requiremodule') === name &&
                        scriptNode.getAttribute('data-requirecontext') === context.contextName) {
                        scriptNode.parentNode.removeChild(scriptNode);
                        return true;
                    }
                });
            }
        }

        function hasPathFallback(id) {
            var pathConfig = getOwn(config.paths, id);
            if (pathConfig && isArray(pathConfig) && pathConfig.length > 1) {
                //Pop off the first array value, since it failed, and
                //retry
                pathConfig.shift();
                context.require.undef(id);

                //Custom require that does not do map translation, since
                //ID is "absolute", already mapped/resolved.
                context.makeRequire(null, {
                    skipMap: true
                })([id]);

                return true;
            }
        }

        //Turns a plugin!resource to [plugin, resource]
        //with the plugin being undefined if the name
        //did not have a plugin prefix.
        function splitPrefix(name) {
            var prefix,
                index = name ? name.indexOf('!') : -1;
            if (index > -1) {
                prefix = name.substring(0, index);
                name = name.substring(index + 1, name.length);
            }
            return [prefix, name];
        }

        /**
         * Creates a module mapping that includes plugin prefix, module
         * name, and path. If parentModuleMap is provided it will
         * also normalize the name via require.normalize()
         *
         * @param {String} name the module name
         * @param {String} [parentModuleMap] parent module map
         * for the module name, used to resolve relative names.
         * @param {Boolean} isNormalized: is the ID already normalized.
         * This is true if this call is done for a define() module ID.
         * @param {Boolean} applyMap: apply the map config to the ID.
         * Should only be true if this map is for a dependency.
         *
         * @returns {Object}
         */
        function makeModuleMap(name, parentModuleMap, isNormalized, applyMap) {
            var url, pluginModule, suffix, nameParts,
                prefix = null,
                parentName = parentModuleMap ? parentModuleMap.name : null,
                originalName = name,
                isDefine = true,
                normalizedName = '';

            //If no name, then it means it is a require call, generate an
            //internal name.
            if (!name) {
                isDefine = false;
                name = '_@r' + (requireCounter += 1);
            }

            nameParts = splitPrefix(name);
            prefix = nameParts[0];
            name = nameParts[1];

            if (prefix) {
                prefix = normalize(prefix, parentName, applyMap);
                pluginModule = getOwn(defined, prefix);
            }

            //Account for relative paths if there is a base name.
            if (name) {
                if (prefix) {
                    if (isNormalized) {
                        normalizedName = name;
                    } else if (pluginModule && pluginModule.normalize) {
                        //Plugin is loaded, use its normalize method.
                        normalizedName = pluginModule.normalize(name, function (name) {
                            return normalize(name, parentName, applyMap);
                        });
                    } else {
                        // If nested plugin references, then do not try to
                        // normalize, as it will not normalize correctly. This
                        // places a restriction on resourceIds, and the longer
                        // term solution is not to normalize until plugins are
                        // loaded and all normalizations to allow for async
                        // loading of a loader plugin. But for now, fixes the
                        // common uses. Details in #1131
                        normalizedName = name.indexOf('!') === -1 ?
                            normalize(name, parentName, applyMap) :
                            name;
                    }
                } else {
                    //A regular module.
                    normalizedName = normalize(name, parentName, applyMap);

                    //Normalized name may be a plugin ID due to map config
                    //application in normalize. The map config values must
                    //already be normalized, so do not need to redo that part.
                    nameParts = splitPrefix(normalizedName);
                    prefix = nameParts[0];
                    normalizedName = nameParts[1];
                    isNormalized = true;

                    url = context.nameToUrl(normalizedName);
                }
            }

            //If the id is a plugin id that cannot be determined if it needs
            //normalization, stamp it with a unique ID so two matching relative
            //ids that may conflict can be separate.
            suffix = prefix && !pluginModule && !isNormalized ?
                '_unnormalized' + (unnormalizedCounter += 1) :
                '';

            return {
                prefix: prefix,
                name: normalizedName,
                parentMap: parentModuleMap,
                unnormalized: !!suffix,
                url: url,
                originalName: originalName,
                isDefine: isDefine,
                id: (prefix ?
                    prefix + '!' + normalizedName :
                    normalizedName) + suffix
            };
        }

        function getModule(depMap) {
            var id = depMap.id,
                mod = getOwn(registry, id);

            if (!mod) {
                mod = registry[id] = new context.Module(depMap);
            }

            return mod;
        }

        function on(depMap, name, fn) {
            var id = depMap.id,
                mod = getOwn(registry, id);

            if (hasProp(defined, id) &&
                (!mod || mod.defineEmitComplete)) {
                if (name === 'defined') {
                    fn(defined[id]);
                }
            } else {
                mod = getModule(depMap);
                if (mod.error && name === 'error') {
                    fn(mod.error);
                } else {
                    mod.on(name, fn);
                }
            }
        }

        function onError(err, errback) {
            var ids = err.requireModules,
                notified = false;

            if (errback) {
                errback(err);
            } else {
                each(ids, function (id) {
                    var mod = getOwn(registry, id);
                    if (mod) {
                        //Set error on module, so it skips timeout checks.
                        mod.error = err;
                        if (mod.events.error) {
                            notified = true;
                            mod.emit('error', err);
                        }
                    }
                });

                if (!notified) {
                    req.onError(err);
                }
            }
        }

        /**
         * Internal method to transfer globalQueue items to this context's
         * defQueue.
         */
        function takeGlobalQueue() {
            //Push all the globalDefQueue items into the context's defQueue
            if (globalDefQueue.length) {
                each(globalDefQueue, function (queueItem) {
                    var id = queueItem[0];
                    if (typeof id === 'string') {
                        context.defQueueMap[id] = true;
                    }
                    defQueue.push(queueItem);
                });
                globalDefQueue = [];
            }
        }

        handlers = {
            'require': function (mod) {
                if (mod.require) {
                    return mod.require;
                } else {
                    return (mod.require = context.makeRequire(mod.map));
                }
            },
            'exports': function (mod) {
                mod.usingExports = true;
                if (mod.map.isDefine) {
                    if (mod.exports) {
                        return (defined[mod.map.id] = mod.exports);
                    } else {
                        return (mod.exports = defined[mod.map.id] = {});
                    }
                }
            },
            'module': function (mod) {
                if (mod.module) {
                    return mod.module;
                } else {
                    return (mod.module = {
                        id: mod.map.id,
                        uri: mod.map.url,
                        config: function () {
                            return getOwn(config.config, mod.map.id) || {};
                        },
                        exports: mod.exports || (mod.exports = {})
                    });
                }
            }
        };

        function cleanRegistry(id) {
            //Clean up machinery used for waiting modules.
            delete registry[id];
            delete enabledRegistry[id];
        }

        function breakCycle(mod, traced, processed) {
            var id = mod.map.id;

            if (mod.error) {
                mod.emit('error', mod.error);
            } else {
                traced[id] = true;
                each(mod.depMaps, function (depMap, i) {
                    var depId = depMap.id,
                        dep = getOwn(registry, depId);

                    //Only force things that have not completed
                    //being defined, so still in the registry,
                    //and only if it has not been matched up
                    //in the module already.
                    if (dep && !mod.depMatched[i] && !processed[depId]) {
                        if (getOwn(traced, depId)) {
                            mod.defineDep(i, defined[depId]);
                            mod.check(); //pass false?
                        } else {
                            breakCycle(dep, traced, processed);
                        }
                    }
                });
                processed[id] = true;
            }
        }

        function checkLoaded() {
            var err, usingPathFallback,
                waitInterval = config.waitSeconds * 1000,
                //It is possible to disable the wait interval by using waitSeconds of 0.
                expired = waitInterval && (context.startTime + waitInterval) < new Date().getTime(),
                noLoads = [],
                reqCalls = [],
                stillLoading = false,
                needCycleCheck = true;

            //Do not bother if this call was a result of a cycle break.
            if (inCheckLoaded) {
                return;
            }

            inCheckLoaded = true;

            //Figure out the state of all the modules.
            eachProp(enabledRegistry, function (mod) {
                var map = mod.map,
                    modId = map.id;

                //Skip things that are not enabled or in error state.
                if (!mod.enabled) {
                    return;
                }

                if (!map.isDefine) {
                    reqCalls.push(mod);
                }

                if (!mod.error) {
                    //If the module should be executed, and it has not
                    //been inited and time is up, remember it.
                    if (!mod.inited && expired) {
                        if (hasPathFallback(modId)) {
                            usingPathFallback = true;
                            stillLoading = true;
                        } else {
                            noLoads.push(modId);
                            removeScript(modId);
                        }
                    } else if (!mod.inited && mod.fetched && map.isDefine) {
                        stillLoading = true;
                        if (!map.prefix) {
                            //No reason to keep looking for unfinished
                            //loading. If the only stillLoading is a
                            //plugin resource though, keep going,
                            //because it may be that a plugin resource
                            //is waiting on a non-plugin cycle.
                            return (needCycleCheck = false);
                        }
                    }
                }
            });

            if (expired && noLoads.length) {
                //If wait time expired, throw error of unloaded modules.
                err = makeError('timeout', 'Load timeout for modules: ' + noLoads, null, noLoads);
                err.contextName = context.contextName;
                return onError(err);
            }

            //Not expired, check for a cycle.
            if (needCycleCheck) {
                each(reqCalls, function (mod) {
                    breakCycle(mod, {}, {});
                });
            }

            //If still waiting on loads, and the waiting load is something
            //other than a plugin resource, or there are still outstanding
            //scripts, then just try back later.
            if ((!expired || usingPathFallback) && stillLoading) {
                //Something is still waiting to load. Wait for it, but only
                //if a timeout is not already in effect.
                if ((isBrowser || isWebWorker) && !checkLoadedTimeoutId) {
                    checkLoadedTimeoutId = setTimeout(function () {
                        checkLoadedTimeoutId = 0;
                        checkLoaded();
                    }, 50);
                }
            }

            inCheckLoaded = false;
        }

        Module = function (map) {
            this.events = getOwn(undefEvents, map.id) || {};
            this.map = map;
            this.shim = getOwn(config.shim, map.id);
            this.depExports = [];
            this.depMaps = [];
            this.depMatched = [];
            this.pluginMaps = {};
            this.depCount = 0;

            /* this.exports this.factory
               this.depMaps = [],
               this.enabled, this.fetched
            */
        };

        Module.prototype = {
            init: function (depMaps, factory, errback, options) {
                options = options || {};

                //Do not do more inits if already done. Can happen if there
                //are multiple define calls for the same module. That is not
                //a normal, common case, but it is also not unexpected.
                if (this.inited) {
                    return;
                }

                this.factory = factory;

                if (errback) {
                    //Register for errors on this module.
                    this.on('error', errback);
                } else if (this.events.error) {
                    //If no errback already, but there are error listeners
                    //on this module, set up an errback to pass to the deps.
                    errback = bind(this, function (err) {
                        this.emit('error', err);
                    });
                }

                //Do a copy of the dependency array, so that
                //source inputs are not modified. For example
                //"shim" deps are passed in here directly, and
                //doing a direct modification of the depMaps array
                //would affect that config.
                this.depMaps = depMaps && depMaps.slice(0);

                this.errback = errback;

                //Indicate this module has be initialized
                this.inited = true;

                this.ignore = options.ignore;

                //Could have option to init this module in enabled mode,
                //or could have been previously marked as enabled. However,
                //the dependencies are not known until init is called. So
                //if enabled previously, now trigger dependencies as enabled.
                if (options.enabled || this.enabled) {
                    //Enable this module and dependencies.
                    //Will call this.check()
                    this.enable();
                } else {
                    this.check();
                }
            },

            defineDep: function (i, depExports) {
                //Because of cycles, defined callback for a given
                //export can be called more than once.
                if (!this.depMatched[i]) {
                    this.depMatched[i] = true;
                    this.depCount -= 1;
                    this.depExports[i] = depExports;
                }
            },

            fetch: function () {
                if (this.fetched) {
                    return;
                }
                this.fetched = true;

                context.startTime = (new Date()).getTime();

                var map = this.map;

                //If the manager is for a plugin managed resource,
                //ask the plugin to load it now.
                if (this.shim) {
                    context.makeRequire(this.map, {
                        enableBuildCallback: true
                    })(this.shim.deps || [], bind(this, function () {
                        return map.prefix ? this.callPlugin() : this.load();
                    }));
                } else {
                    //Regular dependency.
                    return map.prefix ? this.callPlugin() : this.load();
                }
            },

            load: function () {
                var url = this.map.url;

                //Regular dependency.
                if (!urlFetched[url]) {
                    urlFetched[url] = true;
                    context.load(this.map.id, url);
                }
            },

            /**
             * Checks if the module is ready to define itself, and if so,
             * define it.
             */
            check: function () {
                if (!this.enabled || this.enabling) {
                    return;
                }

                var err, cjsModule,
                    id = this.map.id,
                    depExports = this.depExports,
                    exports = this.exports,
                    factory = this.factory;

                if (!this.inited) {
                    // Only fetch if not already in the defQueue.
                    if (!hasProp(context.defQueueMap, id)) {
                        this.fetch();
                    }
                } else if (this.error) {
                    this.emit('error', this.error);
                } else if (!this.defining) {
                    //The factory could trigger another require call
                    //that would result in checking this module to
                    //define itself again. If already in the process
                    //of doing that, skip this work.
                    this.defining = true;

                    if (this.depCount < 1 && !this.defined) {
                        if (isFunction(factory)) {
                            //If there is an error listener, favor passing
                            //to that instead of throwing an error. However,
                            //only do it for define()'d  modules. require
                            //errbacks should not be called for failures in
                            //their callbacks (#699). However if a global
                            //onError is set, use that.
                            if ((this.events.error && this.map.isDefine) ||
                                req.onError !== defaultOnError) {
                                try {
                                    exports = context.execCb(id, factory, depExports, exports);
                                } catch (e) {
                                    err = e;
                                }
                            } else {
                                exports = context.execCb(id, factory, depExports, exports);
                            }

                            // Favor return value over exports. If node/cjs in play,
                            // then will not have a return value anyway. Favor
                            // module.exports assignment over exports object.
                            if (this.map.isDefine && exports === undefined) {
                                cjsModule = this.module;
                                if (cjsModule) {
                                    exports = cjsModule.exports;
                                } else if (this.usingExports) {
                                    //exports already set the defined value.
                                    exports = this.exports;
                                }
                            }

                            if (err) {
                                err.requireMap = this.map;
                                err.requireModules = this.map.isDefine ? [this.map.id] : null;
                                err.requireType = this.map.isDefine ? 'define' : 'require';
                                return onError((this.error = err));
                            }

                        } else {
                            //Just a literal value
                            exports = factory;
                        }

                        this.exports = exports;

                        if (this.map.isDefine && !this.ignore) {
                            defined[id] = exports;

                            if (req.onResourceLoad) {
                                var resLoadMaps = [];
                                each(this.depMaps, function (depMap) {
                                    resLoadMaps.push(depMap.normalizedMap || depMap);
                                });
                                req.onResourceLoad(context, this.map, resLoadMaps);
                            }
                        }

                        //Clean up
                        cleanRegistry(id);

                        this.defined = true;
                    }

                    //Finished the define stage. Allow calling check again
                    //to allow define notifications below in the case of a
                    //cycle.
                    this.defining = false;

                    if (this.defined && !this.defineEmitted) {
                        this.defineEmitted = true;
                        this.emit('defined', this.exports);
                        this.defineEmitComplete = true;
                    }

                }
            },

            callPlugin: function () {
                var map = this.map,
                    id = map.id,
                    //Map already normalized the prefix.
                    pluginMap = makeModuleMap(map.prefix);

                //Mark this as a dependency for this plugin, so it
                //can be traced for cycles.
                this.depMaps.push(pluginMap);

                on(pluginMap, 'defined', bind(this, function (plugin) {
                    var load, normalizedMap, normalizedMod,
                        bundleId = getOwn(bundlesMap, this.map.id),
                        name = this.map.name,
                        parentName = this.map.parentMap ? this.map.parentMap.name : null,
                        localRequire = context.makeRequire(map.parentMap, {
                            enableBuildCallback: true
                        });

                    //If current map is not normalized, wait for that
                    //normalized name to load instead of continuing.
                    if (this.map.unnormalized) {
                        //Normalize the ID if the plugin allows it.
                        if (plugin.normalize) {
                            name = plugin.normalize(name, function (name) {
                                return normalize(name, parentName, true);
                            }) || '';
                        }

                        //prefix and name should already be normalized, no need
                        //for applying map config again either.
                        normalizedMap = makeModuleMap(map.prefix + '!' + name,
                            this.map.parentMap,
                            true);
                        on(normalizedMap,
                            'defined', bind(this, function (value) {
                                this.map.normalizedMap = normalizedMap;
                                this.init([], function () { return value; }, null, {
                                    enabled: true,
                                    ignore: true
                                });
                            }));

                        normalizedMod = getOwn(registry, normalizedMap.id);
                        if (normalizedMod) {
                            //Mark this as a dependency for this plugin, so it
                            //can be traced for cycles.
                            this.depMaps.push(normalizedMap);

                            if (this.events.error) {
                                normalizedMod.on('error', bind(this, function (err) {
                                    this.emit('error', err);
                                }));
                            }
                            normalizedMod.enable();
                        }

                        return;
                    }

                    //If a paths config, then just load that file instead to
                    //resolve the plugin, as it is built into that paths layer.
                    if (bundleId) {
                        this.map.url = context.nameToUrl(bundleId);
                        this.load();
                        return;
                    }

                    load = bind(this, function (value) {
                        this.init([], function () { return value; }, null, {
                            enabled: true
                        });
                    });

                    load.error = bind(this, function (err) {
                        this.inited = true;
                        this.error = err;
                        err.requireModules = [id];

                        //Remove temp unnormalized modules for this module,
                        //since they will never be resolved otherwise now.
                        eachProp(registry, function (mod) {
                            if (mod.map.id.indexOf(id + '_unnormalized') === 0) {
                                cleanRegistry(mod.map.id);
                            }
                        });

                        onError(err);
                    });

                    //Allow plugins to load other code without having to know the
                    //context or how to 'complete' the load.
                    load.fromText = bind(this, function (text, textAlt) {
                        /*jslint evil: true */
                        var moduleName = map.name,
                            moduleMap = makeModuleMap(moduleName),
                            hasInteractive = useInteractive;

                        //As of 2.1.0, support just passing the text, to reinforce
                        //fromText only being called once per resource. Still
                        //support old style of passing moduleName but discard
                        //that moduleName in favor of the internal ref.
                        if (textAlt) {
                            text = textAlt;
                        }

                        //Turn off interactive script matching for IE for any define
                        //calls in the text, then turn it back on at the end.
                        if (hasInteractive) {
                            useInteractive = false;
                        }

                        //Prime the system by creating a module instance for
                        //it.
                        getModule(moduleMap);

                        //Transfer any config to this other module.
                        if (hasProp(config.config, id)) {
                            config.config[moduleName] = config.config[id];
                        }

                        try {
                            req.exec(text);
                        } catch (e) {
                            return onError(makeError('fromtexteval',
                                'fromText eval for ' + id +
                                ' failed: ' + e,
                                e,
                                [id]));
                        }

                        if (hasInteractive) {
                            useInteractive = true;
                        }

                        //Mark this as a dependency for the plugin
                        //resource
                        this.depMaps.push(moduleMap);

                        //Support anonymous modules.
                        context.completeLoad(moduleName);

                        //Bind the value of that module to the value for this
                        //resource ID.
                        localRequire([moduleName], load);
                    });

                    //Use parentName here since the plugin's name is not reliable,
                    //could be some weird string with no path that actually wants to
                    //reference the parentName's path.
                    plugin.load(map.name, localRequire, load, config);
                }));

                context.enable(pluginMap, this);
                this.pluginMaps[pluginMap.id] = pluginMap;
            },

            enable: function () {
                enabledRegistry[this.map.id] = this;
                this.enabled = true;

                //Set flag mentioning that the module is enabling,
                //so that immediate calls to the defined callbacks
                //for dependencies do not trigger inadvertent load
                //with the depCount still being zero.
                this.enabling = true;

                //Enable each dependency
                each(this.depMaps, bind(this, function (depMap, i) {
                    var id, mod, handler;

                    if (typeof depMap === 'string') {
                        //Dependency needs to be converted to a depMap
                        //and wired up to this module.
                        depMap = makeModuleMap(depMap,
                            (this.map.isDefine ? this.map : this.map.parentMap),
                            false,
                            !this.skipMap);
                        this.depMaps[i] = depMap;

                        handler = getOwn(handlers, depMap.id);

                        if (handler) {
                            this.depExports[i] = handler(this);
                            return;
                        }

                        this.depCount += 1;

                        on(depMap, 'defined', bind(this, function (depExports) {
                            if (this.undefed) {
                                return;
                            }
                            this.defineDep(i, depExports);
                            this.check();
                        }));

                        if (this.errback) {
                            on(depMap, 'error', bind(this, this.errback));
                        } else if (this.events.error) {
                            // No direct errback on this module, but something
                            // else is listening for errors, so be sure to
                            // propagate the error correctly.
                            on(depMap, 'error', bind(this, function (err) {
                                this.emit('error', err);
                            }));
                        }
                    }

                    id = depMap.id;
                    mod = registry[id];

                    //Skip special modules like 'require', 'exports', 'module'
                    //Also, don't call enable if it is already enabled,
                    //important in circular dependency cases.
                    if (!hasProp(handlers, id) && mod && !mod.enabled) {
                        context.enable(depMap, this);
                    }
                }));

                //Enable each plugin that is used in
                //a dependency
                eachProp(this.pluginMaps, bind(this, function (pluginMap) {
                    var mod = getOwn(registry, pluginMap.id);
                    if (mod && !mod.enabled) {
                        context.enable(pluginMap, this);
                    }
                }));

                this.enabling = false;

                this.check();
            },

            on: function (name, cb) {
                var cbs = this.events[name];
                if (!cbs) {
                    cbs = this.events[name] = [];
                }
                cbs.push(cb);
            },

            emit: function (name, evt) {
                each(this.events[name], function (cb) {
                    cb(evt);
                });
                if (name === 'error') {
                    //Now that the error handler was triggered, remove
                    //the listeners, since this broken Module instance
                    //can stay around for a while in the registry.
                    delete this.events[name];
                }
            }
        };

        function callGetModule(args) {
            //Skip modules already defined.
            if (!hasProp(defined, args[0])) {
                getModule(makeModuleMap(args[0], null, true)).init(args[1], args[2]);
            }
        }

        function removeListener(node, func, name, ieName) {
            //Favor detachEvent because of IE9
            //issue, see attachEvent/addEventListener comment elsewhere
            //in this file.
            if (node.detachEvent && !isOpera) {
                //Probably IE. If not it will throw an error, which will be
                //useful to know.
                if (ieName) {
                    node.detachEvent(ieName, func);
                }
            } else {
                node.removeEventListener(name, func, false);
            }
        }

        /**
         * Given an event from a script node, get the requirejs info from it,
         * and then removes the event listeners on the node.
         * @param {Event} evt
         * @returns {Object}
         */
        function getScriptData(evt) {
            //Using currentTarget instead of target for Firefox 2.0's sake. Not
            //all old browsers will be supported, but this one was easy enough
            //to support and still makes sense.
            var node = evt.currentTarget || evt.srcElement;

            //Remove the listeners once here.
            removeListener(node, context.onScriptLoad, 'load', 'onreadystatechange');
            removeListener(node, context.onScriptError, 'error');

            return {
                node: node,
                id: node && node.getAttribute('data-requiremodule')
            };
        }

        function intakeDefines() {
            var args;

            //Any defined modules in the global queue, intake them now.
            takeGlobalQueue();

            //Make sure any remaining defQueue items get properly processed.
            while (defQueue.length) {
                args = defQueue.shift();
                if (args[0] === null) {
                    return onError(makeError('mismatch', 'Mismatched anonymous define() module: ' +
                        args[args.length - 1]));
                } else {
                    //args are id, deps, factory. Should be normalized by the
                    //define() function.
                    callGetModule(args);
                }
            }
            context.defQueueMap = {};
        }

        context = {
            config: config,
            contextName: contextName,
            registry: registry,
            defined: defined,
            urlFetched: urlFetched,
            defQueue: defQueue,
            defQueueMap: {},
            Module: Module,
            makeModuleMap: makeModuleMap,
            nextTick: req.nextTick,
            onError: onError,

            /**
             * Set a configuration for the context.
             * @param {Object} cfg config object to integrate.
             */
            configure: function (cfg) {
                //Make sure the baseUrl ends in a slash.
                if (cfg.baseUrl) {
                    if (cfg.baseUrl.charAt(cfg.baseUrl.length - 1) !== '/') {
                        cfg.baseUrl += '/';
                    }
                }

                // Convert old style urlArgs string to a function.
                if (typeof cfg.urlArgs === 'string') {
                    var urlArgs = cfg.urlArgs;
                    cfg.urlArgs = function (id, url) {
                        return (url.indexOf('?') === -1 ? '?' : '&') + urlArgs;
                    };
                }

                //Save off the paths since they require special processing,
                //they are additive.
                var shim = config.shim,
                    objs = {
                        paths: true,
                        bundles: true,
                        config: true,
                        map: true
                    };

                eachProp(cfg, function (value, prop) {
                    if (objs[prop]) {
                        if (!config[prop]) {
                            config[prop] = {};
                        }
                        mixin(config[prop], value, true, true);
                    } else {
                        config[prop] = value;
                    }
                });

                //Reverse map the bundles
                if (cfg.bundles) {
                    eachProp(cfg.bundles, function (value, prop) {
                        each(value, function (v) {
                            if (v !== prop) {
                                bundlesMap[v] = prop;
                            }
                        });
                    });
                }

                //Merge shim
                if (cfg.shim) {
                    eachProp(cfg.shim, function (value, id) {
                        //Normalize the structure
                        if (isArray(value)) {
                            value = {
                                deps: value
                            };
                        }
                        if ((value.exports || value.init) && !value.exportsFn) {
                            value.exportsFn = context.makeShimExports(value);
                        }
                        shim[id] = value;
                    });
                    config.shim = shim;
                }

                //Adjust packages if necessary.
                if (cfg.packages) {
                    each(cfg.packages, function (pkgObj) {
                        var location, name;

                        pkgObj = typeof pkgObj === 'string' ? { name: pkgObj } : pkgObj;

                        name = pkgObj.name;
                        location = pkgObj.location;
                        if (location) {
                            config.paths[name] = pkgObj.location;
                        }

                        //Save pointer to main module ID for pkg name.
                        //Remove leading dot in main, so main paths are normalized,
                        //and remove any trailing .js, since different package
                        //envs have different conventions: some use a module name,
                        //some use a file name.
                        config.pkgs[name] = pkgObj.name + '/' + (pkgObj.main || 'main')
                            .replace(currDirRegExp, '')
                            .replace(jsSuffixRegExp, '');
                    });
                }

                //If there are any "waiting to execute" modules in the registry,
                //update the maps for them, since their info, like URLs to load,
                //may have changed.
                eachProp(registry, function (mod, id) {
                    //If module already has init called, since it is too
                    //late to modify them, and ignore unnormalized ones
                    //since they are transient.
                    if (!mod.inited && !mod.map.unnormalized) {
                        mod.map = makeModuleMap(id, null, true);
                    }
                });

                //If a deps array or a config callback is specified, then call
                //require with those args. This is useful when require is defined as a
                //config object before require.js is loaded.
                if (cfg.deps || cfg.callback) {
                    context.require(cfg.deps || [], cfg.callback);
                }
            },

            makeShimExports: function (value) {
                function fn() {
                    var ret;
                    if (value.init) {
                        ret = value.init.apply(global, arguments);
                    }
                    return ret || (value.exports && getGlobal(value.exports));
                }
                return fn;
            },

            makeRequire: function (relMap, options) {
                options = options || {};

                function localRequire(deps, callback, errback) {
                    var id, map, requireMod;

                    if (options.enableBuildCallback && callback && isFunction(callback)) {
                        callback.__requireJsBuild = true;
                    }

                    if (typeof deps === 'string') {
                        if (isFunction(callback)) {
                            //Invalid call
                            return onError(makeError('requireargs', 'Invalid require call'), errback);
                        }

                        //If require|exports|module are requested, get the
                        //value for them from the special handlers. Caveat:
                        //this only works while module is being defined.
                        if (relMap && hasProp(handlers, deps)) {
                            return handlers[deps](registry[relMap.id]);
                        }

                        //Synchronous access to one module. If require.get is
                        //available (as in the Node adapter), prefer that.
                        if (req.get) {
                            return req.get(context, deps, relMap, localRequire);
                        }

                        //Normalize module name, if it contains . or ..
                        map = makeModuleMap(deps, relMap, false, true);
                        id = map.id;

                        if (!hasProp(defined, id)) {
                            return onError(makeError('notloaded', 'Module name "' +
                                id +
                                '" has not been loaded yet for context: ' +
                                contextName +
                                (relMap ? '' : '. Use require([])')));
                        }
                        return defined[id];
                    }

                    //Grab defines waiting in the global queue.
                    intakeDefines();

                    //Mark all the dependencies as needing to be loaded.
                    context.nextTick(function () {
                        //Some defines could have been added since the
                        //require call, collect them.
                        intakeDefines();

                        requireMod = getModule(makeModuleMap(null, relMap));

                        //Store if map config should be applied to this require
                        //call for dependencies.
                        requireMod.skipMap = options.skipMap;

                        requireMod.init(deps, callback, errback, {
                            enabled: true
                        });

                        checkLoaded();
                    });

                    return localRequire;
                }

                mixin(localRequire, {
                    isBrowser: isBrowser,

                    /**
                     * Converts a module name + .extension into an URL path.
                     * *Requires* the use of a module name. It does not support using
                     * plain URLs like nameToUrl.
                     */
                    toUrl: function (moduleNamePlusExt) {
                        var ext,
                            index = moduleNamePlusExt.lastIndexOf('.'),
                            segment = moduleNamePlusExt.split('/')[0],
                            isRelative = segment === '.' || segment === '..';

                        //Have a file extension alias, and it is not the
                        //dots from a relative path.
                        if (index !== -1 && (!isRelative || index > 1)) {
                            ext = moduleNamePlusExt.substring(index, moduleNamePlusExt.length);
                            moduleNamePlusExt = moduleNamePlusExt.substring(0, index);
                        }

                        return context.nameToUrl(normalize(moduleNamePlusExt,
                            relMap && relMap.id, true), ext, true);
                    },

                    defined: function (id) {
                        return hasProp(defined, makeModuleMap(id, relMap, false, true).id);
                    },

                    specified: function (id) {
                        id = makeModuleMap(id, relMap, false, true).id;
                        return hasProp(defined, id) || hasProp(registry, id);
                    }
                });

                //Only allow undef on top level require calls
                if (!relMap) {
                    localRequire.undef = function (id) {
                        //Bind any waiting define() calls to this context,
                        //fix for #408
                        takeGlobalQueue();

                        var map = makeModuleMap(id, relMap, true),
                            mod = getOwn(registry, id);

                        mod.undefed = true;
                        removeScript(id);

                        delete defined[id];
                        delete urlFetched[map.url];
                        delete undefEvents[id];

                        //Clean queued defines too. Go backwards
                        //in array so that the splices do not
                        //mess up the iteration.
                        eachReverse(defQueue, function (args, i) {
                            if (args[0] === id) {
                                defQueue.splice(i, 1);
                            }
                        });
                        delete context.defQueueMap[id];

                        if (mod) {
                            //Hold on to listeners in case the
                            //module will be attempted to be reloaded
                            //using a different config.
                            if (mod.events.defined) {
                                undefEvents[id] = mod.events;
                            }

                            cleanRegistry(id);
                        }
                    };
                }

                return localRequire;
            },

            /**
             * Called to enable a module if it is still in the registry
             * awaiting enablement. A second arg, parent, the parent module,
             * is passed in for context, when this method is overridden by
             * the optimizer. Not shown here to keep code compact.
             */
            enable: function (depMap) {
                var mod = getOwn(registry, depMap.id);
                if (mod) {
                    getModule(depMap).enable();
                }
            },

            /**
             * Internal method used by environment adapters to complete a load event.
             * A load event could be a script load or just a load pass from a synchronous
             * load call.
             * @param {String} moduleName the name of the module to potentially complete.
             */
            completeLoad: function (moduleName) {
                var found, args, mod,
                    shim = getOwn(config.shim, moduleName) || {},
                    shExports = shim.exports;

                takeGlobalQueue();

                while (defQueue.length) {
                    args = defQueue.shift();
                    if (args[0] === null) {
                        args[0] = moduleName;
                        //If already found an anonymous module and bound it
                        //to this name, then this is some other anon module
                        //waiting for its completeLoad to fire.
                        if (found) {
                            break;
                        }
                        found = true;
                    } else if (args[0] === moduleName) {
                        //Found matching define call for this script!
                        found = true;
                    }

                    callGetModule(args);
                }
                context.defQueueMap = {};

                //Do this after the cycle of callGetModule in case the result
                //of those calls/init calls changes the registry.
                mod = getOwn(registry, moduleName);

                if (!found && !hasProp(defined, moduleName) && mod && !mod.inited) {
                    if (config.enforceDefine && (!shExports || !getGlobal(shExports))) {
                        if (hasPathFallback(moduleName)) {
                            return;
                        } else {
                            return onError(makeError('nodefine',
                                'No define call for ' + moduleName,
                                null,
                                [moduleName]));
                        }
                    } else {
                        //A script that does not call define(), so just simulate
                        //the call for it.
                        callGetModule([moduleName, (shim.deps || []), shim.exportsFn]);
                    }
                }

                checkLoaded();
            },

            /**
             * Converts a module name to a file path. Supports cases where
             * moduleName may actually be just an URL.
             * Note that it **does not** call normalize on the moduleName,
             * it is assumed to have already been normalized. This is an
             * internal API, not a public one. Use toUrl for the public API.
             */
            nameToUrl: function (moduleName, ext, skipExt) {
                var paths, syms, i, parentModule, url,
                    parentPath, bundleId,
                    pkgMain = getOwn(config.pkgs, moduleName);

                if (pkgMain) {
                    moduleName = pkgMain;
                }

                bundleId = getOwn(bundlesMap, moduleName);

                if (bundleId) {
                    return context.nameToUrl(bundleId, ext, skipExt);
                }

                //If a colon is in the URL, it indicates a protocol is used and it is just
                //an URL to a file, or if it starts with a slash, contains a query arg (i.e. ?)
                //or ends with .js, then assume the user meant to use an url and not a module id.
                //The slash is important for protocol-less URLs as well as full paths.
                if (req.jsExtRegExp.test(moduleName)) {
                    //Just a plain path, not module name lookup, so just return it.
                    //Add extension if it is included. This is a bit wonky, only non-.js things pass
                    //an extension, this method probably needs to be reworked.
                    url = moduleName + (ext || '');
                } else {
                    //A module that needs to be converted to a path.
                    paths = config.paths;

                    syms = moduleName.split('/');
                    //For each module name segment, see if there is a path
                    //registered for it. Start with most specific name
                    //and work up from it.
                    for (i = syms.length; i > 0; i -= 1) {
                        parentModule = syms.slice(0, i).join('/');

                        parentPath = getOwn(paths, parentModule);
                        if (parentPath) {
                            //If an array, it means there are a few choices,
                            //Choose the one that is desired
                            if (isArray(parentPath)) {
                                parentPath = parentPath[0];
                            }
                            syms.splice(0, i, parentPath);
                            break;
                        }
                    }

                    //Join the path parts together, then figure out if baseUrl is needed.
                    url = syms.join('/');
                    url += (ext || (/^data\:|^blob\:|\?/.test(url) || skipExt ? '' : '.js'));
                    url = (url.charAt(0) === '/' || url.match(/^[\w\+\.\-]+:/) ? '' : config.baseUrl) + url;
                }

                return config.urlArgs && !/^blob\:/.test(url) ?
                    url + config.urlArgs(moduleName, url) : url;
            },

            //Delegates to req.load. Broken out as a separate function to
            //allow overriding in the optimizer.
            load: function (id, url) {
                req.load(context, id, url);
            },

            /**
             * Executes a module callback function. Broken out as a separate function
             * solely to allow the build system to sequence the files in the built
             * layer in the right sequence.
             *
             * @private
             */
            execCb: function (name, callback, args, exports) {
                return callback.apply(exports, args);
            },

            /**
             * callback for script loads, used to check status of loading.
             *
             * @param {Event} evt the event from the browser for the script
             * that was loaded.
             */
            onScriptLoad: function (evt) {
                //Using currentTarget instead of target for Firefox 2.0's sake. Not
                //all old browsers will be supported, but this one was easy enough
                //to support and still makes sense.
                if (evt.type === 'load' ||
                    (readyRegExp.test((evt.currentTarget || evt.srcElement).readyState))) {
                    //Reset interactive script so a script node is not held onto for
                    //to long.
                    interactiveScript = null;

                    //Pull out the name of the module and the context.
                    var data = getScriptData(evt);
                    context.completeLoad(data.id);
                }
            },

            /**
             * Callback for script errors.
             */
            onScriptError: function (evt) {
                var data = getScriptData(evt);
                if (!hasPathFallback(data.id)) {
                    var parents = [];
                    eachProp(registry, function (value, key) {
                        if (key.indexOf('_@r') !== 0) {
                            each(value.depMaps, function (depMap) {
                                if (depMap.id === data.id) {
                                    parents.push(key);
                                    return true;
                                }
                            });
                        }
                    });
                    return onError(makeError('scripterror', 'Script error for "' + data.id +
                        (parents.length ?
                            '", needed by: ' + parents.join(', ') :
                            '"'), evt, [data.id]));
                }
            }
        };

        context.require = context.makeRequire();
        return context;
    }

    /**
     * Main entry point.
     *
     * If the only argument to require is a string, then the module that
     * is represented by that string is fetched for the appropriate context.
     *
     * If the first argument is an array, then it will be treated as an array
     * of dependency string names to fetch. An optional function callback can
     * be specified to execute when all of those dependencies are available.
     *
     * Make a local req variable to help Caja compliance (it assumes things
     * on a require that are not standardized), and to give a short
     * name for minification/local scope use.
     */
    req = requirejs = function (deps, callback, errback, optional) {

        //Find the right context, use default
        var context, config,
            contextName = defContextName;

        // Determine if have config object in the call.
        if (!isArray(deps) && typeof deps !== 'string') {
            // deps is a config object
            config = deps;
            if (isArray(callback)) {
                // Adjust args if there are dependencies
                deps = callback;
                callback = errback;
                errback = optional;
            } else {
                deps = [];
            }
        }

        if (config && config.context) {
            contextName = config.context;
        }

        context = getOwn(contexts, contextName);
        if (!context) {
            context = contexts[contextName] = req.s.newContext(contextName);
        }

        if (config) {
            context.configure(config);
        }

        return context.require(deps, callback, errback);
    };

    /**
     * Support require.config() to make it easier to cooperate with other
     * AMD loaders on globally agreed names.
     */
    req.config = function (config) {
        return req(config);
    };

    /**
     * Execute something after the current tick
     * of the event loop. Override for other envs
     * that have a better solution than setTimeout.
     * @param  {Function} fn function to execute later.
     */
    req.nextTick = typeof setTimeout !== 'undefined' ? function (fn) {
        setTimeout(fn, 4);
    } : function (fn) { fn(); };

    /**
     * Export require as a global, but only if it does not already exist.
     */
    if (!require) {
        require = req;
    }

    req.version = version;

    //Used to filter out dependencies that are already paths.
    req.jsExtRegExp = /^\/|:|\?|\.js$/;
    req.isBrowser = isBrowser;
    s = req.s = {
        contexts: contexts,
        newContext: newContext
    };

    //Create default context.
    req({});

    //Exports some context-sensitive methods on global require.
    each([
        'toUrl',
        'undef',
        'defined',
        'specified'
    ], function (prop) {
        //Reference from contexts instead of early binding to default context,
        //so that during builds, the latest instance of the default context
        //with its config gets used.
        req[prop] = function () {
            var ctx = contexts[defContextName];
            return ctx.require[prop].apply(ctx, arguments);
        };
    });

    if (isBrowser) {
        head = s.head = document.getElementsByTagName('head')[0];
        //If BASE tag is in play, using appendChild is a problem for IE6.
        //When that browser dies, this can be removed. Details in this jQuery bug:
        //http://dev.jquery.com/ticket/2709
        baseElement = document.getElementsByTagName('base')[0];
        if (baseElement) {
            head = s.head = baseElement.parentNode;
        }
    }

    /**
     * Any errors that require explicitly generates will be passed to this
     * function. Intercept/override it if you want custom error handling.
     * @param {Error} err the error object.
     */
    req.onError = defaultOnError;

    /**
     * Creates the node for the load command. Only used in browser envs.
     */
    req.createNode = function (config, moduleName, url) {
        var node = config.xhtml ?
            document.createElementNS('http://www.w3.org/1999/xhtml', 'html:script') :
            document.createElement('script');
        node.type = config.scriptType || 'text/javascript';
        node.charset = 'utf-8';
        node.async = true;
        return node;
    };

    /**
     * Does the request to load a module for the browser case.
     * Make this a separate function to allow other environments
     * to override it.
     *
     * @param {Object} context the require context to find state.
     * @param {String} moduleName the name of the module.
     * @param {Object} url the URL to the module.
     */
    req.load = function (context, moduleName, url) {
        var config = (context && context.config) || {},
            node;
        if (isBrowser) {
            //In the browser so use a script tag
            node = req.createNode(config, moduleName, url);

            node.setAttribute('data-requirecontext', context.contextName);
            node.setAttribute('data-requiremodule', moduleName);

            //Set up load listener. Test attachEvent first because IE9 has
            //a subtle issue in its addEventListener and script onload firings
            //that do not match the behavior of all other browsers with
            //addEventListener support, which fire the onload event for a
            //script right after the script execution. See:
            //https://connect.microsoft.com/IE/feedback/details/648057/script-onload-event-is-not-fired-immediately-after-script-execution
            //UNFORTUNATELY Opera implements attachEvent but does not follow the script
            //script execution mode.
            if (node.attachEvent &&
                //Check if node.attachEvent is artificially added by custom script or
                //natively supported by browser
                //read https://github.com/requirejs/requirejs/issues/187
                //if we can NOT find [native code] then it must NOT natively supported.
                //in IE8, node.attachEvent does not have toString()
                //Note the test for "[native code" with no closing brace, see:
                //https://github.com/requirejs/requirejs/issues/273
                !(node.attachEvent.toString && node.attachEvent.toString().indexOf('[native code') < 0) &&
                !isOpera) {
                //Probably IE. IE (at least 6-8) do not fire
                //script onload right after executing the script, so
                //we cannot tie the anonymous define call to a name.
                //However, IE reports the script as being in 'interactive'
                //readyState at the time of the define call.
                useInteractive = true;

                node.attachEvent('onreadystatechange', context.onScriptLoad);
                //It would be great to add an error handler here to catch
                //404s in IE9+. However, onreadystatechange will fire before
                //the error handler, so that does not help. If addEventListener
                //is used, then IE will fire error before load, but we cannot
                //use that pathway given the connect.microsoft.com issue
                //mentioned above about not doing the 'script execute,
                //then fire the script load event listener before execute
                //next script' that other browsers do.
                //Best hope: IE10 fixes the issues,
                //and then destroys all installs of IE 6-9.
                //node.attachEvent('onerror', context.onScriptError);
            } else {
                node.addEventListener('load', context.onScriptLoad, false);
                node.addEventListener('error', context.onScriptError, false);
            }
            node.src = url;

            //Calling onNodeCreated after all properties on the node have been
            //set, but before it is placed in the DOM.
            if (config.onNodeCreated) {
                config.onNodeCreated(node, config, moduleName, url);
            }

            //For some cache cases in IE 6-8, the script executes before the end
            //of the appendChild execution, so to tie an anonymous define
            //call to the module name (which is stored on the node), hold on
            //to a reference to this node, but clear after the DOM insertion.
            currentlyAddingScript = node;
            if (baseElement) {
                head.insertBefore(node, baseElement);
            } else {
                head.appendChild(node);
            }
            currentlyAddingScript = null;

            return node;
        } else if (isWebWorker) {
            try {
                //In a web worker, use importScripts. This is not a very
                //efficient use of importScripts, importScripts will block until
                //its script is downloaded and evaluated. However, if web workers
                //are in play, the expectation is that a build has been done so
                //that only one script needs to be loaded anyway. This may need
                //to be reevaluated if other use cases become common.

                // Post a task to the event loop to work around a bug in WebKit
                // where the worker gets garbage-collected after calling
                // importScripts(): https://webkit.org/b/153317
                setTimeout(function () { }, 0);
                importScripts(url);

                //Account for anonymous modules
                context.completeLoad(moduleName);
            } catch (e) {
                context.onError(makeError('importscripts',
                    'importScripts failed for ' +
                    moduleName + ' at ' + url,
                    e,
                    [moduleName]));
            }
        }
    };

    function getInteractiveScript() {
        if (interactiveScript && interactiveScript.readyState === 'interactive') {
            return interactiveScript;
        }

        eachReverse(scripts(), function (script) {
            if (script.readyState === 'interactive') {
                return (interactiveScript = script);
            }
        });
        return interactiveScript;
    }

    //Look for a data-main script attribute, which could also adjust the baseUrl.
    if (isBrowser && !cfg.skipDataMain) {
        //Figure out baseUrl. Get it from the script tag with require.js in it.
        eachReverse(scripts(), function (script) {
            //Set the 'head' where we can append children by
            //using the script's parent.
            if (!head) {
                head = script.parentNode;
            }

            //Look for a data-main attribute to set main script for the page
            //to load. If it is there, the path to data main becomes the
            //baseUrl, if it is not already set.
            dataMain = script.getAttribute('data-main');
            if (dataMain) {
                //Preserve dataMain in case it is a path (i.e. contains '?')
                mainScript = dataMain;

                //Set final baseUrl if there is not already an explicit one,
                //but only do so if the data-main value is not a loader plugin
                //module ID.
                if (!cfg.baseUrl && mainScript.indexOf('!') === -1) {
                    //Pull off the directory of data-main for use as the
                    //baseUrl.
                    src = mainScript.split('/');
                    mainScript = src.pop();
                    subPath = src.length ? src.join('/') + '/' : './';

                    cfg.baseUrl = subPath;
                }

                //Strip off any trailing .js since mainScript is now
                //like a module name.
                mainScript = mainScript.replace(jsSuffixRegExp, '');

                //If mainScript is still a path, fall back to dataMain
                if (req.jsExtRegExp.test(mainScript)) {
                    mainScript = dataMain;
                }

                //Put the data-main script in the files to load.
                cfg.deps = cfg.deps ? cfg.deps.concat(mainScript) : [mainScript];

                return true;
            }
        });
    }

    /**
     * The function that handles definitions of modules. Differs from
     * require() in that a string for the module should be the first argument,
     * and the function to execute after dependencies are loaded should
     * return a value to define the module corresponding to the first argument's
     * name.
     */
    define = function (name, deps, callback) {
        var node, context;

        //Allow for anonymous modules
        if (typeof name !== 'string') {
            //Adjust args appropriately
            callback = deps;
            deps = name;
            name = null;
        }

        //This module may not have dependencies
        if (!isArray(deps)) {
            callback = deps;
            deps = null;
        }

        //If no name, and callback is a function, then figure out if it a
        //CommonJS thing with dependencies.
        if (!deps && isFunction(callback)) {
            deps = [];
            //Remove comments from the callback string,
            //look for require calls, and pull them into the dependencies,
            //but only if there are function args.
            if (callback.length) {
                callback
                    .toString()
                    .replace(commentRegExp, commentReplace)
                    .replace(cjsRequireRegExp, function (match, dep) {
                        deps.push(dep);
                    });

                //May be a CommonJS thing even without require calls, but still
                //could use exports, and module. Avoid doing exports and module
                //work though if it just needs require.
                //REQUIRES the function to expect the CommonJS variables in the
                //order listed below.
                deps = (callback.length === 1 ? ['require'] : ['require', 'exports', 'module']).concat(deps);
            }
        }

        //If in IE 6-8 and hit an anonymous define() call, do the interactive
        //work.
        if (useInteractive) {
            node = currentlyAddingScript || getInteractiveScript();
            if (node) {
                if (!name) {
                    name = node.getAttribute('data-requiremodule');
                }
                context = contexts[node.getAttribute('data-requirecontext')];
            }
        }

        //Always save off evaluating the def call until the script onload handler.
        //This allows multiple modules to be in a file without prematurely
        //tracing dependencies, and allows for anonymous module support,
        //where the module name is not known until the script onload event
        //occurs. If no context, use the global queue, and get it processed
        //in the onscript load callback.
        if (context) {
            context.defQueue.push([name, deps, callback]);
            context.defQueueMap[name] = true;
        } else {
            globalDefQueue.push([name, deps, callback]);
        }
    };

    define.amd = {
        jQuery: true
    };

    /**
     * Executes the text. Normally just uses eval, but can be modified
     * to use a better, environment-specific call. Only used for transpiling
     * loader plugins, not for plain JS modules.
     * @param {String} text the text to execute/evaluate.
     */
    req.exec = function (text) {
        /*jslint evil: true */
        return eval(text);
    };

    //Set up with config info.
    req(cfg);
}(this, (typeof setTimeout === 'undefined' ? undefined : setTimeout)));

// End Bundle: _requirejs

// Start Bundle: _configjs
requirejs.config({
    baseUrl: PORTAL_URL,
    paths: {
        "mockup-patterns-datatables": "++resource++mockup/datatables/pattern",
        "datatables.net-fixedcolumns": "++plone++static/components/datatables.net-fixedcolumns/js/dataTables.fixedColumns.min",
        "less": "++plone++static/components/less/dist/less",
        "text": "++plone++static/components/requirejs-text/text",
        "plone_app_imagecropping_cropperpattern": "++resource++plone.app.imagecropping.static/cropperpattern",
        "mockup-patterns-formautofocus": "++resource++mockup/formautofocus/pattern",
        "datatables.net-keytable": "++plone++static/components/datatables.net-keytable/js/dataTables.keyTable.min",
        "mockup-patterns-tinymce": "++resource++mockup/tinymce/pattern",
        "mockup-router": "++resource++mockupjs/router",
        "mockup-patterns-relateditems-url": "++resource++mockup/relateditems",
        "jquery.event.drop": "++resource++mockuplib/jquery.event.drop",
        "tinymce-autosave": "++plone++static/components/tinymce-builded/js/tinymce/plugins/autosave/plugin",
        "tinymce-charmap": "++plone++static/components/tinymce-builded/js/tinymce/plugins/charmap/plugin",
        "rer-chefcookie-modal-js": "++plone++rer.agidtheme.base/chefcookie_modal",
        "z3cform_datagridfield_js": "++resource++collective.z3cform.datagridfield/datagridfield",
        "plone-app-event": "++plone++plone.app.event/event",
        "tinymce-save": "++plone++static/components/tinymce-builded/js/tinymce/plugins/save/plugin",
        "redturtle-patterns-slider": "++plone++redturtle-patterns-slider/pattern",
        "tinymce-fullscreen": "++plone++static/components/tinymce-builded/js/tinymce/plugins/fullscreen/plugin",
        "tinymce-noneditable": "++plone++static/components/tinymce-builded/js/tinymce/plugins/noneditable/plugin",
        "plone-app-discussion": "++plone++plone.app.discussion.javascripts/comments",
        "datatables.net-buttons-colvis": "++plone++static/components/datatables.net-buttons/js/buttons.colVis.min",
        "editablemenu-widget": "++plone++collective.editablemenu/dist/widget",
        "ace-mode-javascript": "++plone++static/components/ace-builds/src/mode-javascript",
        "rer-agidtheme-base-icons": "++theme++rer.agidtheme.base/js/dist/rer-icons",
        "tinymce-preview": "++plone++static/components/tinymce-builded/js/tinymce/plugins/preview/plugin",
        "mockup-patterns-tree": "++resource++mockup/tree/pattern",
        "redturtle-patterns-slider-bundle-resource": "++plone++redturtle-patterns-slider/bundle",
        "jqtree-contextmenu": "++plone++static/components/cs-jqtree-contextmenu/src/jqTreeContextMenu",
        "mockup-ui-url": "++resource++mockupjs/ui",
        "jqtree": "++plone++static/components/jqtree/tree.jquery",
        "datatables.net": "++plone++static/components/datatables.net/js/jquery.dataTables",
        "slick.min": "++resource++redturtle-patterns-slider-libraries/slick/slick.min",
        "rer-climate-clock-widget-js": "++plone++rer.agidtheme.base/widget-v2",
        "mockup-i18n": "++resource++mockupjs/i18n",
        "underscore": "++plone++static/components/underscore/underscore",
        "datatables.net-colreorder": "++plone++static/components/datatables.net-colreorder/js/dataTables.colReorder.min",
        "pat-logger": "++plone++static/components/patternslib/src/core/logger",
        "ace-mode-text": "++plone++static/components/ace-builds/src/mode-text",
        "backbone.paginator": "++plone++static/components/backbone.paginator/lib/backbone.paginator",
        "datatables.net-responsive-bs": "++plone++static/components/datatables.net-responsive-bs/js/responsive.bootstrap.min",
        "design-plone-theme-js": "++theme++design.plone.theme/js/dist/bundle-compiled",
        "tinymce-textcolor": "++plone++static/components/tinymce-builded/js/tinymce/plugins/textcolor/plugin",
        "mockup-patterns-relateditems-upload": "++resource++mockup/relateditems/upload",
        "tinymce-fullpage": "++plone++static/components/tinymce-builded/js/tinymce/plugins/fullpage/plugin",
        "tinymce-compat3x": "++plone++static/components/tinymce-builded/js/tinymce/plugins/compat3x/plugin",
        "captchapolicy": "++resource++collective.captchacontactinfo/scripts/policy_ajax",
        "pat-jquery-ext": "++plone++static/components/patternslib/src/core/jquery-ext",
        "message_datatables": "++plone++rer.newsletter/scripts/channelhistory",
        "mockup-patterns-thememapper": "++resource++mockup/thememapper/pattern",
        "datatables.net-responsive": "++plone++static/components/datatables.net-responsive/js/dataTables.responsive.min",
        "mockup-patterns-filemanager-url": "++resource++mockup/filemanager",
        "mockup-patterns-structureupdater": "++resource++mockup/structure/pattern-structureupdater",
        "datatables": "++plone++rer.newsletter/scripts/manageusers",
        "bootstrap-transition": "++plone++static/components/bootstrap/js/transition",
        "picker.time": "++plone++static/components/pickadate/lib/picker.time",
        "tinymce-spellchecker": "++plone++static/components/tinymce-builded/js/tinymce/plugins/spellchecker/plugin",
        "mockup-patterns-tinymce-url": "++resource++mockup/tinymce",
        "mockup-patterns-preventdoublesubmit": "++resource++mockup/preventdoublesubmit/pattern",
        "tinymce-visualchars": "++plone++static/components/tinymce-builded/js/tinymce/plugins/visualchars/plugin",
        "expect": "++plone++static/components/expect/index",
        "datatables.net-fixedheader": "++plone++static/components/datatables.net-fixedheader/js/dataTables.fixedHeader.min",
        "tinymce-anchor": "++plone++static/components/tinymce-builded/js/tinymce/plugins/anchor/plugin",
        "redturtle-gallery": "++plone++redturtle.gallery/js/integration",
        "mockup-patterns-filemanager": "++resource++mockup/filemanager/pattern",
        "datatables.net-bs": "++plone++static/components/datatables.net-bs/js/dataTables.bootstrap",
        "mockup-patterns-backdrop": "++resource++mockup/backdrop/pattern",
        "mockup-patterns-cookietrigger": "++resource++mockup/cookietrigger/pattern",
        "tinymce": "++plone++static/components/tinymce-builded/js/tinymce/tinymce",
        "mockup-patterns-modal": "++resource++mockup/modal/pattern",
        "jquery.cookie": "++plone++static/components/jquery.cookie/jquery.cookie",
        "tinymce-wordcount": "++plone++static/components/tinymce-builded/js/tinymce/plugins/wordcount/plugin",
        "mockup-utils": "++resource++mockupjs/utils",
        "tinymce-advlist": "++plone++static/components/tinymce-builded/js/tinymce/plugins/advlist/plugin",
        "plone-patterns-portletmanager": "++resource++manage-portlets",
        "jquery.browser": "++plone++static/components/jquery.browser/dist/jquery.browser",
        "tinymce-modern-theme": "++plone++static/components/tinymce-builded/js/tinymce/themes/modern/theme",
        "datatables.net-buttons-print": "++plone++static/components/datatables.net-buttons/js/buttons.print.min",
        "tinymce-paste": "++plone++static/components/tinymce-builded/js/tinymce/plugins/paste/plugin",
        "z3cform-jsonwidget": "++plone++collective.z3cform.jsonwidget/dist/dev/main",
        "resource-plone-app-discussion-javascripts-comments": "++resource++plone.app.discussion.javascripts/comments",
        "babel-polyfill": "++resource++redturtle.tiles.management/polyfill.min",
        "editablemenu": "++plone++collective.editablemenu/dist/editablemenu",
        "mockup-patterns-upload-url": "++resource++mockup/upload",
        "js-shortcuts": "++plone++static/components/js-shortcuts/js-shortcuts",
        "tinymce-bbcode": "++plone++static/components/tinymce-builded/js/tinymce/plugins/bbcode/plugin",
        "resource-plone-formwidget-contenttree-contenttree": "++resource++plone.formwidget.contenttree/contenttree",
        "mockup-patterns-querystring": "++resource++mockup/querystring/pattern",
        "logging": "++plone++static/components/logging/src/logging",
        "ace": "++plone++static/components/ace-builds/src/ace",
        "collective-slick-import": "++plone++collective.slick/bundle",
        "design-plone-theme-icons": "++theme++design.plone.theme/js/dist/rt-icons",
        "resource-plone-formwidget-autocomplete-jquery": "++resource++plone.formwidget.autocomplete/jquery.autocomplete.min",
        "resource-plone-formwidget-autocomplete-formwidget": "++resource++plone.formwidget.autocomplete/formwidget-autocomplete",
        "tiles-management-pattern": "++resource++redturtle.tiles.management/integration",
        "mockup-patterns-thememapper-url": "++resource++mockup/thememapper",
        "tinymce-autolink": "++plone++static/components/tinymce-builded/js/tinymce/plugins/autolink/plugin",
        "mockup-patterns-formunloadalert": "++resource++mockup/formunloadalert/pattern",
        "picker": "++plone++static/components/pickadate/lib/picker",
        "mockup-patterns-structure-url": "++resource++mockup/structure",
        "tinymce-autoresize": "++plone++static/components/tinymce-builded/js/tinymce/plugins/autoresize/plugin",
        "tinymce-image": "++plone++static/components/tinymce-builded/js/tinymce/plugins/image/plugin",
        "marked": "++plone++static/components/marked/lib/marked",
        "ace-mode-css": "++plone++static/components/ace-builds/src/mode-css",
        "pat-registry": "++plone++static/components/patternslib/src/core/registry",
        "paperboy": "++plone++rer.paperboy/paperboy",
        "plone": "++resource++plone",
        "resource-plone-app-jquerytools-js": "++plone++static/components/jquery.recurrenceinput.js/lib/jquery.tools.overlay",
        "mockup-patterns-select2": "++resource++mockup/select2/pattern",
        "mockup-patterns-structure": "++resource++mockup/structure/pattern",
        "datatables.net-buttons-html5": "++plone++static/components/datatables.net-buttons/js/buttons.html5.min",
        "jquery.recurrenceinput": "++plone++static/components/jquery.recurrenceinput.js/src/jquery.recurrenceinput",
        "jquery.form": "++plone++static/components/jquery-form/jquery.form",
        "mockup-patterns-sortable": "++resource++mockup/sortable/pattern",
        "plone_app_imagecropping_cropscaleselect": "++resource++plone.app.imagecropping.static/cropscaleselect",
        "translate": "++resource++mockupjs/i18n-wrapper",
        "plone_app_imagecropping_cropper": "++resource++plone.app.imagecropping.static/cropper-dist/cropper",
        "bootstrap-dropdown": "++plone++static/components/bootstrap/js/dropdown",
        "mockup-patterns-contentloader": "++resource++mockup/contentloader/pattern",
        "datatables.net-select": "++plone++static/components/datatables.net-select/js/dataTables.select.min",
        "tinymce-lists": "++plone++static/components/tinymce-builded/js/tinymce/plugins/lists/plugin",
        "bootstrap-alert": "++plone++static/components/bootstrap/js/alert",
        "tinymce-textpattern": "++plone++static/components/tinymce-builded/js/tinymce/plugins/textpattern/plugin",
        "tinymce-emoticons": "++plone++static/components/tinymce-builded/js/tinymce/plugins/emoticons/plugin",
        "plone_app_imagecropping": "++resource++plone.app.imagecropping.static/bundle",
        "resourceregistry": "++plone++static/resourceregistry",
        "tinymce-table": "++plone++static/components/tinymce-builded/js/tinymce/plugins/table/plugin",
        "tinymce-template": "++plone++static/components/tinymce-builded/js/tinymce/plugins/template/plugin",
        "jquery": "++plone++static/components/jquery/dist/jquery.min",
        "jquery.event.drag": "++resource++mockuplib/jquery.event.drag",
        "collective-slick-js": "++plone++collective.slick/slick.min",
        "tinymce-tabfocus": "++plone++static/components/tinymce-builded/js/tinymce/plugins/tabfocus/plugin",
        "mockup-patterns-recurrence": "++resource++mockup/recurrence/pattern",
        "resource-plone-app-event-event-js": "++resource++plone.app.event/event",
        "jscookie": "++plone++rer.cookieswitcher/js/src/jscookie",
        "tinymce-print": "++plone++static/components/tinymce-builded/js/tinymce/plugins/print/plugin",
        "initializedModal": "++plone++rer.newsletter/scripts/initializedModal",
        "tinymce-link": "++plone++static/components/tinymce-builded/js/tinymce/plugins/link/plugin",
        "pat-compat": "++plone++static/components/patternslib/src/core/compat",
        "datatables.net-rowreorder": "++plone++static/components/datatables.net-rowreorder/js/dataTables.rowReorder.min",
        "rjs": "++plone++static/components/r.js/dist/r",
        "tinymce-hr": "++plone++static/components/tinymce-builded/js/tinymce/plugins/hr/plugin",
        "select2": "++plone++static/components/select2/select2",
        "tinymce-media": "++plone++static/components/tinymce-builded/js/tinymce/plugins/media/plugin",
        "mockup-patterns-texteditor": "++resource++mockup/texteditor/pattern",
        "resource-plone-formwidget-recaptcha-recaptcha_ajax": "++resource++plone.formwidget.recaptcha/recaptcha_ajax",
        "rer-agidtheme-base-js": "++theme++rer.agidtheme.base/js/dist/bundle-compiled",
        "tinymce-pagebreak": "++plone++static/components/tinymce-builded/js/tinymce/plugins/pagebreak/plugin",
        "mockup-patterns-tooltip": "++resource++mockup/tooltip/pattern",
        "resource-plone-app-jquerytools-dateinput-js": "++plone++static/components/jquery.recurrenceinput.js/lib/jquery.tools.dateinput",
        "plone-logged-in": "++resource++plone-logged-in",
        "mockup-patterns-pickadate": "++resource++mockup/pickadate/pattern",
        "datatables.net-autofill-bs": "++plone++static/components/datatables.net-autofill-bs/js/autoFill.bootstrap",
        "JSXTransformer": "++plone++static/components/react/JSXTransformer",
        "datatables.net-buttons-flash": "++plone++static/components/datatables.net-buttons/js/buttons.flash.min",
        "tinymce-contextmenu": "++plone++static/components/tinymce-builded/js/tinymce/plugins/contextmenu/plugin",
        "ace-theme-monokai": "++plone++static/components/ace-builds/src/theme-monokai",
        "mockup-patterns-toggle": "++resource++mockup/toggle/pattern",
        "tinymce-directionality": "++plone++static/components/tinymce-builded/js/tinymce/plugins/directionality/plugin",
        "mockup-patterns-markspeciallinks": "++resource++mockup/markspeciallinks/pattern",
        "rer-customer-satisfaction": "++plone++rer.customersatisfaction/rer-customersatisfaction",
        "tinymce-legacyoutput": "++plone++static/components/tinymce-builded/js/tinymce/plugins/legacyoutput/plugin",
        "pat-base": "++plone++static/components/patternslib/src/core/base",
        "plone-patterns-toolbar": "++plone++static/patterns/toolbar/src/toolbar",
        "mockup-patterns-inlinevalidation": "++resource++mockup/inlinevalidation/pattern",
        "thememapper": "++resource++plone.app.theming/thememapper",
        "linknormativa": "++resource++rer.linknormativa.base/scripts/format_date_widget",
        "tinymce-visualblocks": "++plone++static/components/tinymce-builded/js/tinymce/plugins/visualblocks/plugin",
        "mockup-patterns-resourceregistry-url": "++resource++mockup/resourceregistry",
        "tinymce-insertdatetime": "++plone++static/components/tinymce-builded/js/tinymce/plugins/insertdatetime/plugin",
        "mockup-patterns-base": "++resource++mockup/base/pattern",
        "mockup-patterns-livesearch": "++resource++mockup/livesearch/pattern",
        "mockup-patterns-upload": "++resource++mockup/upload/pattern",
        "picker.date": "++plone++static/components/pickadate/lib/picker.date",
        "datatables.net-buttons-bs": "++plone++static/components/datatables.net-buttons-bs/js/buttons.bootstrap.min",
        "tinymce-searchreplace": "++plone++static/components/tinymce-builded/js/tinymce/plugins/searchreplace/plugin",
        "mockup-patterns-autotoc": "++resource++mockup/autotoc/pattern",
        "tinymce-importcss": "++plone++static/components/tinymce-builded/js/tinymce/plugins/importcss/plugin",
        "tiles-management-bundle": "++resource++redturtle.tiles.management/bundle",
        "backbone": "++plone++static/components/backbone/backbone",
        "mockup-patterns-resourceregistry": "++resource++mockup/resourceregistry/pattern",
        "react": "++plone++static/components/react/react",
        "mockup-patterns-textareamimetypeselector": "++resource++mockup/textareamimetypeselector/pattern",
        "moment": "++plone++static/components/moment/min/moment-with-locales.min",
        "sinon": "++plone++static/components/sinonjs/sinon",
        "tinymce-code": "++plone++static/components/tinymce-builded/js/tinymce/plugins/code/plugin",
        "pat-utils": "++plone++static/components/patternslib/src/core/utils",
        "rer-immersive-reader": "++plone++rer.immersivereader/js/src/index",
        "tinymce-colorpicker": "++plone++static/components/tinymce-builded/js/tinymce/plugins/colorpicker/plugin",
        "jquery.tmpl": "++plone++static/components/jquery.recurrenceinput.js/lib/jquery.tmpl",
        "datatables.net-autofill": "++plone++static/components/datatables.net-autofill/js/dataTables.autoFill.min",
        "bootstrap-collapse": "++plone++static/components/bootstrap/js/collapse",
        "mockup-patterns-moment": "++resource++mockup/moment/pattern",
        "mockup-patterns-relateditems": "++resource++mockup/relateditems/pattern",
        "tinymce-nonbreaking": "++plone++static/components/tinymce-builded/js/tinymce/plugins/nonbreaking/plugin",
        "bootstrap-tooltip": "++plone++static/components/bootstrap/js/tooltip",
        "pat-mockup-parser": "++plone++static/components/patternslib/src/core/mockup-parser",
        "datatables.net-scroller": "++plone++static/components/datatables.net-scroller/js/dataTables.scroller.min",
        "dropzone": "++plone++static/components/dropzone/dist/dropzone-amd-module",
        "datatables.net-buttons": "++plone++static/components/datatables.net-buttons/js/dataTables.buttons.min"
    },
    shim: {
        "resource-plone-app-jquerytools-dateinput-js": {
            deps: ["jquery"]
        },
        "tinymce-autoresize": {
            deps: ["tinymce"]
        },
        "bootstrap-transition": {
            exports: "window.jQuery.support.transition",
            deps: ["jquery"]
        },
        "tinymce-save": {
            deps: ["tinymce"]
        },
        "bootstrap-collapse": {
            exports: "window.jQuery.fn.collapse.Constructor",
            deps: ["jquery"]
        },
        "tinymce-code": {
            deps: ["tinymce"]
        },
        "tinymce-fullpage": {
            deps: ["tinymce"]
        },
        "tinymce-visualchars": {
            deps: ["tinymce"]
        },
        "expect": {
            exports: "window.expect"
        },
        "tinymce-spellchecker": {
            deps: ["tinymce"]
        },
        "jquery.event.drop": {
            exports: "$.drop",
            deps: ["jquery"]
        },
        "tinymce-anchor": {
            deps: ["tinymce"]
        },
        "tinymce-autosave": {
            deps: ["tinymce"]
        },
        "tinymce-contextmenu": {
            deps: ["tinymce"]
        },
        "paperboy": {
            deps: ["jquery"]
        },
        "tinymce-charmap": {
            deps: ["tinymce"]
        },
        "resource-plone-app-jquerytools-js": {
            deps: ["jquery"]
        },
        "tinymce-directionality": {
            deps: ["tinymce"]
        },
        "tinymce": {
            exports: "window.tinyMCE",
            init: function () { this.tinyMCE.DOM.events.domLoaded = true; return this.tinyMCE; }
        },
        "tinymce-image": {
            deps: ["tinymce"]
        },
        "tinymce-legacyoutput": {
            deps: ["tinymce"]
        },
        "tinymce-template": {
            deps: ["tinymce"]
        },
        "jquery.cookie": {
            deps: ["jquery"]
        },
        "jquery.recurrenceinput": {
            deps: ["jquery", "resource-plone-app-jquerytools-js", "resource-plone-app-jquerytools-dateinput-js", "jquery.tmpl"]
        },
        "tinymce-fullscreen": {
            deps: ["tinymce"]
        },
        "tinymce-noneditable": {
            deps: ["tinymce"]
        },
        "tinymce-wordcount": {
            deps: ["tinymce"]
        },
        "tinymce-insertdatetime": {
            deps: ["tinymce"]
        },
        "tinymce-advlist": {
            deps: ["tinymce"]
        },
        "tinymce-visualblocks": {
            deps: ["tinymce"]
        },
        "tinymce-searchreplace": {
            deps: ["tinymce"]
        },
        "jquery.browser": {
            deps: ["jquery"]
        },
        "picker.date": {
            deps: ["picker"]
        },
        "tinymce-modern-theme": {
            deps: ["tinymce"]
        },
        "bootstrap-dropdown": {
            deps: ["jquery"]
        },
        "tinymce-preview": {
            deps: ["tinymce"]
        },
        "tinymce-paste": {
            deps: ["tinymce"]
        },
        "tinymce-pagebreak": {
            deps: ["tinymce"]
        },
        "tinymce-importcss": {
            deps: ["tinymce"]
        },
        "tinymce-lists": {
            deps: ["tinymce"]
        },
        "bootstrap-alert": {
            deps: ["jquery"]
        },
        "jqtree": {
            deps: ["jquery"]
        },
        "tinymce-textpattern": {
            deps: ["tinymce"]
        },
        "backbone": {
            exports: "window.Backbone",
            deps: ["underscore", "jquery"]
        },
        "jquery.tmpl": {
            deps: ["jquery"]
        },
        "tinymce-emoticons": {
            deps: ["tinymce"]
        },
        "sinon": {
            exports: "window.sinon"
        },
        "tinymce-table": {
            deps: ["tinymce"]
        },
        "underscore": {
            exports: "window._"
        },
        "js-shortcuts": {
            deps: ["jquery"]
        },
        "tinymce-bbcode": {
            deps: ["tinymce"]
        },
        "tinymce-print": {
            deps: ["tinymce"]
        },
        "tinymce-colorpicker": {
            deps: ["tinymce"]
        },
        "jquery.event.drag": {
            deps: ["jquery"]
        },
        "backbone.paginator": {
            exports: "window.Backbone.Paginator",
            deps: ["backbone"]
        },
        "tinymce-tabfocus": {
            deps: ["tinymce"]
        },
        "tinymce-textcolor": {
            deps: ["tinymce"]
        },
        "picker.time": {
            deps: ["picker"]
        },
        "JSXTransformer": {
            exports: "window.JSXTransformer"
        },
        "tinymce-compat3x": {
            deps: ["tinymce"]
        },
        "tinymce-link": {
            deps: ["tinymce"]
        },
        "tinymce-nonbreaking": {
            deps: ["tinymce"]
        },
        "bootstrap-tooltip": {
            deps: ["jquery"]
        },
        "tinymce-hr": {
            deps: ["tinymce"]
        },
        "tinymce-autolink": {
            deps: ["tinymce"]
        },
        "jqtree-contextmenu": {
            deps: ["jqtree"]
        },
        "tinymce-media": {
            deps: ["tinymce"]
        }
    },
    optimize: 'uglify',
    wrapShim: true
});
// End Bundle: _configjs

// Start Bundle: captchapolicy
require(["jquery", "mockup-patterns-modal"], function ($, Modal) {
    "use strict";
    $(document).ready(function () {
        var myvet = $(location.pathname.split("/"));
        var mylocation = $(location.pathname.split("/"))[myvet.length - 1];

        if (mylocation === "contact-info") {
            render_policy();
        }
        // disabled because in RER they don't want it anymore
        //  else{
        //    $('a[href$="contact-info"]').each(function() {
        //       var modal = new Modal($(this), {
        //       });
        //       modal.on('after-render', function(){
        //        render_policy()
        //       });
        //     });
        //  }
    });
});

function render_policy() {
    portal_url = $("body").data("portalUrl");
    $.ajax({
        url: portal_url + "/get_policy_page_url",
        dataType: "text",
        success: function (data) {
            if (data) {
                $(
                    '<div class="policyInfo policyTitle" id="policyTitle">Informativa privacy</div> <div class="policyInfo policyText" id="policyText"> </div>'
                ).insertAfter($("#formfield-form-widgets-message")[0]);
                $(".policyInfo.policyText").html($(data));
            }
        },
    });
}

// End Bundle: captchapolicy

// Start Bundle: editablemenu-bundle
!function (e, t, n) { function a(e, t, n) { e.addEventListener ? e.addEventListener(t, n, !1) : e.attachEvent("on" + t, n) } function r(e) { if ("keypress" == e.type) { var t = String.fromCharCode(e.which); return e.shiftKey || (t = t.toLowerCase()), t } return g[e.which] ? g[e.which] : v[e.which] ? v[e.which] : String.fromCharCode(e.which).toLowerCase() } function i(e, t) { return e.sort().join(",") === t.sort().join(",") } function o(e) { var t = []; return e.shiftKey && t.push("shift"), e.altKey && t.push("alt"), e.ctrlKey && t.push("ctrl"), e.metaKey && t.push("meta"), t } function l(e) { e.preventDefault ? e.preventDefault() : e.returnValue = !1 } function s(e) { e.stopPropagation ? e.stopPropagation() : e.cancelBubble = !0 } function u(e) { return "shift" == e || "ctrl" == e || "alt" == e || "meta" == e } function c() { if (!m) { m = {}; for (var e in g) e > 95 && e < 112 || g.hasOwnProperty(e) && (m[g[e]] = e) } return m } function p(e, t, n) { return n || (n = c()[e] ? "keydown" : "keypress"), "keypress" == n && t.length && (n = "keydown"), n } function d(e) { return "+" === e ? ["+"] : (e = e.replace(/\+{2}/g, "+plus")).split("+") } function f(e, t) { var n, a, r, i = []; for (n = d(e), r = 0; r < n.length; ++r)a = n[r], k[a] && (a = k[a]), t && "keypress" != t && y[a] && (a = y[a], i.push("shift")), u(a) && i.push(a); return t = p(a, i, t), { key: a, modifiers: i, action: t } } function b(e, n) { return null !== e && e !== t && (e === n || b(e.parentNode, n)) } function h(e) { function n(e) { e = e || {}; var t, n = !1; for (t in k) e[t] ? n = !0 : k[t] = 0; n || (_ = !1) } function c(e, t, n, a, r, o) { var l, s, c = [], p = n.type; if (!v._callbacks[e]) return []; for ("keyup" == p && u(e) && (t = [e]), l = 0; l < v._callbacks[e].length; ++l)if (s = v._callbacks[e][l], (a || !s.seq || k[s.seq] == s.level) && p == s.action && ("keypress" == p && !n.metaKey && !n.ctrlKey || i(t, s.modifiers))) { var d = !a && s.combo == r, f = a && s.seq == a && s.level == o; (d || f) && v._callbacks[e].splice(l, 1), c.push(s) } return c } function p(e, t, n, a) { v.stopCallback(t, t.target || t.srcElement, n, a) || !1 === e(t, n) && (l(t), s(t)) } function d(e) { "number" != typeof e.which && (e.which = e.keyCode); var t = r(e); t && ("keyup" != e.type || w !== t ? v.handleKey(t, o(e), e) : w = !1) } function b() { clearTimeout(y), y = setTimeout(n, 1e3) } function m(e, t, a, i) { k[e] = 0; for (var o = 0; o < t.length; ++o) { var l = o + 1 === t.length ? function (t) { p(a, t, e), "keyup" !== i && (w = r(t)), setTimeout(n, 10) } : function (t) { return function () { _ = t, ++k[e], b() } }(i || f(t[o + 1]).action); g(t[o], l, i, e, o) } } function g(e, t, n, a, r) { v._directMap[e + ":" + n] = t; var i, o = (e = e.replace(/\s+/g, " ")).split(" "); o.length > 1 ? m(e, o, t, n) : (i = f(e, n), v._callbacks[i.key] = v._callbacks[i.key] || [], c(i.key, i.modifiers, { type: i.action }, a, e, r), v._callbacks[i.key][a ? "unshift" : "push"]({ callback: t, modifiers: i.modifiers, action: i.action, seq: a, level: r, combo: e })) } var v = this; if (e = e || t, !(v instanceof h)) return new h(e); v.target = e, v._callbacks = {}, v._directMap = {}; var y, k = {}, w = !1, O = !1, _ = !1; v._handleKey = function (e, t, a) { var r, i = c(e, t, a), o = {}, l = 0, s = !1; for (r = 0; r < i.length; ++r)i[r].seq && (l = Math.max(l, i[r].level)); for (r = 0; r < i.length; ++r)if (i[r].seq) { if (i[r].level != l) continue; s = !0, o[i[r].seq] = 1, p(i[r].callback, a, i[r].combo, i[r].seq) } else s || p(i[r].callback, a, i[r].combo); var d = "keypress" == a.type && O; a.type != _ || u(e) || d || n(o), O = s && "keydown" == a.type }, v._bindMultiple = function (e, t, n) { for (var a = 0; a < e.length; ++a)g(e[a], t, n) }, a(e, "keypress", d), a(e, "keydown", d), a(e, "keyup", d) } if (e) { for (var m, g = { 8: "backspace", 9: "tab", 13: "enter", 16: "shift", 17: "ctrl", 18: "alt", 20: "capslock", 27: "esc", 32: "space", 33: "pageup", 34: "pagedown", 35: "end", 36: "home", 37: "left", 38: "up", 39: "right", 40: "down", 45: "ins", 46: "del", 91: "meta", 93: "meta", 224: "meta" }, v = { 106: "*", 107: "+", 109: "-", 110: ".", 111: "/", 186: ";", 187: "=", 188: ",", 189: "-", 190: ".", 191: "/", 192: "`", 219: "[", 220: "\\", 221: "]", 222: "'" }, y = { "~": "`", "!": "1", "@": "2", "#": "3", $: "4", "%": "5", "^": "6", "&": "7", "*": "8", "(": "9", ")": "0", _: "-", "+": "=", ":": ";", '"': "'", "<": ",", ">": ".", "?": "/", "|": "\\" }, k = { option: "alt", command: "meta", return: "enter", escape: "esc", plus: "+", mod: /Mac|iPod|iPhone|iPad/.test(navigator.platform) ? "meta" : "ctrl" }, w = 1; w < 20; ++w)g[111 + w] = "f" + w; for (w = 0; w <= 9; ++w)g[w + 96] = w.toString(); h.prototype.bind = function (e, t, n) { var a = this; return e = e instanceof Array ? e : [e], a._bindMultiple.call(a, e, t, n), a }, h.prototype.unbind = function (e, t) { var n = this; return n.bind.call(n, e, function () { }, t) }, h.prototype.trigger = function (e, t) { var n = this; return n._directMap[e + ":" + t] && n._directMap[e + ":" + t]({}, e), n }, h.prototype.reset = function () { var e = this; return e._callbacks = {}, e._directMap = {}, e }, h.prototype.stopCallback = function (e, t) { var n = this; return !((" " + t.className + " ").indexOf(" mousetrap ") > -1) && (!b(t, n.target) && ("INPUT" == t.tagName || "SELECT" == t.tagName || "TEXTAREA" == t.tagName || t.isContentEditable)) }, h.prototype.handleKey = function () { var e = this; return e._handleKey.apply(e, arguments) }, h.addKeycodes = function (e) { for (var t in e) e.hasOwnProperty(t) && (g[t] = e[t]); m = null }, h.init = function () { var e = h(t); for (var n in e) "_" !== n.charAt(0) && (h[n] = function (t) { return function () { return e[t].apply(e, arguments) } }(n)) }, h.init(), e.Mousetrap = h, "undefined" != typeof module && module.exports && (module.exports = h), "function" == typeof define && define.amd && define("mousetrap", [], function () { return h }) } }("undefined" != typeof window ? window : null, "undefined" != typeof window ? document : null), require(["jquery", "mousetrap"], function (e, t) { e(function () { var n = { closed: "editablemenu.submenu.closed", loaded: "editablemenu.submenu.loaded", opened: "editablemenu.submenu.opened" }; e(document).click(function (t) { e(t.target).closest(".globalnavWrapper").length || (e(".globalnavWrapper #submenu-details").slideUp(), e(".tabOpen a").attr("aria-expanded", !1), e(".tabOpen").removeClass("tabOpen")) }), t.bind("esc", function () { e(".tabOpen").length > 0 && (e(".tabOpen a").attr("aria-expanded", !1), e(".tabOpen a.menuTabLink").trigger(n.closed), e(".tabOpen").removeClass("tabOpen"), e(".globalnavWrapper #submenu-details").slideUp()) }), e("a.menuTabLink").click(function (t) { if (e(t.currentTarget).hasClass("clickandgo")) return !0; t.preventDefault(); var a = e(this), r = a.data().tabid, i = a.parent(), o = e(".globalnavWrapper #submenu-details"); if (void 0 !== r) { if (i.hasClass("tabOpen")) return e(".tabOpen a").attr("aria-expanded", !1), e(".tabOpen").removeClass("tabOpen"), void o.slideUp(400, function () { a.trigger(n.closed) }); if (e(".tabOpen a").attr("aria-expanded", !1), e(".tabOpen").removeClass("tabOpen"), i.addClass("tabOpen"), i.find("a").attr("aria-expanded", !0), 1 !== o.length || parseInt(o.attr("class").slice(8), 10) !== r) { var l = e("body").data().baseUrl; e.get(l + "/@@submenu_detail_view?tab_id=" + r, function (t) { var l = /<script.*?id="protect-script".*?<\/script>/g, s = t.replace(l, ""), u = e('<div id="submenu-details" class="submenu-' + r + '" style="display: none;"></div>').html(s); 0 !== e(u).children().length ? 0 !== o.length ? o.is(":visible") ? (o.fadeOut("fast").remove(), i.append(u), e(".globalnavWrapper #submenu-details").fadeIn(400, function () { a.trigger(n.loaded) })) : (o.remove(), i.append(u), e(".globalnavWrapper #submenu-details").slideDown(400, function () { a.trigger(n.loaded), a.trigger(n.opened) })) : (i.append(u), e(".globalnavWrapper #submenu-details").slideDown(400, function () { a.trigger(n.loaded), a.trigger(n.opened) })) : a.trigger(n.loaded) }) } else o.slideDown(400, function () { a.trigger(n.opened) }) } }), e("#portal-top").on("click", ".submenuDetailsContent a", function () { e(this).focus() }), e(".globalnavWrapper").on("click", "a.closeSubmenuLink", function (t) { t.preventDefault(); var a = e(this); e(".tabOpen a").attr("aria-expanded", !1), e(".tabOpen").removeClass("tabOpen"), e(".globalnavWrapper #submenu-details").slideUp(400, function () { a.trigger(n.closed) }); var r = e(a.parents("#submenu-details")); if (0 !== r.length) { var i = r.attr("class").slice(8), o = e('*[data-tabid="' + i + '"]'); 1 === o.length && e(o).focus() } }) }) }), define("js/editablemenu.js", function () { });
//# sourceMappingURL=editablemenu.min.js.map
// End Bundle: editablemenu-bundle

// Start Bundle: linknormativa-bundle
require(['jquery'], function ($) {
    $(document).ready(function () {
        $(
            '.template-linknormativa [name="form.widgets.IDublinCore.effective"]'
        ).attr('data-pat-pickadate', 'time:false');
        $(
            '.portaltype-linknormativa [name="form.widgets.IDublinCore.effective"]'
        ).attr('data-pat-pickadate', 'time:false');
    });
});

// End Bundle: linknormativa-bundle

// Start Bundle: plone-legacy

/* reset requirejs definitions so that people who
   put requirejs in legacy compilation do not get errors */
var _old_define = define;
var _old_require = require;
define = undefined;
require = undefined;
try {

    /* Could not find resource: http://vm410lnx:6130/energia/++resource++plone.app.discussion.javascripts/comments.js */


    /* resource: ++resource++plone.formwidget.recaptcha/recaptcha_ajax.js */
    var RecaptchaTemplates = { VertHtml: '<table id="recaptcha_table" class="recaptchatable" >\n<tr>\n<td colspan="6" class=\'recaptcha_r1_c1\'></td>\n</tr>\n<tr>\n<td class=\'recaptcha_r2_c1\'></td>\n<td colspan="4" class=\'recaptcha_image_cell\'><div id="recaptcha_image"></div></td>\n<td class=\'recaptcha_r2_c2\'></td>\n</tr>\n<tr>\n<td rowspan="6" class=\'recaptcha_r3_c1\'></td>\n<td colspan="4" class=\'recaptcha_r3_c2\'></td>\n<td rowspan="6" class=\'recaptcha_r3_c3\'></td>\n</tr>\n<tr>\n<td rowspan="3" class=\'recaptcha_r4_c1\' height="49">\n<div class="recaptcha_input_area">\n<label for="recaptcha_response_field" class="recaptcha_input_area_text"><span id="recaptcha_instructions_image" class="recaptcha_only_if_image recaptcha_only_if_no_incorrect_sol"></span><span id="recaptcha_instructions_audio" class="recaptcha_only_if_no_incorrect_sol recaptcha_only_if_audio"></span><span id="recaptcha_instructions_error" class="recaptcha_only_if_incorrect_sol"></span></label><br/>\n<input name="recaptcha_response_field" id="recaptcha_response_field" type="text" />\n</div>\n</td>\n<td rowspan="4" class=\'recaptcha_r4_c2\'></td>\n<td><a id=\'recaptcha_reload_btn\'><img id=\'recaptcha_reload\' width="25" height="17" /></a></td>\n<td rowspan="4" class=\'recaptcha_r4_c4\'></td>\n</tr>\n<tr>\n<td><a id=\'recaptcha_switch_audio_btn\' class="recaptcha_only_if_image"><img id=\'recaptcha_switch_audio\' width="25" height="16" alt="" /></a><a id=\'recaptcha_switch_img_btn\' class="recaptcha_only_if_audio"><img id=\'recaptcha_switch_img\' width="25" height="16" alt=""/></a></td>\n</tr>\n<tr>\n<td><a id=\'recaptcha_whatsthis_btn\'><img id=\'recaptcha_whatsthis\' width="25" height="16" /></a></td>\n</tr>\n<tr>\n<td class=\'recaptcha_r7_c1\'></td>\n<td class=\'recaptcha_r8_c1\'></td>\n</tr>\n</table>\n', VertCss: '.recaptchatable td img {\n/* see http://developer.mozilla.org/en/docs/Images%2C_Tables%2C_and_Mysterious_Gaps */\ndisplay: block;\n}\n.recaptchatable .recaptcha_r1_c1 { background: url(IMGROOT/sprite.png) -0px -63px no-repeat; width: 318px; height: 9px; }\n.recaptchatable .recaptcha_r2_c1 { background: url(IMGROOT/sprite.png) -18px -0px no-repeat; width: 9px; height: 57px; }\n.recaptchatable .recaptcha_r2_c2 { background: url(IMGROOT/sprite.png) -27px -0px no-repeat; width: 9px; height: 57px; }\n.recaptchatable .recaptcha_r3_c1 { background: url(IMGROOT/sprite.png) -0px -0px no-repeat; width: 9px; height: 63px; }\n.recaptchatable .recaptcha_r3_c2 { background: url(IMGROOT/sprite.png) -18px -57px no-repeat; width: 300px; height: 6px; }\n.recaptchatable .recaptcha_r3_c3 { background: url(IMGROOT/sprite.png) -9px -0px no-repeat; width: 9px; height: 63px; }\n.recaptchatable .recaptcha_r4_c1 { background: url(IMGROOT/sprite.png) -43px -0px no-repeat; width: 171px; height: 49px; }\n.recaptchatable .recaptcha_r4_c2 { background: url(IMGROOT/sprite.png) -36px -0px no-repeat; width: 7px; height: 57px; }\n.recaptchatable .recaptcha_r4_c4 { background: url(IMGROOT/sprite.png) -214px -0px no-repeat; width: 97px; height: 57px; }\n.recaptchatable .recaptcha_r7_c1 { background: url(IMGROOT/sprite.png) -43px -49px no-repeat; width: 171px; height: 8px; }\n.recaptchatable .recaptcha_r8_c1 { background: url(IMGROOT/sprite.png) -43px -49px no-repeat; width: 25px; height: 8px; }\n.recaptchatable .recaptcha_image_cell center img { height:57px;}\n.recaptchatable .recaptcha_image_cell center { height:57px;}\n.recaptchatable .recaptcha_image_cell {\nbackground-color:white; height:57px;\n}\n/* some people break their style sheet, we need to clean up after them */\n#recaptcha_area, #recaptcha_table {\nwidth: 318px !important;\n}\n.recaptchatable, #recaptcha_area tr, #recaptcha_area td, #recaptcha_area th {\nmargin:0px !important;\nborder:0px !important;\npadding:0px !important;\nborder-collapse: collapse !important;\nvertical-align: middle !important;\n}\n.recaptchatable * {\nmargin:0px;\npadding:0px;\nborder:0px;\nfont-family:helvetica,sans-serif;\nfont-size:8pt;\ncolor:black;\nposition:static;\ntop:auto;\nleft:auto;\nright:auto;\nbottom:auto;\ntext-align:left !important;\n}\n.recaptchatable #recaptcha_image {\nmargin:auto;\n}\n.recaptchatable img {\nborder:0px !important;\nmargin:0px !important;\npadding:0px !important;\n}\n.recaptchatable a, .recaptchatable a:hover {\n-moz-outline:none;\nborder:0px !important;\npadding:0px !important;\ntext-decoration:none;\ncolor:blue;\nbackground:none !important;\nfont-weight: normal;\n}\n.recaptcha_input_area {\nposition:relative !important;\nwidth:146px !important;\nheight:45px !important;\nmargin-left:20px !important;\nmargin-right:5px !important;\nmargin-top:4px !important;\nbackground:none !important;\n}\n.recaptchatable label.recaptcha_input_area_text {\nmargin:0px !important;  \npadding:0px !important;\nposition:static !important;\ntop:auto !important;\nleft:auto !important;\nright:auto !important;\nbottom:auto !important;\nbackground:none !important;\nheight:auto !important;\nwidth:auto !important;\n}\n.recaptcha_theme_red label.recaptcha_input_area_text,\n.recaptcha_theme_white label.recaptcha_input_area_text {\ncolor:black !important;\n}\n.recaptcha_theme_blackglass label.recaptcha_input_area_text {\ncolor:white !important;\n}\n.recaptchatable #recaptcha_response_field  {\nwidth:145px !important;\nposition:absolute !important;\nbottom:7px !important;\npadding:0px !important;\nmargin:0px !important;\nfont-size:10pt;\n}\n.recaptcha_theme_blackglass #recaptcha_response_field,\n.recaptcha_theme_white #recaptcha_response_field {\nborder: 1px solid gray;\n}\n.recaptcha_theme_red #recaptcha_response_field {\nborder:1px solid #cca940;\n}\n.recaptcha_audio_cant_hear_link {\nfont-size:7pt;\ncolor:black;\n}\n.recaptchatable {\nline-height:1em;\n}\n#recaptcha_instructions_error {\ncolor:red !important;\n}\n', CleanHtml: '<table id="recaptcha_table" class="recaptchatable">\n<tr height="73">\n<td class=\'recaptcha_image_cell\' width="302"><center><div id="recaptcha_image"></div></center></td>\n<td style="padding: 10px 7px 7px 7px;">\n<a id=\'recaptcha_reload_btn\'><img id=\'recaptcha_reload\' width="25" height="18" alt="" /></a>\n<a id=\'recaptcha_switch_audio_btn\' class="recaptcha_only_if_image"><img id=\'recaptcha_switch_audio\' width="25" height="15" alt="" /></a><a id=\'recaptcha_switch_img_btn\' class="recaptcha_only_if_audio"><img id=\'recaptcha_switch_img\' width="25" height="15" alt=""/></a>\n<a id=\'recaptcha_whatsthis_btn\'><img id=\'recaptcha_whatsthis\' width="25" height="16" /></a>\n</td>\n<td style="padding: 18px 7px 18px 7px;">\n<img id=\'recaptcha_logo\' alt="" width="71" height="36" />\n</td>\n</tr>\n<tr>\n<td style="padding-left: 7px;">\n<div class="recaptcha_input_area" style="padding-top: 2px; padding-bottom: 7px;">\n<input style="border: 1px solid #3c3c3c; width: 302px;" name="recaptcha_response_field" id="recaptcha_response_field" type="text" />\n</div>\n</td>\n<td></td>\n<td style="padding: 4px 7px 12px 7px;">\n<img id="recaptcha_tagline" width="71" height="17" />\n</td>\n</tr>\n</table>\n', CleanCss: '.recaptchatable td img {\ndisplay: block;\n}\n.recaptchatable .recaptcha_image_cell center img { height:57px;}\n.recaptchatable .recaptcha_image_cell center { height:57px;}\n.recaptchatable .recaptcha_image_cell {\nbackground-color:white; height:57px; \npadding: 7px !important;\n}\n.recaptchatable, #recaptcha_area tr, #recaptcha_area td, #recaptcha_area th {\nmargin:0px !important;\nborder:0px !important;\nborder-collapse: collapse !important;\nvertical-align: middle !important;\n}\n.recaptchatable * {\nmargin:0px;\npadding:0px;\nborder:0px;\ncolor:black;\nposition:static;\ntop:auto;\nleft:auto;\nright:auto;\nbottom:auto;\ntext-align:left !important;\n}\n.recaptchatable #recaptcha_image {\nmargin:auto;\nborder: 1px solid #dfdfdf !important;\n}\n.recaptchatable a img {\nborder:0px;\n}\n.recaptchatable a, .recaptchatable a:hover {\n-moz-outline:none;\nborder:0px !important;\npadding:0px !important;\ntext-decoration:none;\ncolor:blue;\nbackground:none !important;\nfont-weight: normal;\n}\n.recaptcha_input_area {\nposition:relative !important;\nbackground:none !important;\n}\n.recaptchatable label.recaptcha_input_area_text {\nborder:1px solid #dfdfdf !important;\nmargin:0px !important;  \npadding:0px !important;\nposition:static !important;\ntop:auto !important;\nleft:auto !important;\nright:auto !important;\nbottom:auto !important;\n}\n.recaptcha_theme_red label.recaptcha_input_area_text,\n.recaptcha_theme_white label.recaptcha_input_area_text {\ncolor:black !important;\n}\n.recaptcha_theme_blackglass label.recaptcha_input_area_text {\ncolor:white !important;\n}\n.recaptchatable #recaptcha_response_field  {\nfont-size:11pt;\n}\n.recaptcha_theme_blackglass #recaptcha_response_field,\n.recaptcha_theme_white #recaptcha_response_field {\nborder: 1px solid gray;\n}\n.recaptcha_theme_red #recaptcha_response_field {\nborder:1px solid #cca940;\n}\n.recaptcha_audio_cant_hear_link {\nfont-size:7pt;\ncolor:black;\n}\n.recaptchatable {\nline-height:1em;\nborder: 1px solid #dfdfdf !important;\n}\n.recaptcha_error_text {\ncolor:red;\n}\n' }; var RecaptchaStr_en = { visual_challenge: "Get a visual challenge", audio_challenge: "Get an audio challenge", refresh_btn: "Get a new challenge", instructions_visual: "Type the two words:", instructions_audio: "Type what you hear:", help_btn: "Help", play_again: "Play sound again", cant_hear_this: "Download sound as MP3", incorrect_try_again: "Incorrect. Try again." }; var RecaptchaStr_de = { visual_challenge: "Visuelle Aufgabe generieren", audio_challenge: "Audio-Aufgabe generieren", refresh_btn: "Neue Aufgabe generieren", instructions_visual: "Gib die 2 W\u00f6rter ein:", instructions_audio: "Gib die 8 Ziffern ein:", help_btn: "Hilfe", incorrect_try_again: "Falsch. Nochmals versuchen!" }; var RecaptchaStr_es = { visual_challenge: "Obt\u00e9n un reto visual", audio_challenge: "Obt\u00e9n un reto audible", refresh_btn: "Obt\u00e9n un nuevo reto", instructions_visual: "Escribe las 2 palabras:", instructions_audio: "Escribe los 8 n\u00fameros:", help_btn: "Ayuda", incorrect_try_again: "Incorrecto. Otro intento." }; var RecaptchaStr_fr = { visual_challenge: "D\u00e9fi visuel", audio_challenge: "D\u00e9fi audio", refresh_btn: "Nouveau d\u00e9fi", instructions_visual: "Entrez les deux mots:", instructions_audio: "Entrez les huit chiffres:", help_btn: "Aide", incorrect_try_again: "Incorrect." }; var RecaptchaStr_nl = { visual_challenge: "Test me via een afbeelding", audio_challenge: "Test me via een geluidsfragment", refresh_btn: "Nieuwe uitdaging", instructions_visual: "Type de twee woorden:", instructions_audio: "Type de acht cijfers:", help_btn: "Help", incorrect_try_again: "Foute invoer." }; var RecaptchaStr_pt = { visual_challenge: "Obter um desafio visual", audio_challenge: "Obter um desafio sonoro", refresh_btn: "Obter um novo desafio", instructions_visual: "Escreva as 2 palavras:", instructions_audio: "Escreva os 8 numeros:", help_btn: "Ajuda", incorrect_try_again: "Incorrecto. Tenta outra vez." }; var RecaptchaStr_ru = { visual_challenge: "\u0417\u0430\u0433\u0440\u0443\u0437\u0438\u0442\u044c \u0432\u0438\u0437\u0443\u0430\u043b\u044c\u043d\u0443\u044e \u0437\u0430\u0434\u0430\u0447\u0443", audio_challenge: "\u0417\u0430\u0433\u0440\u0443\u0437\u0438\u0442\u044c \u0437\u0432\u0443\u043a\u043e\u0432\u0443\u044e \u0437\u0430\u0434\u0430\u0447\u0443", refresh_btn: "\u0417\u0430\u0433\u0440\u0443\u0437\u0438\u0442\u044c \u043d\u043e\u0432\u0443\u044e \u0437\u0430\u0434\u0430\u0447\u0443", instructions_visual: "\u0412\u0432\u0435\u0434\u0438\u0442\u0435 \u0434\u0432\u0430 \u0441\u043b\u043e\u0432\u0430:", instructions_audio: "\u0412\u0432\u0435\u0434\u0438\u0442\u0435 \u0432\u043e\u0441\u0435\u043c\u044c \u0447\u0438\u0441\u0435\u043b:", help_btn: "\u041f\u043e\u043c\u043e\u0449\u044c", incorrect_try_again: "\u041d\u0435\u043f\u0440\u0430\u0432\u0438\u043b\u044c\u043d\u043e." }; var RecaptchaStr_tr = { visual_challenge: "G\u00f6rsel deneme", audio_challenge: "\u0130\u015Fitsel deneme", refresh_btn: "Yeni deneme", instructions_visual: "\u0130ki kelimeyi yaz\u0131n:", instructions_audio: "Sekiz numaray\u0131 yaz\u0131n:", help_btn: "Yard\u0131m (\u0130ngilizce)", incorrect_try_again: "Yanl\u0131\u015f. Bir daha deneyin." }; var RecaptchaLangMap = { en: RecaptchaStr_en, de: RecaptchaStr_de, es: RecaptchaStr_es, fr: RecaptchaStr_fr, nl: RecaptchaStr_nl, pt: RecaptchaStr_pt, ru: RecaptchaStr_ru, tr: RecaptchaStr_tr }; var RecaptchaStr = RecaptchaStr_en; var RecaptchaOptions; var RecaptchaDefaultOptions = { tabindex: 0, theme: 'red', callback: null, lang: 'en', custom_theme_widget: null, custom_translations: null }; var Recaptcha = { widget: null, timer_id: -1, style_set: false, theme: null, type: 'image', ajax_verify_cb: null, $: function (id) { if (typeof id == "string") return document.getElementById(id); else return id; }, create: function (public_key, element, options) { Recaptcha.destroy(); if (element) Recaptcha.widget = Recaptcha.$(element); Recaptcha._init_options(options); Recaptcha._call_challenge(public_key); }, destroy: function () { var challengefield = Recaptcha.$('recaptcha_challenge_field'); if (challengefield) challengefield.parentNode.removeChild(challengefield); if (Recaptcha.timer_id != -1) clearInterval(Recaptcha.timer_id); Recaptcha.timer_id = -1; var imagearea = Recaptcha.$('recaptcha_image'); if (imagearea) imagearea.innerHTML = ""; if (Recaptcha.widget) { if (Recaptcha.theme != "custom") Recaptcha.widget.innerHTML = ""; else Recaptcha.widget.style.display = "none"; Recaptcha.widget = null; } }, focus_response_field: function () { var $ = Recaptcha.$; var field = $('recaptcha_response_field'); field.focus(); }, get_challenge: function () { if (typeof RecaptchaState == "undefined") return null; return RecaptchaState.challenge; }, get_response: function () { var $ = Recaptcha.$; var field = $('recaptcha_response_field'); if (!field) return null; return field.value; }, ajax_verify: function (callback) { Recaptcha.ajax_verify_cb = callback; var scriptURL = Recaptcha._get_api_server() + "/ajaxverify" + "?c=" + encodeURIComponent(Recaptcha.get_challenge()) + "&response=" + encodeURIComponent(Recaptcha.get_response()); Recaptcha._add_script(scriptURL); }, _ajax_verify_callback: function (data) { Recaptcha.ajax_verify_cb(data); }, _get_api_server: function () { var protocol = window.location.protocol; var server; if (typeof _RecaptchaOverrideApiServer != "undefined") server = _RecaptchaOverrideApiServer; else if (protocol == 'https:') server = "api-secure.recaptcha.net"; else server = "api.recaptcha.net"; return protocol + "//" + server; }, _call_challenge: function (public_key) { var scriptURL = Recaptcha._get_api_server() + "/challenge?k=" + public_key + "&ajax=1&cachestop=" + Math.random(); if (typeof (RecaptchaOptions.extra_challenge_params) != "undefined") scriptURL += "&" + RecaptchaOptions.extra_challenge_params; Recaptcha._add_script(scriptURL); }, _add_script: function (scriptURL) { var scriptTag = document.createElement("script"); scriptTag.type = "text/javascript"; scriptTag.src = scriptURL; Recaptcha._get_script_area().appendChild(scriptTag); }, _get_script_area: function () { var parentElement = document.getElementsByTagName("head"); if (!parentElement || parentElement.length < 1) parentElement = document.body; else parentElement = parentElement[0]; return parentElement; }, _hash_merge: function (hashes) { var r = {}; for (var h in hashes) for (var k in hashes[h]) r[k] = hashes[h][k]; return r; }, _init_options: function (opts) { RecaptchaOptions = Recaptcha._hash_merge([RecaptchaDefaultOptions, opts || {}]); }, challenge_callback: function () { var element = Recaptcha.widget; Recaptcha._reset_timer(); RecaptchaStr = Recaptcha._hash_merge([RecaptchaStr_en, RecaptchaLangMap[RecaptchaOptions.lang] || {}, RecaptchaOptions.custom_translations || {}]); if (window.addEventListener) window.addEventListener('unload', function (e) { Recaptcha.destroy(); }, false); if (Recaptcha._is_ie() && window.attachEvent) window.attachEvent('onbeforeunload', function () { }); if (navigator.userAgent.indexOf("KHTML") > 0) { var iframe = document.createElement('iframe'); iframe.src = "about:blank"; iframe.style.height = "0px"; iframe.style.width = "0px"; iframe.style.visibility = "hidden"; iframe.style.border = "none"; var textNode = document.createTextNode("This frame prevents back/forward cache problems in Safari."); iframe.appendChild(textNode); document.body.appendChild(iframe); } Recaptcha._finish_widget(); }, _add_css: function (css) { var styleTag = document.createElement("style"); styleTag.type = "text/css"; if (styleTag.styleSheet) if (navigator.appVersion.indexOf("MSIE 5") != -1) document.write("<style type='text/css'>" + css + "</style>"); else styleTag.styleSheet.cssText = css; else if (navigator.appVersion.indexOf("MSIE 5") != -1) document.write("<style type='text/css'>" + css + "</style>"); else { var textNode = document.createTextNode(css); styleTag.appendChild(textNode); } Recaptcha._get_script_area().appendChild(styleTag); }, _set_style: function (css) { if (Recaptcha.style_set) return; Recaptcha.style_set = true; Recaptcha._add_css(css + "\n\n" + ".recaptcha_is_showing_audio .recaptcha_only_if_image," + ".recaptcha_isnot_showing_audio .recaptcha_only_if_audio," + ".recaptcha_had_incorrect_sol .recaptcha_only_if_no_incorrect_sol," + ".recaptcha_nothad_incorrect_sol .recaptcha_only_if_incorrect_sol" + "{display:none !important}"); }, _init_builtin_theme: function () { var $ = Recaptcha.$; var $_ = RecaptchaStr; var $ST = RecaptchaState; var css, html, imgfmt; var server_no_slash = $ST.server; if (server_no_slash[server_no_slash.length - 1] == "/") server_no_slash = server_no_slash.substring(0, server_no_slash.length - 1); var IMGROOT = server_no_slash + "/img/" + Recaptcha.theme; if (Recaptcha.theme == 'clean') { css = RecaptchaTemplates.CleanCss; html = RecaptchaTemplates.CleanHtml; imgfmt = 'png'; } else { css = RecaptchaTemplates.VertCss; html = RecaptchaTemplates.VertHtml; imgfmt = 'gif'; } css = css.replace(/IMGROOT/g, IMGROOT); Recaptcha._set_style(css); Recaptcha.widget.innerHTML = "<div id='recaptcha_area'>" + html + "</div>"; $('recaptcha_reload').src = IMGROOT + "/refresh." + imgfmt; $('recaptcha_switch_audio').src = IMGROOT + "/audio." + imgfmt; $('recaptcha_switch_img').src = IMGROOT + "/text." + imgfmt; $('recaptcha_whatsthis').src = IMGROOT + "/help." + imgfmt; if (Recaptcha.theme == 'clean') { $('recaptcha_logo').src = IMGROOT + "/logo." + imgfmt; $('recaptcha_tagline').src = IMGROOT + "/tagline." + imgfmt; } $('recaptcha_reload').alt = $_.refresh_btn; $('recaptcha_switch_audio').alt = $_.audio_challenge; $('recaptcha_switch_img').alt = $_.visual_challenge; $('recaptcha_whatsthis').alt = $_.help_btn; $('recaptcha_reload_btn').href = "javascript:Recaptcha.reload ();"; $('recaptcha_reload_btn').title = $_.refresh_btn; $('recaptcha_switch_audio_btn').href = "javascript:Recaptcha.switch_type('audio');"; $('recaptcha_switch_audio_btn').title = $_.audio_challenge; $('recaptcha_switch_img_btn').href = "javascript:Recaptcha.switch_type('image');"; $('recaptcha_switch_img_btn').title = $_.visual_challenge; $('recaptcha_whatsthis_btn').href = Recaptcha._get_help_link(); $('recaptcha_whatsthis_btn').target = "_blank"; $('recaptcha_whatsthis_btn').title = $_.help_btn; $('recaptcha_whatsthis_btn').onclick = function () { Recaptcha.showhelp(); return false; }; $('recaptcha_table').className = "recaptchatable " + "recaptcha_theme_" + Recaptcha.theme; if ($("recaptcha_instructions_image")) $("recaptcha_instructions_image").appendChild(document.createTextNode($_.instructions_visual)); if ($("recaptcha_instructions_audio")) $("recaptcha_instructions_audio").appendChild(document.createTextNode($_.instructions_audio)); if ($("recaptcha_instructions_error")) $("recaptcha_instructions_error").appendChild(document.createTextNode($_.incorrect_try_again)); }, _finish_widget: function () { var $ = Recaptcha.$; var $_ = RecaptchaStr; var $ST = RecaptchaState; var $OPT = RecaptchaOptions; var theme = $OPT.theme; switch (theme) { case 'red': case 'white': case 'blackglass': case 'clean': case 'custom': break; default: theme = 'red'; break; }if (!Recaptcha.theme) Recaptcha.theme = theme; if (Recaptcha.theme != "custom") Recaptcha._init_builtin_theme(); else Recaptcha._set_style(""); var challengeFieldHolder = document.createElement("span"); challengeFieldHolder.id = "recaptcha_challenge_field_holder"; challengeFieldHolder.style.display = "none"; $('recaptcha_response_field').parentNode.insertBefore(challengeFieldHolder, $('recaptcha_response_field')); $('recaptcha_response_field').setAttribute("autocomplete", "off"); $('recaptcha_image').style.width = '300px'; $('recaptcha_image').style.height = '57px'; Recaptcha.should_focus = false; Recaptcha._set_challenge($ST.challenge, 'image'); if ($OPT.tabindex) { $('recaptcha_response_field').tabIndex = $OPT.tabindex; if (Recaptcha.theme != "custom") { $('recaptcha_whatsthis_btn').tabIndex = $OPT.tabindex; $('recaptcha_switch_img_btn').tabIndex = $OPT.tabindex; $('recaptcha_switch_audio_btn').tabIndex = $OPT.tabindex; $('recaptcha_reload_btn').tabIndex = $OPT.tabindex; } } if (Recaptcha.widget) Recaptcha.widget.style.display = ''; if ($OPT.callback) $OPT.callback(); }, switch_type: function (new_type) { var $C = Recaptcha; $C.type = new_type; $C.reload($C.type == 'audio' ? 'a' : 'v'); }, reload: function (reason) { var $C = Recaptcha; var $ = $C.$; var $ST = RecaptchaState; if (typeof reason == "undefined") reason = 'r'; var scriptURL = $ST.server + "reload?c=" + $ST.challenge + "&k=" + $ST.site + "&reason=" + reason + "&type=" + $C.type + "&lang=" + RecaptchaOptions.lang; if (typeof (RecaptchaOptions.extra_challenge_params) != "undefined") scriptURL += "&" + RecaptchaOptions.extra_challenge_params; if ($C.type == 'audio') if (RecaptchaOptions.audio_beta_12_08) scriptURL += "&audio_beta_12_08=1"; else scriptURL += "&new_audio_default=1"; $C.should_focus = reason != 't'; $C._add_script(scriptURL); }, finish_reload: function (new_challenge, type) { RecaptchaState.is_incorrect = false; Recaptcha._set_challenge(new_challenge, type); }, _set_challenge: function (new_challenge, type) { var $C = Recaptcha; var $ST = RecaptchaState; var $ = $C.$; $ST.challenge = new_challenge; $C.type = type; $('recaptcha_challenge_field_holder').innerHTML = "<input type='hidden' name='recaptcha_challenge_field' id='recaptcha_challenge_field' value='" + $ST.challenge + "'/>"; if (type == 'audio') $("recaptcha_image").innerHTML = Recaptcha.getAudioCaptchaHtml(); else if (type == 'image') { var imageurl = $ST.server + 'image?c=' + $ST.challenge; $('recaptcha_image').innerHTML = "<img style='display:block;' height='57' width='300' src='" + imageurl + "'/>"; } Recaptcha._css_toggle("recaptcha_had_incorrect_sol", "recaptcha_nothad_incorrect_sol", $ST.is_incorrect); Recaptcha._css_toggle("recaptcha_is_showing_audio", "recaptcha_isnot_showing_audio", type == 'audio'); $C._clear_input(); if ($C.should_focus) $C.focus_response_field(); $C._reset_timer(); }, _reset_timer: function () { var $ST = RecaptchaState; clearInterval(Recaptcha.timer_id); Recaptcha.timer_id = setInterval("Recaptcha.reload('t');", ($ST.timeout - 60 * 5) * 1000); }, showhelp: function () { window.open(Recaptcha._get_help_link(), "recaptcha_popup", "width=460,height=570,location=no,menubar=no,status=no,toolbar=no,scrollbars=yes,resizable=yes"); }, _clear_input: function () { var resp = Recaptcha.$('recaptcha_response_field'); resp.value = ""; }, _displayerror: function (msg) { var $ = Recaptcha.$; $('recaptcha_image').innerHTML = ''; $('recaptcha_image').appendChild(document.createTextNode(msg)); }, reloaderror: function (msg) { Recaptcha._displayerror(msg); }, _is_ie: function () { return (navigator.userAgent.indexOf("MSIE") > 0) && !window.opera; }, _css_toggle: function (classT, classF, isset) { var element = Recaptcha.widget; if (!element) element = document.body; var classname = element.className; classname = classname.replace(new RegExp("(^|\\s+)" + classT + "(\\s+|$)"), ' '); classname = classname.replace(new RegExp("(^|\\s+)" + classF + "(\\s+|$)"), ' '); classname += " " + (isset ? classT : classF); element.className = classname; }, _get_help_link: function () { var lang = RecaptchaOptions.lang; return 'http://recaptcha.net/popuphelp/' + (lang == 'en' ? "" : (lang + ".html")); }, playAgain: function () { var $ = Recaptcha.$; $("recaptcha_image").innerHTML = Recaptcha.getAudioCaptchaHtml(); }, getAudioCaptchaHtml: function () { var $C = Recaptcha; var $ST = RecaptchaState; var $ = Recaptcha.$; var httpwavurl = $ST.server + "image?c=" + $ST.challenge; if (httpwavurl.indexOf("https://") == 0) httpwavurl = "http://" + httpwavurl.substring(8); var swfUrl = $ST.server + "/img/audiocaptcha.swf?v2"; var embedCode; if ($C._is_ie()) embedCode = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" id="audiocaptcha" width="0" height="0" codebase="https://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab"><param name="movie" value="' + swfUrl + '" /><param name="quality" value="high" /><param name="bgcolor" value="#869ca7" /><param name="allowScriptAccess" value="always" /></object><br/>'; else embedCode = '<embed src="' + swfUrl + '" quality="high" bgcolor="#869ca7" width="0" height="0" name="audiocaptcha" align="middle" play="true" loop="false" quality="high" allowScriptAccess="always" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer"></embed> '; var cantHearCode = (Recaptcha.checkFlashVer() ? '<br/><a class="recaptcha_audio_cant_hear_link" href="#" onclick="Recaptcha.playAgain(); return false;">' + RecaptchaStr.play_again + '</a>' : '') + '<br/><a class="recaptcha_audio_cant_hear_link" target="_blank" href="' + httpwavurl + '">' + RecaptchaStr.cant_hear_this + '</a>'; return embedCode + cantHearCode; }, gethttpwavurl: function () { var $ST = RecaptchaState; if (Recaptcha.type == 'audio') { var httpwavurl = $ST.server + "image?c=" + $ST.challenge; if (httpwavurl.indexOf("https://") == 0) httpwavurl = "http://" + httpwavurl.substring(8); return httpwavurl; } return ""; }, checkFlashVer: function () { var isIE = (navigator.appVersion.indexOf("MSIE") != -1) ? true : false; var isWin = (navigator.appVersion.toLowerCase().indexOf("win") != -1) ? true : false; var isOpera = (navigator.userAgent.indexOf("Opera") != -1) ? true : false; var flashVer = -1; if (navigator.plugins != null && navigator.plugins.length > 0) { if (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]) { var swVer2 = navigator.plugins["Shockwave Flash 2.0"] ? " 2.0" : ""; var flashDescription = navigator.plugins["Shockwave Flash" + swVer2].description; var descArray = flashDescription.split(" "); var tempArrayMajor = descArray[2].split("."); flashVer = tempArrayMajor[0]; } } else if (isIE && isWin && !isOpera) try { var axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7"); var flashVerStr = axo.GetVariable("$version"); flashVer = flashVerStr.split(" ")[1].split(",")[0]; } catch (e) { } return flashVer >= 9; }, getlang: function () { return RecaptchaOptions.lang; } };
    /* resource: ++resource++plone.formwidget.autocomplete/formwidget-autocomplete.js */
    function formwidget_autocomplete_ready(event, data, formatted) { (function ($) { var input_box = $(event.target); formwidget_autocomplete_new_value(input_box, data[0], data[1]); }(jQuery)); } function formwidget_autocomplete_new_value(input_box, value, label) { (function ($) { var base_id = input_box[0].id.replace(/-widgets-query$/, ""); var base_name = input_box[0].name.replace(/\.widgets\.query$/, ""); var widget_base = $('#' + base_id + "-input-fields"); var all_fields = widget_base.find('input:radio, input:checkbox'); input_box.val(""); widget_base.find('input:radio').prop('checked', false); var selected_field = widget_base.find('input[value="' + value + '"]'); if (selected_field.length) { selected_field.each(function () { this.checked = true; }); return; } widget_base, base_name, base_id; var idx = all_fields.length; var klass = widget_base.data('klass'); var title = widget_base.data('title'); var type = widget_base.data('input_type'); var multiple = widget_base.data('multiple'); var span = $('<span/>').attr("id", base_id + "-" + idx + "-wrapper").attr("class", "option"); span.append($("<label/>").attr("for", base_id + "-" + idx).append($('<input type="' + type + '"' + ' name="' + base_name + (multiple ? ':list"' : '"') + ' checked="checked" />').attr("id", base_id + "-" + idx).attr("title", title).attr("value", value).addClass(klass)).append(" ").append($("<span>").attr("class", "label").text(label))); widget_base.append(span); }(jQuery)); } function formwidget_autocomplete_parser(formatResult, fieldNum) { return function (data) { var parsed = []; if (data !== undefined && data.split) { var rows = data.split("\n"); for (var i = 0; i < rows.length; i++) { var row = $.trim(rows[i]); if (row) { row = row.split("|"); parsed[parsed.length] = { data: row, value: row[fieldNum], result: formatResult(row, row[fieldNum]) }; } } } return parsed; }; }
    /* resource: ++resource++plone.formwidget.autocomplete/jquery.autocomplete.min.js */
    /*
     * Autocomplete - jQuery plugin 1.0.2
     *
     * Copyright (c) 2007 Dylan Verheul, Dan G. Switzer, Anjesh Tuladhar, Jörn Zaefferer
     *
     * Dual licensed under the MIT and GPL licenses:
     *   http://www.opensource.org/licenses/mit-license.php
     *   http://www.gnu.org/licenses/gpl.html
     *
     * Revision: $Id: jquery.autocomplete.js 5747 2008-06-25 18:30:55Z joern.zaefferer $
     *
     */
    ; (function (e) { e.fn.extend({ autocomplete: function (t, n) { var r = typeof t == "string"; n = e.extend({}, e.Autocompleter.defaults, { url: r ? t : null, data: r ? null : t, delay: r ? e.Autocompleter.defaults.delay : 10, max: n && !n.scroll ? 10 : 150 }, n); n.highlight = n.highlight || function (e) { return e }; n.formatMatch = n.formatMatch || n.formatItem; return this.each(function () { new e.Autocompleter(this, n) }) }, result: function (e) { return this.bind("result", e) }, search: function (e) { return this.trigger("search", [e]) }, flushCache: function () { return this.trigger("flushCache") }, setOptions: function (e) { return this.trigger("setOptions", [e]) }, unautocomplete: function () { return this.trigger("unautocomplete") } }); e.Autocompleter = function (t, n) { function d() { var e = c.selected(); if (!e) return false; var t = e.result; o = t; if (n.multiple) { var r = m(i.val()); if (r.length > 1) { t = r.slice(0, r.length - 1).join(n.multipleSeparator) + n.multipleSeparator + t } t += n.multipleSeparator } i.val(t); w(); i.trigger("result", [e.data, e.value]); return true } function v(e, t) { if (f == r.DEL) { c.hide(); return } var s = i.val(); if (!t && s == o) return; o = s; s = g(s); if (s.length >= n.minChars) { i.addClass(n.loadingClass); if (!n.matchCase) s = s.toLowerCase(); S(s, E, w) } else { T(); c.hide() } } function m(t) { if (!t) { return [""] } var r = t.split(n.multipleSeparator); var i = []; e.each(r, function (t, n) { if (e.trim(n)) i[t] = e.trim(n) }); return i } function g(e) { if (!n.multiple) return e; var t = m(e); return t[t.length - 1] } function y(s, u) { if (n.autoFill && g(i.val()).toLowerCase() == s.toLowerCase() && f != r.BACKSPACE) { i.val(i.val() + u.substring(g(o).length)); e.Autocompleter.Selection(t, o.length, o.length + u.length) } } function b() { clearTimeout(s); s = setTimeout(w, 200) } function w() { var r = c.visible(); c.hide(); clearTimeout(s); T(); if (n.mustMatch) { i.search(function (e) { if (!e) { if (n.multiple) { var t = m(i.val()).slice(0, -1); i.val(t.join(n.multipleSeparator) + (t.length ? n.multipleSeparator : "")) } else i.val("") } }) } if (r) e.Autocompleter.Selection(t, t.value.length, t.value.length) } function E(e, t) { if (t && t.length && a) { T(); c.display(t, e); y(e, t[0].value); c.show() } else { w() } } function S(r, i, s) { if (!n.matchCase) r = r.toLowerCase(); var o = u.load(r); if (o && o.length) { i(r, o) } else if (typeof n.url == "string" && n.url.length > 0) { var a = { timestamp: +(new Date) }; e.each(n.extraParams, function (e, t) { a[e] = typeof t == "function" ? t() : t }); e.ajax({ mode: "abort", port: "autocomplete" + t.name, dataType: n.dataType, url: n.url, data: e.extend({ q: g(r), limit: n.max }, a), success: function (e) { var t = n.parse && n.parse(e) || x(e); u.add(r, t); i(r, t) } }) } else { c.emptyList(); s(r) } } function x(t) { var r = []; var i = t.split("\n"); for (var s = 0; s < i.length; s++) { var o = e.trim(i[s]); if (o) { o = o.split("|"); r[r.length] = { data: o, value: o[0], result: n.formatResult && n.formatResult(o, o[0]) || o[0] } } } return r } function T() { i.removeClass(n.loadingClass) } var r = { UP: 38, DOWN: 40, DEL: 46, TAB: 9, RETURN: 13, ESC: 27, COMMA: 188, PAGEUP: 33, PAGEDOWN: 34, BACKSPACE: 8 }; var i = e(t).attr("autocomplete", "off").addClass(n.inputClass); var s; var o = ""; var u = e.Autocompleter.Cache(n); var a = 0; var f; var l = { mouseDownOnSelect: false }; var c = e.Autocompleter.Select(n, t, d, l); var h; var p = navigator.userAgent.indexOf("Opera") != -1; p && e(t.form).bind("submit.autocomplete", function () { if (h) { h = false; return false } }); i.bind((p ? "keypress" : "keydown") + ".autocomplete", function (t) { f = t.keyCode; switch (t.keyCode) { case r.UP: t.preventDefault(); if (c.visible()) { c.prev() } else { v(0, true) } break; case r.DOWN: t.preventDefault(); if (c.visible()) { c.next() } else { v(0, true) } break; case r.PAGEUP: t.preventDefault(); if (c.visible()) { c.pageUp() } else { v(0, true) } break; case r.PAGEDOWN: t.preventDefault(); if (c.visible()) { c.pageDown() } else { v(0, true) } break; case n.multiple && e.trim(n.multipleSeparator) == "," && r.COMMA: case r.TAB: case r.RETURN: if (d()) { t.preventDefault(); h = true; return false } break; case r.ESC: c.hide(); break; default: clearTimeout(s); s = setTimeout(v, n.delay); break } }).focus(function () { a++ }).blur(function () { a = 0; if (!l.mouseDownOnSelect) { b() } }).click(function () { if (a++ > 1 && !c.visible()) { v(0, true) } }).bind("search", function () { function n(e, n) { var r; if (n && n.length) { for (var s = 0; s < n.length; s++) { if (n[s].result.toLowerCase() == e.toLowerCase()) { r = n[s]; break } } } if (typeof t == "function") t(r); else i.trigger("result", r && [r.data, r.value]) } var t = arguments.length > 1 ? arguments[1] : null; e.each(m(i.val()), function (e, t) { S(t, n, n) }) }).bind("flushCache", function () { u.flush() }).bind("setOptions", function () { e.extend(n, arguments[1]); if ("data" in arguments[1]) u.populate() }).bind("unautocomplete", function () { c.unbind(); i.unbind(); e(t.form).unbind(".autocomplete") }); }; e.Autocompleter.defaults = { inputClass: "ac_input", resultsClass: "ac_results", loadingClass: "ac_loading", minChars: 1, delay: 400, matchCase: false, matchSubset: true, matchContains: false, cacheLength: 10, max: 100, mustMatch: false, extraParams: {}, selectFirst: true, formatItem: function (e) { return e[0] }, formatMatch: null, autoFill: false, width: 0, multiple: false, multipleSeparator: ", ", highlight: function (e, t) { return e.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + t.replace(/([\^\$\(\)\[\]\{\}\*\.\+\?\|\\])/gi, "\\$1") + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<strong>$1</strong>") }, scroll: true, scrollHeight: 180 }; e.Autocompleter.Cache = function (t) { function i(e, n) { if (!t.matchCase) e = e.toLowerCase(); var r = e.indexOf(n); if (r == -1) return false; return r == 0 || t.matchContains } function s(e, i) { if (r > t.cacheLength) { u() } if (!n[e]) { r++ } n[e] = i } function o() { if (!t.data) return false; var n = {}, r = 0; if (!t.url) t.cacheLength = 1; n[""] = []; for (var i = 0, o = t.data.length; i < o; i++) { var u = t.data[i]; u = typeof u == "string" ? [u] : u; var a = t.formatMatch(u, i + 1, t.data.length); if (a === false) continue; var f = a.charAt(0).toLowerCase(); if (!n[f]) n[f] = []; var l = { value: a, data: u, result: t.formatResult && t.formatResult(u) || a }; n[f].push(l); if (r++ < t.max) { n[""].push(l) } } e.each(n, function (e, n) { t.cacheLength++; s(e, n) }) } function u() { n = {}; r = 0 } var n = {}; var r = 0; setTimeout(o, 25); return { flush: u, add: s, populate: o, load: function (s) { if (!t.cacheLength || !r) return null; if (!t.url && t.matchContains) { var o = []; for (var u in n) { if (u.length > 0) { var a = n[u]; e.each(a, function (e, t) { if (i(t.value, s)) { o.push(t) } }) } } return o } else if (n[s]) { return n[s] } else if (t.matchSubset) { for (var f = s.length - 1; f >= t.minChars; f--) { var a = n[s.substr(0, f)]; if (a) { var o = []; e.each(a, function (e, t) { if (i(t.value, s)) { o[o.length] = t } }); return o } } } return null } } }; e.Autocompleter.Select = function (t, n, r, i) { function p() { if (!l) return; c = e("<div/>").hide().addClass(t.resultsClass).css("position", "absolute").appendTo(document.body); h = e("<ul/>").appendTo(c).mouseover(function (t) { if (d(t).nodeName && d(t).nodeName.toUpperCase() == "LI") { u = e("li", h).removeClass(s.ACTIVE).index(d(t)); e(d(t)).addClass(s.ACTIVE) } }).click(function (t) { e(d(t)).addClass(s.ACTIVE); r(); n.focus(); return false }).mousedown(function () { i.mouseDownOnSelect = true }).mouseup(function () { i.mouseDownOnSelect = false }); if (t.width > 0) c.css("width", t.width); l = false } function d(e) { var t = e.target; while (t && t.tagName != "LI") t = t.parentNode; if (!t) return []; return t } function v(e) { o.slice(u, u + 1).removeClass(s.ACTIVE); m(e); var n = o.slice(u, u + 1).addClass(s.ACTIVE); if (t.scroll) { var r = 0; o.slice(0, u).each(function () { r += this.offsetHeight }); if (r + n[0].offsetHeight - h.scrollTop() > h[0].clientHeight) { h.scrollTop(r + n[0].offsetHeight - h.innerHeight()) } else if (r < h.scrollTop()) { h.scrollTop(r) } } } function m(e) { u += e; if (u < 0) { u = o.size() - 1 } else if (u >= o.size()) { u = 0 } } function g(e) { return t.max && t.max < e ? t.max : e } function y() { h.empty(); var n = g(a.length); for (var r = 0; r < n; r++) { if (!a[r]) continue; var i = t.formatItem(a[r].data, r + 1, n, a[r].value, f); if (i === false) continue; var l = e("<li/>").html(t.highlight(i, f)).addClass(r % 2 == 0 ? "ac_even" : "ac_odd").appendTo(h)[0]; e.data(l, "ac_data", a[r]) } o = h.find("li"); if (t.selectFirst) { o.slice(0, 1).addClass(s.ACTIVE); u = 0 } if (e.fn.bgiframe) h.bgiframe() } var s = { ACTIVE: "ac_over" }; var o, u = -1, a, f = "", l = true, c, h; return { display: function (e, t) { p(); a = e; f = t; y() }, next: function () { v(1) }, prev: function () { v(-1) }, pageUp: function () { if (u != 0 && u - 8 < 0) { v(-u) } else { v(-8) } }, pageDown: function () { if (u != o.size() - 1 && u + 8 > o.size()) { v(o.size() - 1 - u) } else { v(8) } }, hide: function () { c && c.hide(); o && o.removeClass(s.ACTIVE); u = -1 }, visible: function () { return c && c.is(":visible") }, current: function () { return this.visible() && (o.filter("." + s.ACTIVE)[0] || t.selectFirst && o[0]) }, show: function () { var r = e(n).offset(); c.css({ width: typeof t.width == "string" || t.width > 0 ? t.width : e(n).width(), top: r.top + n.offsetHeight, left: r.left }).show(); if (t.scroll) { h.scrollTop(0); h.css({ maxHeight: t.scrollHeight, overflow: "auto" }); if (navigator.userAgent.indexOf("MSIE") != 1 && typeof document.body.style.maxHeight === "undefined") { var i = 0; o.each(function () { i += this.offsetHeight }); var s = i > t.scrollHeight; h.css("height", s ? t.scrollHeight : i); if (!s) { o.width(h.width() - parseInt(o.css("padding-left")) - parseInt(o.css("padding-right"))) } } } }, selected: function () { var t = o && o.filter("." + s.ACTIVE).removeClass(s.ACTIVE); return t && t.length && e.data(t[0], "ac_data") }, emptyList: function () { h && h.empty() }, unbind: function () { c && c.remove() } } }; e.Autocompleter.Selection = function (e, t, n) { if (e.createTextRange) { var r = e.createTextRange(); r.collapse(true); r.moveStart("character", t); r.moveEnd("character", n); r.select() } else if (e.setSelectionRange) { e.setSelectionRange(t, n) } else { if (e.selectionStart) { e.selectionStart = t; e.selectionEnd = n } } e.focus() } })(jQuery)

    /* resource: ++resource++plone.formwidget.contenttree/contenttree.js */
    if (jQuery) (function ($) { $.extend($.fn, { showDialog: function () { $(document.body).append($(document.createElement("div")).addClass("contenttreeWindowBlocker")); this[0].oldparent = $(this).parent()[0]; $(".contenttreeWindowBlocker").before(this); $(this).show(); $(this).width($(window).width() * 0.75); $(this).height($(window).height() * 0.75); $(this).css({ 'left': $(window).width() * 0.125, 'top': $(window).height() * 0.125 }); }, contentTreeAdd: function () { var contenttree_window = this.parents(".contenttreeWindow"); var input_box = $('#' + contenttree_window[0].id.replace(/-contenttree-window$/, "-widgets-query")); contenttree_window.find('.navTreeCurrentItem > a').each(function () { formwidget_autocomplete_new_value(input_box, $(this).attr('href'), $.trim($(this).text())); }); $(this).contentTreeCancel(); input_box.parents('.datagridwidget-cell').triggerHandler('change'); }, contentTreeCancel: function () { $(".contenttreeWindowBlocker").remove(); var popup = $(this).parents(".contenttreeWindow"); popup.hide(); $(popup[0].oldparent).append(popup); popup[0].oldparent = null; }, contentTree: function (o, h) { if (!o) var o = {}; if (o.script == undefined) o.script = 'fetch'; if (o.folderEvent == undefined) o.folderEvent = 'click'; if (o.selectEvent == undefined) o.selectEvent = 'click'; if (o.expandSpeed == undefined) o.expandSpeed = -1; if (o.collapseSpeed == undefined) o.collapseSpeed = -1; if (o.multiFolder == undefined) o.multiFolder = true; if (o.multiSelect == undefined) o.multiSelect = false; function loadTree(c, t, r) { $(c).addClass('wait'); $.post(o.script, { href: t, rel: r }, function (data) { $(c).removeClass('wait').append(data); $(c).find('ul:hidden').slideDown({ duration: o.expandSpeed }); bindTree(c); }); } function handleFolderEvent() { var li = $(this).parent(); if (li.hasClass('collapsed')) { if (!o.multiFolder) { li.parent().find('ul:visible').slideUp({ duration: o.collapseSpeed }); li.parent().find('li.navTreeFolderish').removeClass('expanded').addClass('collapsed'); } if (li.find('ul').length == 0) loadTree(li, escape($(this).attr('href')), escape($(this).attr('rel'))); else li.find('ul:hidden').slideDown({ duration: o.expandSpeed }); li.removeClass('collapsed').addClass('expanded'); } else { li.find('ul').slideUp({ duration: o.collapseSpeed }); li.removeClass('expanded').addClass('collapsed'); } return false; } function handleSelectEvent(event) { var li = $(this).parent(); var selected = true; var root = $(this).parents('ul.navTree'); if (!li.hasClass('navTreeCurrentItem')) { var multi_key = ((event.ctrlKey) || (navigator.userAgent.toLowerCase().indexOf('macintosh') != -1 && event.metaKey)); if (!o.multiSelect || !multi_key) root.find('li.navTreeCurrentItem').removeClass('navTreeCurrentItem'); li.addClass('navTreeCurrentItem'); selected = true; } else { li.removeClass('navTreeCurrentItem'); selected = false; } h(event, true, $(this).attr('href'), $.trim($(this).text())); } function bindTree(t) { $(t).find('li.navTreeFolderish a').unbind(o.folderEvent); $(t).find('li.selectable a').unbind(o.selectEvent); $(t).find('li a').bind('click', function () { return false; }); $(t).find('li.navTreeFolderish a').bind(o.folderEvent, handleFolderEvent); $(t).find('li.selectable a').bind(o.selectEvent, handleSelectEvent); } if ($(this).children('ul.navTree').length <= 0) $(this).each(function () { loadTree(this, o.rootUrl, 0); }); } }); })(jQuery);
    /* resource: ++plone++plone.app.event/event.js */
    (function ($) { var eventedit = { start_end_delta: 1 / 24, $start_input: undefined, $start_container: undefined, $pickadate_starttime: undefined, $end_input: undefined, $end_container: undefined, $pickadate_endtime: undefined, $whole_day_input: undefined, $open_end_input: undefined, get_dom_element: function (sel, $container) { var $el; if ($container) $el = $(sel, $container); else $el = $(sel); return $el.length ? $el : undefined; }, getDateTime: function (datetimewidget) { var date, time, datetime, set_time; date = $('input[name="_submit"]:first', datetimewidget).prop('value'); if (!date) return; date = date.split('-'); time = $('input[name="_submit"]:last', datetimewidget).prop('value'); if (!time) { set_time = true; time = '00:00'; } time = time.split(':'); datetime = new Date(parseInt(date[0], 10), parseInt(date[1], 10) - 1, parseInt(date[2], 10), parseInt(time[0], 10), parseInt(time[1], 10)); if (set_time) $('.pattern-pickadate-time', datetimewidget).pickatime('picker').set('select', datetime); return datetime; }, initStartEndDelta: function (start_container, end_container) { var start_datetime = this.getDateTime(start_container); var end_datetime = this.getDateTime(end_container); if (!start_datetime || !end_datetime) return; this.start_end_delta = (end_datetime - start_datetime) / 1000 / 60; }, updateEndDate: function (start_container, end_container) { var start_date = this.getDateTime(start_container); if (!start_date) return; var new_end_date = new Date(start_date); new_end_date.setMinutes(start_date.getMinutes() + this.start_end_delta); $('.pattern-pickadate-date', end_container).pickadate('picker').set('select', new_end_date); $('.pattern-pickadate-time', end_container).pickatime('picker').set('select', new_end_date); }, validateEndDate: function (start_container, end_container) { var start_datetime = this.getDateTime(start_container); var end_datetime = this.getDateTime(end_container); if (!start_datetime || !end_datetime) return; if (end_datetime < start_datetime) start_container.addClass('error'); else end_container.removeClass('error'); }, show_hide_widget: function (widget, hide, fade) { var $widget = $(widget); if (hide === true) if (fade === true) $widget.fadeOut(); else $widget.hide(); else if (fade === true) $widget.fadeIn(); else $widget.show(); }, event_listing_calendar_init: function (cal) { if ($().dateinput && cal.length > 0) { var get_req_param, val; get_req_param = function (name) { if (name === new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)').exec(window.location.search)) return decodeURIComponent(name[1]); }; val = get_req_param('date'); if (val === undefined) val = new Date(); else val = new Date(val); cal.dateinput({ selectors: true, trigger: true, format: 'yyyy-mm-dd', yearRange: [-10, 10], firstDay: 1, value: val, change: function () { var value = this.getValue('yyyy-mm-dd'); window.location.href = 'event_listing?mode=day&date=' + value; } }).unbind('change').bind('onShow', function () { var trigger_offset = $(this).next().offset(); $(this).data('dateinput').getCalendar().offset({ top: trigger_offset.top + 20, left: trigger_offset.left }); }); } }, initilize_event: function () { var $start_container = this.$start_container, $end_container = this.$end_container, $pickadate_starttime = this.$pickadate_starttime, $pickadate_endtime = this.$pickadate_endtime, $open_end_input = this.$open_end_input, $whole_day_input = this.$whole_day_input; if ($whole_day_input.length > 0) { $whole_day_input.bind('change', function (e) { this.show_hide_widget($pickadate_starttime, e.target.checked, true); this.show_hide_widget($pickadate_endtime, e.target.checked, true); }.bind(this)); this.show_hide_widget($pickadate_starttime, $whole_day_input.get(0).checked, false); this.show_hide_widget($pickadate_endtime, $whole_day_input.get(0).checked, false); } if ($open_end_input.length > 0) { $open_end_input.bind('change', function (e) { this.show_hide_widget($end_container, e.target.checked, true); }.bind(this)); this.show_hide_widget($end_container, $open_end_input.get(0).checked, false); } $start_container.on('focus', '.picker__input', function () { this.initStartEndDelta($start_container, $end_container); }.bind(this)); $start_container.on('change', '.picker__input', function () { this.updateEndDate($start_container, $end_container); }.bind(this)); $end_container.on('focus', '.picker__input', function () { this.initStartEndDelta($start_container, $end_container); }.bind(this)); $end_container.on('change', '.picker__input', function () { this.validateEndDate($start_container, $end_container); }.bind(this)); } }; $(document).ready(function () { eventedit.$start_input = eventedit.get_dom_element('form input.event_start'); if (!eventedit.$start_input) return; eventedit.$end_input = eventedit.get_dom_element('form input.event_end'); if (!eventedit.$end_input) return; eventedit.$start_container = eventedit.$start_input.closest('div'); eventedit.$end_container = eventedit.$end_input.closest('div'); eventedit.$whole_day_input = eventedit.get_dom_element('form input.event_whole_day'); eventedit.$open_end_input = eventedit.get_dom_element('form input.event_open_end'); var interval = setInterval(function () { eventedit.$pickadate_starttime = !eventedit.$pickadate_starttime && eventedit.get_dom_element('.pattern-pickadate-time-wrapper', eventedit.$start_container); eventedit.$pickadate_endtime = !eventedit.$pickadate_endtime && eventedit.get_dom_element('.pattern-pickadate-time-wrapper', eventedit.$end_container); if (eventedit.$pickadate_starttime && eventedit.$pickadate_endtime) { clearInterval(interval); eventedit.initilize_event(); } }, 100); eventedit.event_listing_calendar_init($('#event_listing_calendar')); }); })(jQuery);
    /* resource: ++plone++plone.app.discussion.javascripts/comments.js */
    if (require === undefined) require = function (reqs, torun) { 'use strict'; return torun(window.jQuery); }; require(['jquery'], function ($) { 'use strict'; $.createReplyForm = function (comment_div) { var comment_id = comment_div.attr('id'); var reply_button = comment_div.find('.reply-to-comment-button'); var reply_div = $('#commenting').clone(true); reply_div.find('#formfield-form-widgets-captcha').find('script').remove(); reply_div.appendTo(comment_div).css('display', 'none'); reply_div.removeAttr('id'); $(reply_button).css('display', 'none'); var reply_form = reply_div.find('form'); reply_form.find('#formfield-form-widgets-comment-text').attr('id', 'formfield-form-widgets-new-textarea' + comment_id); reply_form.find('#form-widgets-comment-text').attr('id', 'form-widgets-new-textarea' + comment_id); reply_form.find('input[name="form.widgets.in_reply_to"]').val(comment_id); var cancel_reply_button = reply_div.find('.cancelreplytocomment'); cancel_reply_button.attr('id', comment_id); reply_form.find('input[name="form.buttons.cancel"]').css('display', 'inline'); reply_div.slideDown('slow'); cancel_reply_button.css('display', 'inline'); }; $.clearForm = function (form_div) { form_div.find('.error').removeClass('error'); form_div.find('.fieldErrorBox').remove(); form_div.find('input[type="text"]').attr('value', ''); form_div.find('textarea').attr('value', ''); }; $(window).load(function () { var post_comment_div = $('#commenting'); var in_reply_to_field = post_comment_div.find('input[name="form.widgets.in_reply_to"]'); if (in_reply_to_field.length !== 0 && in_reply_to_field.val() !== '') { var current_reply_id = '#' + in_reply_to_field.val(); var current_reply_to_div = $('.discussion').find(current_reply_id); $.createReplyForm(current_reply_to_div); $.clearForm(post_comment_div); } $('.reply-to-comment-button').bind('click', function (e) { var comment_div = $(this).parents().filter('.comment'); $.createReplyForm(comment_div); $.clearForm(comment_div); }); $('#commenting #form-buttons-cancel').bind('click', function (e) { e.preventDefault(); var reply_to_comment_button = $(this).parents().filter('.comment').find('.reply-to-comment-button'); $.reply_to_comment_form = $(this).parents().filter('.reply'); $.reply_to_comment_form.slideUp('slow', function () { $(this).remove(); }); reply_to_comment_button.css('display', 'inline'); }); $('input[name="form.button.PublishComment"]').on('click', function () { var trigger = this; var form = $(this).parents('form'); var data = $(form).serialize(); var form_url = $(form).attr('action'); $.ajax({ type: 'GET', url: form_url, data: data, context: trigger, success: function (msg) { form.find('input[name="form.button.PublishComment"]').remove(); form.parents('.state-pending').toggleClass('state-pending').toggleClass('state-published'); }, error: function (msg) { return true; } }); return false; }); if ($.fn.prepOverlay) $('form[name="edit"]').prepOverlay({ cssclass: 'overlay-edit-comment', width: '60%', subtype: 'ajax', filter: '#content>*' }); $('input[name="form.button.DeleteComment"]').on('click', function () { var trigger = this; var form = $(this).parents('form'); var data = $(form).serialize(); var form_url = $(form).attr('action'); $.ajax({ type: 'POST', url: form_url, data: data, context: $(trigger).parents('.comment'), success: function (data) { var comment = $(this); var clss = comment.attr('class'); var treelevel = parseInt(clss[clss.indexOf('replyTreeLevel') + 'replyTreeLevel'.length], 10); var selector = '.replyTreeLevel' + treelevel; for (var i = 0; i < treelevel; i++)selector += ', .replyTreeLevel' + i; comment.nextUntil(selector).each(function () { $(this).fadeOut('fast', function () { $(this).remove(); }); }); var parent = comment.prev('[class*="replyTreeLevel' + (treelevel - 1) + '"]'); parent.find('form[name="delete"]').css('display', 'inline'); $(this).fadeOut('fast', function () { $(this).remove(); }); }, error: function (req, error) { return true; } }); return false; }); $('.reply').find('input[name="form.buttons.reply"]').css('display', 'none'); $('.reply').find('input[name="form.buttons.cancel"]').css('display', 'none'); $('.reply-to-comment-button').removeClass('hide'); }); });
} catch (e) {
    // log it
    if (typeof console !== "undefined") {
        console.log('Error loading javascripts!' + e);
    }
} finally {
    define = _old_define;
    require = _old_require;
}

// End Bundle: plone-legacy