$(function(){
    //CHANGING SOUND ICON
    $('.sound-bg').click(function(){
        $('.sound-bg img').toggleClass('sound-off');
    });

    //OPENING MENU
    $('.logo-bg').click(function(){
        $('.sidebar').slideDown();
        $('.overlay').fadeIn(function(){
             $('.overlay').click(function(){
                 $('.sidebar').slideUp();
                 $('.overlay').fadeOut();
             });
        }); 
    });

    //CHANGING HEADER BACKGROUND
    $(window).scroll(function(){
        if ($(this).scrollTop() > 20) 
        {
            $('header').removeClass('header');
            $('header').addClass('header-scroll');
        } 
        else
        {
            $('header').removeClass('header-scroll');
            $('header').addClass('header');
        }
    });
});