/******Login Page******/
/******Some Common Functions In ppb.js******/

function bindLoginPageEvent() {

    $('.register-btn').bind('click', function () {
        window.location.href = "/Account/Register";
    });

    $('.login-submit-btn').bind('click', function () {
        //to-do client submit funtion
        return true;
    });
}

/******Login Page******/