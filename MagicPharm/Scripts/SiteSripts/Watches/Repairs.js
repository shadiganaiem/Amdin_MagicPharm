$(document).ready(function () {
    InitializeRepairsTable()
});

function GetRepairsPartial() {
    var modal = document.getElementById("OptionsModal");
    $.ajax({
        type: 'GET',
        url: '/Watches/AddRepair',
        success: function (data) {
            $('#popup_options').html(data);
            $('#OptionsModal').fadeIn();
        }
    });
    window.onclick = function (event) {
        if (event.target == modal)
            $('#OptionsModal').fadeOut();
    }
}

function EditRepair(id) {
    var modal = document.getElementById("OptionsModal");
    $.ajax({
        type: 'GET',
        url: '/Watches/EditRepair',
        data: { repairId: id },
        success: function (data) {
            $('#popup_options').html(data);
            $('#OptionsModal').fadeIn();
        }
    });
    window.onclick = function (event) {
        if (event.target == modal)
            $('#OptionsModal').fadeOut();
    }
}

function InitializeRepairsTable() {
    $('#repairsTable').DataTable().destroy();
    $('#repairsTable').DataTable({
        "ajax": {
            "url": "/Watches/GetAllRepairs",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "ClientName" },
            { "data": "ClientPhone" },
            { "data": "WatchBrand" },
            { "data": "WatchBarcode" },
            {
                "data": "CreationDate", "render": function (prop) {
                    var date = new Date(parseInt(prop.substr(6)));
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + month + "/" + date.getFullYear()
                }
            },
            {
                "data": "EndDate", "render": function (prop) {
                    if (prop == null)
                        return "";
                    var date = new Date(parseInt(prop.substr(6)));
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + month + "/" + date.getFullYear()
                }
            },
            {
                "data": "ReceiptDate", "render": function (prop) {
                    if (prop == null)
                        return "";
                    var date = new Date(parseInt(prop.substr(6)));
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + month + "/" + date.getFullYear()
                }
            },
            { "data": "Description" },
            { "data": "Status" },
            {
                "data": "ID", "render": function (data) {
                    return '<button type="button" class="btn-blue" onclick="EditRepair(' + data + ')">עריכה</button>';
                }
            }
        ],
        "pagingType": "numbers",
        "fnDrawCallback": function () {
            var pages = document.getElementsByClassName('paginate_button');
            if (pages.length === 1) {
                $('.dataTables_paginate')[0].style.display = "none";
            }
        },
        "language": {
            "search": "חיפוש כללי ",
            "sLengthMenu": "מציג _MENU_ רשומות"
        }
    });
}