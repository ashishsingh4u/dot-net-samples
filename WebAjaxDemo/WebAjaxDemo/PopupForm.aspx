<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupForm.aspx.cs" Inherits="WebAjaxDemo.PopupForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">

<head>
  <title></title>
<script type="text/javascript">

    document.onclick = check;

    var Ary = [];

    function check(e) {
        var target = (e && e.target) || (event && event.srcElement);
        while (target.parentNode) {
            if (target.className.match('pop') || target.className.match('poplink')) return;
            target = target.parentNode;
        }
        var ary = zxcByClassName('pop')
        for (var z0 = 0; z0 < ary.length; z0++) {
            ary[z0].style.display = 'none';
        }
    }
    function zxcByClassName(nme, el, tag) {
        if (typeof (el) == 'string') el = document.getElementById(el);
        el = el || document;
        for (var tag = tag || '*', reg = new RegExp('\\b' + nme + '\\b'), els = el.getElementsByTagName(tag), ary = [], z0 = 0; z0 < els.length; z0++) {
            if (reg.test(els[z0].className)) ary.push(els[z0]);
        }
        return ary;
    }

    function toggle(layer_ref) {
        var hza = document.getElementById(layer_ref);
        if (hza && hza.style) {
            if (!hza.set) { hza.set = true; Ary.push(hza); }
            hza.style.display = (hza.style.display == '') ? 'none' : '';
        }
    }

</script>
</head>

<body>
<a href="#"  class="poplink" onclick="toggle('div1');return false;" id="link">Link</a><br />
<a href="#"  class="poplink" onclick="toggle('tst');return false;" id="link">Link2</a>

<div id="div1" class="pop" style="position: relative; margin: 0 auto; width: 800px; display: none; border: dotted;">
	<div style="width: 800px; height: 35px; background-color: #000;"></div><br /><br /><br /><br />
<input id="tst"  class="pop" type="button" name="" value="TEST" />

</div>
</body>

</html>