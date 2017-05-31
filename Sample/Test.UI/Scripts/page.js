var pageIndex = 0;
var pageSize = 20;
var recordCount = 0;
var pageCount = 0;

//初始化分页
function initPage() {
    pageCount = Math.ceil(recordCount / pageSize);
    document.getElementById("RecordCount").innerHTML = recordCount;
    document.getElementById("CurrentPageIndex").innerHTML = pageIndex + 1;
    document.getElementById("PageCount").innerHTML = pageCount;
    if (document.getElementById("strongRecord") != null) {
        document.getElementById("strongRecord").innerHTML = recordCount;
    }
    if (pageCount == 1) {
        document.getElementById("btnFirst").disabled = true;
        document.getElementById("btnNext").disabled = true;
        document.getElementById("btnBack").disabled = true;
        document.getElementById("btnEnd").disabled = true;
    }
    if (pageCount == pageIndex + 1) {
        document.getElementById("btnNext").disabled = true;
        document.getElementById("btnEnd").disabled = true;
    }
    else {
        document.getElementById("btnNext").disabled = false;
        document.getElementById("btnBack").disabled = false;
        document.getElementById("btnFirst").disabled = false;
        document.getElementById("btnEnd").disabled = false;
    }
    var obj = document.getElementById("SelectPage");
    if (obj) {
        obj.length = 0;
        for (var j = 1; j <= pageCount; j++) {
            var item = document.createElement("option");
            item.value = j;
            item.text = j;
            obj.options.add(item);
        }
    }
}

function setPage() {
    document.getElementById("CurrentPageIndex").innerHTML = pageIndex + 1;
    if (document.getElementById("SelectPage")) {
        if (document.getElementById("SelectPage").options[pageIndex] != null) {
            document.getElementById("SelectPage").options[pageIndex].selected = true;
        }
    }
    window.parent.pageIndex = pageIndex;
}

//首页
function goFirst() {
    document.getElementById("btnEnd").disabled = false;
    document.getElementById("btnNext").disabled = false;
    document.getElementById("btnBack").disabled = false;
    pageIndex = 0;
    document.getElementById("btnBack").disabled = true;
    document.getElementById("btnFirst").disabled = true;
    initTable();
    setPage();
}
//上一页
function goBack() {
    document.getElementById("btnNext").disabled = false;
    document.getElementById("btnBack").disabled = false;
    document.getElementById("btnEnd").disabled = false;
    if (pageIndex > 0) {
        pageIndex--;
    }
    if (pageIndex == 0) {
        document.getElementById("btnBack").disabled = true;
        document.getElementById("btnFirst").disabled = true;
    }
    initTable();
    setPage();
}
//下一页
function goNext() {
    document.getElementById("btnNext").disabled = false;
    document.getElementById("btnBack").disabled = false;
    document.getElementById("btnFirst").disabled = false;
    if (recordCount != 0) {
        if (pageIndex < pageCount - 1) {
            pageIndex++;
        }
        if (pageIndex == pageCount - 1) {
            document.getElementById("btnNext").disabled = true;
            document.getElementById("btnEnd").disabled = true;
        }
    }
    initTable();
    setPage();
}
//尾页
function goEnd() {
    document.getElementById("btnFirst").disabled = false;
    document.getElementById("btnNext").disabled = false;
    document.getElementById("btnBack").disabled = false;
    pageIndex = pageCount - 1;
    document.getElementById("btnNext").disabled = true;
    document.getElementById("btnEnd").disabled = true;
    initTable();
    setPage();
    
}
//跳到某一页
function gotoPage(index) {
    document.getElementById("btnFirst").disabled = false;
    document.getElementById("btnEnd").disabled = false;
    document.getElementById("btnBack").disabled = false;
    document.getElementById("btnNext").disabled = false;
    pageIndex = index;
    if (pageIndex == 0) {
        document.getElementById("btnBack").disabled = true;
        document.getElementById("btnFirst").disabled = true;
    }
    if (pageIndex == pageCount - 1) {
        document.getElementById("btnNext").disabled = true;
        document.getElementById("btnEnd").disabled = true;
    }
    initTable();
    setPage();
}


