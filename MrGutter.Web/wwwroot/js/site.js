// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    var passwordField = document.getElementById('passwordField');
    var loginWithOtpCheckbox = document.getElementById('custom-check-2');

    // Function to update visibility based on checkbox state
    function updatePasswordFieldVisibility() {
        if (loginWithOtpCheckbox.checked) {
            passwordField.style.display = 'none';
        } else {
            passwordField.style.display = 'block';
        }
    }

    // Initialize visibility on page load
    updatePasswordFieldVisibility();

    // Add event listener to checkbox
    loginWithOtpCheckbox.addEventListener('change', updatePasswordFieldVisibility);
});
;