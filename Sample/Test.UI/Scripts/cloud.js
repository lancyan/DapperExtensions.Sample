(function () {
    var radius = 120;
    var dtr = Math.PI / 180;
    var dis = 300;
    var mcList = [];
    var active = false;
    var lasta = 1, lastb = 1;
    var distr = true;
    var speed = 2;
    var size = 250;
    var mouseX = 0, mouseY = 0;
    var howElliptical = 1;
    var aA = [];
    var oDiv = null;

    $(document).ready(function () {
        //var company = QueryString('companyID');
        //var url = "/Charts/GetE4";
        //$.get(url, { noticeType: 8, companyId: company }, function (result) {
        //    //showData(result);
        //});
        var result = [];
        showData(result);
    });

    function showData(result) {
        if (result == "[]")
            return;
        oDiv = document.getElementById('container');
        //var data = eval('(' + result + ')');
        //var dd = eval('(' + data[0].StatisticsValue + ')');
        var arr = [];
        arr.push({ "name": "你好", "value": 10, "href":"http://www.baidu.com" });
        arr.push({ "name": "谢 谢", "value": 11, "href": "http://www.baidu.com" });
        arr.push({ "name": "再见", "value": 12, "href": "http://www.baidu.com" });
        arr.push({ "name": "对不起", "value": 13, "href": "http://www.baidu.com" });
        arr.push({ "name": "see you", "value": 14, "href": "http://www.baidu.com" });
        arr.push({ "name": "yes", "value": 15 , "href":"http://www.baidu.com"});
        arr.push({ "name": "NO NO", "value": 101 , "href":"http://www.baidu.com"});
        arr.push({ "name": "OK", "value": 102 , "href":"http://www.baidu.com"});
        arr.push({ "name": "下次", "value": 103 , "href":"http://www.baidu.com"});
        arr.push({ "name": "你好吗", "value": 104 , "href":"http://www.baidu.com"});
        arr.push({ "name": "不好", "value": 105 , "href":"http://www.baidu.com"});
        arr.push({ "name": "看上去", "value": 106 , "href":"http://www.baidu.com"});
        arr.push({ "name": "咣", "value": 107 , "href":"http://www.baidu.com"});
        arr.push({ "name": "马云", "value": 108, "href":"http://www.baidu.com" });
        //for (var attribute in dd) {
        //    arr.push({ "name": attribute, "value": dd[attribute] });
        //}
        arr.sort(function (obj1, obj2) { return obj2.value - obj1.value; });
        for (var j = 0, len = arr.length; j < len; j++) {
            var aNode = document.createElement("A");
            aNode.innerHTML = arr[j].name;
            aNode.style.color = getRandomColor(j);
            aNode.style.background = "transparent";
            //aNode.style.backgroundColor = "#ffffff";
            aNode.href = arr[j].href;
            oDiv.appendChild(aNode);
            aA.push(aNode);
            mcList.push({ "offsetWidth": aNode.offsetWidth, "offsetHeight": aNode.offsetHeight });
        }
        sineCosine(0, 0, 0);
        positionAll();
        oDiv.onmouseover = function () {
            active = false;
        };
        oDiv.onmouseout = function () {
            active = true;
        };
        oDiv.onmousemove = function (ev) {
            var oEvent = window.event || ev;
            mouseX = oEvent.clientX - (oDiv.offsetLeft + oDiv.offsetWidth / 2);
            mouseY = oEvent.clientY - (oDiv.offsetTop + oDiv.offsetHeight / 2);
            mouseX /= 5;
            mouseY /= 5;
        };
        setInterval(update, 5);
    }
    function getRandomColor(index) {
        //var str = "#7F8084";
        //switch (index) {
        //    case 0:
        //        str = '#5491D8';
        //        break;
        //    case 1:
        //        str = '#E77338';
        //        break;
        //    case 2:
        //        str = '#EAC85D';
        //        break;
        //    case 3:
        //        str = '#61CE99';
        //        break;
        //    case 4:
        //        str = '#555555';
        //        break;
        //}
        //return str;
            try {
                //return '#' + Math.floor(Math.random() * 16777215).toString(16);
                //return '#' + (Math.random() * 0xffffff << 0).toString(16);
                return '#' + ('00000' + (Math.random() * 0x1000000 << 0).toString(16)).slice(-6);
            }
            catch (ex) {
                alert(ex.message);
            }
    }

    function update() {
        var a, b;
        if (active) {
            a = (-Math.min(Math.max(-mouseY, -size), size) / radius) * speed;
            b = (Math.min(Math.max(-mouseX, -size), size) / radius) * speed;
            //speed = speed * 0.99;

        }
        else {
            a = lasta * 0.98;
            b = lastb * 0.98;
        }
        lasta = a;
        lastb = b;
        if (Math.abs(a) <= 0.01 && Math.abs(b) <= 0.01) {
            return;
        }
        var c = 0;
        sineCosine(a, b, c);
        for (var j = 0; j < mcList.length; j++) {
            var rx1 = mcList[j].cx;
            var ry1 = mcList[j].cy * ca + mcList[j].cz * (-sa);
            var rz1 = mcList[j].cy * sa + mcList[j].cz * ca;
            var rx2 = rx1 * cb + rz1 * sb;
            var ry2 = ry1;
            var rz2 = rx1 * (-sb) + rz1 * cb;
            var rx3 = rx2 * cc + ry2 * (-sc);
            var ry3 = rx2 * sc + ry2 * cc;
            var rz3 = rz2;
            mcList[j].cx = rx3;
            mcList[j].cy = ry3;
            mcList[j].cz = rz3;
            per = dis / (dis + rz3);
            mcList[j].x = (howElliptical * rx3 * per) - (howElliptical * 2);
            mcList[j].y = ry3 * per;
            mcList[j].scale = per;
            //mcList[j].alpha = per;
            //mcList[j].alpha = (mcList[j].alpha - 0.6) * (10 / 6);
        }
        doPosition();
        depthSort();
    }

    function depthSort() {
        var aTmp = [];
        for (var i = 0, len = aA.length; i < len; i++) {
            aTmp.push(aA[i]);
        }
        aTmp.sort(
		function (vItem1, vItem2) {
		    if (vItem1.cz > vItem2.cz) {
		        return -1;
		    }
		    else if (vItem1.cz < vItem2.cz) {
		        return 1;
		    }
		    else {
		        return 0;
		    }
		});
        for (var i = 0, len = aTmp.length; i < len; i++) {
            aTmp[i].style.zIndex = i;
        }
    }

    function positionAll() {
        var phi = 0;
        var theta = 0;
        var aTmp = [];
        var oFragment = document.createDocumentFragment();
        //随机排序
        for (var i = 0, len = aA.length; i < len ; i++) {
            aTmp.push(aA[i]);
        }
        aTmp.sort(
		function () {
		    return Math.random() < 0.5 ? 1 : -1;
		});

        for (var i = 0, len = aTmp.length; i < len; i++) {
            oFragment.appendChild(aTmp[i]);
        }
        oDiv.appendChild(oFragment);
        for (var i = 1, max = mcList.length; i < max + 1; i++) {
            if (distr) {
                phi = Math.acos(-1 + (2 * i - 1) / max);
                theta = Math.sqrt(max * Math.PI) * phi;
            }
            else {
                phi = Math.random() * (Math.PI);
                theta = Math.random() * (2 * Math.PI);
            }
            //坐标变换
            mcList[i - 1].cx = radius * Math.cos(theta) * Math.sin(phi);
            mcList[i - 1].cy = radius * Math.sin(theta) * Math.sin(phi);
            mcList[i - 1].cz = radius * Math.cos(phi);
            aA[i - 1].style.left = mcList[i - 1].cx + oDiv.offsetWidth / 2 - mcList[i - 1].offsetWidth / 2 + 'px';
            aA[i - 1].style.top = mcList[i - 1].cy + oDiv.offsetHeight / 2 - mcList[i - 1].offsetHeight / 2 + 'px';
        }
    }

    function doPosition() {
        var l = oDiv.offsetWidth / 2;
        var t = oDiv.offsetHeight / 2;
        var aaa = [40, 34, 30, 26, 22];
        for (var i = 0; i < mcList.length; i++) {
            aA[i].style.left = mcList[i].cx + l - mcList[i].offsetWidth / 2 + 'px';
            aA[i].style.top = mcList[i].cy + t - mcList[i].offsetHeight / 2 + 'px';
            aA[i].style.fontSize = i < aaa.length ? aaa[i] + "px" : "20px";
            //Math.ceil(12 * mcList[i].scale / 2) + 8 + 'px';
            //aA[i].style.filter = "alpha(opacity=" + 100 * mcList[i].alpha + ")";
            aA[i].style.opacity = mcList[i].alpha;
        }
    }

    function sineCosine(a, b, c) {
        sa = Math.sin(a * dtr);
        ca = Math.cos(a * dtr);
        sb = Math.sin(b * dtr);
        cb = Math.cos(b * dtr);
        sc = Math.sin(c * dtr);
        cc = Math.cos(c * dtr);
    }
})();

