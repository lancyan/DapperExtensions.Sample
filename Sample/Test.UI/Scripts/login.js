document.onkeydown = function (e) {
    e = e || window.event;
    if (e.keyCode == 13 || e.which == 13) {
        var src = e.srcElement || e.target;
        var val = src.value.trim();
        if (val.length > 0) {
            var arr = document.getElementsByTagName("INPUT");
            for (var i = 0, len = arr.length; i < len; i++) {
                var item = arr[i];
                if (src == item) {
                    if (i < len - 1) {
                        arr[i + 1].focus();
                        break;
                    }
                }
            }
        }
        //if (e && e.preventDefault) {
        //    e.preventDefault();

        //} else {
        //    e.returnValue = false;
        //}
    }
}
function setTDMessage(o, msg) {
    var td = o.parentNode;
    var tr = td.parentNode;
    var count = 0;
    for (var i = 0; i < tr.childNodes.length; i++) {
        if (tr.childNodes[i].nodeName == "TD") {
            count++;
        }
    }
    if (count > 2) {
        var nextTD = td.nextSibling;
        while (nextTD != null && nextTD.nodeName != "TD") {
            nextTD = nextTD.nextSibling;
        }
        if (nextTD != null) {
            nextTD.style.color = "red";
            nextTD.style.paddingLeft = "5px";
            nextTD.innerHTML = msg ? msg : "";
        }
    }
    else {
        var nextSPAN = o.nextSibling;
        while (nextSPAN != null && nextSPAN.nodeName != "SPAN") {
            nextSPAN = nextSPAN.nextSibling;
        }
        if (nextSPAN != null) {
            nextSPAN.style.color = "red";
            nextSPAN.style.marginLeft = "5px";
            nextSPAN.innerHTML = msg ? msg : "";
        }
    }
    //o.focus();
}

$(window).resize(function () {
    $("#myTab").css({
        position: "absolute",
        left: ($(window).width() - $("#myTab").outerWidth()) / 2,
        top: ($(window).height() - $("#myTab").outerHeight()) / 2
    });
});
$(function () {
    $(window).resize();
    document.getElementById("txtUserName").focus();
    var un = localData.get("checkedUserName");
    var isChecked = localData.get("userNameChecked");
    if (un != null && un != undefined && un != "") {
        document.getElementById("txtUserName").value = un;
        document.getElementById("txtPassword").focus();
    }
    if (isChecked != null && isChecked != undefined && isChecked != "") {
        document.getElementById("chxCheckedMe").checked = isChecked == 1;
    }
});

function btnLogin() {
    var txtUN = document.getElementById("txtUserName");
    var txtPWD = document.getElementById("txtPassword");
    var txtValidateCode = document.getElementById("txtValidateCode");
    var un = txtUN.value.trim();
    var pwd = txtPWD.value;
    var validateCode = txtValidateCode.value.trim();
    if (un.length == 0) {
        setTDMessage(txtUN, "用户名不能为空");
        return;
    }
    if (pwd.length == 0) {
        setTDMessage(txtPWD, "密码不能为空");
        return;
    }
    //if (validateCode.length == 0) {
    //    setTDMessage(txtValidateCode, "验证码不能为空");
    //    return;
    //}
    $.ajax({
        type: "post",
        url: "/home/Login",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: "{un:'" + un + "',pwd:'" + pwd + "',validateCode:'" + validateCode + "'}",
        complete: function (xmlReq, textStatus) {
            getValidateCode();
        },
        success: function (res) {
            var flag = res.d ? res.d : res;
            if (flag == 1) {
                setTDMessage(txtUN, "用户名不能为空");
            }
            else if (flag == 2) {
                setTDMessage(txtPWD, "密码不能为空");
            }
            else if (flag == 3) {
                setTDMessage(txtValidateCode, "验证码不能为空");
            }
            else if (flag == 4) {
                setTDMessage(txtValidateCode, "验证码不正确");
            }
            else if (flag == 5) {
                setTDMessage(txtPWD, "用户名或密码不正确");
            }
            else {
                if (document.getElementById("chxCheckedMe").checked) {
                    localData.set("checkedUserName", un);
                    localData.set("userNameChecked", 1);
                }
                else {
                    localData.set("userNameChecked", 0);
                    localData.set("checkedUserName", "");
                }
                window.location.href = "/main/index";
            }
        },
        error: function (xmlReq, err, textStatus) {
            alert("error:" + err);
        }
    });
}

function getValidateCode() {
    document.getElementById("imgValidate").onclick();
}

function btnRegist() {
    window.location.href = "Home/Register";
}

////防止页面后退
//history.pushState(null, null, document.URL);
//window.addEventListener('popstate', function () {
//    history.pushState(null, null, document.URL);
//});