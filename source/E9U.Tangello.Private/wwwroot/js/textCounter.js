$('textarea').keyup(function(){

    if(this.value.length > $(this).attr('maxlength')){
        return false;
    }

    if( $(this).is("disabled") ) {
        $("#charactersRemaining").html("");
    }
    
    $("#charactersRemaining").html("Remaining characters : " +($(this).attr('maxlength') - this.value.length));
    
});