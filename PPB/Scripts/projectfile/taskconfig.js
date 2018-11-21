var taskObj = { Id: -1, AreaName: '', SectionName: '', StationName: '', TaskName: '',  TaskInstance: -1, ModelAffiliation: '' };

function bindTaskConfigEvent() {
    $(document).on('click', '.task-cancel-btn', function () {
        $('.editTaskConfigDiv').hide();
    });

    $(document).on('click', '.viewAction', function () {
        viewTaskInstances(this);
    });

    $(document).on('click', '.editAction', function () {
        editTaskConfig(this);
        $('.editTaskConfigDiv').show();
    });

    $(document).on('click', '.task-config-int-array', function () {
        editIntArrayTaskConfig(this);
    });

    $(document).on('click', '.task-config-bool-array', function () {
        editBoolArrayTaskConfig(this);
    });

    $(document).on('click', '.task-save-btn', function () {
        disableCurrentControl(this);
        saveTaskConfig();
    });

    $(document).on('click', '.array-property-save-btn', function () {
        disableCurrentControl(this);
        saveIntArrayTaskConfig();
    });

    $(document).on('click', '.bool-array-property-save-btn', function () {
        disableCurrentControl(this);
        saveBoolArrayTaskConfig();
    });

    $(document).on('click', '.task-download-btn', function () {
        disableCurrentControl(this);
        downloadTaskConfig();
    });

    $(document).on('click', '.task-upload-btn', function () {
        disableCurrentControl(this);
        uploadTaskConfig();
    });
}

function editTaskConfig(obj) {
    var rowObj = $(obj).closest('tr');
    taskObj.Id = $(rowObj).data('id');
    taskObj.AreaName = $(rowObj).data('area');
    taskObj.SectionName = $(rowObj).data('section');
    taskObj.StationName = $(rowObj).data('station');
    taskObj.TaskName = $(rowObj).data('task');
    taskObj.TaskInstance = $(rowObj).data('instance');
    taskObj.ModelAffiliation = $(rowObj).data('affiliation');

    var taskJsonStr = JSON.stringify(taskObj);
    $.ajax({
        url: '/ProjectFile/ReloadTaskConfig',
        data: { taskJsonStr: taskJsonStr },
        type: 'POST',
        success: function (data) {
            $('.editTaskConfigDiv').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Edit task config info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function editIntArrayTaskConfig(obj) {
    var iaMemberType = $(obj).data('type');
    var taskConfigObj = { AreaName: taskObj.AreaName, SectionName: taskObj.SectionName, StationName: taskObj.StationName, TaskName: taskObj.TaskName, TaskInstance: taskObj.TaskInstance, MemberName: $(obj).data('name'), DisplayName: $(obj).data('displayname') };
    var taskConfigJsonStr = JSON.stringify(taskConfigObj);
    $.ajax({
        url: '/ProjectFile/ReloadIntArrayTaskConfig',
        data: { taskConfigJsonStr: taskConfigJsonStr, memberType: iaMemberType },
        type: 'POST',
        success: function (data) {
            $('.array-property-edit-popup').modal('show');
            $('.array-property-edit-popup').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Edit int array task config info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function editBoolArrayTaskConfig(obj) {
    var iaMemberType = $(obj).data('type');
    var taskConfigObj = { AreaName: taskObj.AreaName, SectionName: taskObj.SectionName, StationName: taskObj.StationName, TaskName: taskObj.TaskName, TaskInstance: taskObj.TaskInstance, MemberName: $(obj).data('name'), DisplayName: $(obj).data('displayname') };
    var taskConfigJsonStr = JSON.stringify(taskConfigObj);
    $.ajax({
        url: '/ProjectFile/ReloadBoolArrayTaskConfig',
        data: { taskConfigJsonStr: taskConfigJsonStr, memberType: iaMemberType },
        type: 'POST',
        success: function (data) {
            $('.array-property-edit-popup').modal('show');
            $('.array-property-edit-popup').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Edit bool array task config info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function saveTaskConfig() {
    if (validateInputs()) {
        var taskConfig = getTaskConfigObj()
        var taskConfigJsonStr = JSON.stringify(taskConfig);
        $.ajax({
            url: '/ProjectFile/SaveTaskConfig',
            data: { taskConfigJsonStr: taskConfigJsonStr },
            type: 'POST',
            success: function (data) {
                if (data == 'success') {
                    reloadTaskInstancesDataTable();
                    $('.editTaskConfigDiv').hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Save task configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            }
        });
    }
    else {
        //to-do
    }
}

function getTaskConfigObj() {
    var taskConfigList = [];

    $('.task-config').each(function (index, item) {
        var taskCofig = {};
        if ($(item).prop('type') == 'checkbox') {
            var memValue = 'False';
            if ($(item).prop('checked')) {
                memValue = 'True';
            }
            taskCofig = { AreaName: taskObj.AreaName, SectionName: taskObj.SectionName, StationName: taskObj.StationName, TaskName: taskObj.TaskName, Id: $(item).data('configid'), MemberName: $(item).data('name'), MemberValue: memValue, MemberType: $(item).data('type'), BaseTag: $(item).data('basetag'), TaskInstance: taskObj.TaskInstance };
        }
        else {
            taskCofig = { AreaName: taskObj.AreaName, SectionName: taskObj.SectionName, StationName: taskObj.StationName, TaskName: taskObj.TaskName, Id: $(item).data('configid'), MemberName: $(item).data('name'), MemberValue: $(item).val(), MemberType: $(item).data('type'), BaseTag: $(item).data('basetag'), TaskInstance: taskObj.TaskInstance };
        }
        taskConfigList.push(taskCofig);
    });
    return taskConfigList;
}

function downloadTaskConfig() {
    var tasks = getSelectedTaskObj()
    var taskJsonStr = JSON.stringify(tasks);
    $.ajax({
        url: '/ProjectFile/DownloadTaskConfig',
        data: { taskJsonStr: taskJsonStr },
        type: 'POST',
        success: function (data) {
            if (data == 'success') {
                alert("Download selected tasks successfully.");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Download task configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function uploadTaskConfig() {
    var tasks = getSelectedTaskObj()
    var taskJsonStr = JSON.stringify(tasks);
    $.ajax({
        url: '/ProjectFile/UploadTaskConfig',
        data: { taskJsonStr: taskJsonStr },
        type: 'POST',
        success: function (data) {
            if (data == 'success') {
                alert("Upload selected tasks successfully.");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Upload task configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function getSelectedTaskObj() {
    var taskList = [];

    $('#TasksDataTbl input:checked,#TaskInstancesDataTbl input:checked').each(function (index, item) {
        var rowObj = $(item).closest('tr');
        var id = $(rowObj).data('id');
        var area = $(rowObj).data('area');
        var section = $(rowObj).data('section');
        var station = $(rowObj).data('station');
        var task = $(rowObj).data('task');
        var instance = $(rowObj).data('instance');
        var taskObj = { Id: id, AreaName: area, SectionName: section, StationName: station, TaskName: task, TaskInstance: instance };
        taskList.push(taskObj);
    });
    return taskList;
}

function viewTaskInstances(obj) {
    var rowObj = $(obj).closest('tr');
    taskObj.Id = $(rowObj).data('id'); //set the global variable
    reloadTaskInstancesDataTable();
}

function reloadTaskInstancesDataTable() {
    var taskJsonStr = JSON.stringify(taskObj);
    $.ajax({
        url: '/ProjectFile/ReloadTaskInstancesDataTable',
        data: { taskJsonStr: taskJsonStr },
        type: 'POST',
        success: function (data) {
            $('.admin-content').html(data);
            initCommonDataTable('#TaskInstancesDataTbl');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("View task instances info error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
        }
    });
}

function saveIntArrayTaskConfig() {
    if (validateInputs()) {
        var taskConfig = getIntArrayTaskConfigObj()
        var taskConfigJsonStr = JSON.stringify(taskConfig);
        $.ajax({
            url: '/ProjectFile/SaveTaskConfig',
            data: { taskConfigJsonStr: taskConfigJsonStr },
            type: 'POST',
            success: function (data) {
                if (data == 'success') {
                    $('.array-property-edit-popup').modal('hide');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Save int array task configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            }
        });
    }
    else {
        //to-do
    }
}

function getIntArrayTaskConfigObj() {
    var taskConfigList = [];

    $('.int-array-task-config').each(function (index, item) {
        var taskConfig = { Id: $(item).data('configid'), MemberValue: $(item).val() };
        taskConfigList.push(taskConfig);
    });
    return taskConfigList;
}

function saveBoolArrayTaskConfig() {
    if (validateInputs()) {
        var taskConfig = getBoolArrayTaskConfigObj()
        var taskConfigJsonStr = JSON.stringify(taskConfig);
        $.ajax({
            url: '/ProjectFile/SaveTaskConfig',
            data: { taskConfigJsonStr: taskConfigJsonStr },
            type: 'POST',
            success: function (data) {
                if (data == 'success') {
                    $('.array-property-edit-popup').modal('hide');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Save bool array task configuration error! status:" + XMLHttpRequest.status + " readyState:" + XMLHttpRequest.readyState + " textStatus:" + textStatus);
            }
        });
    }
    else {
        //to-do
    }
}

function getBoolArrayTaskConfigObj() {
    var taskConfigList = [];
    //calculate the final value based on the checkbox
    var len = $('.bool-array-task-config').length;
    var memValue = 0;
    var configId = 0;
    $('.bool-array-task-config').each(function (index, item) {
        if ($(item).prop('checked')) {
            if (index == len - 1) {
                memValue -= 1 << index; //Math.pow(2, index);
            }
            else {
                memValue += 1 << index; // Math.pow(2, index);
            }
        }
        configId = $(item).data('configid');
    });
    var taskConfig = { Id: configId, MemberValue: memValue };
    taskConfigList.push(taskConfig);
    return taskConfigList;
}

function validateInputs() {
    var valResult = true;

    //to-do....

    return valResult;
}