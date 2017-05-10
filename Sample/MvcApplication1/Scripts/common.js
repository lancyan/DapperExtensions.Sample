function queryString(fieldName) {
    var urlString = document.location.search.toLowerCase();
    fieldName = fieldName.toLowerCase();
    if (urlString != null) {
        var typeQu = fieldName + "=";
        if (urlString.indexOf('?' + typeQu) != -1 || urlString.indexOf('&' + typeQu) != -1) {
            var urlEnd = urlString.indexOf(typeQu);
            if (urlEnd != -1) {
                var paramsUrl = urlString.substring(urlEnd + typeQu.length);
                var isEnd = paramsUrl.indexOf('&');
                return isEnd == -1 ? paramsUrl : paramsUrl.substring(0, isEnd);
            }
        }
    }
    return null;
}
var localData = {
    hname: location.hostname ? location.hostname : 'localStatus',
    isLocalStorage: window.localStorage ? true : false,
    dataDom: null,

    initDom: function () { //初始化userData
        if (!this.dataDom) {
            try {
                this.dataDom = document.createElement('input'); //这里使用hidden的input元素
                this.dataDom.type = 'hidden';
                this.dataDom.style.display = "none";
                this.dataDom.addBehavior('#default#userData'); //这是userData的语法
                document.body.appendChild(this.dataDom);
                var exDate = new Date();
                exDate = exDate.getDate() + 30;
                this.dataDom.expires = exDate.toUTCString(); //设定过期时间
            } catch (ex) {
                return false;
            }
        }
        return true;
    },

    set: function (key, value) {
        if (this.isLocalStorage) {
            window.localStorage.setItem(key, value);
        } else {
            if (this.initDom()) {
                this.dataDom.load(this.hname);
                this.dataDom.setAttribute(key, value);
                this.dataDom.save(this.hname)
            }
        }
    },
    get: function (key) {
        if (this.isLocalStorage) {
            return window.localStorage.getItem(key);
        } else {
            if (this.initDom()) {
                this.dataDom.load(this.hname);
                return this.dataDom.getAttribute(key);
            }
        }
    },
    remove: function (key) {
        if (this.isLocalStorage) {
            localStorage.removeItem(key);
        } else {
            if (this.initDom()) {
                this.dataDom.load(this.hname);
                this.dataDom.removeAttribute(key);
                this.dataDom.save(this.hname)
            }
        }
    }
}
function getEvent(event) {
    var ev = event || window.event;
    if (!ev) {
        var c = this.getEvent.caller;
        while (c) {
            ev = c.arguments[0];
            if (ev && (Event == ev.constructor || MouseEvent == ev.constructor)) {
                break;
            }
            c = c.caller;
        }
    }
    return ev;
}

var setTR = function (tab) {
    for (var j = 0, count = tab.rows.length; j < count; j++) {
        var row = tab.rows[j];
        if (j == 0) {
            row.onclick = function (a) {
                orderBy = a.srcElement.getAttribute("bind");
                initTable();
            }
        }
        else {
            row.onclick = function () {
                if (window.cur) {
                    window.cur.style.backgroundColor = '#FFFFFF';
                }
                this.style.backgroundColor = '#C5EEFF';
                window.cur = this;
            }
            row.ondblclick = function () {
                if (window.cur)
                    window.cur.style.backgroundColor = '#FFFFFF';
                this.style.backgroundColor = '#C5EEFF';
                window.cur = this;
                $(this).find("a").each(function (a, b) {
                    if ($(b).text().trim() == "查看") {
                        $(b).trigger("click");
                        var url = b.href.toLowerCase();
                        if (url != "" && url.indexOf("javascript") == -1) {
                            window.location.href = b.href;
                        }
                    }
                });
            }
        }
    }
}

String.prototype.trim = function (s) {
    return this.replace(/(^\s*)|(\s*$)/g, '');
}
// 返回字符的长度，一个中文算2个
String.prototype.chineseLength = function () {
    return this.replace(/[^\x00-\xff]/g, "**").length;
}
// 判断字符串是否以指定的字符串结束
String.prototype.endsWith = function (str) {
    return this.substr(this.length - str.length) == str;
}

// 判断字符串是否以指定的字符串开始
String.prototype.startsWith = function (str) {
    return this.substr(0, str.length) == str;
}
String.prototype.trimChar = function () {
    var str = this.trim();
    var s1 = str.substr(0, 1);
    var s2 = str.substr(str.length - 1);
    if (s1 == s2 || s1 == '{' && s2 == '}' || s1 == '[' && s2 == ']' || s1 == '(' && s2 == ')') {
        return str.substr(1, str.length - 2);
    }
    return str;
}
String.prototype.format = function (args) {
    var result = this;
    var len = arguments.length;
    if (len > 0) {
        if (len == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("\\{" + i + "\\}", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < len; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("\\{" + i + "\\}", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}
Array.prototype.distinct = function () {
    var a = [], b = [];
    for (var prop in this) {
        var d = this[prop];
        if (d === a[prop])
            continue; //防止循环到prototype
        if (b[d] != 1) {
            a.push(d);
            b[d] = 1;
        }
    }
    return a;
}

function setCookie(name, value) {
    var Days = 30; 
    var exp = new Date(); 
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;

}
function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
function toggleLoading() {
    var objBg = document.getElementById('alert-background');
    var objloading = document.getElementById('alert-loading');
    if (objloading) {
        if (objloading.style.display == 'none') {
            objloading.style.display = '';
            objBg.style.display = '';
        }
        else {
            objloading.style.display = 'none';
            objBg.style.display = 'none';
        }
    }
    else {
        $('body').append('<div id="alert-loading" style="top:25%;border:1px solid #6CD0FF;color:#000;width:200px;height:50px; background-color:#f0ffff;margin: 0 auto;text-align: center;line-height:50px;">载入中......</div>');
        objBg.style.display = '';
    }
    window.setTimeout(function () {
        document.getElementById('alert-loading').style.display == 'none';
    }, 10000)
}

function initSelect(id, dict,flag) {
    var obj = document.getElementById(id);
    if (obj) {
        obj.length = 0;
        if (flag) {
            var option = document.createElement("option");
            option.text = "===请选择===";
            option.value = "0";
            obj.options.add(option);
        }
        if (isArray(dict)) {
            for (var i = 0; i < dict.length; i++) {
                var item = document.createElement("option");
                item.value = dict[i].value;
                item.text = dict[i].text;
                obj.options.add(item);
            }
        }
        else {
            for (var attr in dict) {
                var item = document.createElement("option");
                item.value = attr;
                item.text = dict[attr];
                obj.options.add(item);
            }
        }
    }
}
function isArray(obj) {
    return Object.prototype.toString.call(obj) === '[object Array]';
}

$(document).ready(function () {
    //这句话放最前面
    initBg();
});

(function () {
    
    var oldAlert = window.alert;
    var oldConfirm = window.confirm;

    window.alert = function (msg, callback) {

        var objModal = document.getElementById('alert-modal');
        var objBg = document.getElementById('alert-background');
        if (!objBg) {
            initBg();
        }
        if (!objModal) {
            initWin(msg, 1);
        }
        else {
            $(objModal).find('.tc').html(msg);
        }
        $(objModal).find("button").last().css("display", "none");
        $(objBg).show();
        $(objModal).show();

        initCallback(callback);
    }

    window.confirm = function (msg, callback) {
        var objModal = document.getElementById('alert-modal');
        var objBg = document.getElementById('alert-background');

        if (!objBg) {
            initBg();
        }
        if (!objModal) {
            initWin(msg, 0);
        }
        else {
            $(objModal).find('.tc').html(msg);
            $(objModal).find("button").last().css("display", "");
        }

        $(objBg).show();
        $(objModal).show();
        if (typeof callback === "function") {
            initCallback(callback);
        }
        else {
            var btns = $(objModal).find("button");
            btns[0].onclick = function () {
                setTimeout(xWinClose, 200);
            };
        }
    }

    function initCallback(callback) {
        var am = $('#alert-modal');
        var left = ($(window).width() - am.outerWidth()) / 2;
        var top = ($(window).height() - am.outerHeight()) / 2;
        am.css({ "left": left, "top": top, "display": "block" });
        var btns = $("#alert-modal").find("button");
        btns[0].onclick = function () {
            if (typeof callback === "function") {
                var cs = callback.toString();
                if (cs.indexOf('alert') == -1 && cs.indexOf('confirm') == -1) {
                    setTimeout(xWinClose, 200);
                }
                callback();
            }
            else {
                setTimeout(xWinClose, 200);
            }
        };
        btns[1].onclick = function () {
            xWinClose();
        };
    }

    function initWin(msg, isAlert) {
        var arr = [];
        arr.push('<div id="alert-modal" style="left: 0px; top: 0px; width: auto; display:none; position: fixed; z-index: 100000;">');
        arr.push('<div style="width:auto; min-width:280px;position:relative;width:auto;margin:10px;border:6px solid #6CD0FF;" >');
        arr.push('<div style="position:relative;background-color:#fff;-webkit-background-clip:padding-box;background-clip:padding-box;outline:0;">');
        arr.push('<div style="position:relative;padding:15px;">');
        arr.push('<p class="tc">' + msg + '</p>');
        arr.push('</div>');
        arr.push('<div style="padding:15px;text-align:right;border-top:1px solid #6CD0FF;">');
        arr.push('<button type="button" class="">确定</button>&nbsp;');
        if (isAlert) {
            arr.push('<button style="display:none;" type="button" class="" data-dismiss="modal">取消</button>');
        }
        else {
            arr.push('<button type="button" class="" data-dismiss="modal">取消</button>');
        }
        arr.push("</div>");
        arr.push("</div>");
        arr.push("</div>");

        $('body').append(arr.join(""));
    }

    function xWinClose() {
        $('#alert-modal').css("display", "none");
        $('#alert-background').css("display", "none");
    }

    $(window).resize(function () {
        var am = $("#alert-modal");
        am.css({
            left: ($(window).width() - am.outerWidth()) / 2,
            top: ($(window).height() - am.outerHeight()) / 2
        });
    });

})();

function initBg() {
    var objBg = document.getElementById('alert-background');
    if (!objBg) {
        $('body').append('<div id="alert-background" style="display:none;position: fixed;top: 0;right: 0;bottom: 0;left: 0; z-index: 99999;background-color: #000;opacity:0.3;filter: alpha(opacity=30);"></div>');
    }
}

//批量给表单赋值
function deserialize(obj, doc, flag) {
    var doc = doc || document;
    for (var attrName in obj) {
        var itemArr = doc.getElementsByName(attrName);
        for (var i = 0; i < itemArr.length; i++) {
            var item = itemArr[i];
            if (item) {
                var itemValue = obj[attrName];
                var type = item.type;
                var tagName = item.tagName;
                if (itemValue == null) {
                    itemValue = '';
                }
                else if (itemValue == '0001-01-01 00:00:00') {
                    itemValue = '';
                }
                if ((tagName == "INPUT" && item.type == "text") || tagName == "TEXTAREA" || (tagName == "INPUT" && item.type == "hidden")) {
                    item.value = itemValue;
                }
                else if (tagName == "SELECT") {
                    if (item.className.split(' ').indexOf("selectpicker") != -1) { //兼容bootstrap dropdown
                        $(item).selectpicker('val', itemValue);
                    }
                    else {
                        if (type == "select-one") {
                            for (var j = 0; j < item.options.length; j++) {
                                if (itemValue == item.options[j].value) {
                                    item.selectedIndex = j;
                                    $(item).trigger("change");
                                    break;
                                }
                            }
                        }
                    }
                }
                else if ((tagName == "INPUT" && item.type == "radio")) {
                    if (item.value == itemValue) {
                        item.checked = true;
                    }
                }
                else if ((tagName == "INPUT" && item.type == "checkbox")) {
                    if (typeof itemValue == 'string') {
                        var chxArr = itemValue.split(';')
                        if (chxArr.length > 1) {
                            if (chxArr.indexOf(item.value) != -1) {
                                item.checked = true;
                            }
                        }
                        else {
                            item.checked = itemValue ? true : false;
                        }
                    }
                    else {
                        item.checked = itemValue == 0 ? false : true;
                    }
                    //if ($(item).parent().hasClass('icheckbox'))
                    //{
                    //    if (itemValue) {
                    //        $(item).parent().addClass('checked');
                    //    }
                    //    else {
                    //        $(item).parent().removeClass('checked');
                    //    }
                    //}
                }
                else {
                    item.innerHTML = itemValue;
                }
            }
        }
    }
}

function serialize(doc, attach) {
    var doc = doc || document;
    var items1 = doc.getElementsByTagName("INPUT");
    var items2 = doc.getElementsByTagName("SELECT");
    var items3 = doc.getElementsByTagName("textarea");
    var obj = {};
    for (var i = 0, len = items1.length; i < len; i++) {
        var item = items1[i];
        var type = item.type;
        if (item.name && item.name != "__VIEWSTATE" && item.name != "__VIEWSTATEGENERATOR") {
            if (type == "text" || type == "hidden" || type == "password") {
                attachObj(item, obj);
            }
            else if ((type == "checkbox" || type == "radio") && item.checked == true) {
                attachObj(item, obj);
            }
        }
    }
    for (var i = 0, len = items2.length; i < len; i++) {
        attachObj(items2[i], obj);
    }
    for (var i = 0, len = items3.length; i < len; i++) {
        attachObj(items3[i], obj);
    }
    //======================
    var arr = [];
    for (var attr in obj) {
        arr.push(attr + ":'" + obj[attr] + "'");
    }
    if (attach) {
        for (var pro in attach) {
            arr.push(pro + ":'" + attach[pro] + "'")
        }
    }
    return "{" + arr.join(',') + "}";
}

function attachObj(item, obj) {
    var name = item.name;
    if (name) {
        if (obj[name]) {
            obj[name] = obj[name] + ";" + item.value;
        }
        else {
            obj[name] = item.value;
        }
    }
}

//disbale 表单
function disableForm(obj, doc, disableArr) {
    var doc = doc || document;
    for (var attrName in obj) {
        var itemArr = doc.getElementsByName(attrName);
        var item = itemArr.length == 1 && itemArr[0];
        if (disableArr) {
            for (var i = 0; i < disableArr.length; i++) {
                if (attrName == disableArr[i]) {
                    if (item.tagName == "SELECT" && item.className.split(' ').indexOf("selectpicker") != -1) {
                        $(item).prop('disabled', true);
                    }
                    else {
                        item.disabled = true;
                    }
                    break;
                }
            }
        }
        else {
            if (item) {
                if (item.tagName == "SELECT" && item.className.split(' ').indexOf("selectpicker") != -1) {
                    $(item).prop('disabled', true);
                }
                else {
                    item.disabled = true;
                }
            }
        }
    }
}

//阻止键盘回删事件
function StopKeyboard(e) {
    var ev = e || window.event;
    var src = ev.target || ev.srcElement;
    var b = (ev.keyCode || ev.which || ev.charCode) == 8;
    if (b) {
        window.event.keyCode = 0;
        window.event.returnValue = false;
        window.event.preventDefault();
        return false;
    }
}
 

