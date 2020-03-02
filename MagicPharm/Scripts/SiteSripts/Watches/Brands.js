$(document).ready(function () {
    InitializeBrandsTable();
});

function GetAddBrandPartial() {
    var modal = document.getElementById("OptionsModal");
    $.ajax({
        type: 'GET',
        url: '/Watches/AddBrand',
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

function GetEditBrandPartial(id) {
    var modal = document.getElementById("OptionsModal");
    $.ajax({
        type: 'GET',
        url: '/Watches/EditBrand',
        data: { brandId: id },
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

function InitializeBrandsTable() {
    $('#brandsTable').DataTable().destroy();
    $('#brandsTable').DataTable({
        "ajax": {
            "url": "/Watches/GetAllBrands",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "ID" },
            { "data": "Name" },
            { "data": "Symbol" },
            { "data": "Barcode" },
            {
                "data": "ID", "render": function (data) {
                    return '<button type="button" class="btn-blue" onclick="GetEditBrandPartial(' + data + ')">עריכה</button>' +
                        '<button type="button" class="btn-blue" onclick="AddWatchOrder()" style="margin-right:10px">מחיקה</button>';
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