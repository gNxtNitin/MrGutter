$(document).ready(function () {
    // Initialize DataTable
    $("#companyDatatable").DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        ordering: true,
        searching: true,
        info: true,
        ajax: {
            url: "/userManager/companyList",
            type: "POST",
            datatype: "json",
        },
        dom: '<"top"l<"right-section"fB>>rt<"bottom"ip><"clear">',
        columns: [
            {
                data: null, name: "companyName", autoWidth: true,
                render: function (data, type, row) {
                    let logoHtml = '';
                    if (row.companyLogo) {
                        logoHtml = `<img src="${row.companyLogo}" alt="${row.companyName} Logo" class="rounded-circle" style="width: 30px; height: 30px; margin-right: 5px;">`;
                    }
                    return `${logoHtml}${row.companyName}`;
                }
            },
            { data: "contactPerson", name: "contactPerson", autoWidth: true },
            { data: "companyEmail", name: "companyEmail", autoWidth: true },
            { data: "companyPhone", name: "companyPhone", autoWidth: true },
            {
                data: null, searchable: false,
                orderable: false,
                render: function (data, type, row) {
                    return `
                        <a class="text-danger" style="cursor: pointer;" title="Edit Company" onclick="openEditModal(${row.companyId})">
                            <i class='fa-solid fa-pen-to-square text-primary'></i>
                        </a>
                        <a href="#" class="text-danger" style="cursor: pointer;" title="Delete Company" onclick="deleteCmpRow(${row.companyId})">
                            <i class='ti ti-trash text-danger'></i>
                        </a>
                      
                    `;
                },
            },
        ],
        buttons: [
            {
                text: '<i class="ti ti-plus me-0 me-sm-1 ti-xs"></i><span class="d-none d-sm-inline-block me-2">Add New Company</span>',
                className: "add-new btn btn-primary waves-effect waves-light me-3",
                attr: {
                    "data-bs-toggle": "offcanvas",
                    "data-bs-target": "#offcanvasAddCompany",
                },
            },
        ],
    });

    // Event listener for row selection
    const selectedCompanies = new Set();
    $("#companyDatatable").on("change", ".select-row", function () {
        const companyId = $(this).data("company-id");
        if (this.checked) {
            selectedCompanies.add(companyId);
        } else {
            selectedCompanies.delete(companyId);
        }
        toggleBatchActions();
    });

    // "Select All" functionality
    $("#selectAll").on("change", function () {
        const isChecked = this.checked;
        $(".select-row").each(function () {
            this.checked = isChecked;
            const companyId = $(this).data("company-id");
            if (isChecked) {
                selectedCompanies.add(companyId);
            } else {
                selectedCompanies.delete(companyId);
            }
        });
        toggleBatchActions();
    });

    // Enable or disable batch actions
    function toggleBatchActions() {
        if (selectedCompanies.size > 0) {
            $("#batchActions").removeClass("d-none");
        } else {
            $("#batchActions").addClass("d-none");
        }
    }
});


function deleteCmpRow(CompanyId) {
    $('#deletecmp').modal('show');
    $('#deletecmp .confirm-delete').data('user-id', CompanyId);
}

$('#deletecmp .confirm-delete').on('click', function () {
    var CompanyId = $(this).data('user-id');
    $.ajax({
        url: '/userManager/DeleteCompany',
        method: 'POST',
        data: { CompanyId: CompanyId },
        success: function (response) {
            if (response.success) {
                $('#deletecmp').modal('hide');
                $('#companyDatatable').DataTable().ajax.reload();
                alert("User deleted successfully.");
            } else {
                $('#deletecmp').modal('hide');
                $('#companyDatatable').DataTable().ajax.reload();
            }
        },
        error: function (xhr, status, error) {
            // Log the error for debugging
            $('#deletecmp').modal('hide');
            $('#companyDatatable').DataTable().ajax.reload();
        }
    });
});


// Open edit modal
function openEditModal(companyId) {
    $.ajax({
        url: "/userManager/EditCompany?companyId=" + companyId,
        method: "GET",
        success: function (response) {
            resetEditModal(); // Clear old data
            populateEditModalforCompany(response);
            $("#editModal1").modal("show");
        },
        error: function () {
            alert("Failed to fetch company details.");
        },
    });
}

function resetEditModal() {
    $("#editModal1").find("input, select").val(""); // Clear modal fields
}

function populateEditModalforCompany(data) {
    $("#editModal1 #editCompanyId").val(data.companyId);
    $("#editModal1 #editCompanyName").val(data.companyName);
    $("#editModal1 #editContactPerson").val(data.contactPerson);
    $("#editModal1 #editCompanyEmail").val(data.companyEmail);
    $("#editModal1 #editCompanyPhone").val(data.companyPhone);
}

// Save changes
function saveChanges() {
    const companyData = {
        companyId: $("#editModal1 #editCompanyId").val(),
        companyName: $("#editModal1 #editCompanyName").val(),
        contactPerson: $("#editModal1 #editContactPerson").val(),
        companyEmail: $("#editModal1 #editCompanyEmail").val(),
        companyPhone: $("#editModal1 #editCompanyPhone").val(),
    };

    $.ajax({
        url: "/userManager/EditCompany",
        method: "POST",
        data: companyData,
        success: function (response) {
            if (response.success) {
                $("#editModal1").modal("hide");
                $("#companyDatatable").DataTable().ajax.reload();
                location.reload();
            } else {
                $("#editModal1").modal("hide");
                $("#companyDatatable").DataTable().ajax.reload();
                location.reload();
            }
        },
        error: function () {
            $("#editModal1").modal("hide");
            $("#companyDatatable").DataTable().ajax.reload();
            location.reload();
        },
    });
}
