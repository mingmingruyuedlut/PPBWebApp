var areaName = "";

//Area page and partial
function bindAreasEvent() {
    $(document).on('click', '.viewAction', function () {
        viewAreas(this);
    });
}

function viewAreas(obj) {
    var rowObj = $(obj).closest('tr');
    areaName = $(rowObj).data('area'); //set the global variable
    reloadAreaConfigDataTable();
}

function reloadAreaConfigDataTable() {
    var areaObj = { AreaName: areaName };
    var areaJsonStr = JSON.stringify(areaObj);
    $.ajax({
        url: "/ProjectFile/ReloadAreaConfigDataTable",
        data: { AreaJsonStr: areaJsonStr },
        type: "POST",
        success: function (data) {
            $('.admin-content').html(data);
            initCommonDataTable('#AreaConfigDataTbl');
            $('.ac-add-btn-box').show();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("View areas info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}



//Area config and partial
function bindAreaConfigEvent() {

    $(document).on('click', '.editAction', function () {
        editAreaConfig(this);
    });

    $(document).on('click', '.deleteAction', function () {
        deleteAreaConfig(this);
    });

    $(document).on('click', '.ac-add-btn', function () {
        addAreaConfig(this);
    });

    $(document).on('click', '.ac-save-btn', function () {
        saveAreaConfig();
    });

    $(document).on('click', '.ac-cancel-btn', function () {
        cancelAreaConfig();
    });
}

function editAreaConfig(obj) {
    var rowObj = $(obj).closest('tr');
    $('.ac-model-name').data('id', $(rowObj).data('id'));
    $('.ac-model-name').data('mnumber', $(rowObj).data('mnumber'));
    $('.ac-model-name').val($(rowObj).data('mname'));
    //$('.ac-model-display').text($(rowObj).data('display-name'));
    $('.ac-model-number').text($(rowObj).data('mnumber'));
    $('.addAreaConfigDiv').show();
}

function deleteAreaConfig(obj) {
    var rowObj = $(obj).closest('tr');
    var acObj = { Id: $(rowObj).data('id'), AreaName: areaName, ModelNumber: $(rowObj).data('mnumber'), ModelName: $(rowObj).data('mname') };
    var acObjJsonStr = JSON.stringify(acObj);
    $.ajax({
        url: '/ProjectFile/DeleteAreaConfig',
        data: { AreaConfigJsonStr: acObjJsonStr },
        type: 'POST',
        success: function (data) {
            if (data == 'success') {
                reloadAreaConfigDataTable();
                clearAllValues();
                $('.addAreaConfigDiv').hide();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Delete area config info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function addAreaConfig(obj) {
    clearAllValues();
    $('.addAreaConfigDiv').show();
    //ajax to get the max model number
    $.ajax({
        url: '/ProjectFile/GetAreaConfigNextModelNumber',
        data: { },
        type: 'POST',
        success: function (data) {
            //update the ui
            //$('.ac-model-display').text(data + '-Model ID:');
            $('.ac-model-number').text(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Get area config next model number error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function cancelAreaConfig(obj) {
    clearAllValues();
    $('.addAreaConfigDiv').hide();
}

function saveAreaConfig() {
    if (validateInputs()) {
        var acItem = $('.ac-model-name');
        var acObj = { Id: $(acItem).data('id'), AreaName: areaName, ModelNumber: $(acItem).data('mnumber'), ModelName: $(acItem).val() };
        var acObjJsonStr = JSON.stringify(acObj);
        $.ajax({
            url: '/ProjectFile/SaveAreaConfig',
            data: { AreaConfigJsonStr: acObjJsonStr},
            type: 'POST',
            success: function (data) {
                if (data == 'success') {
                    reloadAreaConfigDataTable();
                    clearAllValues();
                    $('.addAreaConfigDiv').hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Save area config info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            }
        });
    }
    else {
        //to-do
    }
}

function validateInputs() {
    var valResult = true;

    var acModelName = $('.ac-model-name').val();

    if (acModelName.Trim() == "") {
        valResult = false;
        alert("Please input model name.");
    }

    return valResult;
}

function clearAllValues() {
    $('.ac-model-name').data('id', 0);
    $('.ac-model-name').data('mnumber', 0);
    $('.ac-model-name').val('');
    $('.ac-model-name').val('');
}