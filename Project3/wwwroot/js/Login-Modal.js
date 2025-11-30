document.addEventListener('DOMContentLoaded', function () {
    var modalElement = document.getElementById('loginRequiredModal');
    if (!modalElement) return;

    if (window.bootstrap && bootstrap.Modal) {
        var loginModal = new bootstrap.Modal(modalElement, {
            backdrop: 'static',
            keyboard: false
        });
        loginModal.show();
    }
});