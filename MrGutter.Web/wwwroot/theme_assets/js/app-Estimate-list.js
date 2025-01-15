/**
 * Page User List
 */

'use strict';

//Datatable Custom
$(document).ready(function () {
    $(".datatables-estimate").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        'ordering': true,
        'searching': true,
        'info': true,
        "ajax": {
            "url": "/estimates/estimateData",
            "type": "POST",
            "datatype": "json"
        },
        "dom": '<"top"<"right-section">>rt<"bottom"ilp><"clear">',
        "columns": [
            { "data": "est_no", "name": "no.", "autoWidth": true },
            { "data": "est_name", "name": "name", "autoWidth": true },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "salesperson", "name": "estimator", "autoWidth": true },
            { "data": "created", "name": "created", "autoWidth": true },
            { "data": "status", "name": "status", "autoWidth": true }
        ]
    });
});



