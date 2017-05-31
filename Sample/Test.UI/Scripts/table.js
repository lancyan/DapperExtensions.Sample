//自定义表格创建类
function MyTable(tableName, selectedName) {
    this.tabName = tableName;
    this.bindArr = [];
    this.tabHeadTemplate = "";
    this.tabBodyTemplate = [];
    this.bindObj = {};
    this.orderBy = "";
    this.selectName = selectedName;

    //初始化table模板
    this.initTabTemplate = function () {
        var tab = document.getElementById(this.tabName);
        if (tab) {
            var tabHead = tab.rows[0];
            var tabBody = tab.rows[1];
            this.tabHeadTemplate = tabHead.innerHTML;
            for (var i = 0, len = tabHead.cells.length; i < len; i++) {
                var name = tabHead.cells[i].getAttribute("bind")
                if (name) {
                    this.bindObj[name] = tabHead.cells[i].innerHTML;
                    this.bindArr.push(name);
                }
                this.tabBodyTemplate.push(tabBody.cells[i].innerHTML);
            }
            //调用common.js
            initSelect(this.selectName, this.bindObj);
        }
    }

    //自动创建表格
    this.createTable = function (data) {
        var tab = document.getElementById(this.tabName);
        var divTab = tab.parentNode;
        var arr = [], r1 = /\{(.+?)\}/g, r2 = /<.+?>/, r3 = /\((.+?)\)/;
        arr.push('<table id="' + tab.id + '" class="' + tab.className + '" style="' + tab.style.cssText + '">');
        arr.push("<tr>" + this.tabHeadTemplate + "</tr>");
        for (var i = 0, l1 = data.length; i < l1; i++) {
            var item = data[i];
            arr.push("<tr>");
            for (var j = 0, l2 = this.tabBodyTemplate.length; j < l2; j++) {
                var cellHtml = this.tabBodyTemplate[j];
                while (r1.exec(cellHtml)) {
                    var itemValue = item[RegExp.$1];
                    if (itemValue == 0) {
                    }
                    else if (!itemValue) {
                        itemValue = '';
                    }
                    cellHtml = cellHtml.replace(RegExp.lastMatch, itemValue);
                }
                if (r2.test(cellHtml)) {
                    arr.push('<td>' + cellHtml + '</td>');
                }
                else if (r3.test(cellHtml)) {
                    arr.push('<td>' + eval(cellHtml) + '</td>');
                }
                else {
                    arr.push('<td>' + (cellHtml.length > 50 ? (cellHtml.substr(0, 40) + ".....") : cellHtml) + '</td>');
                }
            }
            arr.push("</tr>");
        }
        arr.push("</table>");

        divTab.innerHTML = arr.join('');
      
        setTR(this.tabName);
    }



    //设置table的TR的颜色和点击事件
    function setTR(tbName) {
        tab = document.getElementById(tbName);
        for (var j = 0, count = tab.rows.length; j < count; j++) {
            var row = tab.rows[j];
            if (j == 0) {
                row.onclick = function (event) {
                    var obj = event.srcElement ? event.srcElement : event.target;
                    var bindName = obj.getAttribute("bind");
                    if (this.orderBy && this.orderBy.indexOf('desc') != -1) {
                        this.orderBy = bindName + " asc";
                    }
                    else {
                        this.orderBy = bindName + " desc";
                    }
                    if (bindName) {
                        //调用页面的初始化表格方法
                        initTable();
                    }
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
                    if (window.cur) {
                        window.cur.style.backgroundColor = '#FFFFFF';
                    }
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

}

