$(document).ready(function () {
    $('#watchOrdersTable').DataTable({
        "ajax": {
            "url": "/Watches/GetAllWatchesOrders",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "ID" },
            { "data": "ClientName" },
            { "data": "ClientPhone" },
            { "data": "WatchBrand" },
            { "data": "WatchCatalogNumber" },
            {
                "data": "CreationDate", "render": function (prop) {
                    var date = new Date(parseInt(prop.substr(6)));
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + month + "/" + date.getFullYear()
                }
            },
            {
                "data": "GuaranteeDate", "render": function (prop) {
                    var date = new Date(parseInt(prop.substr(6)));
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + month + "/" + date.getFullYear()
                }
            },
            {
                "data": "ID", "render": function (data) {
                    return '<button type="button" class="btn-blue" onclick="AddWatchOrder()">פרטי לקוח</button>';
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

function AddWatchOrder() {
    var modal = document.getElementById("OptionsModal");
    $.ajax({
        type: 'GET',
        url: '/Watches/AddOrder',
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