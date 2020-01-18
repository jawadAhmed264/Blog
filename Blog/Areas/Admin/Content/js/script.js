$(document).ready(function(){          
    $('.txtUsername').focusout(function(){
        if($('.txtUsername').val() != ''){
            $('.float-lbl1').addClass('float-lbl1-focus');
            $('.float-lbl1').css('transition','0.2s ease-in-out');
        }
        else{
            $('.float-lbl1').removeClass('float-lbl1-focus');
        }
    });
    $('.txtPassword').focusout(function(){
        if($('.txtPassword').val() != ''){
            $('.float-lbl2').addClass('float-lbl2-focus');
            $('.float-lbl2').css('transition','0.2s ease-in-out');
        }
        else{
            $('.float-lbl2').removeClass('float-lbl2-focus');
        }
    });
    
    //Header Code
    $('.search-box').click(function(){
        $('.hiddenbar-bg').slideDown('slow');
    })
    $('.aside, .main').mousedown(function(){
        $('.hiddenbar-bg').slideUp('slow');
    })

    //menu-bar
    $('.user').click(function(){  
        if($('.dropdown').css('display') == 'none')
        {
            $('.dropdown').slideDown();
        }
        else if($('.dropdown').css('display') == 'block')
        {
            $('.dropdown').slideUp();
        }
    });

});//Document.ready