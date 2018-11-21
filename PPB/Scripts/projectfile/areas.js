function bindAreasEvent() {
    $(document).on('click', '.viewAction', function () {
        viewAreas(this);
    });
}

function bindAreaConfigEvent() {
    $(document).on('click', '.viewAction', function () {
        editAreaConfig(this);
    });
}

function viewAreas(obj) {
    var rowObj = $(obj).closest('tr');
    areaName = $(rowObj).data('area');

    var areaObj = { AreaName: areaName };

    var areaJsonStr = JSON.stringify(areaObj);
    $.ajax({
        url: "/ProjectFile/ReloadAreaConfigDataTable",
        data: { AreaJsonStr: areaJsonStr },
        type: "POST",
        success: function (data) {
            $('.admin-content').html(data);
            initCommonDataTable('#AreaConfigDataTbl');
            bindAreaConfigEvent();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("View areas info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function editAreaConfig(obj) {

}