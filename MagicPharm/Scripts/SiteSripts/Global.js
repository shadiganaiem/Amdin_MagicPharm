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