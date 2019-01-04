
var check = function () {
    if (document.getElementById('inputPassword').value ==
        document.getElementById('confirmPassword').value) {
        document.getElementById('messagePassword').style.color = 'green';
        document.getElementById('messagePassword').innerHTML = 'Wachtwoord komt overeen';
    } else {
        document.getElementById('messagePassword').style.color = 'red';
        document.getElementById('messagePassword').innerHTML = 'Wachtwoord komt niet overeen';
    }
}
   
var checkmatchingEmail = function () {
    if (document.getElementById('inputEmail').value ==
        document.getElementById('confirmEmail').value) {
        document.getElementById('messageEmail').style.color = 'green';
        document.getElementById('messageEmail').innerHTML = 'E-mail komt overeen';
    } else {
        document.getElementById('messageEmail').style.color = 'red';
        document.getElementById('messageEmail').innerHTML = 'E-mail komt niet overeen';
    }
}  
        
function EmailCheck(){  
                
var emailcheck = document.getElementById('inputEmail').value;
var pattern = /^[A-Za-z0-9._]{3,}@@[A_Za-z]{3,}[.]{1}[A-Za-z.]{2,6}$/;
            
    if(pattern.test(emailcheck)){
        document.getElementById('status').innerHTML = " "
    }
    else{
        document.getElementById('status').style.color = 'red';
        document.getElementById('status').innerHTML = "E-mail is onjuist! Voorbeeld: Example@example.com"
    }
}
    