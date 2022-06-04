var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
var toastElList = [].slice.call(document.querySelectorAll('.toast'));

var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
});

var toastList = toastElList.map(function (toastEl) {
    console.log(toastEl);
    return new bootstrap.Toast(toastEl);
});

toastList.forEach(toast => {
    toast.show();
});
