
var menuArr = [];

$(function () {




    $.ajax({
        type: "get",
        url: "/Menus/GetLeftMenus",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (res) {
            var data = typeof res == "string" ? eval('(' + res + ')') : res;
            menuArr = data;
            InitLeftMenu();
            tabClose();
            tabCloseEven();

        },
        error: function (xmlReq, err, c) {
            //debugger;
            alert("error:" + err);
        }
    });




    $('#editpass').click(function () {
        $('#w').window('open');
    });
    $('#btnCancel').click(function () {
        $('#w').window('close');
    });
    $('#btnEp').click(function () {
        serverLogin();
    });

    $.messager.defaults = { ok: "是", cancel: "否" };

    $('#loginOut').click(function () {
        $.messager.confirm('系统提示', '确定退出本次登录吗?', function (r) {
            if (r) {
                $.ajax({
                    type: "post",
                    url: "/Main/Logout",
                    contentType: "application/json;charset=utf-8", //发送的是json格式的数据
                    //dataType: "json", //客户端收到的是json格式的数据
                    //complete: function (xmlReq, textStatus) {
                    //    delCookie("checkedUserName");
                    //    delCookie("checkedPassword");
                    //},
                    success: function (res) {
                        var data = typeof res == "string" ? eval('(' + res + ')') : res;
                        if (1 == data) {
                            delCookie("checkedUserName");
                            delCookie("checkedPassword");
                            window.location.href = "/Home/index";
                        }
                    },
                    error: function (xmlReq, err, c) {
                        alert("error:" + err);
                    }
                });
            }
        });
    });

    //$('#tabs').tabs({
    //    onSelect: function (title) {
    //        var currTab = $('#tabs').tabs('getTab', title);
    //        var iframe = $(currTab.panel('options').content);
    //        var src = iframe.attr('src');
    //        if (src) {
    //            $('#tabs').tabs('update', { tab: currTab, options: { content: createFrame(src) } });
    //        }
    //    }
    //});

});
function delCookie(name) {
    var dt = new Date();
    dt.setTime(dt.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + dt.toGMTString();
}
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null)
        return unescape(arr[2]);
    return null;
}
//设置登录窗口
function openPwd() {
    $('#w').window({
        title: '修改密码',
        width: 300,
        modal: true,
        shadow: true,
        closed: true,
        height: 160,
        resizable: false
    });
}

//修改密码
function serverLogin() {
    var oldpass = $("#txtOldPass").val();
    var newpass = $('#txtNewPass').val();
    var rePass = $('#txtRePass').val();

    if (newpass == '') {
        msgShow('系统提示', '请输入密码！', 'warning');
        return false;
    }
    if (rePass == '') {
        msgShow('系统提示', '请在一次输入密码！', 'warning');
        return false;
    }

    if (newpass != rePass) {
        msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
        return false;
    }

    $.ajax({
        type: "post",
        url: "/Main/UpdatePwd",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: "{pwd1:'" + oldpass + "',pwd2:'" + newpass + "'}",
        success: function (res) {
            var d1 = typeof res == "string" ? eval('(' + res + ')') : res;
            if (d1) {
                msgShow('系统提示', '恭喜，密码修改成功！', 'info', function () {
                    $.ajax({
                        type: "post",
                        url: "/Main/Logout",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            var d2 = typeof res == "string" ? eval('(' + res + ')') : res;
                            if (d2) {
                                delCookie("checkedUserName");
                                delCookie("checkedPassword");
                                window.location.href = "/Home/Index";
                            }
                        },
                        error: function (xmlReq, err, c) {
                            alert("error:" + err);
                        }
                    });
                });
                
            }
            else {
                msgShow('系统提示', '用户名或密码错误！', 'error');
            }
        },
        error: function (xmlReq, err, c) {
            alert("error:" + err);
        }
    });


}

//初始化左侧
function InitLeftMenu() {
    $("#nav").accordion({ animate: false });
    $.each(menuArr, function (i, n) {
        var menulist = '';
        menulist += '<ul>';
        if (n.children) {
            $.each(n.children, function (j, o) {
                menulist += '<li><div><a ref="' + o.id + '" href="#" rel="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.name + '</span></a></div></li> ';
            })
        }
        menulist += '</ul>';
        $('#nav').accordion('add', {
            title: n.name,
            content: menulist,
            iconCls: 'icon ' + n.icon
        });
    });

    $('.easyui-accordion li a').click(function () {
        var tabTitle = $(this).children('.nav').text();
        var url = $(this).attr("rel");
        var id = $(this).attr("ref");
        var icon = getIcon(id, icon);
        addTab(tabTitle, url, icon);
        $('.easyui-accordion li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });

    //选中第一个
    var panels = $('#nav').accordion('panels');
    if (panels && panels.length > 0) {
        $('#nav').accordion('select', panels[0].panel('options').title);
    }
}

//获取左侧导航的图标
function getIcon(menuid) {
    var icon = 'icon ';
    $.each(menuArr, function (i, n) {
        if (n.children) {
            $.each(n.children, function (j, o) {
                if (o.id == menuid) {
                    icon += o.icon;
                }
            })
        }
    });
    return icon;
}

function addTab(subtitle, url, icon) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            icon: icon
        });
    } else {
        $('#tabs').tabs('select', subtitle);
        $('#mm-tabupdate').click();
    }
    tabClose();
}

function createFrame(url) {
    return '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}

//绑定右键菜单事件
function tabCloseEven() {
    //刷新
    $('#mm-tabupdate').click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        })
    })
    //关闭当前
    $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    })
    //全部关闭
    $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother').click(function () {
        $('#mm-tabcloseright').click();
        $('#mm-tabcloseleft').click();
    });
    //关闭当前右侧的TAB
    $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            //msgShow('系统提示','后边没有啦~~','error');
            alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });

    //退出
    $("#mm-exit").click(function () {
        $('#mm').menu('hide');
    })
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType, fn) {
    $.messager.alert(title, msgString, msgType, function () { if(fn){
        fn();
    }});
}
