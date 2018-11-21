var areaName = '';
var sectionName = '';
var stationName = '';
var stationInstance = '';

function bindStationConfigEvent() {
    $(document).on('click', '.station-cancel-btn', function () {
        $('.editStationConfigDiv').hide();
    });

    $(document).on('click', '.editAction', function () {
        editStationConfig(this);
        $('.editStationConfigDiv').show();
    });

    $(document).on('click', '.station-save-btn', function () {
        disableCurrentControl(this);
        saveStationConfig();
    });

    $(document).on('click', '.station-config-int-array', function () {
        editIntArrayStationConfig(this);
    });

    $(document).on('click', '.array-property-save-btn', function () {
        disableCurrentControl(this);
        saveIntArrayStationConfig();
    });

    $(document).on('click', '.station-download-btn', function () {
        disableCurrentControl(this);
        downloadStationConfig();
    });

    $(document).on('click', '.station-upload-btn', function () {
        disableCurrentControl(this);
        uploadStationConfig();
    });
}

function saveStationConfig() {
    if (validateInputs()) {
        var stationConfig = getStationConfigObj()
        var stationConfigJsonStr = JSON.stringify(stationConfig);
        $.ajax({
            url: "/ProjectFile/SaveStationConfig",
            data: { StationConfigJsonStr: stationConfigJsonStr },
            type: "POST",
            success: function (data) {
                if (data == 'success') {
                    reloadStationConfigDataTablePartial();
                    $('.editStationConfigDiv').hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Save station configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            }
        });
    }
    else {
        //to-do
    }
}

function validateInputs() {
    var valResult = true;

    //to-do....

    return valResult;
}

function getStationConfigObj() {
    var stationConfigList = [];

    $('.station-config').each(function (index, item) {
        var stationCofig = {};
        if ($(item).prop('type') == 'checkbox') {
            var memValue = 'False';
            if ($(item).prop('checked')) {
                memValue = 'True';
            }
            stationCofig = { AreaName: areaName, SectionName: sectionName, StationName: stationName, Id: $(item).data('configid'), MemberName: $(item).data('name'), MemberValue: memValue, MemberType: $(item).data('type'), BaseTag: $(item).data('basetag'), StationInstance: stationInstance };
        }
        else {
            stationCofig = { AreaName: areaName, SectionName: sectionName, StationName: stationName, Id: $(item).data('configid'), MemberName: $(item).data('name'), MemberValue: $(item).val(), MemberType: $(item).data('type'), BaseTag: $(item).data('basetag'), StationInstance: stationInstance };
        }
        stationConfigList.push(stationCofig);
    });
    return stationConfigList;
}

function reloadStationConfigDataTablePartial() {
    $.ajax({
        url: "/ProjectFile/ReloadStationConfigDataTable",
        data: {},
        type: "POST",
        success: function (data) {
            $('.admin-content').html(data);
            initCommonDataTable('#StationConfigDataTbl');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Reload station config datatable error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function editStationConfig(obj) {
    var rowObj = $(obj).closest('tr');
    areaName = $(rowObj).data('area');
    sectionName = $(rowObj).data('section');
    stationName = $(rowObj).data('station');
    stationInstance = $(rowObj).data('instance');

    var stationObj = { AreaName: areaName, SectionName: sectionName, StationName: stationName, StationInstance: stationInstance };

    var stationJsonStr = JSON.stringify(stationObj);
    $.ajax({
        url: "/ProjectFile/ReloadStationConfig",
        data: { StationJsonStr: stationJsonStr },
        type: "POST",
        success: function (data) {
            $('.editStationConfigDiv').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Edit station config info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function editIntArrayStationConfig(obj) {
    var iaMemberType = $(obj).data('type');
    var stationConfigObj = { AreaName: areaName, SectionName: sectionName, StationName: stationName, StationInstance: stationInstance, MemberName: $(obj).data('name'), DisplayName: $(obj).data('displayname') };
    var stationConfigJsonStr = JSON.stringify(stationConfigObj);
    $.ajax({
        url: '/ProjectFile/ReloadIntArrayStationConfig',
        data: { StationConfigJsonStr: stationConfigJsonStr, MemberType: iaMemberType },
        type: 'POST',
        success: function (data) {
            $('.array-property-edit-popup').modal('show');
            $('.array-property-edit-popup').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Edit int array station config info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function saveIntArrayStationConfig() {
    if (validateInputs()) {
        var stationConfig = getIntArrayStationConfigObj()
        var stationConfigJsonStr = JSON.stringify(stationConfig);
        $.ajax({
            url: "/ProjectFile/SaveStationConfig",
            data: { StationConfigJsonStr: stationConfigJsonStr },
            type: "POST",
            success: function (data) {
                if (data == 'success') {
                    $('.array-property-edit-popup').modal('hide');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Save int array station configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            }
        });
    }
    else {
        //to-do
    }
}

function getIntArrayStationConfigObj() {
    var stationConfigList = [];

    $('.int-array-station-config').each(function (index, item) {
        var stationConfig = { Id: $(item).data('configid'), MemberValue: $(item).val() };
        stationConfigList.push(stationConfig);
    });
    return stationConfigList;
}

function downloadStationConfig() {
    var stations = getSelectedStationObj()
    var stationJsonStr = JSON.stringify(stations);
    $.ajax({
        url: '/ProjectFile/DownloadStationConfig',
        data: { StationJsonStr: stationJsonStr },
        type: 'POST',
        success: function (data) {
            if (data == 'success') {
                alert("Download selected stations successfully.");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Download station configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function uploadStationConfig() {
    var stations = getSelectedStationObj()
    var stationJsonStr = JSON.stringify(stations);
    $.ajax({
        url: '/ProjectFile/UploadStationConfig',
        data: { stationJsonStr: stationJsonStr },
        type: 'POST',
        success: function (data) {
            if (data == 'success') {
                alert("Upload selected stations successfully.");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Upload station configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function getSelectedStationObj() {
    var stationList = [];

    $('#StationConfigDataTbl input:checked').each(function (index, item) {
        var rowObj = $(item).closest('tr');
        var id = $(rowObj).data('id');
        var area = $(rowObj).data('area');
        var section = $(rowObj).data('section');
        var station = $(rowObj).data('station');
        var instance = $(rowObj).data('instance');
        var stationObj = { Id: id, AreaName: area, SectionName: section, StationName: station, StationInstance: instance };
        stationList.push(stationObj);
    });
    return stationList;
}

