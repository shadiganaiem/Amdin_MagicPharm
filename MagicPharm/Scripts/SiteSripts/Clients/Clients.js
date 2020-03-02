$(document).ready(function () {
    $('#clientsTable').DataTable({
        "ajax": {
            "url": "/Clients/GetAllClients",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "ID" },
            { "data": "FullName" },
            { "data": "Phone" },
            { "data": "TelePhone" },
            {
                "data": "LastOrderdate", "render": function (prop) {
                    if (prop != null) {
                        var date = new Date(parseInt(prop.substr(6)));
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + month + "/" + date.getFullYear()
                    }
                    else return "";
                }
            },
            {
                "data": "CreationDate", "render": function (prop) {
                    var date = new Date(parseInt(prop.substr(6)));
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + month + "/" + date.getFullYear()
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

});