/**
 * Page User List
 */

'use strict';

// Datatable (jquery)
$(function () {
    let borderColor, bodyBg, headingColor;

    if (isDarkStyle) {
        borderColor = config.colors_dark.borderColor;
        bodyBg = config.colors_dark.bodyBg;
        headingColor = config.colors_dark.headingColor;
    } else {
        borderColor = config.colors.borderColor;
        bodyBg = config.colors.bodyBg;
        headingColor = config.colors.headingColor;
    }

    // Variable declaration for table
    var dt_user_table = $('.datatables-users1'),
        select2 = $('.select2'),
        userView = '#',
        statusObj = {
            1: { title: 'Pending', class: 'bg-label-warning' },
            2: { title: 'Active', class: 'bg-label-success' },
            3: { title: 'Inactive', class: 'bg-label-secondary' }
        };

    if (select2.length) {
        var $this = select2;
        $this.wrap('<div class="position-relative"></div>').select2({
            placeholder: 'Select Country',
            dropdownParent: $this.parent()
        });
    }

    // Users datatable

    if (dt_user_table.length) {
        $('.addbutton').on('click', function () {
            debugger
            // Fetch user data and pop
            $.ajax({
                url: '/UserManager/User1', // The URL of the controller action
                type: 'GET',
                success: function (data) {
                    // Show user data in alert
                    console.log(data);
                    alert("ID: " + data.id + "\nFull Name: " + data.full_Name + "\nEmail: " + data.email + "\nPhone: " + data.phone + "\nRole: " + data.role + "\nStatus: " + data.status + "\nAvtar: " + data.avatar);

                    // Initialize DataTable with the returned data
                    dt_user_table.DataTable({
                        columns: [
                            { data: 'id' },
                            { data: 'id' }, // This column might need a different unique key
                            { data: 'full_name' },
                            { data: 'email' },
                            { data: 'phone' },
                            { data: 'role' },
                            { data: 'status' },
                            { data: 'avatar' }
                        ],
                        columnDefs: [
                            {
                                className: 'control',
                                searchable: false,
                                orderable: false,
                                responsivePriority: 2,
                                targets: 0,
                                render: function (data, type, full, meta) {
                                    return '';
                                }
                            },
                            {
                                targets: 1,
                                orderable: false,
                                checkboxes: {
                                    selectAllRender: '<input type="checkbox" class="form-check-input">'
                                },
                                render: function (data, type, full, meta) {
                                    return '<input type="checkbox" class="dt-checkboxes form-check-input">';
                                },
                                searchable: false
                            },
                            {
                                targets: 2,
                                render: function (data, type, full, meta) {
                                    var $name = full.full_name;
                                    var $email = full.email;
                                    var $image = full.avatar;

                                    var $row_output =
                                        '<div class="d-flex justify-content-start align-items-center user-name">' +
                                        '<div class="d-flex flex-column">' +
                                        '<a href="' + userView + '" class="text-heading text-truncate"><span class="fw-medium">' + $name + '</span></a>' +
                                        '</div>' +
                                        '</div>';
                                    return $row_output;
                                }
                            },
                            {
                                targets: 3,
                                render: function (data, type, full, meta) {
                                    var $email = full.email;
                                    return (
                                        "<span class='text-truncate d-flex align-items-center text-heading'>" + $email + '</span>'
                                    );
                                }
                            },
                            {
                                targets: 5,
                                render: function (data, type, full, meta) {
                                    var $role = full.role;
                                    var roleBadgeObj = {
                                        SuperAdmin: '<i class="ti ti-crown ti-md text-primary me-2"></i>',
                                        Estimator: '<i class="ti ti-edit ti-md text-warning me-2"></i>',
                                        Admin: '<i class="ti ti-device-desktop ti-md text-danger me-2"></i>'
                                    };
                                    return (
                                        "<span class='text-truncate d-flex align-items-center text-heading'>" +
                                        roleBadgeObj[$role] + $role + '</span>'
                                    );
                                }
                            },
                            {
                                targets: 6,
                                render: function (data, type, full, meta) {
                                    var $status = full.status;

                                    var statusObj = {
                                        Active: { class: 'badge-success', title: 'Active' },
                                        Inactive: { class: 'badge-danger', title: 'Inactive' },
                                        Suspended: { class: 'badge-warning', title: 'Suspended' }
                                    };

                                    return (
                                        '<span class="badge ' + statusObj[$status].class + '" text-capitalized>' + statusObj[$status].title + '</span>'
                                    );
                                }
                            },
                            {
                                targets: -1,
                                title: 'Actions',
                                searchable: false,
                                orderable: false,
                                render: function (data, type, full, meta) {
                                    return (
                                        '<div class="d-flex align-items-center">' +
                                        '<a href="' + userView + '" class="btn btn-icon btn-text-secondary waves-effect waves-light rounded-pill"><i class="ti ti-eye ti-md"></i></a>' +
                                        '<a data-bs-target="#editModal" data-bs-toggle="modal" data-bs-dismiss="modal" class="btn btn-icon btn-text-primary waves-effect waves-light rounded-pill"><i class="ti ti-edit ti-md me-2"></i></a>' +
                                        '<a data-bs-target="#delete" data-bs-toggle="modal" data-bs-dismiss="modal" data-row-index="ROW_INDEX" class="btn btn-icon btn-text-danger waves-effect waves-light rounded-pill delete-record"><i class="ti ti-trash text-danger ti-md"></i></a>' +
                                        '</div>'
                                    );
                                }
                            }
                        ],
                        order: [[2, 'desc']],
                        dom: '<"row"' +
                            '<"col-md-2"<"ms-n2"l>>' +
                            '<"col-md-10"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-end flex-md-row flex-column mb-6 mb-md-0 mt-n6 mt-md-0"fB>>' +
                            '>t' +
                            '<"row"' +
                            '<"col-sm-12 col-md-6"i>' +
                            '<"col-sm-12 col-md-6"p>' +
                            '>',
                        language: {
                            sLengthMenu: '_MENU_',
                            search: '',
                            searchPlaceholder: 'Search User',
                            paginate: {
                                next: '<i class="ti ti-chevron-right ti-sm"></i>',
                                previous: '<i class="ti ti-chevron-left ti-sm"></i>'
                            }
                        },
                        buttons: [
                            {
                                text: '<i class="ti ti-plus me-0 me-sm-1 ti-xs"></i><span class="d-none d-sm-inline-block me-1">Add New User</span>',
                                className: 'add-new btn btn-primary waves-effect waves-light mx-0 ms-2',
                                attr: {
                                    'data-bs-toggle': 'offcanvas',
                                    'data-bs-target': '#offcanvasAddUser'
                                }
                            }
                        ],
                        responsive: {
                            details: {
                                display: $.fn.dataTable.Responsive.display.modal({
                                    header: function (row) {
                                        var data = row.data();
                                        return 'Details of ' + data['full_name'];
                                    }
                                }),
                                type: 'column',
                                renderer: function (api, rowIdx, columns) {
                                    var data = $.map(columns, function (col, i) {
                                        return col.title !== '' ?
                                            '<tr data-dt-row="' + col.rowIndex + '" data-dt-column="' + col.columnIndex + '">' +
                                            '<td>' + col.title + ':</td> <td>' + col.data + '</td>' +
                                            '</tr>' : '';
                                    }).join('');
                                    return data ? $('<table class="table"/><tbody />').append(data) : false;
                                }
                            }
                        }
                    });
                }
            });
        });
    }



    // Delete Record
    //$('.datatables-users tbody').on('click', '.delete-record', function () {
    //    dt_user.row($(this).parents('tr')).remove().draw();
    //});



    $(document).ready(function () {
        let rowToDelete = null; // Variable to store the row reference for deletion

        // Trigger delete confirmation modal
        $('.datatables-users tbody').on('click', '.delete-record', function () {
            rowToDelete = dt_user.row($(this).parents('tr')); // Store the row reference
            $('#delete').modal('show'); // Show the delete confirmation modal
        });

        // Handle the "Yes" button click in the modal
        $('#delete .confirm-delete').on('click', function () {
            if (rowToDelete) {
                rowToDelete.remove().draw(); // Delete the row and redraw the table
                rowToDelete = null; // Reset the variable
            }
        });
    });



    // Filter form control to default size
    // ? setTimeout used for multilingual table initialization
    setTimeout(() => {
        $('.dataTables_filter .form-control').removeClass('form-control-sm');
        $('.dataTables_length .form-select').removeClass('form-select-sm');
    }, 300);
});

// Validation & Phone mask
(function () {
    const phoneMaskList = document.querySelectorAll('.phone-mask'),
        addNewUserForm = document.getElementById('addNewUserForm');

    // Phone Number
    if (phoneMaskList) {
        phoneMaskList.forEach(function (phoneMask) {
            new Cleave(phoneMask, {
                phone: true,
                phoneRegionCode: 'US'
            });
        });
    }
    // Add New User Form Validation
    const fv = FormValidation.formValidation(addNewUserForm, {
        fields: {
            userFullname: {
                validators: {
                    notEmpty: {
                        message: 'Please enter fullname '
                    }
                }
            },
            userEmail: {
                validators: {
                    notEmpty: {
                        message: 'Please enter your email'
                    },
                    emailAddress: {
                        message: 'The value is not a valid email address'
                    }
                }
            }
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            bootstrap5: new FormValidation.plugins.Bootstrap5({
                // Use this for enabling/changing valid/invalid class
                eleValidClass: '',
                rowSelector: function (field, ele) {
                    // field is the field name & ele is the field element
                    return '.mb-6';
                }
            }),
            submitButton: new FormValidation.plugins.SubmitButton(),
            // Submit the form when all fields are valid
            // defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
            autoFocus: new FormValidation.plugins.AutoFocus()
        }
    });
})();
