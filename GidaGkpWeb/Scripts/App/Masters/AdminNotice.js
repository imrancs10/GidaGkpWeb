$(document).ready(function () {
    FillNoticeType();
    FillDepartmentNotice();

    function FillNoticeType(selectedNoticeTypeID = null) {
        let dropdown = $('#NoticeType');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: 0,lookupType: "NoticeType" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedNoticeTypeID != null) {
                    dropdown.val(selectedNoticeTypeID);
                }
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    }

    $('#NoticeType').on('change', function (e) {
        var selectedText = $("#NoticeType option:selected").text();
        if (selectedText == 'Highlight Notice') {
            $('#divDepartment').hide();
            $('#divNoticeDate').hide();
        }
        else if (selectedText == 'Latest Notice') {
            $('#divDepartment').hide();
            $('#divNoticeDate').hide();
            $('#divNewTag').hide();
        }
        else {
            $('#divDepartment').show();
            $('#divNoticeDate').show();
            $('#divNewTag').show();
        }
    });

    function FillDepartmentNotice(selectedDepartmentNoticeID = null) {
        let dropdown = $('#DepartmentNotice');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{lookupTypeId: 0,lookupType: "DepartmentNotice" }',
            url: '/Masters/GetLookupDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.LookupId).text(entry.LookupName));
                });
                if (selectedDepartmentNoticeID != null) {
                    dropdown.val(selectedDepartmentNoticeID);
                }
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    }
})