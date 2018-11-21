var areaName = '';
var sectionName = '';
var stationName = '';
var plcName = '';

function bindPlcConfigEvent() {
    $(document).on('click', '.plc-cancel-btn', function () {
        $('.editPlcConfigDiv').hide();
    });

    $(document).on('click', '.editAction', function () {
        editPlcConfig(this);
        $('.editPlcConfigDiv').show();
    });

    $(document).on('click', '.plc-save-btn', function () {
        savePlcConfig();
    });
}

function savePlcConfig() {
    if (validateInputs()) {
        var plcConfig = getPlcConfigObj()
        var plcConfigJsonStr = JSON.stringify(plcConfig);
        $.ajax({
            url: "/ProjectFile/SavePlcConfig",
            data: { PlcConfigJsonStr: plcConfigJsonStr },
            type: "POST",
            success: function (data) {
                if (data == 'success') {
                    reloadPlcConfigDataTablePartial();
                    $('.editPlcConfigDiv').hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Save plc configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            }
        });
    }
    else {
        //to-do
    }
}

function validateInputs() {
    var valResult = true;

    var addressFirst = $('.address-first').val();
    var addressSecond = $('.address-second').val();
    var addressThird = $('.address-third').val();
    var addressFourth = $('.address-fourth').val();
    var addressSlot = $('.address-slot').val();

    if (addressFirst.Trim() == '' || !IsNumber(addressFirst.Trim())) {
        valResult = false;
        alert("Please input right format address first part");
    }
    else if (addressSecond.Trim() == '' || !IsNumber(addressSecond.Trim())) {
        valResult = false;
        alert("Please input right format address second part");
    }
    else if (addressThird.Trim() == '' || !IsNumber(addressThird.Trim())) {
        valResult = false;
        alert("Please input right format address third part");
    }
    else if (addressFourth.Trim() == '' || !IsNumber(addressFourth.Trim())) {
        valResult = false;
        alert("Please input right format address fourth part");
    }
    else if (addressSlot.Trim() == '' || !IsNumber(addressSlot.Trim())) {
        valResult = false;
        alert("Please input right format address slot");
    }

    return valResult;
}

function getPlcConfigObj() {
    var plcConfigList = [];
    var plcConfig = { AreaName: areaName, SectionName: sectionName, StationName: stationName, PlcName: plcName, Id: $('.plc-type').data('configid'), MemberName: $('.plc-type').data('name'), MemberValue: $('.plc-type').text()};
    plcConfigList.push(plcConfig);

    $('.plc-config').each(function (index, item) {
        if ($(item).prop('type') == 'checkbox') {
            var memValue = 'False';
            if ($(item).prop('checked')) {
                memValue = 'True';
            }
            plcConfig = { AreaName: areaName, SectionName: sectionName, StationName: stationName, PlcName: plcName, Id: $(item).data('configid'), MemberName: $(item).data('name'), MemberValue: memValue };
        }
        else {
            plcConfig = { AreaName: areaName, SectionName: sectionName, StationName: stationName, PlcName: plcName, Id: $(item).data('configid'), MemberName: $(item).data('name'), MemberValue: $(item).val() };
        }
        plcConfigList.push(plcConfig);
    });
    return plcConfigList;
}

function reloadPlcConfigDataTablePartial() {
    $.ajax({
        url: "/ProjectFile/ReloadPlcConfigDataTable",
        data: {},
        type: "POST",
        success: function (data) {
            $('.admin-content').html(data);
            initCommonDataTable('#PlcConfigDataTbl');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Reload plc config datatable error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function editPlcConfig(obj) {
    var rowObj = $(obj).closest('tr');
    areaName = $(rowObj).data('area');
    sectionName = $(rowObj).data('section');
    stationName = $(rowObj).data('station');
    plcName = '_' + $(rowObj).data('plc'); //why add "_" as prefix ??????  It's just for VB.NET version.

    var plcHierObj = { AreaName: areaName, SectionName: sectionName, StationName: stationName, PlcName: plcName };

    var plcHierJsonStr = JSON.stringify(plcHierObj);
    $.ajax({
        url: "/ProjectFile/ReloadPlcConfig",
        data: { PlcHierJsonStr: plcHierJsonStr },
        type: "POST",
        success: function (data) {
            $('.editPlcConfigDiv').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Edit plc config info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}