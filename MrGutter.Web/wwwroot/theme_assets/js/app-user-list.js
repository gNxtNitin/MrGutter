$(document).ready(function () {
    $("#userDatatable").DataTable({
        "dom": 'Blfrtip',
        //"buttons": [
        //    'copy', 'csv', 'excel', 'pdf', 'print'
        //],
        "processing": true,
        "serverSide": true,
        "filter": true,
        'ordering': true,
        'searching': true,
        'info': true,
        //"lengthMenu": [[50, 100, - 1], [50, 100, "All"]],
        // "pageLength": 50,
        "ajax": {
            "url": "/userManager/userList",
            "type": "POST",
            "datatype": "json"
        },
        //"columnDefs": [{
        //    "targets": [0],
        //    "visible": false,
        //    "searchable": false
        //}],
        "columns": [
            { "data": "firstName", "name": "firstName", "autoWidth": true },
            { "data": "lastName", "name": "lastName", "autoWidth": true },
            { "data": "emailID", "name": "emailID", "autoWidth": true },
            //{ "data": "contact", "name": "Country", "autoWidth": true },
            //{ "data": "email", "name": "Email", "autoWidth": true },
            //{ "data": "dateOfBirth", "name": "Date Of Birth", "autoWidth": true },
            {
                /*   "render": function (data, row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id + "'); >Delete</a>"; }*/
                "data": null,
                "render": function (data, type, row) {
                    return `
                      <a href="/User/#?userID=${row.userID}" class="text-danger" style="cursor: pointer;">
                           <i class='fa-solid fa-pen-to-square text-danger'></i>
                      </a>
                       <a href="#" class="text-danger" style="cursor: pointer;">
                           <i class='fa-solid fa-shield-halved text-danger'></i>
                      </a>
                          <a href="#" class="text-danger" style="cursor: pointer;">
                           <i class='fa-regular fa-clipboard text-danger'></i>
                      </a>
        </a>
                `;
                }
            },
        ]
    });
});