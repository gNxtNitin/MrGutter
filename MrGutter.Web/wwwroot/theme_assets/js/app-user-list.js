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
                "data": null, searchable: false,
                orderable: false,
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
            $('#delete').modal('hide');
            $('#userDatatable').DataTable().ajax.reload();
        }
    });
});

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
          //  $('#editModal #editStatus').val(response.userStatus);
            $('#editModal #editIsActive').prop('checked', response.isActive);
            // Populate status as radio buttons
            const statusContainer = $('#editStatus');
            statusContainer.empty();
            const statuses = [
                { value: 'Active', label: 'Active' },
                { value: 'InActive', label: 'InActive' }
            ];
            statuses.forEach(status => {
                const isChecked = response.userStatus === status.value;
                const radioButton = `
                    <div class="form-check form-check-inline">
                        <input type="radio" id="status_${status.value}" name="userStatus" value="${status.value}" class="form-check-input" ${isChecked ? 'checked' : ''}>
                        <label for="status_${status.value}" class="form-check-label">${status.label}</label>
                    </div>
                `;
                statusContainer.append(radioButton);
            });
            const roleCheckboxContainer = $('#editModal #editRoleCheckbox');
            roleCheckboxContainer.empty();
            if (response.roleList && response.roleList.length > 0) {
                response.roleList.forEach(role => {
                   const isChecked = role.isActive === true; 
                    const checkbox = `
                        <div class="form-check form-check-inline">
                            <input type="checkbox" id="role_${role.roleId}" name="roleList[]" value="${role.roleId}" class="form-check-input" ${isChecked ? 'checked' : ''}>
                            <label for="role_${role.roleId}" class="form-check-label">${role.roleName}</label>
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
                    const isCheckedCompany = company.isActive === true;
                    const checkbox = `
                        <div class="form-check form-check-inline">
                            <input type="checkbox" id="company_${company.companyId}" name="companyList[]" value="${company.companyId}" class="form-check-input" ${isCheckedCompany ? 'checked' : ''}>
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
    });
}



function UserViewEdit(userID) {
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

//main
function saveChanges1() {
    var selectedRoleIDs = $('#editModal #editRoleCheckbox input');
    var selectedCompanyIDs = $('#editModal #editCompanyCheckbox input');
    // Collect the selected status from the radio buttons
    var selectedStatus = $('#editModal input[name="userStatus"]:checked').val();

    var userData = {
        userID: $('#editModal #editUserID').val(),
        firstName: $('#editModal #fName').val(),
        lastName: $('#editModal #lName').val(),
        email: $('#editModal #editEmail').val(),
        mobile: $('#editModal #editPhone').val(),
        userStatus: selectedStatus, // Use the selected radio button value
        isActive: $('#editModal #editIsActive').is(':checked'),
        roleList: selectedRoleIDs.map(function () {
                    return {
                        UserId: $('#editModal #editUserID').val(),
                        RoleId: $(this).val(),
                        IsActive: $(this).is(':checked') // True if checked, false if not
                    };
                }).get(),

                // Process all company checkboxes (both checked and unchecked)
        companyList: selectedCompanyIDs.map(function () {
                    return {
                        UserId: $('#editModal #editUserID').val(),
                        CompanyId: $(this).val(),
                        IsActive: $(this).is(':checked') // True if checked, false if not
                    };
                }).get()
    };

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






