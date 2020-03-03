var initializeWatchBrandsList = function (elementId, selectedObject = null) {
    var config = {
        allowClear: true,
        placeholder: 'בחר מותג',
        ajax: {
            url: "/Watches/GetWatchBrands",
            dataType: "json",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data, params) {
                return {
                    results: data
                };
            }
        }
    };
    $("#" + elementId).select2(config);

    if (selectedObject !== null) {
        var newOption = new Option(selectedObject.text, selectedObject.id, true, true);
        $('#' + elementId).append(newOption).trigger('change');
    }
}

function EditClientParial(id) {
    var modal = document.getElementById("OptionsModal");
    $.ajax({
        type: 'GET',
        url: '/Clients/Edit',
        data: { id: id },
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


//<input class="form-control inputStyle text-box single-line LightBlueFont text-right" style="padding:0;height: 26px !important;" type="text" name="DueDate" id="dateRange"
//    placeholder="בחר תאריך  ▼" data-default-date="" />

//$('#dateRange').daterangepicker({
//    "locale": {
//        "direction": "rtl",
//        "format": "DD/MM/YYYY",
//        "separator": " - ",
//        "applyLabel": "בחר",
//        "cancelLabel": "ביטול",
//        "customRangeLabel": "בחירת טווח"
//    },
//    "ranges": {
//        "היום": [
//            moment(),
//            moment()
//        ],
//        "אתמול": [
//            moment().subtract(1, 'days'),
//            moment().subtract(1, 'days')
//        ],
//        "7 ימים אחרונים": [
//            moment().subtract(6, 'days'),
//            moment()
//        ],
//        "30 ימים אחרונים": [
//            moment().subtract(29, 'days'),
//            moment()
//        ],
//        "החודש": [
//            moment().startOf('month'),
//            moment().endOf('month')
//        ],
//        "חודש שעבר": [
//            moment().subtract(1, 'month').startOf('month'),
//            moment().subtract(1, 'month').endOf('month')
//        ]
//    },
//    //"startDate": "02/10/2020",
//    //"endDate": "02/16/2020",
//    "opens": "center"
//}, function (start, end, label) {
//    console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
//});

//$('.ranges').css('direction', 'rtl');
//$('.ranges').css('text-align', 'right');

//$('#dateRange').val('');