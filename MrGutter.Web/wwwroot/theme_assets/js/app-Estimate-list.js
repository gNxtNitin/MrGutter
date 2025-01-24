function initializeEstimateTable(estimatorId) {
    var table = $(".datatables-estimate").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ordering": true,
        "searching": true,
        "info": true,
        "ajax": {
            "url": "/estimates/estimateData",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                // Send estimatorId to the server
                d.estimatorId = estimatorId;
            }
        },
        "columns": [
            { "data": "est_no", "name": "estimate#", "autoWidth": true },
            { "data": "est_name", "name": "name", "autoWidth": true },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "salesperson", "name": "estimator", "autoWidth": true },
            { "data": "created", "name": "created", "autoWidth": true },
            { "data": "status", "name": "status", "autoWidth": true }
        ],
        "order": [[2, 'desc']],
        "dom": '<"row"' +
            '<"col-md-10"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-end flex-md-row flex-column mb-6 mb-md-0 mt-n6 mt-md-0">>' +
            '>t' +
            '<"row"' +
            '<"col-sm-12 col-md-6"i>' +
            '<"col-sm-12 col-md-6 d-flex align-items-center justify-content-end"lp>' +
            '>'
    });
    $('.searchInputDataTable').on('keyup', function () {
        debugger
        var searchTerm = this.value.toLowerCase(); 
        table.search(searchTerm).draw();  
    });
}
