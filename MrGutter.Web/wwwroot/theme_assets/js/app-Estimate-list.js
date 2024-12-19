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
    var dt_user_table = $('.datatables-estimate'),
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
        var dt_user = dt_user_table.DataTable({
            ajax: assetsPath + 'json/estimate-list.json', // JSON file to add data
            columns: [
                // columns according to JSON
                //{ data: 'id' },
                { data: 'id' },
                { data: 'est_no' },
                { data: 'est_name'},
                { data: 'address' },
                { data: 'salesperson' },
                { data: 'created' },
                { data: 'status' },
                { data: 'revenue' },
            
            ],
            columnDefs: [
                {
                    // For Responsive
                    className: 'control',
                    searchable: false,
                    orderable: false,
                    responsivePriority: 2,
                    targets: 0,
                    render: function (data, type, full, meta) {
                        return '';
                    }
                },
                //{
                //    // For Checkboxes
                //    targets: 1,
                //    orderable: false,
                //    checkboxes: {
                //        selectAllRender: '<input type="checkbox" class="form-check-input">'
                //    },
                //    render: function () {
                //        return '<input type="checkbox" class="dt-checkboxes form-check-input" >';
                //    },
                //    searchable: false
                //},
                {
                    // Estimate name
                    targets: 1,
                    responsivePriority: 4,
                    render: function (data, type, full, meta) {
                        var $name = full['est_no'];
                        
                        // Creates full output for row
                        var $row_output =
                            '<div class="d-flex justify-content-start align-items-center user-name">' +
                            '<div class="avatar-wrapper">' +
                            //'<div class="avatar avatar-sm me-4">' +
                            //$output +
                            //'</div>' +
                            '</div>' +
                            '<div class="d-flex flex-column">' +
                            '<a href="' +
                            userView +
                            '" class="text-heading text-truncate"><span class="fw-medium">' +
                            $name +
                            '</span></a>' +
                            //'<small>' +
                            //$email +
                            //'</small>' +
                            '</div>' +
                            '</div>';
                        return $row_output;
                    }
                },
                {
                    // Estimate no
                    targets: 2,
                    render: function (data, type, full, meta) {
                        var $email = full['est_name'];
                        return (
                            "<span class='text-truncate d-flex align-items-center text-heading'>" +
                            $email +
                            '</span>'
                        );
                    }
                },
                
             
                
                {
                    // address
                    targets: 3,
                    render: function (data, type, full, meta) {
                        var $email = full['address'];
                        return (
                            "<span class='text-truncate d-flex align-items-center text-heading'>" +
                            $email +
                            '</span>'
                        );
                    }
                },

                {
                    // salesperson
                    targets: 4,
                    render: function (data, type, full, meta) {
                        var $email = full['salesperson'];
                        return (
                            "<span class='text-truncate d-flex align-items-center text-heading'>" +
                            $email +
                            '</span>'
                        );
                    }
                },

                {
                    // created
                    targets: 5,
                    render: function (data, type, full, meta) {
                        var $email = full['created'];
                        return (
                            "<span class='text-truncate d-flex align-items-center text-heading'>" +
                            $email +
                            '</span>'
                        );
                    }
                },
                {
                    // status
                    targets: 6,
                    render: function (data, type, full, meta) {
                        var $email = full['status'];
                        return (
                            "<span class='text-truncate d-flex align-items-center text-heading'>" +
                            $email +
                            '</span>'
                        );
                    }
                },

                {
                    // revenue
                    targets: 7,
                    render: function (data, type, full, meta) {
                        var $email = full['revenue'];
                        return (
                            "<span class='text-truncate d-flex align-items-center text-heading'>" +
                            $email +
                            '</span>'
                        );
                    }
                },
               
            ],

            order: [[2, 'desc']],
            dom:
                '<"row"' +
                '<"col-md-2"<"ms-n2"l>>' +
                '<"col-md-10"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-end flex-md-row flex-column mb-6 mb-md-0 mt-n6 mt-md-0"f>>' +
                '>t' +
                '<"row"' +
                '<"col-sm-12 col-md-6"i>' +
                '<"col-sm-12 col-md-6"p>' +
                '>',
            language: {
                sLengthMenu: '_MENU_',
                search: '',
                searchPlaceholder: 'Search Estimate',
                paginate: {
                    next: '<i class="ti ti-chevron-right ti-sm"></i>',
                    previous: '<i class="ti ti-chevron-left ti-sm"></i>'
                }
            },
            // Buttons with Dropdown
       
            // For responsive popup
          
           
        });
    }

    // Delete Record
    $('.datatables-estimate tbody').on('click', '.delete-record', function () {
        dt_user.row($(this).parents('tr')).remove().draw();
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
