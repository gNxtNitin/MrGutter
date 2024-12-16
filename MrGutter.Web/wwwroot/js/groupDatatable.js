$(document).ready(function () {
    $("#groupDatatable").DataTable({
        "dom": 'Blfrtip',
        "buttons": [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "processing": true,
        "serverSide": true,
        "filter": true,
       
        'ordering': true,
        'searching': true,
        'info': true,
        
        //"lengthMenu": [[50, 100, - 1], [50, 100, "All"]],
       // "pageLength": 50,

        "ajax": {
            "url": "/home/getGroup",
            "type": "POST",
            "datatype": "json"
        },
        
        
        //"columnDefs": [{
        //    "targets": [0],
        //    "visible": false,
        //    "searchable": false
        //}],
        "columns": [
            { "data": "groupID", "name": "GroupID", "autoWidth": true },
            { "data": "groupName", "name": "GroupName", "autoWidth": true },
            { "data": "description", "name": "Description", "autoWidth": true },
            //{ "data": "contact", "name": "Country", "autoWidth": true },
            //{ "data": "email", "name": "Email", "autoWidth": true },
            //{ "data": "dateOfBirth", "name": "Date Of Birth", "autoWidth": true },
            {
                "render": function (data, row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id + "'); >Delete</a>"; }
            },
        ]
    });
  
});