var initializeWatchBrandsList = function (elementId) {
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