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
                           <i class='fa-solid fa-pen-to-square text-primary'></i>
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


//function openEditModal1(userID) {
//    //alert("inside user edit")
//    $.ajax({
//        url: '/UserManager/EditUser?userID=' + userID,
//        method: 'GET',
//        success: function (response) {
//            // Populate other modal fields
//            $('#editModal #editUserID').val(response.userID);
//            $('#editModal #fName').val(response.firstName);
//            $('#editModal #lName').val(response.lastName);
//            $('#editModal #editEmail').val(response.email);
//            $('#editModal #editPhone').val(response.mobile);
//            $('#editModal #editStatus').val(response.userStatus);
//            $('#editModal #editIsActive').val(response.isActive);

//            // Populate roles dropdown
//            const roleDropdown = $('#editModal #editRole');
//            roleDropdown.empty();

//            if (response.roles && response.roles.length > 0) {
//                response.roles.forEach(role => {
//                    const isSelected = role.roleID.toString() === response.roleID.toString();
//                    const option = `<option value="${role.roleID}" ${isSelected ? 'selected' : ''}>${role.roleName}</option>`;
//                    roleDropdown.append(option);
//                });
//            } else {
//                roleDropdown.append('<option value="">No roles available</option>');
//            }

//            $('#editModal').modal('show');
//        },
//        error: function (xhr, status, error) {
//            alert("Error: " + error);
//        }
//    });
//}

//With check box
//function openEditModal1(userID) {
//    $.ajax({
//        url: '/UserManager/EditUser?userID=' + userID,
//        method: 'GET',
//        success: function (response) {
//            alert("response is :" + response); // Log full response to debug

//            if (!response) {
//                alert('No response received.');
//                return;
//            }

//            // Populate user data
//            $('#editModal #editUserID').val(response.userID);
//            $('#editModal #fName').val(response.firstName);
//            $('#editModal #lName').val(response.lastName);
//            $('#editModal #editEmail').val(response.email);
//            $('#editModal #editPhone').val(response.mobile);
//            $('#editModal #editStatus').val(response.userStatus);
//            $('#editModal #editIsActive').val(response.isActive);

//            // Populate roles checkboxes
//            const roleCheckboxContainer = $('#editModal #editRoleCheckbox');
//            roleCheckboxContainer.empty(); // Clear any previous checkboxes

//            if (response.roles && response.roles.length > 0) {
//                response.roles.forEach(role => {
//                    const isChecked = response.roleID === role.roleID;

//                    console.log(`Creating checkbox for: ${role.roleName}, Checked: ${isChecked}`);

//                    const checkbox = `
//                        <div class="form-check form-check-inline">
//                            <input type="checkbox" id="role_${role.roleID}" name="roleID[]" value="${role.roleID}" class="form-check-input" ${isChecked ? 'checked' : ''}>
//                            <label for="role_${role.roleID}" class="form-check-label">${role.roleName}</label>
//                        </div>
//                    `;
//                    roleCheckboxContainer.append(checkbox); // Add each checkbox to the container
//                });
//            } else {
//                roleCheckboxContainer.append('<div>No roles available</div>');
//            }

//            // Populate company checkboxes
//            const companyCheckboxContainer = $('#editModal #editCompanyCheckbox');
//            companyCheckboxContainer.empty(); // Clear any previous checkboxes

//            if (response.companies && response.companies.length > 0) {
//                response.companies.forEach(company => {
//                    const isChecked = response.companyId === company.companyId;

//                    console.log(`Creating checkbox for: ${company.companyName}, Checked: ${isChecked}`);

//                    const checkbox = `
//                        <div class="form-check form-check-inline">
//                            <input type="checkbox" id="company_${company.companyId}" name="companyId[]" value="${company.companyId}" class="form-check-input" ${isChecked ? 'checked' : ''}>
//                            <label for="company_${company.companyId}" class="form-check-label">${company.companyName}</label>
//                        </div>
//                    `;
//                    companyCheckboxContainer.append(checkbox); // Add each checkbox to the container
//                });
//            } else {
//                companyCheckboxContainer.append('<div>No companies available</div>');
//            }

//            // Show the modal
//            $('#editModal').modal('show');
//        },
//        error: function (xhr, status, error) {
//            alert("Error: " + error);
//        }
//    });
//}

function openEditModal1(userID) {
    $.ajax({
        url: '/UserManager/EditUser?userID=' + userID,
        method: 'GET',
        success: function (response) {
            if (!response) {
                alert('No response received.');
                return;
            }

            // Populate user data
            $('#editModal #editUserID').val(response.userID);
            $('#editModal #fName').val(response.firstName);
            $('#editModal #lName').val(response.lastName);
            $('#editModal #editEmail').val(response.email);
            $('#editModal #editPhone').val(response.mobile);
            $('#editModal #editStatus').val(response.userStatus);
            $('#editModal #editIsActive').prop('checked', response.isActive); 

            const roleCheckboxContainer = $('#editModal #editRoleCheckbox');
            roleCheckboxContainer.empty(); 
            if (response.roleList && response.roleList.length > 0) {
                response.roleList.forEach(role => {
                    const isChecked = role.isActive === true; 
                    const checkbox = `
                        <div class="form-check form-check-inline">
                            <input type="checkbox" id="role_${role.roleID}" name="roleList[]" value="${role.roleID}" class="form-check-input" ${isChecked ? 'checked' : ''}>
                            <label for="role_${role.roleID}" class="form-check-label">${role.roleName}</label>
                        </div>
                    `;
                    // Check the checkbox if the role is active
                   
                    roleCheckboxContainer.append(checkbox); 
                });
            } else {
                roleCheckboxContainer.append('<div>No roles available</div>');
            }

            const companyCheckboxContainer = $('#editModal #editCompanyCheckbox');
            companyCheckboxContainer.empty(); 

            if (response.companyList && response.companyList.length > 0) {
                response.companyList.forEach(company => {
                
                    const isChecked = response.companyList.some(userCompany => userCompany.isActive === company.isActive);
                    const checkbox = `
                        <div class="form-check form-check-inline">
                            <input type="checkbox" id="company_${company.companyId}" name="companyList[]" value="${company.companyId}" class="form-check-input" ${isChecked ? 'checked' : ''}>
                            <label for="company_${company.companyId}" class="form-check-label">${company.companyName}</label>
                        </div>
                    `;
                    companyCheckboxContainer.append(checkbox); 
                });
            } else {
                companyCheckboxContainer.append('<div>No companies available</div>');
            }

            $('#editModal').modal('show');
        }
        //error: function (xhr, status, error) {
        //    alert("Error: " + error);
        //}
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
    // Get selected Role(s) - Only selected roles (checked checkboxes)
    var selectedRoleIDs = $('#editModal #editRoleCheckbox input:checked').map(function () {
        return $(this).val(); // Get the value (RoleID) of the checked options
    }).get(); // Get the array of selected RoleIDs

    // Get selected Company IDs - Only selected companies (checked checkboxes)
    var selectedCompanyIDs = $('#editModal #editCompanyCheckbox input:checked').map(function () {
        return $(this).val(); // Get the value (CompanyID) of the checked options
    }).get(); // Get the array of selected CompanyIDs

    // Prepare userData to be sent in AJAX request
    var userData = {
        userID: $('#editModal #editUserID').val(),
        firstName: $('#editModal #fName').val(),
        lastName: $('#editModal #lName').val(),
        email: $('#editModal #editEmail').val(),
        mobile: $('#editModal #editPhone').val(),
        userType: $('#editModal #editRoleCheckbox option:selected').map(function () { return $(this).text(); }).get().join(', '), // Join selected role names
        roleID: selectedRoleIDs, // Send only the selected RoleIDs
        userStatus: $('#editModal #editStatus').val(),
        isActive: $('#editModal #editIsActive').is(':checked'), // Get the boolean value of checkbox
        companyId: selectedCompanyIDs, // Send only the selected CompanyIDs
        roleList: selectedRoleIDs, // Send the list of selected role IDs
        companyList: selectedCompanyIDs // Send the list of selected company IDs
    };

    // Optional: Log the userData to verify the collected data
    alert("User data is: " + JSON.stringify(userData));

    // Perform AJAX request
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
                $('#editModal').modal('hide');
                $('#userDatatable').DataTable().ajax.reload();
                location.reload();
            }
        },
        error: function (xhr, status, error) {
            $('#editModal').modal('hide');
            $('#userDatatable').DataTable().ajax.reload();
            location.reload();
        }
    });
}
