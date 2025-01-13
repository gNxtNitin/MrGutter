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
            { data: "companyName", name: "companyName", autoWidth: true },
            { data: "contactPerson", name: "contactPerson", autoWidth: true },
            { data: "companyEmail", name: "companyEmail", autoWidth: true },
            { data: "companyPhone", name: "companyPhone", autoWidth: true },
            {
                data: null,
                render: function (data, type, row) {
                    return `
                        <a class="text-danger" style="cursor: pointer;" onclick="openEditModal(${row.companyId})">
                            <i class='fa-solid fa-pen-to-square text-danger'></i>
                        </a>
                        <a href="#" class="text-danger" style="cursor: pointer;" onclick="deleteRow(${row.companyId})">
                            <i class='ti ti-trash text-danger'></i>
                        </a>
                        <a href="#" class="text-danger" style="cursor: pointer;">
                            <i class='fa-regular fa-clipboard text-danger'></i>
                        </a>
                    `;
                },
            },
            {
                data: "",
                name: "checkbox",
                autoWidth: true,
                render: function (data, type, row) {
                    return `<input type="checkbox" class="select-row" data-company-id="${row.companyId}" />`;
                },
                orderable: false,
                searchable: false,
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

// Row deletion
function deleteRow(companyId) {
    if (confirm("Are you sure you want to delete this company?")) {
        $.ajax({
            url: "/userManager/DeleteCompany",
            method: "POST",
            data: { companyId },
            success: function (response) {
                if (response.success) {
                    $("#companyDatatable").DataTable().ajax.reload();
                    alert("Company deleted successfully.");
                } else {
                    alert("Error deleting company: " + response.error);
                }
            },
            error: function () {
                alert("An error occurred while deleting the company.");
            },
        });
    }
}

// Open edit modal
function openEditModal(companyId) {

    alert("in get method")
    $.ajax({
        url: "/userManager/EditCompany?companyId=" + companyId,
        method: "GET",

        success: function (response) {
            resetEditModal(); // Clear old data
            populateEditModal(response);
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

function populateEditModal(data) {
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
            } else {
                alert("Error updating company: " + response.error);
            }
        },
        error: function () {
            alert("An error occurred while updating the company.");
        },
    });
}
