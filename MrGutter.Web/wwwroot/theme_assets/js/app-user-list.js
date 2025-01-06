$(document).ready(function () {
    $("#userDatatable").DataTable({
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
            { "data": "userName", "name": "userName", "autoWidth": true },
            { "data": "email", "name": "email", "autoWidth": true },
            { "data": "mobile", "name": "mobile", "autoWidth": true },
            { "data": "userType", "name": "userType", "autoWidth": true },
            { "data": "userStatus", "name": "userStatus", "autoWidth": true },
          
            //{ "data": "contact", "name": "Country", "autoWidth": true },
            //{ "data": "email", "name": "Email", "autoWidth": true },
            //{ "data": "dateOfBirth", "name": "Date Of Birth", "autoWidth": true },
            {
                /*   "render": function (data, row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id + "'); >Delete</a>"; }*/
                "data": null,
                "render": function (data, type, row) {
                    return `
                      <a data-bs-target="#editModal" data-bs-toggle="modal" data-bs-dismiss="modal" class="text-danger" style="cursor: pointer;">
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
            {
                "data": null,
                "name": "checkbox",
                "autoWidth": true,
                "render": function (data, type, row) {
                    return `<input type="checkbox" class="select-row" data-user-id="${row.userID}" />`;
                },
                "orderable": false,
                "searchable": false
            },
        ]
    });
});