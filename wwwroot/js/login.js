$(document).ready(function () {
    $('#btnLogin').click(function () {
        var username = $('#loginUsername').val();
        var password = $('#loginPassword').val();

        $.ajax({
            url: '/Home/Login', 
            type: 'POST',
            data: {
                Username: username,
                Password: password
            },
            success: function (data) {
                if (data.success) {
                    alert(data.message); 
                    window.location.href = '/'; 
                } else {
                    alert(data.message); 
                }
            },
            error: function () {
                alert('Something went wrong.');
            }
        });
    });
});