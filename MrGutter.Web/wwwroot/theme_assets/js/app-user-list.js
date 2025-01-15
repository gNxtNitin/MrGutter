$(document).ready(function () {
    $("#userDatatable").DataTable({

        "processing": true,
        "serverSide": true,
        "filter": true,
        'ordering': true,
        'searching': true,
        'info': true,
        "ajax": {
            "url": "/userManager/userList",
            "type": "POST",
            "datatype": "json"
        },
        "dom": '<"top"l<"right-section"fB>>rt<"bottom"ip><"clear">',

     
        "columns": [
     
            { "data": "userName", "name": "userName", "autoWidth": true },
            { "data": "email", "name": "email", "autoWidth": true },
            { "data": "mobile", "name": "mobile", "autoWidth": true },
            { "data": "userType", "name": "userType", "autoWidth": true },
            { "data": "userStatus", "name": "userStatus", "autoWidth": true },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                      <a class="text-danger" style="cursor: pointer;" title="Edit User" onclick="openEditModal1(${row.userID})">
                           <i class='fa-solid fa-pen-to-square text-danger'></i>
                      </a>
                       <a href="#" class="text-danger" style="cursor: pointer;" title="Delete User" onclick="deleteUserRow(${row.userID})">
                           <i class='ti ti-trash text-danger'></i>
                      </a>
                  

                    `;
                }
            },
            //{
            //    "data": "",
            //    "name": "checkbox",
            //    "autoWidth": true,
            //    "render": function (data, type, row) {
            //        return `<input type="checkbox" class="select-row" data-user-id="${row.userID}" />`;
            //    },
            //    "orderable": false,
            //    "searchable": false
            //},
           
        ],
               buttons: [
                    
            {
                       text: '<i class="ti ti-plus me-0 me-sm-1 ti-xs"></i><span class="d-none d-sm-inline-block me-2">Add New User</span>',
                className: 'add-new btn btn-primary waves-effect waves-light me-3',
                attr: {
                    'data-bs-toggle': 'offcanvas',
                    'data-bs-target': '#offcanvasAddUser'
                }
        }
      

        ],
    });


});


const selectedUsers = new Set();

    // Handle individual row selection
    $('#userDatatable').on('change', '.select-row', function () {
        const userID = $(this).data('user-id');
        if (this.checked) {
            selectedUsers.add(userID);
        } else {
            selectedUsers.delete(userID);
        }
        toggleBatchActions();
    });

    // Handle "Select All" functionality
    $('#selectAll').on('change', function () {
        const isChecked = this.checked;
        $('.select-row').each(function () {
            this.checked = isChecked;
            const userID = $(this).data('user-id');
            if (isChecked) {
                selectedUsers.add(userID);
            } else {
                selectedUsers.delete(userID);
            }
        });
        toggleBatchActions();
    });

    // Enable or disable batch actions based on selection
    function toggleBatchActions() {
        if (selectedUsers.size > 0) {
            $('#batchActions').removeClass('d-none');
        } else {
            $('#batchActions').addClass('d-none');
        }
    }


//Row deletion
//function deleteUserRow(userID) {
//    // Confirm the deletion with the user
//    if (confirm("Are you sure you want to delete this user?")) {
//        // Perform the AJAX request to delete the user
//        $.ajax({
//            url: '/userManager/DeleteUser',
//            method: 'POST',
//            data: { userID: userID },
//            success: function (response) {
//                if (response.success) {
//                    // User deleted successfully, reload the DataTable
//                    $('#userDatatable').DataTable().ajax.reload();
//                    alert("User deleted successfully.");
//                    location.reload();

//                } else {
//                    // If there was an issue deleting the user
//                    alert("User deleted successfully.");
//                    location.reload();                }
//            },
//            error: function (xhr, status, error) {
//                // If there was an error with the AJAX request
//                alert("User deleted successfully.");
//                location.reload();            }
//        });
//    }
//}

function deleteUserRow(userID) {
    $('#delete').modal('show');

    $('#delete .confirm-delete').data('user-id', userID);
}

$('#delete .confirm-delete').on('click', function () {
    var userID = $(this).data('user-id'); 

    $.ajax({
        url: '/userManager/DeleteUser', 
        method: 'POST',
        data: { userID: userID },  
        success: function (response) {
            console.log("Response from server:", response); 

            if (response.success) {
                
                $('#delete').modal('hide');
                $('#userDatatable').DataTable().ajax.reload();
                alert("User deleted successfully.");
            } else {
                $('#delete').modal('hide');
                $('#userDatatable').DataTable().ajax.reload();
                
            }
        },
        error: function (xhr, status, error) {
            // Log the error for debugging
            //console.error("Error with AJAX request:", error);
            //alert("Error: Unable to delete the user.");
            $('#delete').modal('hide');
            $('#userDatatable').DataTable().ajax.reload();
           
        }
    });
});


function openEditModal1(userID) {
    //alert("inside user edit")
    $.ajax({
        url: '/UserManager/EditUser?userID=' + userID,
        method: 'GET',
        success: function (response) {
            // Populate other modal fields
            $('#editModal #editUserID').val(response.userID);
            $('#editModal #fName').val(response.firstName);
            $('#editModal #lName').val(response.lastName);
            $('#editModal #editEmail').val(response.email);
            $('#editModal #editPhone').val(response.mobile);
            $('#editModal #editStatus').val(response.userStatus);
            $('#editModal #editIsActive').val(response.isActive);

            // Populate roles dropdown
            const roleDropdown = $('#editModal #editRole');
            roleDropdown.empty(); 

            if (response.roles && response.roles.length > 0) {
                response.roles.forEach(role => {
                    const isSelected = role.roleID.toString() === response.roleID.toString();
                    const option = `<option value="${role.roleID}" ${isSelected ? 'selected' : ''}>${role.roleName}</option>`;
                    roleDropdown.append(option);
                });
            } else {
                roleDropdown.append('<option value="">No roles available</option>');
            }

            $('#editModal').modal('show');
        },
        error: function (xhr, status, error) {
            alert("Error: " + error);
        }
    });
}


function UserViewEdit(userID) {
    alert("inside UserView")
    debugger

    $.ajax({
 
        url: '/UserManager/UserView?userID=' + userID,
        method: 'GET',
        success: function (response) {
            alert("response : " + response);
            // Populate other modal fields
            $('#userView #editUserID').val(response.userID);
            $('#userView #fName').val(response.firstName);
            $('#userView #lName').val(response.lastName);
            $('#userView #editEmail').val(response.email);
            $('#userView #editPhone').val(response.mobile);
            $('#userView #editStatus').val(response.userStatus);
            $('#userView #editIsActive').val(response.isActive);

            // Populate roles dropdown
            const roleDropdown = $('#userView #editRole');
            roleDropdown.empty();

            if (response.roles && response.roles.length > 0) {
                response.roles.forEach(role => {
                    const isSelected = role.roleID.toString() === response.roleID.toString();
                    const option = `<option value="${role.roleID}" ${isSelected ? 'selected' : ''}>${role.roleName}</option>`;
                    roleDropdown.append(option);
                });
            } else {
                roleDropdown.append('<option value="">No roles available</option>');
            }

            $('#userView').modal('show');
        },
        error: function (xhr, status, error) {
            alert("Error: " + error);
        }
    });
}


function saveChanges1() {
    
    var selectedRoleID = $('#editModal #editRole').val();      
    var selectedRoleName = $('#editModal #editRole option:selected').text(); 

    var companyId = $('#editModal #companyId').val();      
    var userData = {
        userID: $('#editModal #editUserID').val(),
        firstName: $('#editModal #fName').val(),
        lastName: $('#editModal #lName').val(),
        email: $('#editModal #editEmail').val(),
        mobile: $('#editModal #editPhone').val(),
        userType: selectedRoleName, 
        roleID: selectedRoleID,
        userStatus: $('#editModal #editStatus').val(),
        isActive: $('#editModal #editIsActive').val(),
        companyId: companyId
    };
   
    //alert("User data is: " + JSON.stringify(userData)); 

    $.ajax({
        url: '/userManager/EditUser',
        method: 'POST',
        data: userData,
        success: function (response) {
            if (response.success) {
                $('#editModal').modal('hide');
                $('#userDatatable').DataTable().ajax.reload();
                location.reload();
               
            } else {
                //alert('Error updating user.' + response.error);
                $('#editModal').modal('hide');
                $('#userDatatable').DataTable().ajax.reload();
                location.reload();
            }
          
        },
        error: function (xhr, status, error) {
            //alert("Error" + error)
            $('#editModal').modal('hide');
            $('#userDatatable').DataTable().ajax.reload();
            location.reload();
        }
    });
}
